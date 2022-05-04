using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Core;
using WebApplication11.Core.Models;

namespace WebApplication11.Persistence
{
    public class SalesPointRepository : ISalesPointRepository
    {
        private readonly AppDbContext _context;

        public SalesPointRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(SalesPoint salesPoint)
        {
            await _context.SalesPoints.AddAsync(salesPoint);
        }

        public void Delete(int id)
        {
            var salesPoint = new SalesPoint { Id = id };
            _context.Entry(salesPoint).State = EntityState.Deleted;
        }

        public async Task<SalesPoint> GetById(int id)
        {
            return await _context.SalesPoints.Include(x => x.ProvidedProducts).SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<SalesPoint>> List()
        {
            return await _context.SalesPoints.Include(x => x.ProvidedProducts).ToListAsync();
        }

        public void Update(SalesPoint salesPoint)
        {
            _context.SalesPoints.Update(salesPoint);
        }
    }
}

