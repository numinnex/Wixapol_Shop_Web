using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public double? DiscountAmount { get; set; }
        public double? SubTotal { get; set; }
        public double? TaxAmount { get; set; }
        public double? Total { get; set; }
        public string UserId { get; set; }

    }
}
