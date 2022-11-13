using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models
{
    public class Rate
    {
        //TODO - Update database and model to include CreationDate prop
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public int RateValue { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string? Text { get; set; }
        public DateTime RateDate { get; set; }
    }
}
