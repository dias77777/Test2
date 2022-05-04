using System.Threading.Tasks;
using WebApplication11.Core.Models;

namespace WebApplication11.Core
{

    public interface ISaleService
    {
        Task<int> Order(Order order);

    }


}
