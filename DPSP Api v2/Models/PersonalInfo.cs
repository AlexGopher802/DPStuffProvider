using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class PersonalInfo
    {
        public PersonalInfo()
        {
            ClientInfos = new HashSet<ClientInfo>();
            CourierInfos = new HashSet<CourierInfo>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }

        public virtual ICollection<ClientInfo> ClientInfos { get; set; }
        public virtual ICollection<CourierInfo> CourierInfos { get; set; }
    }
}
