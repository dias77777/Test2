using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Core.Models;

namespace WebApplication11.Core
{

    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> List();
        Task<Sale> GetById(int id);
        void Update(Sale sale);
        Task Create(Sale sale);
        void Delete(int id);
    }


}
