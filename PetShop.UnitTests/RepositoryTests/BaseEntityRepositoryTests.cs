using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetShop.Data.Base;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetShop.UnitTests.RepositoryTests
{
    [TestClass]
    public class BaseEntityRepositoryTests
    {
        private readonly PetShopDataContext context;
        public BaseEntityRepositoryTests()
        {
            DbContextOptions<PetShopDataContext> options = new DbContextOptionsBuilder<PetShopDataContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;
            context = new PetShopDataContext(options);
        }
        [TestMethod]

        public async Task AddAsync_ShouldCallDbContextMethods()
        {
            //Arrange
            EntityBaseRepository<Animal> repo = new EntityBaseRepository<Animal>(context);
            Animal animal = new();
            //Act
            await repo.AddAsync(animal);

            //Assert
            IEnumerable<Animal> animals = repo.GetAllAsync().Result;
            Assert.AreEqual(1, animal);
        }
        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnCorrectAnimal()
        {

            //Arrange
            EntityBaseRepository<Animal> repo = new EntityBaseRepository<Animal>(context);
            TestHelper.Seed(context, TestHelper.GetFakeAnimals());
            //Act
            Animal result = await repo.GetByIdAsync(1);
            
            //Assert
            Assert.AreEqual("omer", result.Name);
        }
        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteAnimal()
        {


            //Arrange
            EntityBaseRepository<Animal> repo = new EntityBaseRepository<Animal>(context);
            TestHelper.Cleanup<Animal>(context);
            repo.AddAsync(new Animal() { Id = 1 });
            //Act
            await repo.DeleteAsync(1);

            //Assert
            Assert.AreEqual(0, repo.GetAllAsync().Result.ToList().Count);
        }
        [TestMethod]
        public void GetAllAsync_ShouldReturnCorrectAnimal()
        {
            //Arrange
            EntityBaseRepository<Animal> repo = new EntityBaseRepository<Animal>(context);
            TestHelper.Cleanup<Animal>(context);
            var expected = TestHelper.GetFakeAnimals();
            TestHelper.Seed(context, expected);
            //Act
            var result = repo.GetAllAsync().Result;

            //Assert
            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateAnimal()
        {
            //Arrange
            EntityBaseRepository<Animal> repo = new EntityBaseRepository<Animal>(context);
            TestHelper.Cleanup<Animal>(context);
            int id = 10;
            string expectedName = "ben";
            Animal animal = new Animal() { Id = id, Name = "omer" };
            await repo.AddAsync(animal);
            
            //Act
            animal.Name = expectedName;
            await repo.UpdateAsync(id,animal);

            //Assert
            var result = repo.GetByIdAsync(id).Result.Name;
            Assert.AreEqual(expectedName, result);
        }
    }
}
