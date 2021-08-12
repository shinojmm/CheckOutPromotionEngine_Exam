using CheckOut.Promotion.Engine.Entities;
using System.Collections.Generic;

namespace CheckOut.Promotion.Engine.Interface
{
    public interface IRepository
    {
        List<Product> GetAvailableProducts();

        List<Entities.Promotion> GetProductOffers();
    }
}
