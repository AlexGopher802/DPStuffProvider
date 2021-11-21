using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class VendorInfo
    {
        public VendorInfo()
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
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
