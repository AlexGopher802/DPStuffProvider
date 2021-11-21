using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int IdProduct { get; set; }

        public virtual Product IdProductNavigation { get; set; }
    }
}
