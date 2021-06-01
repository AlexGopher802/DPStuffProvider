using DPSP_Api.Models.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DPSP_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly DPSPDBContext _context;

        public ClientsController(DPSPDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Авторизация клиента по логину и паролю
        /// </summary>
        [HttpGet]
        [Route("{login}/{password}")]
        public ActionResult<ClientsView> GetClient(string login, string password)
        {
            var result = from client in _context.Clients
                         where client.Login == login && client.Password == SHA256.Create().ComputeHash(Encoding.Default.GetBytes(password))
                         join person in _context.PersonalInfos on client.IdPersonalInfo equals person.Id
                         join contacts in _context.Contacts on client.IdContacts equals contacts.Id
                         select new ClientsView(client, person, contacts);

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        public ActionResult<ClientsView> RegClient(ClientsView clientInfo, string noHashPassword)
        {
            PersonalInfo person = new PersonalInfo() { LastName = clientInfo.LastName, FirstName = clientInfo.FirstName, Patronymic = clientInfo.Patronymic };
            _context.PersonalInfos.Add(person);

            Contact contact = new Contact() { Email = clientInfo.Email, Phone = clientInfo.Phone };
            _context.Contacts.Add(contact);

            Client client = new Client() { IdPersonalInfoNavigation = person, IdContactsNavigation = contact, Login = clientInfo.Login,
                Password = SHA256.Create().ComputeHash(Encoding.Default.GetBytes(noHashPassword))
            };
            _context.Clients.Add(client);

            _context.SaveChanges();

            var result = from clientR in _context.Clients
                         where clientR.Login == clientInfo.Login
                         join personR in _context.PersonalInfos on clientR.IdPersonalInfo equals personR.Id
                         join contacts in _context.Contacts on clientR.IdContacts equals contacts.Id
                         select new ClientsView(clientR, personR, contacts);

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }
    }
}
