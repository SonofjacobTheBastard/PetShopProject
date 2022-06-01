using PetShop.Data.Base;
using PetShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Interfaces
{
    public interface ICommentsService : IEntityBaseRepository<Comment>
    {
    }
}
