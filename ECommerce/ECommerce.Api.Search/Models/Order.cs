using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
        // public string CustomerName { get; set; }
    }
}
