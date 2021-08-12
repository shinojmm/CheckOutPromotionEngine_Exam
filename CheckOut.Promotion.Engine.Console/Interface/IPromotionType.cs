using CheckOut.Promotion.Engine.Entities;
using System.Collections.Generic;

namespace CheckOut.Promotion.Engine.Interface
{
    /// <summary>
    /// Type of Offers
    /// </summary>
    public interface IPromotionType
    {
        bool CanApply(OrderDetails orderDetails, List<Entities.Promotion> promotions);

        double CalculateOrderPrice(List<OrderDetails> orderDetails);
    }
}
