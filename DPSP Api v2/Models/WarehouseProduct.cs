using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class WarehouseProduct
    {
        public int Id { get; set; }
        public int IdWarehouse { get; set; }
        public int IdProduct { get; set; }

        public virtual Product IdProductNavigation { get; set; }
        public virtual Warehouse IdWarehouseNavigation { get; set; }
    }
}
