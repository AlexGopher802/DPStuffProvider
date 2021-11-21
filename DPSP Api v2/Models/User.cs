using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class User
    {
        public User()
        {
            ClientInfos = new HashSet<ClientInfo>();
            CourierInfos = new HashSet<CourierInfo>();
            VendorInfos = new HashSet<VendorInfo>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int IdType { get; set; }
        public int IdStatus { get; set; }

        public virtual UserStatus IdStatusNavigation { get; set; }
        public virtual UserType IdTypeNavigation { get; set; }
        public virtual ICollection<ClientInfo> ClientInfos { get; set; }
        public virtual ICollection<CourierInfo> CourierInfos { get; set; }
        public virtual ICollection<VendorInfo> VendorInfos { get; set; }
    }
}
