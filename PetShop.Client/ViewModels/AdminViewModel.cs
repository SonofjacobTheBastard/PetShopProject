using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Data.Base;
using PetShop.Data.Model;

namespace PetShop.Client.ViewModels
{
    public class AdminViewModel
    {
        public int AnimalId { get;  set; }
        public string AnimalName { get;  set; }
        public string? AnimalDescription { get;  set; }
        public DateTime? AnimalBirthDate { get;  set; }
        public string? AnimalPhotoUrl { get;  set; }
        public int? AnimalCategoryId { get;  set; }
        public Animal CreateAnimalFromData() => new Animal()
        {
            BirthDate = AnimalBirthDate,
            Name = AnimalName,
            Description = AnimalDescription,
            PhotoUrl = AnimalPhotoUrl,
            CategoryId = AnimalCategoryId
        };
    }
}
