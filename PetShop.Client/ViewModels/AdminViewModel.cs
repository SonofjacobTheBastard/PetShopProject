using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Data.Base;
using PetShop.Data.Model;

namespace PetShop.Client.ViewModels
{
    public class AdminViewModel
    {
        public int AnimalId { get; internal set; }
        public string AnimalName { get; internal set; }
        public string? AnimalDescription { get; internal set; }
        public DateTime? AnimalBirthDate { get; internal set; }
        public string? AnimalPhotoUrl { get; internal set; }
        public int? AnimalCategoryId { get; internal set; }
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
