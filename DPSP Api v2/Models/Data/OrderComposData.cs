using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSPApiV2.Models.Data
{
    public class OrderComposData
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Vendor { get; set; }
        public int Quantity { get; set; }
    }
}
