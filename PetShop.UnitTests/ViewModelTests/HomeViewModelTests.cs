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
            IEnumerable<Animal> expected = new List<Animal>() { c, a};
            IEnumerable<Animal> result = vm.GetTopCommentedAnimals(animals, 2).ToList();

            //Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }
    }
}