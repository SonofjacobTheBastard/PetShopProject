using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetShop.Data.Base;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.Service;
using PetShop.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.UnitTests.RepositoryTests
{
    [TestClass]
    public class AnimalRepositoryTests
    {
        StubRepository stubRepo;
        public AnimalRepositoryTests()
        {
        }

        [TestMethod]
        public void GetAnimalsByCategoryId_ReturnsAnimalsWhenExists()
        {
            //Arrange)
            stubRepo = new();

            //Act    
            var result = stubRepo.GetAnimalsByCategoryId(2);

            //Assert
            Assert.AreEqual(1, result.ToList().Count);
        }

        [TestMethod]
        public void AddComment_ShouldGetCorrectAnimal()
        {
            //Arrange
            stubRepo = new();
            //Act
            stubRepo.AddComment(3, "A new content");
            //Assert
            var actual = stubRepo.GetById(3).Comments.Last().Content;
            Assert.AreEqual( "A new content", actual);
        }

    }
    public class StubRepository
    {
        public IEnumerable<Animal> GetAnimalsByCategoryId(int id) => GetAllAsync().Where(a => a.CategoryId == id);

        private List<Animal> GetAllAsync() => TestHelper.GetFakeAnimals();

        public void AddComment(int id, string content)
        {
            Animal animal = GetById(id);
            Comment comment = new Comment() { Content = content, AnimalId = id };
            animal.Comments.Add(comment);
        }

        public Animal GetById(int id)
        {
            return TestHelper.GetFakeAnimals().FirstOrDefault(n => n.Id == id);
        }
    }

}
