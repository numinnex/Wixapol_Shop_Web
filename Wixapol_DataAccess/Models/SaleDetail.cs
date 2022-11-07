using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models
{
    public class SaleDetail
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public DateTime WarrantyExpiration { get; set; }
        public double? DiscountAmount { get; set; }
        public int Quantity { get; set; }
        public double? SubTotal { get; set; }
        public double? Tax { get; set; }
        public double? Total { get; set; }
    }
}
