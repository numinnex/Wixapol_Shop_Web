using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;

namespace Wixapol_DataAccess.RateRepository.Interfaces
{
    public interface IRateRepository
    {
        public void CreateRate(Rate rate);
        public List<Rate> GetByProductId(int productId);
        public Rate GetById(int id);
        public List<Rate> GetAll();
        public void UpdateRate(Rate rate);
        public void DeleteRate(int id);

    }
}
