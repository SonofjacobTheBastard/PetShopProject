using Microsoft.AspNetCore.Mvc;
using PetShop.Client.ViewModels;
using PetShop.Data.Base;
using PetShop.Data.Model;
using PetShop.Service;

namespace PetShop.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly CommentRepository commentRepo;
        private readonly AnimalRepository animalRepo;
        private readonly HomeViewModel vm;
        const int AMOUNT_OF_ANIMALS = 2;
        const int MAXAMOUNT_OF_COMMENTS = 3;
        public HomeController(IEntityBaseRepository<Animal> animalRepo, IEntityBaseRepository<Comment> commentRepo)
        {
            this.commentRepo = (CommentRepository)commentRepo;
            this.animalRepo = (AnimalRepository)animalRepo;
            vm = new HomeViewModel();
        }

        public IActionResult Index()
        {
            vm.Animals = vm.GetTopCommentedAnimals(animalRepo.GetAllAsync().Result.ToList()
                                                                                    , AMOUNT_OF_ANIMALS);
            foreach (Animal animal in vm.Animals)
                animal.Comments = (ICollection<Comment>)vm.BindCommentsToAnimals(commentRepo.GetAllAsync().Result,
                                                                                                                animal,
                                                                                                        MAXAMOUNT_OF_COMMENTS);
            return View(vm);
        }

    }
}