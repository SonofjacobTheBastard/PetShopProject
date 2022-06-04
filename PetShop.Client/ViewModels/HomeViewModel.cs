using PetShop.Data.Base;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.Service;

namespace PetShop.Client.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Animal> Animals { get; set; }
        public HomeViewModel()
        {
            Animals = new List<Animal>();
        }

        public IEnumerable<Animal> GetTopCommentedAnimals(IEnumerable<Animal> animals, int amount)
        {
            return animals.OrderByDescending(a => a.Comments.Count).Take(amount);
        }


        //Check If Needed ??? 
        public IEnumerable<Comment> BindCommentsToAnimals(IEnumerable<Comment> comments, Animal animal, int amount)
        {
            return comments
                         .Where(c => c.AnimalId == animal.Id)
                         .Take(amount)
                         .ToList();
        }
    }
}
