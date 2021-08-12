using CheckOut.Promotion.Engine.Entities;
using CheckOut.Promotion.Engine.Infrastructure;
using CheckOut.Promotion.Engine.Interface;
using CheckOut.Promotion.Engine.Presentation;
using CheckOut.Promotion.Engine.Repository;
using CheckOut.Promotion.Engine.Service;
using System;
using System.Collections.Generic;

namespace CheckOut.Promotion.Engine.BusinessLayer
{
    /// <summary>
    /// Responsible for CheckoutProducts,ApplyPromotion and DisplayTotalPrice
    /// </summary>
    public class BusinessObject
    {
        readonly IDisplay _consoleLayer;
        IPromotionEngineService _promotionEngineService;
        IRepository _configManagement;

        IEnumerable<OrderDetails> _orderDetails;
        Order _order;

        public BusinessObject()
        {
            _consoleLayer = new DisplayLayer();
            _promotionEngineService = new PromotionEngineService();
        }

        public bool OrderProducts()
        {
            try
            {
                _orderDetails = _consoleLayer.LoadUserInput();
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Error in Checking out Products :" + ex.Message);
            }
            return false;
        }

        internal bool ApplyPromotion()
        {
            try
            {
                _order = _promotionEngineService.ApplyPromotion(_orderDetails, LoadProductOffers());
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Error in  Applying Promotion :" + ex.Message);
            }
            return false;
        }

        public bool DisplayTotalPrice()
        {
            try
            {
                if (_order.OrderDetails != null)
                {
                    _consoleLayer.DisplayTotalPrice(_order);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Error in  Displaying TotalPrice:" + ex.Message);
            }

            return false;
        }
        public IEnumerable<Entities.Promotion> LoadProductOffers()
        {
            try
            {
                _configManagement = new ConfigRepository();
                return _configManagement.GetProductOffers();
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Error in Getting Product Offers :" + ex.Message);
            }
            return new List<Entities.Promotion>();
        }

        public IEnumerable<Product> GetAvailableProducts()
        {
            try
            {
                _configManagement = new ConfigRepository();
                return _configManagement.GetAvailableProducts();
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Error in Getting AvailableProducts :" + ex.Message);

            }
            return new List<Product>();
        }
    }
}
