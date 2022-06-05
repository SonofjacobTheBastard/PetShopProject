using Microsoft.EntityFrameworkCore;
using PetShop.Data.Base;
using PetShop.Data.Contexts;
using PetShop.Data.Interfaces;
using PetShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.UnitTests.Helpers
{

    public static class TestHelper
    {
        public static List<Animal> GetFakeAnimals()
        {
            return new List<Animal>()
                {
                    new Animal()
                    {
                        Id = 1,
                        Name = "omer",
                        CategoryId = 1,
                        Comments = new List<Comment>()
                      {
                          new Comment() { Content = "Opa"}
                      }
                    },
                  new Animal()
                  {
                      Id=2,
                      Name = "itai",
                        CategoryId = 1,
                      Comments = new List<Comment>()
                      {
                          new Comment() { Content = "gangam" },
                          new Comment() { Content = "style"}
                      }
                  },
                  new Animal()
                  {
                      Id=3,
                      Name = "asaf",
                        CategoryId = 2,
                      Comments = new List<Comment>()
                      {
                          new Comment() { Content = "op op op"},
                          new Comment() { Content = "Opa Gangam Style"}
                      }
                  }
                };
        }

        public static void Seed(DbContext context, IEnumerable<Animal> animals)
        {
            foreach (var animal in animals)
                context.Add(animal);

            context.SaveChanges();
        }

        public static void Cleanup<T>(PetShopDataContext context)
        {
            Type type = typeof(T);

            if (type == typeof(Animal))
                context.RemoveRange(context.Animals);
            if (type == typeof(Comment))
                context.RemoveRange(context.Comments);
            if (type == typeof(Category))
                context.RemoveRange(context.Categories);

            context.SaveChanges();
        }
    }
}
