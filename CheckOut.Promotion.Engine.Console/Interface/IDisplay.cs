using CheckOut.Promotion.Engine.Entities;
using System.Collections.Generic;

namespace CheckOut.Promotion.Engine.Interface
{
    /// <summary>
    /// Interface to access various functionality for presentation .
    /// </summary>
    public interface IDisplay
    {
        /// <summary>
        /// Load UserInput
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderDetails> LoadUserInput();

        /// <summary>
        /// Display TotalPrice
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool DisplayTotalPrice(Order order);
    }
}
