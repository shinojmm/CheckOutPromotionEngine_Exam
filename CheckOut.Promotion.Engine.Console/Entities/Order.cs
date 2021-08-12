using System.Collections.Generic;

namespace CheckOut.Promotion.Engine.Entities
{
    public class Order
    {
        public List<OrderDetails> OrderDetails { get; set; }
        public double TotalPrice { get; set; }
    }
}
