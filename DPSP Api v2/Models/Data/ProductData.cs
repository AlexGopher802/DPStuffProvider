using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSPApiV2.Models.Data
{
    public class ProductData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Rating { get; set; }
        public bool Avail { get; set; }
        public string Category { get; set; }
        public string Vendor { get; set; }
        public List<ProductImageData> Images { get; set; }
        public List<ProductAttributeData> Attributes { get; set; }
    }
}
