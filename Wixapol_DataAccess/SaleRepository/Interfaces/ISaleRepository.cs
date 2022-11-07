using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wixapol_DataAccess.Models;
using Wixapol_DataAccess.Models.DTO;

namespace Wixapol_DataAccess.SaleRepository.Interfaces
{
    public interface ISaleRepository
    {
        public int CreateSale(Sale sale);
        public void UpdateSale(Sale sale);
        public void UpdateSessionInformation(int id, string sessionId, string paymentIntentId);
        public void UpdateStatus(int id, string orderStatus, string paymentStatus);
        public void DeleteSale(int? id);
        public List<Sale> GetAll();
        public Sale GetById(int? id);
    }
}
