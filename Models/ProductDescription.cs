using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class ProductDescription
    {
        public int Id { get; set; }
        public string AttrValue { get; set; }
        public int IdProduct { get; set; }
        public int IdProductAttribute { get; set; }

        public virtual ProductAttribute IdProductAttributeNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
