using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Core.Models;
using WebApplication11.Persistence;

namespace WebApplication11
{
    public static class SeedData
    {
        public static async Task Seed(AppDbContext context)
        {
            var products = new Product[] {
                new Product {Id = 1, Name = "iPhone", Price =999.99M },
                new Product {Id = 2, Name = "MacBook", Price =2999.29M },
                new Product {Id = 3, Name = "Apple Watch", Price =1999.15M }

            };

            context.Products.AddRange(products);

            var salesPoints = new SalesPoint[] {
                new SalesPoint {Id = 1, Name = "Shop-1", ProvidedProducts ={ new ProvidedProduct {ProductId = products[0].Id, ProductQuantity=5 },
                                                                             new ProvidedProduct { ProductId = products[1].Id, ProductQuantity = 2 },
                                                                             new ProvidedProduct { ProductId = products[2].Id, ProductQuantity = 9 }}  },
                new SalesPoint {Id = 2, Name = "Shop-2", ProvidedProducts ={ new ProvidedProduct {ProductId = products[0].Id, ProductQuantity=2 },
                                                                             new ProvidedProduct { ProductId = products[1].Id, ProductQuantity = 1 },
                                                                             new ProvidedProduct { ProductId = products[2].Id, ProductQuantity = 0 }}  },
                new SalesPoint {Id = 3, Name = "Shop-3", ProvidedProducts ={ new ProvidedProduct {ProductId = products[0].Id, ProductQuantity=4 },
                                                                             new ProvidedProduct { ProductId = products[1].Id, ProductQuantity = 7 },
                                                                             new ProvidedProduct { ProductId = products[2].Id, ProductQuantity = 20 }}  }

            };

            context.SalesPoints.AddRange(salesPoints);

            var buyers = new Buyer[] {
                new Buyer {Name = "Jack"},
                new Buyer {Name = "Charley" },
                new Buyer {Name = "Michael" }

            };

            context.Buyers.AddRange(buyers);


            var sales = new Sale[] {
                new Sale {BuyerId = buyers[0].Id, Date =new DateTime(2022,4,29), Time =new TimeSpan(9,15,55), SalesPointId=salesPoints[0].Id, SalesData=GetNewSaleData(products[1], products[2]) },
                new Sale {BuyerId = buyers[0].Id, Date =new DateTime(2022,4,28), Time =new TimeSpan(9,25,59), SalesPointId=salesPoints[1].Id, SalesData=GetNewSaleData(products[0], products[1], products[2])},
                new Sale {BuyerId = buyers[1].Id, Date =new DateTime(2022,3,5), Time =new TimeSpan(12,17,25), SalesPointId=salesPoints[2].Id, SalesData=GetNewSaleData(products[2])},
                new Sale {BuyerId = buyers[2].Id, Date =new DateTime(2022,3,4), Time =new TimeSpan(17,19,22), SalesPointId=salesPoints[1].Id, SalesData=GetNewSaleData(products[0], products[1])},
                new Sale {BuyerId = buyers[2].Id, Date =new DateTime(2022,2,15), Time =new TimeSpan(19,29,7), SalesPointId=salesPoints[2].Id, SalesData=GetNewSaleData(products[0])}

            };

            foreach (var sale in sales)
                sale.TotalAmount = GetTotalAmount(sale.SalesData);

            context.Sales.AddRange(sales);

            buyers[0].SalesIds = new SalesId[] { new SalesId { Value = sales[0].Id }, new SalesId { Value = sales[1].Id } };
            buyers[1].SalesIds = new SalesId[] { new SalesId { Value = sales[2].Id } };
            buyers[2].SalesIds = new SalesId[] { new SalesId { Value = sales[3].Id }, new SalesId { Value = sales[4].Id } };



            await context.SaveChangesAsync();
        }
        static SaleData[] GetNewSaleData(params Product[] products)
        {

            var rnd = new Random();
            var arr = new SaleData[products.Length];
            int n = 0;
            foreach (var product in products)
            {
                int productQuantity = rnd.Next(0, 10);
                arr[n] = new SaleData { ProductId = product.Id, ProductQuantity = productQuantity, ProductIdAmount = product.Price * productQuantity };
                n++;
            }

            return arr;

        }
        static decimal GetTotalAmount(IList<SaleData> salesData)
        {
            decimal totalAmount = 0;
            foreach (var d in salesData)
                totalAmount += d.ProductIdAmount;

            return totalAmount;
        }
    }

}
