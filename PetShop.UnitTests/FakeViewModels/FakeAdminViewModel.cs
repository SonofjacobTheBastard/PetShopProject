using PetShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.UnitTests.FakeViewModels
{
    internal class FakeAdminViewModel
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
