using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WixapolShop.Areas.Customer.Models
{
    public class SearchModel
    {
        public string? Phrase { get; set; }
        public int Categoryid { get; set; }
    }
}
