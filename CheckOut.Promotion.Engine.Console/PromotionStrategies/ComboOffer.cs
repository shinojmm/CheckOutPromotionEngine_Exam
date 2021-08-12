using CheckOut.Promotion.Engine.Entities;
using CheckOut.Promotion.Engine.Infrastructure;
using CheckOut.Promotion.Engine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckOut.Promotion.Engine.PromotionStrategies
{
    public class ComboOffer : IPromotionType
    {
        Entities.Promotion _appliedPromotion;
        OrderDetails _recentProductCheckout;
        List<OrderDetails> _orderDetails;

        public bool CanApply(OrderDetails orderDetails, List<Entities.Promotion> promotions)
        {
            _recentProductCheckout = orderDetails;
            _appliedPromotion = promotions.Where(x => x.ProductCode.Split(';').Contains(orderDetails.Code)).FirstOrDefault();
            if (_appliedPromotion != null && !orderDetails.IsValidated && _appliedPromotion.Type == PromotionTypeConstants.Combo)
            {
                return true;
            }

            return false;
        }


        public double CalculateOrderPrice(List<OrderDetails> productCheckoutList)
        {
            _orderDetails = new List<OrderDetails>();

            double finalPrice = 0;


            try
            {
                var str = _appliedPromotion.ProductCode.Split(';').ToArray();
                foreach (OrderDetails item in productCheckoutList)
                {
                    if (str.Contains(item.Code))
                    {
                        _orderDetails.Add(item);
                        item.IsValidated = true;
                    }
                }

                var quantity_first = 0;
                var quantity_second = 0;
                if (_orderDetails.Count > 1)
                {
                    quantity_first = _orderDetails[0].Quantity;
                    quantity_second = _orderDetails[1].Quantity;
                }
                if (quantity_first == 0 || quantity_second == 0)
                {
                    return _recentProductCheckout.DefaultPrice;

                }

                if (quantity_first == quantity_second)
                {
                    finalPrice = _appliedPromotion.Price * quantity_first;
                }
                else if (quantity_first > quantity_second)
                {
                    var additionalItems = quantity_first - quantity_second;
                    finalPrice = (_recentProductCheckout.DefaultPrice * additionalItems) + (_appliedPromotion.Price * quantity_second);
                }
                else if (quantity_first < quantity_second)
                {
                    var additionalItems = quantity_second - quantity_first;
                    finalPrice = (_recentProductCheckout.DefaultPrice * additionalItems) + (_appliedPromotion.Price * quantity_first);
                }
            }
            catch (ArithmeticException ex)
            {
                LogWriter.LogWrite("Error in ComboOffer :" + ex.Message);
            }
            catch (Exception e)
            {
                LogWriter.LogWrite("Error in ComboOffer :" + e.Message);
            }

            return finalPrice;
        }


    }
}
