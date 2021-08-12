using System.Collections.Generic;
using CheckOut.Promotion.Engine.Entities;
using Promotions = CheckOut.Promotion.Engine.Entities.Promotion;
using CheckOut.Promotion.Engine.Interface;
using CheckOut.Promotion.Engine.Service;
using Xunit;
using Moq;

namespace CheckOut.Promotion.Engine.Test
{
    public class PromotionEngineServiceTest
    {
        List<Promotions> _promotions;

        IPromotionEngineService _promotionService;

        public Mock<IPromotionType> _mockPromotionType = new Mock<IPromotionType>();

        public PromotionEngineServiceTest()
        {
            _promotionService = new PromotionEngineService();
            _promotions = new List<Promotions>() { new Promotions { Type = "Single", ProductCode = "A", Price = 130, Quantity = 3 }, new Promotions { Type = "Single", ProductCode = "B", Price = 45, Quantity = 2 }, new Promotions() { Type = "Combo", ProductCode = "C;D", Price = 30, Quantity = 3 } };

        }

        /// <summary>
        /// 1	* A	50
        /// 1	* B	30
        /// 1	* C	20
        /// </summary>
        [Fact]
        public void ScenarioA_NoOffer()
        {
            var orderCart = new List<OrderDetails>() { new OrderDetails { Code = "A", Quantity = 1, DefaultPrice = 50 }, new OrderDetails { Code = "B", Quantity = 1, DefaultPrice = 30 }, new OrderDetails { Code = "C", Quantity = 1, DefaultPrice = 20 } };
            const double expectedValue = 100;

            var actualValue = _promotionService.ApplyPromotion(orderCart, _promotions).TotalPrice;
            Assert.Equal(expectedValue, actualValue);
        }

        /// <summary>
        /// Scenario B
        /// 5 * A =130 + 2*50
        /// 5 * B =45 + 45 + 30
        /// 1 * C =20
        //Total = 370 
        /// </summary>
        [Fact]
        public void ScenarioB_TwoOffer_Single()
        {
            var orderCart = new List<OrderDetails> { new OrderDetails { Code = "A", Quantity = 5, DefaultPrice = 50 }, new OrderDetails { Code = "B", Quantity = 5, DefaultPrice = 30 }, new OrderDetails { Code = "C", Quantity = 1, DefaultPrice = 20 } };
            const double expectedValue = 370;
            var actualValue = _promotionService.ApplyPromotion(
                orderCart,
                _promotions).TotalPrice;
            Assert.Equal(expectedValue, actualValue);
        }

        /// <summary>
        /// Scenario C
        /// 3* A =130
        /// 5* B =45 + 45 + 1 * 30
        /// 1* C =-
        /// 1* D =30
        /// </summary>
        [Fact]
        public void ScenarioC_TwoOffer_Combo()
        {
            var orderCart = new List<OrderDetails> { new OrderDetails { Code = "A", Quantity = 3, DefaultPrice = 50 }, new OrderDetails { Code = "B", Quantity = 5, DefaultPrice = 30 }, new OrderDetails { Code = "C", Quantity = 1, DefaultPrice = 20 }, new OrderDetails { Code = "D", Quantity = 1, DefaultPrice = 15 } };
            const double expectedValue = 280;
            var actualValue = _promotionService.ApplyPromotion(orderCart, _promotions).TotalPrice;
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
