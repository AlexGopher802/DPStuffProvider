using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPSPApiV2.Models;
using DPSPApiV2.Models.Data;
using DPSPApiV2.Logics;

namespace DPSPApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private DPSPdbV2Context _context;

        public ClientController(DPSPdbV2Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Регистрация клиента
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Registration(UserData userData)
        {
            try
            {
                User user = new User()
                {
                    Login = userData.Login,
                    Password = HashHandler.Sha256(userData.Password),
                    IdTypeNavigation = _context.UserTypes.Where(t => t.Name == "Клиент").FirstOrDefault(),
                    IdStatusNavigation = _context.UserStatuses.Where(s => s.Name == "Подтвержден").FirstOrDefault()
                };
                _context.Users.Add(user);

                PersonalInfo personalInfo = new PersonalInfo()
                {
                    LastName = userData.LastName,
                    FirstName = userData.FirstName,
                    Patronymic = userData.Patronymic
                };
                _context.PersonalInfos.Add(personalInfo);

                Contact contacts = new Contact()
                {
                    Phone = userData.Phone,
                    Email = userData.Email
                };
                _context.Contacts.Add(contacts);

                ClientInfo clientInfo = new ClientInfo()
                {
                    IdPersonalInfoNavigation = personalInfo,
                    IdContactsNavigation = contacts,
                    IdUserNavigation = user
                };
                _context.ClientInfos.Add(clientInfo);
                _context.SaveChanges();

                var result = (from userDb in _context.Users
                              where userDb.Login == userData.Login
                              select new
                              {
                                  id = userDb.Id,
                                  login = userDb.Login
                              }).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("User was not registered");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Авторизация клиента
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Auth(User userData)
        {
            try
            {
                var result = (from clientInfo in _context.ClientInfos
                              where clientInfo.IdUserNavigation.Login == userData.Login &&
                                    clientInfo.IdUserNavigation.Password == HashHandler.Sha256(userData.Password)
                              select new UserData()
                              {
                                  UserId = clientInfo.IdUserNavigation.Id,
                                  Login = clientInfo.IdUserNavigation.Login,
                                  Password = clientInfo.IdUserNavigation.Password,
                                  LastName = clientInfo.IdPersonalInfoNavigation.LastName,
                                  FirstName = clientInfo.IdPersonalInfoNavigation.FirstName,
                                  Patronymic = clientInfo.IdPersonalInfoNavigation.Patronymic,
                                  Phone = clientInfo.IdContactsNavigation.Phone,
                                  Email = clientInfo.IdContactsNavigation.Email,
                                  Status = clientInfo.IdUserNavigation.IdStatusNavigation.Name
                              }).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("Login and/or password invalid");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }
    }
}
