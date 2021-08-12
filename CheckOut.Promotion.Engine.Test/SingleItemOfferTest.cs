using System.Collections.Generic;
using CheckOut.Promotion.Engine.Entities;
using CheckOut.Promotion.Engine.Interface;
using CheckOut.Promotion.Engine.PromotionStrategies;
using Xunit;

namespace CheckOut.Promotion.Engine.Test
{
    public class SingleItemOfferTest
    {
        private readonly List<Entities.Promotion> _promotions;
        private readonly OrderDetails _productWithOffer;
        private readonly OrderDetails _productWithoutOffer;
        private readonly OrderDetails _productWithOfferExtra;
        private readonly IPromotionType _promotionStrategy;

        public SingleItemOfferTest()
        {
            _promotionStrategy = new SingleItemOffer();
            _productWithOffer = new OrderDetails() { Code = "A", Quantity = 3, DefaultPrice = 50 };
            _productWithOfferExtra = new OrderDetails() { Code = "A", Quantity = 4, DefaultPrice = 50 };
            _productWithoutOffer = new OrderDetails() { Code = "A", Quantity = 2, DefaultPrice = 50 };
            _promotions = new List<Entities.Promotion>() { new Entities.Promotion() { Type = "Single", ProductCode = "A", Price = 130, Quantity = 3 }, new Entities.Promotion() { Type = "Single", ProductCode = "B", Price = 45, Quantity = 2 }, new Entities.Promotion() { Type = "Combo", ProductCode = "C;D", Price = 30, Quantity = 3 } };
        }

        [Fact]
        public void Scenario_AdditionalItemOffer_WithOffer()
        {
            var orderCart = new List<OrderDetails> {_productWithOffer};
            const double expectedValue = 130;
            var canExecute = _promotionStrategy.CanApply(_productWithOffer, _promotions);
            if (canExecute)
            {
                var actualValue = _promotionStrategy.CalculateOrderPrice(orderCart);
                Assert.Equal(expectedValue, actualValue);
            }

        }

        [Fact]
        public void Scenario_AdditionalItemOffer_WithOffer_ExtraItems()
        {
            var orderCart = new List<OrderDetails> {_productWithOfferExtra};
            const double expectedValue = 180;
            var canExecute = _promotionStrategy.CanApply(_productWithOfferExtra, _promotions);
            if (canExecute)
            {
                var actualValue = _promotionStrategy.CalculateOrderPrice(orderCart);
                Assert.Equal(expectedValue, actualValue);
            }

        }
        [Fact]
        public void Scenario_AdditionalItemOffer_WithoutOffer()
        {
            var orderCart = new List<OrderDetails>();
            orderCart.Add(_productWithoutOffer);
            const double expectedValue = 100;
            var canExecute = _promotionStrategy.CanApply(_productWithoutOffer, _promotions);
            if (canExecute)
            {
                var actualValue = _promotionStrategy.CalculateOrderPrice(orderCart);
                Assert.Equal(expectedValue, actualValue);
            }

        }
    }
}
