using System.Threading.Tasks;
using System.Transactions;
using WebApplication11.Core;

namespace WebApplication11.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
    }


}
