using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPSP_Api.Models;
using DPSP_Api.Models.Views;

namespace DPSP_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DPSPDBContext _context;

        public OrdersController(DPSPDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Выводит список всех заказов
        /// </summary>
        [HttpGet]
        public IEnumerable<OrdersView> GetOrders()
        {
            var orders = from order in _context.Ordereds
                         join client in _context.Clients on order.IdClient equals client.Id
                         join person in _context.PersonalInfos on client.IdPersonalInfo equals person.Id
                         join contacts in _context.Contacts on client.IdContacts equals contacts.Id
                         join status in _context.OrderStatuses on order.IdOrderStatus equals status.Id
                         join address in _context.AddressDeliveries on order.IdAddress equals address.Id
                         select new OrdersView(order, address, person, contacts, status);
            return orders;
        }

        /// <summary>
        /// Выводит список товаров, входящих в заказ
        /// </summary>
        [Route("{id}")]
        [HttpGet]
        public ActionResult<IEnumerable<OrderComposView>> GetOrders(int id)
        {
            var orderCompos = from orderComp in _context.ProductCompos
                              where orderComp.IdOrder == id
                              join product in _context.Products on orderComp.IdProduct equals product.Id
                              join store in _context.StoreInfos on product.IdStoreInfo equals store.Id
                              select new OrderComposView(orderComp, product, store);

            if(orderCompos.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(orderCompos);
        }

        /// <summary>
        /// Выводит информацию о заказчике
        /// </summary>
        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult<ClientsView> GetClient(int id)
        {
            var clientOrder = from client in _context.Clients
                              where client.Id == id
                              join person in _context.PersonalInfos on client.IdPersonalInfo equals person.Id
                              join contacts in _context.Contacts on client.IdContacts equals contacts.Id
                              select new ClientsView(client, person, contacts);

            if(clientOrder.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(clientOrder);
        }
    }
}
