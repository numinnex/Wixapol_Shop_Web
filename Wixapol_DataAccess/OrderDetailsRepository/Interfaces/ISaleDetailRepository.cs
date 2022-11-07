using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;

namespace Wixapol_DataAccess.SaleDetailRepository.Interfaces
{
    public interface ISaleDetailRepository
    {
        public void CreateSaleDetail(SaleDetail saleDetail);
        public void UpdateSaleDetail(SaleDetail saleDetail);
        public void DeleteSaleDetail(int? id);
        public List<SaleDetail> GetAll();
        public SaleDetail GetById(int? id);
        public List<SaleDetail> GetBySaleId(int? id);


    }
}
