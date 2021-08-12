using CheckOut.Promotion.Engine.Entities;
using CheckOut.Promotion.Engine.Infrastructure;
using CheckOut.Promotion.Engine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckOut.Promotion.Engine.PromotionStrategies
{
    public class SingleItemOffer : IPromotionType
    {
        private Entities.Promotion _appliedPromotion;
        private OrderDetails _productCheckout;

        public SingleItemOffer()
        {
            _appliedPromotion = new Entities.Promotion();
            _productCheckout = new OrderDetails();
        }

        /// <summary>
        /// Can Execute
        /// </summary>
        /// <param name="product"></param>
        /// <param name="promotions"></param>
        /// <returns></returns>
        public bool CanApply(OrderDetails product, List<Entities.Promotion> promotions)
        {
            _productCheckout = product;
            _appliedPromotion = promotions.Where(x => x.ProductCode == product.Code).FirstOrDefault();
            if (_appliedPromotion != null && _appliedPromotion.Type == PromotionTypeConstants.Single)
            {
                product.IsValidated = true;
                return true;
            }

            return false;
        }

        public double CalculateOrderPrice(List<OrderDetails> productCheckoutList)
        {
            double finalPrice = 0;
            try
            {
                int totalEligibleItems = _productCheckout.Quantity / _appliedPromotion.Quantity;
                int remainingItems = _productCheckout.Quantity % _appliedPromotion.Quantity;
                finalPrice = _appliedPromotion.Price * totalEligibleItems + remainingItems * (_productCheckout.DefaultPrice);

            }
            catch (ArithmeticException ex)
            {
                LogWriter.LogWrite("Error in AdditionalItemOffer :" + ex.Message);
            }
            catch (Exception e)
            {
                LogWriter.LogWrite("Error in AdditionalItemOffer :" + e.Message);
            }

            return finalPrice;
        }
    }
}
