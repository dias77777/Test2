using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Core;
using WebApplication11.Core.Models;

namespace WebApplication11.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Delete(int id)
        {
            var product = new Product { Id = id };
            _context.Entry(product).State = EntityState.Deleted;
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Product>> List()
        {
            return await _context.Products.ToListAsync();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}

