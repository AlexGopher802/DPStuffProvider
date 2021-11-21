using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class OrderProduct
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public float Summ { get; set; }
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }

        public virtual Ordered IdOrderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
