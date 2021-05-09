using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class PersonalInfo
    {
        public PersonalInfo()
        {
            Clients = new HashSet<Client>();
            Couriers = new HashSet<Courier>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Courier> Couriers { get; set; }
    }
}
