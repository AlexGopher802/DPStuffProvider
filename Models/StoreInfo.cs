using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class StoreInfo
    {
        public StoreInfo()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public string Tin { get; set; }
        public string Bank { get; set; }
        public string Bic { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
