using CheckOut.Promotion.Engine.BusinessLayer;
using CheckOut.Promotion.Engine.Infrastructure;
using System;

namespace CheckOut.Promotion.Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LogWriter.LogWrite("Promotion Engine is initialized : ");

                var businessObject = new BusinessObject();


                businessObject.OrderProducts();  

                businessObject.ApplyPromotion();

                businessObject.DisplayTotalPrice(); 

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Exception in Promotion Engine .... : " + ex.Message);
            }
        }
    }
}
