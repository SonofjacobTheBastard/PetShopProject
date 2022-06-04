﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetShop.Client.Controllers;
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
