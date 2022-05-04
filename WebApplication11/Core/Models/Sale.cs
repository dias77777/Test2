using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication11.Core.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        public int SalesPointId { get; set; }
        public int? BuyerId { get; set; }


        public IList<SaleData> SalesData { get; set; } = new List<SaleData>();
        public decimal TotalAmount { get; set; }


    }
    public class SaleData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductIdAmount { get; set; }

    }

    public class Order
    {
        public int SalesPointId { get; set; }
        public int? BuyerId { get; set; }

        public IList<OrderItem> SalesData { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public int ProductId { get; private set; }
        public int ProductQuantity { get; private set; }
        public OrderItem(int productId, int productQuantity)
        {
            ProductId = productId;
            ProductQuantity = productQuantity;
        }
    }



}
