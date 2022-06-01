using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetShop.Data.Model;
using PetShop.UnitTests.FakeViewModels;
using System;

namespace PetShop.UnitTests
{
    [TestClass]
    public class AdminViewModelTests
    {
        [TestMethod]
        public void Should_Bind_Animal_Properties_To_View_Model_Properties()
        {
            FakeAdminViewModel viewModel = new FakeAdminViewModel()
            {
                AnimalBirthDate = DateTime.Now,
                AnimalName = "name",
                AnimalDescription = "description",
                AnimalPhotoUrl = "www.photourl.com",
                AnimalCategoryId = 1
            };

            Animal animal1 = viewModel.CreateAnimalFromData();



            Assert.IsNotNull(animal1.Name);
            Assert.IsNotNull(animal1.CategoryId);
            Assert.IsNotNull(animal1.Description);
            Assert.IsNotNull(animal1.PhotoUrl);
            Assert.IsNotNull(animal1.BirthDate);
        }
    }
}
