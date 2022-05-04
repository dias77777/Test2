using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Core;
using WebApplication11.Core.Models;

namespace WebApplication11.Persistence
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
        }

        public void Delete(int id)
        {
            var sale = new Sale { Id = id };
            _context.Entry(sale).State = EntityState.Deleted;
        }

        public async Task<Sale> GetById(int id)
        {
            return await _context.Sales.Include(x => x.SalesData).SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Sale>> List()
        {
            return await _context.Sales.Include(x => x.SalesData).ToListAsync();
        }

        public void Update(Sale sale)
        {
            _context.Sales.Update(sale);
        }
    }
}

