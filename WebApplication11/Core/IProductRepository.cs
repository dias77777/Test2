using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Core.Models;

namespace WebApplication11.Core
{

        public interface IProductRepository
    {
            Task<IEnumerable<Product>> List();
            Task<Product> GetById(int id);
            void Update(Product product);
            Task Create(Product product);
            void Delete(int id);
        }

    
}
