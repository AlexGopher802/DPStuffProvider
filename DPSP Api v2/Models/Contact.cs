using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class Contact
    {
        public Contact()
        {
            ClientInfos = new HashSet<ClientInfo>();
            CourierInfos = new HashSet<CourierInfo>();
        }

        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ClientInfo> ClientInfos { get; set; }
        public virtual ICollection<CourierInfo> CourierInfos { get; set; }
    }
}
