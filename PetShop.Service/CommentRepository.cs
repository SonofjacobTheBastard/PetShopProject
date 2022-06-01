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
    public class CommentRepository : EntityBaseRepository<Comment>, ICommentsService
    {
        public CommentRepository(PetShopDataContext context) : base(context) { }
        
    }
}
