using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.GenericRepository.Implementation;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.RateRepository.Interfaces;

namespace Wixapol_DataAccess.RateRepository.Implementation
{
    public class RateRepository : Repository<Rate>, IRateRepository
    {
        private readonly IConfiguration _config;
        public RateRepository(IConfiguration config) : base(config)
        {
            _config = config;
        }

        public void CreateRate(Rate rate)
        {
            SaveData("db_product.spRate_Insert", new
            {
                ProductId = rate.ProductId,
                UserId = rate.UserId,
                RateValue = rate.RateValue,
                Likes = 0,
                Dislikes = 0,
                Text = rate.Text,
                RateDate = rate.RateDate

            }, "DefualtConnection");
        }

        public void DeleteRate(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rate> GetAll()
        {
            throw new NotImplementedException();
        }

        public Rate GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rate> GetByProductId(int productId)
        {
            return LoadData("db_product.spRate_GetByProductId", new { ProductId = productId }, "DefualtConnection");
        }

        public void UpdateRate(Rate rate)
        {
            throw new NotImplementedException();
        }
    }
}
