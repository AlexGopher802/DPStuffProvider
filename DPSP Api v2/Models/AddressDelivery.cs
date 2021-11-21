using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class AddressDelivery
    {
        public AddressDelivery()
        {
            ClientAddresses = new HashSet<ClientAddress>();
            Ordereds = new HashSet<Ordered>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public int? FrontDoor { get; set; }
        public int? ApartmentNum { get; set; }
        public int? FloorNum { get; set; }
        public string Intercom { get; set; }

        public virtual ICollection<ClientAddress> ClientAddresses { get; set; }
        public virtual ICollection<Ordered> Ordereds { get; set; }
    }
}
