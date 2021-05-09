using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class ProductAttribute
    {
        public ProductAttribute()
        {
            ProductDescriptions = new HashSet<ProductDescription>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductDescription> ProductDescriptions { get; set; }
    }
}
