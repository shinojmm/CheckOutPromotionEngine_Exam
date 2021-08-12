using Microsoft.Extensions.Configuration;
using CheckOut.Promotion.Engine.Entities;
using CheckOut.Promotion.Engine.Infrastructure;
using CheckOut.Promotion.Engine.Interface;
using System;
using System.Collections.Generic;
using System.IO;

namespace CheckOut.Promotion.Engine.Repository
{

    public class ConfigRepository : IRepository
    {
        IConfiguration configuration;


        public ConfigRepository()
        {
            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(Constants.DataStore, false);

                configuration = builder.Build();
            }
            catch (UnauthorizedAccessException ex)
            {
                LogWriter.LogWrite("Error in Config file Loading :" + ex.Message);
            }
            catch (Exception ex)
            {

                LogWriter.LogWrite("Error in Config file Loading :" + ex.Message);
            }

        }

        public List<Product> GetAvailableProducts()
        {
            var productList = new List<Product>();

            foreach (var item in configuration.GetSection(Constants.Products).GetChildren())
            {
                var product = new Product();
                configuration.GetSection(item.Path).Bind(product);
                productList.Add(product);
            }

            return productList;
        }

        public List<Entities.Promotion> GetProductOffers()
        {
            var lstPromotions = new List<Entities.Promotion>();
            foreach (var item in configuration.GetSection(Constants.Promotions).GetChildren())
            {
                var product = new Entities.Promotion();
                configuration.GetSection(item.Path).Bind(product);
                lstPromotions.Add(product);
            }
            return lstPromotions;
        }
    }
}
