using CheckOut.Promotion.Engine.Entities;
using CheckOut.Promotion.Engine.Interface;
using CheckOut.Promotion.Engine.Repository;
using System;
using System.Collections.Generic;

namespace CheckOut.Promotion.Engine.Presentation
{
   public class DisplayLayer: IDisplay
    {
        ConfigRepository _configManagement;

        public DisplayLayer()
        {
            _configManagement = new ConfigRepository();
        }

        public IEnumerable<OrderDetails> LoadUserInput()
        {
            var orderDetailsList = new List<OrderDetails>();
            var lstProduct = LoadAvailableProducts();

            Console.WriteLine("Enter User Inputs");
            try
            {
               
                foreach (var item in lstProduct)
                {
                    Console.WriteLine("Input quantity of " + item.Code);
                    var quantity = Convert.ToInt32(Console.ReadLine());

                    orderDetailsList.Add(new OrderDetails
                    {
                        Code = item.Code,
                        Quantity = quantity,
                        DefaultPrice = item.Price
                    });
                }

            }
            catch (FormatException ex)
            {

                Console.WriteLine("Error in User Entry: " + ex.Message);
            }
            catch (OverflowException ex)
            {

                Console.WriteLine("Error in User Entry: " + ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error in User Entry: "+ ex.Message);
            }
           

            return orderDetailsList;
        }

        private List<Product> LoadAvailableProducts()
        {
            return _configManagement.GetAvailableProducts();
        }
        public bool DisplayTotalPrice(Order order)
        {
            Console.WriteLine("Calculating Final Price..........................");
            Console.WriteLine("ProductCode" + "-" + "Quantity" + " - " + "FinalPrice" + " - " + "HasOffer");
            foreach (var item in order.OrderDetails)
            {
                Console.WriteLine(item.Code + "-" + item.Quantity + "-" + item.FinalPrice + "-" + item.HasOffer);
            }
            Console.WriteLine("Total Price : " + order.TotalPrice);
            return true;
        }
    }
}
