using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Core.Models;

namespace WebApplication11.Core
{

        public interface IBuyerRepository
        {
            Task<IEnumerable<Buyer>> List();
            Task<Buyer> GetById(int id);
            void Update(Buyer buyer);
            Task Create(Buyer buyer);
            void Delete(int id);
        }

    
}
