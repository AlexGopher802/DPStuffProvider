using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Ordereds = new HashSet<Ordered>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ordered> Ordereds { get; set; }
    }
}
