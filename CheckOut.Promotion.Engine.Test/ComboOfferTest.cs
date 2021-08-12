using System.Collections.Generic;
using System.Linq;
using CheckOut.Promotion.Engine.Entities;
using CheckOut.Promotion.Engine.Interface;
using CheckOut.Promotion.Engine.PromotionStrategies;
using Xunit;

namespace CheckOut.Promotion.Engine.Test
{
    public class ComboOfferTest
    {
        private readonly List<Entities.Promotion> _promotions;
        private readonly IPromotionType _promotionStrategy;
        private readonly List<OrderDetails> _productWithOffer;
        private readonly List<OrderDetails> _productWithoutOffer;

        
        public ComboOfferTest()
        {
            _promotionStrategy = new ComboOffer();
            _productWithoutOffer = new List<OrderDetails>() { new() { Code = "C", Quantity = 1, DefaultPrice = 20 } };
            _productWithOffer = new List<OrderDetails>() { new() { Code = "C", Quantity = 1, DefaultPrice = 20 }, new() { Code = "D", Quantity = 1, DefaultPrice = 15 } };
            _promotions = new List<Entities.Promotion>() { new() { Type = "Single", ProductCode = "A", Price = 130, Quantity = 3 }, new() { Type = "Single", ProductCode = "B", Price = 45, Quantity = 2 }, new() { Type = "Combo", ProductCode = "C;D", Price = 30, Quantity = 3 } };
        }

        [Fact]
        public void Scenario_ComboOffer_WithOffer()
        {
            const double expectedValue = 30;
            var canExecute = _promotionStrategy.CanApply(_productWithOffer.FirstOrDefault(), _promotions);
            if (canExecute)
            {
                double actualValue = _promotionStrategy.CalculateOrderPrice(_productWithOffer);
                Assert.Equal(expectedValue, actualValue);
            }

        }
        [Fact]
        public void Scenario_ComboOffer_WithoutOffer()
        {
            const double expectedValue = 20;
            var canExecute = _promotionStrategy.CanApply(_productWithoutOffer.FirstOrDefault(), _promotions);
            if (canExecute)
            {
                var actualValue = _promotionStrategy.CalculateOrderPrice(_productWithoutOffer);
                Assert.Equal(expectedValue, actualValue);
            }

        }
    }
}
