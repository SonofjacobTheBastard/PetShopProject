using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Client.ViewModels;
using PetShop.Data.Base;
using PetShop.Data.Model;
using PetShop.Service;

namespace PetShop.Client.Controllers
{

    public class CatalogController : Controller
    {

        readonly CatalogViewModel vm;
        private AnimalRepository animalRepo;
        private CategoryRepository categoryRepo;
        private CommentRepository commentRepo;

        public CatalogController(IEntityBaseRepository<Animal> animalRepo,
                                            IEntityBaseRepository<Category> categoryRepo,
                                                IEntityBaseRepository<Comment> commentRepo)
        {

            vm = new CatalogViewModel();
            this.animalRepo = (AnimalRepository)animalRepo;
            this.categoryRepo = (CategoryRepository)categoryRepo;
            this.commentRepo = (CommentRepository)commentRepo;
        }
        public IActionResult Index(int? id)
        {
            id ??= 0;
            SelectList list = new SelectList(categoryRepo.GetAllAsync().Result, "Id", "Name");
            ViewBag.Categories = list;
            ViewBag.SelectedOption = id;
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

        public async Task<IActionResult> Details(int id)
        {
            Animal animal = await animalRepo.GetByIdAsync(id);
            vm.Animal = animal;
            animal.Comments = (ICollection<Comment>)vm.GetAnimalComments(commentRepo.GetAllAsync().Result, animal);
            return View(animal);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(int id, string content)
        {
            await animalRepo.AddComment(id, content);
            return RedirectToAction(nameof(Details),new { id = id });
        }

    }
}