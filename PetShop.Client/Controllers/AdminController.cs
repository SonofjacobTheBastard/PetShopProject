using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Client.ViewModels;
using PetShop.Data.Base;
using PetShop.Data.Model;
using PetShop.Service;

namespace PetShop.Client.Controllers
{
    public class AdminController : Controller
    {
        private AnimalRepository animalRepo;
        private CategoryRepository categoryRepo;
        public AdminController(IEntityBaseRepository<Animal> animalRepo,
                                    IEntityBaseRepository<Category> categoryRepo)
        {
            this.animalRepo = (AnimalRepository)animalRepo;
            this.categoryRepo = (CategoryRepository)categoryRepo;
        }
        public IActionResult Index(int? id)
        {
            id ??= 0;
            ViewBag.Categories = new SelectList(categoryRepo.GetAllAsync().Result, "Id", "Name");
            if (id == 0)
                return View(animalRepo.GetAllAsync().Result);
            else
                return View(animalRepo.GetAnimalsByCategoryId((int)id));
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryRepo.GetAllAsync().Result, "Id", "Name");
            return View(new Animal());
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description,BirthDate,PhotoUrl,CategoryId")] Animal animal)
        {
            await animalRepo.AddAsync(animal);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var animal = await animalRepo.GetByIdAsync(id);
            if (animal == null) return Content("NotFound");

            AdminViewModel viewModel = new AdminViewModel()
            {
                AnimalId = animal.Id,
                AnimalName = animal.Name,
                AnimalDescription = animal.Description,
                AnimalBirthDate = animal.BirthDate,
                AnimalPhotoUrl = animal.PhotoUrl,
                AnimalCategoryId = animal.CategoryId,
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdminViewModel vm)
        {
            if (id != vm.AnimalId) return Content("NotFound");

            if (!ModelState.IsValid)
            {
                return Content("NotFound");
            }
            
            await animalRepo.UpdateAsync(id,vm.CreateAnimalFromData());
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await animalRepo.GetByIdAsync(id);
            if (animal == null) return Content("NotFound");
            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await animalRepo.GetByIdAsync(id);
            if (animal == null) return Content("NotFound");

            await animalRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id) => View(await animalRepo.GetByIdAsync(id));

    }
}
