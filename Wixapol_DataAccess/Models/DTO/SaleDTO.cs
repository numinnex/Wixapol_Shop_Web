using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models.DTO
{
    public class SaleDTO
    {
        public string UserId { get; set; }
        public DateTime? SaleDate { get; set; } = DateTime.Now;
        public DateTime? ShippingDate { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string SessionId { get; set; }
        public string PaymentIntentId { get; set; }
        public int? OrderId { get; set; }


    }
}
