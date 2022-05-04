using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Core.Models;

namespace WebApplication11.Core
{

    public interface ISalesPointRepository
    {
        Task<IEnumerable<SalesPoint>> List();
        Task<SalesPoint> GetById(int id);
        void Update(SalesPoint salesPoint);
        Task Create(SalesPoint salesPoint);
        void Delete(int id);
    }


}
