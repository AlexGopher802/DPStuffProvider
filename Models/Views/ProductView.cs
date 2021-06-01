using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSP_Api.Models.Views
{
    public class ProductView
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double? rating { get; set; }
        public bool? avail { get; set; }
        public string category { get; set; }
        public string store { get; set; }
        public int? quantity { get; set; } = 1;
    }
}
