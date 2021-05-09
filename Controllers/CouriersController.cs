using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DPSP_Api.Models;
using System.Security.Cryptography;
using System.Text;

namespace DPSP_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouriersController : ControllerBase
    {
        private readonly DPSPDBContext _context;

        public CouriersController(DPSPDBContext context)
        {
            _context = context;
        }

        /*
        //Выводит список всех курьеров
        [HttpGet]
        public IEnumerable<object> GetCouriers()
        {
            var emps = from emp in _context.Couriers
                       join person in _context.PersonalInfos on emp.IdPersonalInfo equals person.Id
                       join contacts in _context.Contacts on emp.IdContacts equals contacts.Id
                       select new CourierView(emp, person, contacts);
            return emps;
        }
        
        [Route("{id}")]
        [HttpGet]
        public ActionResult<IEnumerable<CourierView>> GetCouriers(int id)
        {
            var emps = from emp in _context.Couriers
                       where emp.Id == id
                       join person in _context.PersonalInfos on emp.IdPersonalInfo equals person.Id
                       join contacts in _context.Contacts on emp.IdContacts equals contacts.Id
                       select new CourierView(emp, person, contacts);
            
            if(emps.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(emps);
        }
        */

        /// <summary>
        /// Выводит информацию о курьере по логину и паролю
        /// </summary>
        [Route("{login}/{password}")]
        [HttpGet]
        public ActionResult<CourierView> GetCouriers(string login, string password)
        {
            var emps = from emp in _context.Couriers
                        where emp.Login == login && emp.Password == SHA256.Create().ComputeHash(Encoding.Default.GetBytes(password))
                        join person in _context.PersonalInfos on emp.IdPersonalInfo equals person.Id
                        join contacts in _context.Contacts on emp.IdContacts equals contacts.Id
                        select new CourierView(emp, person, contacts);

            if (emps.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(emps);
        }
    }
}
