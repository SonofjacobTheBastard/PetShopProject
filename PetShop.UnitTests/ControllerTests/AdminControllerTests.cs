using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetShop.Client.Controllers;
using PetShop.Client.ViewModels;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.UnitTests.ControllerTests
{
    [TestClass]
    public class AdminControllerTests
    {
        Mock<PetShopDataContext> context;
        Mock<AnimalRepository> animalRepoMock;
        Mock<CategoryRepository> categoryRepoMock;
        public AdminControllerTests()
        {
            context = new Mock<PetShopDataContext>();
            animalRepoMock = new(context.Object);
            categoryRepoMock = new(context.Object);
        }
        [TestMethod]
        public void Index_ShouldReturnIndexView()
        {
            //Arrange

            animalRepoMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(GetFakeAnimals());
            var controller = new AdminController(animalRepoMock.Object, categoryRepoMock.Object);

            //Act

            ViewResult result = (ViewResult)controller.Index(0);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]

        public void Create_ShouldReturnCreateView()
        {
            //Arrange
            var controller = new AdminController(animalRepoMock.Object, categoryRepoMock.Object);

            //Act

            ViewResult result = (ViewResult)controller.Create();
            result.ViewName = "Create";

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Create_ShouldInstantiateViewBagPropertiesForCreateView()
        {
            //Arrange
            var property = GetFakeViewBagProperties().Take(1);
            List<object> viewBagProperties = new List<object>()
            {
                property
            };
            var controller = new AdminController(animalRepoMock.Object, categoryRepoMock.Object);

            //Act

            ViewResult result = (ViewResult)controller.Create();
            ViewDataDictionary viewData = controller.View().ViewData;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(viewData.Count, viewBagProperties.Count);
        }

        [TestMethod]
        public void Create_ShouldRedirectToActionIndex()
        {
            //Arrange
            var controller = new AdminController(animalRepoMock.Object, categoryRepoMock.Object);

            //Act
            var result = controller.Create(GetFakeAnimals().First()).Result as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void Details_ShouldReturnDetailsView()
        {
            var controller = new AdminController(animalRepoMock.Object, categoryRepoMock.Object);

            //Act

            ViewResult result = (ViewResult)controller.Details(0).Result;
            result.ViewName = "Details";

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void Edit_ShouldReturnEditView()
        {
            //Arrange
            animalRepoMock.Setup(x => x.GetByIdAsync(0).Result).Returns(new Animal());
            var controller = new AdminController(animalRepoMock.Object, categoryRepoMock.Object);
            //Act
            ViewResult result = (ViewResult)controller.Edit(0).Result;
            result.ViewName = "Edit";
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void Edit_ShouldRedirectToIndexAction()
        {

            //Arrange
            AdminViewModel viewModel = new AdminViewModel()
            {
                AnimalId = 0
            };
            animalRepoMock.Setup(x => x.UpdateAsync(0, new Animal() { Id = 0}));

            var controller = new AdminController(animalRepoMock.Object, categoryRepoMock.Object);

            //Act
            var result = controller.Edit(0,viewModel).Result as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", result.ActionName);
        }
        [TestMethod]
        public void Delete_ShouldReturnDeleteView()
        {
            //Arrange
            animalRepoMock.Setup(x => x.GetByIdAsync(0).Result).Returns(new Animal());
            var controller = new AdminController(animalRepoMock.Object, categoryRepoMock.Object);
            //Act
            ViewResult result = (ViewResult)controller.Delete(0).Result;
            result.ViewName = "Delete";
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmed_ShouldRedirectToIndexAction()
        {
            //Arrange
            AdminViewModel viewModel = new AdminViewModel()
            {
                AnimalId = 0
            };
            animalRepoMock.Setup(x => x.GetByIdAsync(0).Result).Returns(new Animal());
            animalRepoMock.Setup(x => x.DeleteAsync(0));

            var controller = new AdminController(animalRepoMock.Object, categoryRepoMock.Object);
            

            //Act
            var result = controller.DeleteConfirmed(0).Result as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", result.ActionName);
        }

       

        private List<Animal> GetFakeAnimals()
        {
            return new List<Animal>()
                {
                    new Animal()
                    {
                        Name = "omer",
                        Comments = new List<Comment>()
                      {
                          new Comment() { Content = "Opa"}
                      }
                    },
                  new Animal()
                  {
                      Name = "itai",
                      Comments = new List<Comment>()
                      {
                          new Comment() { Content = "gangam" },
                          new Comment() { Content = "style"}
                      }
                  },
                  new Animal()
                  {
                      Name = "asaf",
                      Comments = new List<Comment>()
                      {
                          new Comment() { Content = "op op op"},
                          new Comment() { Content = "Opa Gangam Style"}
                      }
                  }
                };
        }
        private List<object> GetFakeViewBagProperties()
        {
            return new List<object>()
            {
                new object(),
                new object()
            };
        }

    }
}
