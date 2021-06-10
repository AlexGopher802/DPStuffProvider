using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DPSP_Api.Models
{
    [Serializable]
    public class CourierView
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("LastName")]
        public string LastName { get; set; }
        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("Patronymic")]
        public string Patronymic { get; set; }
        [JsonPropertyName("Login")]
        public string Login { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; } //Пароль хранится в хэш-строке SHA256
        [JsonPropertyName("OrderQuantity")]
        public int? OrderQuantity { get; set; }
        [JsonPropertyName("Phone")]
        public string Phone { get; set; }
        [JsonPropertyName("Email")]
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

        public CourierView()
        {

        }
    }
}
