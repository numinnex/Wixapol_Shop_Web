using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.GenericRepository.Implementation;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.SaleDetailRepository.Interfaces;

namespace Wixapol_DataAccess.SaleDetailRepository.Implemenation
{
    public class SaleDetailRepository : Repository<SaleDetail>, ISaleDetailRepository
    {
        private readonly IConfiguration _config;
        public SaleDetailRepository(IConfiguration config) : base(config)
        {
            _config = config;
        }

        public void CreateSaleDetail(SaleDetail saleDetail)
        {
            SaveData("db_product.spSaleDetail_Insert", saleDetail, "DefualtConnection");
        }

        public void DeleteSaleDetail(int? id)
        {
            throw new NotImplementedException();
        }

        public List<SaleDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public SaleDetail GetById(int? id)
        {
            return LoadData("", new { Id = id }, "DefualtConnection").FirstOrDefault();
        }

        public List<SaleDetail> GetBySaleId(int? id)
        {
            throw new NotImplementedException();
        }

        public void UpdateSaleDetail(SaleDetail saleDetail)
        {
            throw new NotImplementedException();
        }
    }
}
