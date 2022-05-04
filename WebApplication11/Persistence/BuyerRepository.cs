using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Core;
using WebApplication11.Core.Models;

namespace WebApplication11.Persistence
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly AppDbContext _context;

        public BuyerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Buyer buyer)
        {
            await _context.Buyers.AddAsync(buyer);
        }

        public void Delete(int id)
        {
            var buyer = new Buyer { Id = id };
            _context.Entry(buyer).State = EntityState.Deleted;
        }

        public async Task<Buyer> GetById(int id)
        {
            return await _context.Buyers.Include(x => x.SalesIds).SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Buyer>> List()
        {
            return await _context.Buyers.Include(x => x.SalesIds).ToListAsync();
        }

        public void Update(Buyer buyer)
        {
            _context.Buyers.Update(buyer);
        }
    }
}

