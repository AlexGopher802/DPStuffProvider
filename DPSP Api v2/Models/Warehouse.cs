using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            WarehouseProducts = new HashSet<WarehouseProduct>();
        }

        public int Id { get; set; }
        public string Address { get; set; }

        public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; }
    }
}
