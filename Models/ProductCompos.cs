using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class ProductCompos
    {
        public int? Id { get; set; }
        public int? Quantity { get; set; }
        public double? Summ { get; set; }
        public int? IdProduct { get; set; }
        public int? IdOrder { get; set; }

        public virtual Ordered IdOrderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
