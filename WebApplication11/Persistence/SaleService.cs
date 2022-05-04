using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using WebApplication11.Core;
using WebApplication11.Core.Models;


namespace WebApplication11.Persistence
{
    public class SaleService : ISaleService
    {
        private readonly AppDbContext _context;


        public SaleService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<int> Order(Order order)
        {

            var salesPoint = await _context.SalesPoints.Include(x => x.ProvidedProducts).SingleOrDefaultAsync(i => i.Id == order.SalesPointId);
            if (salesPoint == null) throw new Exception($"Sales point {order.SalesPointId} not found.");


            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {

                List<int> productIds = new List<int>();
                var saleData = new List<SaleData>();
                decimal totalAmount = 0;
                foreach (var orderItem in order.SalesData)
                {
                    var providedProduct = salesPoint.ProvidedProducts.SingleOrDefault(x => x.ProductId == orderItem.ProductId);
                    if (providedProduct == null) throw new Exception($"Product (productId: {orderItem.ProductId}) not found.");

                    if (providedProduct.ProductQuantity < orderItem.ProductQuantity) throw new Exception($"Not enough items (productId: {orderItem.ProductId}) in the store. In stock: {providedProduct.ProductQuantity}.");

                    var price = await GetProductPrice(orderItem.ProductId);
                    if (price == null) throw new Exception($"Product (productId: {orderItem.ProductId}) not found.");

                    decimal amount = (decimal)price * orderItem.ProductQuantity;
                    totalAmount += amount;
                    saleData.Add(new SaleData() { ProductId = orderItem.ProductId, ProductQuantity = orderItem.ProductQuantity, ProductIdAmount = amount });

                    providedProduct.ProductQuantity -= orderItem.ProductQuantity;
                    productIds.Add(orderItem.ProductId);
                }
                var now = DateTime.Now;

                var sale = new Sale()
                {
                    BuyerId = order.BuyerId,
                    SalesPointId = order.SalesPointId,
                    SalesData = saleData,
                    TotalAmount = totalAmount,
                    Date = now.Date,
                    Time = now.TimeOfDay
                };


                _context.Sales.Add(sale);

                if (order.BuyerId != null)
                {
                    var buyer = _context.Buyers.Find(order.BuyerId.Value);
                    buyer.SalesIds.Add(new SalesId { Value = sale.Id });
                }

                await _context.SaveChangesAsync();

                return sale.Id;


            }


            async Task<decimal?> GetProductPrice(int id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null) return null;
                return product.Price;
            }


        }

    }
}

