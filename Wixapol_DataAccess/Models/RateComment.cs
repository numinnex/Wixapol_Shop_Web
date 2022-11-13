using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models
{
    public class RateComment
    {
        public int RateId { get; set; }
        public string UserId { get; set; }
        public bool? Like { get; set; }
        public bool? DisLike { get; set; }
        public string? Comment { get; set; }
    }
}
