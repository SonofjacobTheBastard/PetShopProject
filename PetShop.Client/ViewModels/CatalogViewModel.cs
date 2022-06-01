using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Data.Base;
using PetShop.Data.Model;
using PetShop.Service;

namespace PetShop.Client.ViewModels
{
    public class CatalogViewModel
    {
        public Animal Animal { get; set; }
        public CatalogViewModel()
        {
            Animal = new Animal();
        }
        public IEnumerable<Comment> GetAnimalComments(IEnumerable<Comment> comments, Animal animal)
        {
            return comments.Where(c => c.AnimalId == animal.Id).ToList();
        }
    }
}
