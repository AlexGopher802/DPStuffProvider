using DPSPApiV2.Logics;
using DPSPApiV2.Models;
using DPSPApiV2.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSPApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private DPSPdbV2Context _context;

        public CourierController(DPSPdbV2Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Регистрация курьера
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
                    IdTypeNavigation = _context.UserTypes.Where(t => t.Name == "Курьер").FirstOrDefault(),
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

                CourierInfo courierInfo = new CourierInfo()
                {
                    IdPersonalInfoNavigation = personalInfo,
                    IdContactsNavigation = contacts,
                    IdUserNavigation = user,
                    OrderQuantity = 0
                };
                _context.CourierInfos.Add(courierInfo);
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
        /// Авторизация курьера
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Auth(User userData)
        {
            try
            {
                var result = (from courierInfo in _context.CourierInfos
                              where courierInfo.IdUserNavigation.Login == userData.Login &&
                                    courierInfo.IdUserNavigation.Password == HashHandler.Sha256(userData.Password)
                              select new UserData()
                              {
                                  UserId = courierInfo.IdUserNavigation.Id,
                                  Login = courierInfo.IdUserNavigation.Login,
                                  Password = courierInfo.IdUserNavigation.Password,
                                  LastName = courierInfo.IdPersonalInfoNavigation.LastName,
                                  FirstName = courierInfo.IdPersonalInfoNavigation.FirstName,
                                  Patronymic = courierInfo.IdPersonalInfoNavigation.Patronymic,
                                  Phone = courierInfo.IdContactsNavigation.Phone,
                                  Email = courierInfo.IdContactsNavigation.Email,
                                  Status = courierInfo.IdUserNavigation.IdStatusNavigation.Name
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
