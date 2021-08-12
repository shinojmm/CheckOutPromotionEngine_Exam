using CheckOut.Promotion.Engine.Entities;
using CheckOut.Promotion.Engine.Infrastructure;
using CheckOut.Promotion.Engine.Interface;
using CheckOut.Promotion.Engine.PromotionStrategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckOut.Promotion.Engine.Service
{

    public class PromotionEngineService : IPromotionEngineService
    {
        public Order ApplyPromotion(IEnumerable<OrderDetails> checkoutList, IEnumerable<Entities.Promotion> promotions)
        {
            Order appliedOffer = new();

            var strategies = new List<IPromotionType>
            {
                new SingleItemOffer(),
                new ComboOffer()
            };

            try
            {
                foreach (var item in checkoutList)
                {
                    if (item.Quantity > 0)
                    {
                        foreach (var strategy in strategies)
                        {
                            if (strategy.CanApply(item, promotions.ToList()))
                            {
                                item.HasOffer = true;
                                item.FinalPrice = strategy.CalculateOrderPrice(checkoutList.ToList());
                                appliedOffer.TotalPrice += item.FinalPrice;
                                break;
                            }
                        }
                    }
                }
                appliedOffer.OrderDetails = checkoutList.ToList();
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Error in Applying Promotion in PromotionStrategy:" + ex.Message);
            }

            return appliedOffer;
        }


    }
}
