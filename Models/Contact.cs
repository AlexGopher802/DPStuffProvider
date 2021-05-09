using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class Contact
    {
        public Contact()
        {
            Clients = new HashSet<Client>();
            Couriers = new HashSet<Courier>();
        }

        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Courier> Couriers { get; set; }
    }
}
