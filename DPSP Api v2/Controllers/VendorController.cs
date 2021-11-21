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
    public class VendorController : ControllerBase
    {
        private DPSPdbV2Context _context;

        public VendorController(DPSPdbV2Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Регистрация вендора
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Registration(VendorData vendorData)
        {
            try
            {
                User user = new User()
                {
                    Login = vendorData.Login,
                    Password = HashHandler.Sha256(vendorData.Password),
                    IdTypeNavigation = _context.UserTypes.Where(t => t.Name == "Вендор").FirstOrDefault(),
                    IdStatusNavigation = _context.UserStatuses.Where(s => s.Name == "Не подтвержден").FirstOrDefault()
                };
                _context.Users.Add(user);

                VendorInfo vendorInfo = new VendorInfo()
                {
                    Name = vendorData.Name,
                    Fullname = vendorData.Fullname,
                    Tin = vendorData.Tin,
                    Bank = vendorData.Bank,
                    Bic = vendorData.Bic,
                    Email = vendorData.Email,
                    Phone = vendorData.Phone,
                    Address = vendorData.Address,
                    IdUserNavigation = user
                };
                _context.VendorInfos.Add(vendorInfo);
                _context.SaveChanges();

                var result = (from vendor in _context.VendorInfos
                              where vendor.IdUserNavigation == user
                              select new
                              {
                                  Login = vendor.IdUserNavigation.Login,
                                  FullName = vendor.Fullname
                              }).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("Vendor was not registered");
                }
         
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Авторизация вендора
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Auth(User userData)
        {
            try
            {
                var result = (from vendor in _context.VendorInfos
                              where vendor.IdUserNavigation.Login == userData.Login &&
                                    vendor.IdUserNavigation.Password == HashHandler.Sha256(userData.Password)
                              select new VendorData()
                              {
                                  Login = vendor.IdUserNavigation.Login,
                                  Password = vendor.IdUserNavigation.Password,
                                  Status = vendor.IdUserNavigation.IdStatusNavigation.Name,
                                  Name = vendor.Name,
                                  Fullname = vendor.Fullname,
                                  Tin = vendor.Tin,
                                  Bank = vendor.Bank,
                                  Bic = vendor.Bic,
                                  Email = vendor.Email,
                                  Phone = vendor.Phone,
                                  Address = vendor.Address
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
