using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class ProductDescription
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int IdProduct { get; set; }
        public int IdProductAttribute { get; set; }

        public virtual ProductAttribute IdProductAttributeNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
