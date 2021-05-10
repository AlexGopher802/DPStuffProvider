using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSP_Api.Models.Views
{
    public class ClientsView
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } //Пароль хранится в хэш-строке SHA256
        public string Phone { get; set; }
        public string Email { get; set; }

        public ClientsView(Client client, PersonalInfo personalInfo, Contact contact)
        {
            Id = client.Id;
            LastName = personalInfo.LastName;
            FirstName = personalInfo.FirstName;
            Patronymic = personalInfo.Patronymic;
            Login = client.Login;

            string str = "";
            foreach (byte i in client.Password)
            {
                str += $"{i:X2}";
            }
            Password = str;

            Phone = contact.Phone;
            Email = contact.Email;
        }
    }
}
