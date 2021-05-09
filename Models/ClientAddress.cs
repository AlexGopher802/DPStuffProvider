using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class ClientAddress
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public int IdAddress { get; set; }

        public virtual AddressDelivery IdAddressNavigation { get; set; }
        public virtual Client IdClientNavigation { get; set; }
    }
}
