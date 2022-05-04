using System.Threading.Tasks;

namespace WebApplication11.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }

}
