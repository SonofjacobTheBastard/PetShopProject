using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetShop.Client.ViewModels;
using PetShop.Data.Base;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.Service;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.UnitTests
{
    [TestClass]
    public class HomeViewModelTests
    {
        HomeViewModel vm;
        public HomeViewModelTests()
        {
            vm = new HomeViewModel();
        }

        [TestMethod]
        public void GetTopCommentedAnimals_ShouldReturnCorrectAnimals()
        {
            //Arrange

            Animal a = new()
            {
                Name = "a",
                Comments = new List<Comment>()
                {
                    new Comment(){Content ="content"},
                    new Comment(){Content ="content"},
                    new Comment(){Content ="content"},
                    new Comment(){Content ="content"}
                }
            };
            Animal b = new()
            {
                Name = "b",
                Comments = new List<Comment>()
                {
                    new Comment(){Content ="content"},
                    new Comment(){Content ="content"},
                    new Comment(){Content ="content"}
                }
            };
            Animal c = new()
            {
                Name = "c",
                Comments = new List<Comment>()
                {
                    new Comment{Content ="content"},
                    new Comment{Content ="content"},
                    new Comment{Content ="content"},
                    new Comment{Content ="content"},
                    new Comment{Content ="content"},
                }
            };
            Animal d = new()
            {
                Name = "d"
            };
            IEnumerable<Animal> animals = new List<Animal>() { a, b, c, d };

            //Act
            IEnumerable<Animal> expected = new List<Animal>() { c, a };
            IEnumerable<Animal> result = vm.GetTopCommentedAnimals(animals, 2).ToList();

            //Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }
        [TestMethod]
        public void BindCommentsToAnimals_ShouldBindComments_WhenAnimalExists()
        {

            //Arrange

            const int MAXAMOUNT_OF_COMMENTS = 2;
            List<Comment> comments = new List<Comment>()
            {
            new Comment() { AnimalId = 1, Content = "content" },
            new Comment() { AnimalId = 1, Content = "content" }
            };
            Animal animal = new() { Id = 1 };

            //Act

            animal.Comments = (ICollection<Comment>)vm.BindCommentsToAnimals(comments, animal, MAXAMOUNT_OF_COMMENTS);

            //Assert

            Assert.IsTrue(comments.SequenceEqual(animal.Comments));
        }
    }
}