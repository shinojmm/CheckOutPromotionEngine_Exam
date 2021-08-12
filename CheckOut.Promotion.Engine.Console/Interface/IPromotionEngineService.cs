using CheckOut.Promotion.Engine.Entities;
using System.Collections.Generic;

namespace CheckOut.Promotion.Engine.Interface
{
    public interface IPromotionEngineService
    {
        Order ApplyPromotion(IEnumerable<OrderDetails> orderDetails, IEnumerable<Entities.Promotion> promotions);
    }
}
