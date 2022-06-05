using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetShop.Client.Controllers;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.Service;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PetShop.UnitTests.ControllerTests
{
    [TestClass]
    public class CatalogControllerTests
    {
        Mock<PetShopDataContext> context;
        Mock<AnimalRepository> animalRepoMock;
        Mock<CategoryRepository> categoryRepoMock;
        Mock<CommentRepository> commentRepoMock;
        public CatalogControllerTests()
        {
            context = new Mock<PetShopDataContext>();
            animalRepoMock = new(context.Object);
            categoryRepoMock = new(context.Object);
            commentRepoMock = new(context.Object);
        }
        [TestMethod]
        public void Index_ShouldReturnIndexView()
        {
            //Arrange

            animalRepoMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(GetFakeAnimals());
            var controller = new CatalogController(animalRepoMock.Object, categoryRepoMock.Object, commentRepoMock.Object);

            //Act

            ViewResult result = (ViewResult)controller.Index(0);
            result.ViewName = "Index";
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Index", result.ViewName);

        }
        [TestMethod]
        public void Index_ShouldIstantiateViewBagProperties()
        {
            //Arrange
            
            var controller = new CatalogController(animalRepoMock.Object, categoryRepoMock.Object, commentRepoMock.Object);

            //Act

            ViewResult result = (ViewResult)controller.Index(0);

            ViewDataDictionary viewData = controller.View().ViewData;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(viewData.Count, GetFakeViewBagProperties().Count);
            //    var model = IEnumerable<Animal>.GetTypeInfo().


            //     var model2 = Assert.IsAssignableFrom<IEnumerable<Animal>>(
            //viewResult.ViewData.Model);

        }

        [TestMethod]
        public void Create_ShouldReturnCreateView()
        {
            //Arrange
            var controller = new CatalogController(animalRepoMock.Object, categoryRepoMock.Object, commentRepoMock.Object);

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
            var controller = new CatalogController(animalRepoMock.Object, categoryRepoMock.Object, commentRepoMock.Object);

            //Act

            ViewResult result = (ViewResult)controller.Create();
            ViewDataDictionary viewData = controller.View().ViewData;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(viewData.Count , viewBagProperties.Count);
        }

        [TestMethod]
        public void Create_ShouldRedirectToActionIndex()
        {
            //Arrange
            var controller = new CatalogController(animalRepoMock.Object, categoryRepoMock.Object, commentRepoMock.Object);

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
            var controller = new CatalogController(animalRepoMock.Object, categoryRepoMock.Object, commentRepoMock.Object);

            //Act

            ViewResult result = (ViewResult)controller.Create();
            result.ViewName = "Details";

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void Comment_ShouldRedirectToActionDetails()
        {

            //Arrange
            //animalRepoMock.Setup(x => x.AddComment(It.IsAny<int>(), It.IsAny<string>())).Verifiable();

            animalRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()).Result).Returns(GetFakeAnimals().First());

            var controller = new CatalogController(animalRepoMock.Object, categoryRepoMock.Object, commentRepoMock.Object);

            //Act
            var result = controller.Comment(0, "content").Result as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Details", result.ActionName);

        }

        private List<object> GetFakeViewBagProperties()
        {
            return new List<object>()
            {
                new object(),
                new object()
            };
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
    }
}
