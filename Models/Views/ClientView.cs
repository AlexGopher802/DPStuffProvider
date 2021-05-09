using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSP_Api.Models
{
    public class ClientView
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ClientView(Client client, PersonalInfo personalInfo, Contact contact)
        {
            Id = client.Id;
            LastName = personalInfo.LastName;
            FirstName = personalInfo.FirstName;
            Patronymic = personalInfo.Patronymic;
            Phone = contact.Phone;
            Email = contact.Email;
        }
    }
}
