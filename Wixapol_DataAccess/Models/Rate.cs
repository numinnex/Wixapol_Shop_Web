using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_DataAccess.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        [Required]
        [Range(1, 5)]
        public int RateValue { get; set; } = 1;
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        [Required]
        [MaxLength(500)]
        public string? Text { get; set; }
        public DateTime RateDate { get; set; } = DateTime.UtcNow;
    }
}
