using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSP_Api.Models
{
    public class CourierView
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } //Пароль хранится в хэш-строке SHA256
        public int? OrderQuantity { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public CourierView(Courier courier, PersonalInfo personalInfo, Contact contact)
        {
            Id = courier.Id;
            LastName = personalInfo.LastName;
            FirstName = personalInfo.FirstName;
            Patronymic = personalInfo.Patronymic;
            Login = courier.Login;

            string str = "";
            foreach(byte i in courier.Password)
            {
                str += $"{i:X2}";
            }
            Password = str;

            OrderQuantity = courier.OrderQuantity;
            Phone = contact.Phone;
            Email = contact.Email;
        }
    }
}
