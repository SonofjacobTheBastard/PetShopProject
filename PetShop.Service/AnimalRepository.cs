using PetShop.Data.Base;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service
{
    public class AnimalRepository : EntityBaseRepository<Animal>, IAnimalsService
    {
        public AnimalRepository(PetShopDataContext context) : base(context) { }

        //public async Task AddAnimalAsync(Animal animal) => await AddAsync(animal);
        public IEnumerable<Animal> GetAnimalsByCategoryId(int id) => GetAllAsync().Result.Where(a => a.CategoryId == id);

        public async Task AddComment(int id, string content)
        {
            Animal animal = GetByIdAsync(id).Result;
            Comment comment = new Comment() {Content = content, AnimalId = id };
            animal.Comments.Add(comment);
            await UpdateAsync(id, animal);
        } 
    }
}
