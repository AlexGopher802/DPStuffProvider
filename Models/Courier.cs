using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class Courier
    {
        public Courier()
        {
            Ordereds = new HashSet<Ordered>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public int? OrderQuantity { get; set; }
        public int IdPersonalInfo { get; set; }
        public int IdContacts { get; set; }

        public virtual Contact IdContactsNavigation { get; set; }
        public virtual PersonalInfo IdPersonalInfoNavigation { get; set; }
        public virtual ICollection<Ordered> Ordereds { get; set; }
    }
}
