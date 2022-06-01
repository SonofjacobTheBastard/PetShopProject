using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetShop.Client.ViewModels;
using PetShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.UnitTests.ViewModelTests
{
    [TestClass]
    public class CatalogViewModelTests
    {

        [TestMethod]
        public void GetAnimalComments_ShouldReturnComments_WhenAnimalExists()
        {
            //Arrange
            CatalogViewModel vm = new CatalogViewModel();
            List<Comment> comments = new List<Comment>()
            {
                new Comment() { Content = "first", AnimalId = 2 },
                new Comment() { Content = "second", AnimalId = 2 }
            };
            Animal animal = new Animal() { Id = 2};

            //Act
            animal.Comments = (ICollection<Comment>)vm.GetAnimalComments(comments, animal);

            //Assert

            Assert.IsTrue(animal.Comments.SequenceEqual(comments));
        }
    }
}

