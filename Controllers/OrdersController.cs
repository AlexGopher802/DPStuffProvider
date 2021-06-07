using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPSP_Api.Models;
using DPSP_Api.Models.Views;
using System.Security.Cryptography;
using System.Text;

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
        /// Выводит список заказов по статусу
        /// </summary>
        [HttpGet]
        [Route("[action]/{statusName}")]
        public ActionResult<IEnumerable<OrdersView>> GetOrdersByStatus(string statusName)
        {
            var result = (from order in _context.Ordereds
                         join client in _context.Clients on order.IdClient equals client.Id
                         join person in _context.PersonalInfos on client.IdPersonalInfo equals person.Id
                         join contacts in _context.Contacts on client.IdContacts equals contacts.Id
                         join status in _context.OrderStatuses on order.IdOrderStatus equals status.Id
                         join address in _context.AddressDeliveries on order.IdAddress equals address.Id
                         where status.Name == statusName
                         select new OrdersView(order, address, person, contacts, status)).ToList();

            if(result.Count == 0)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }

        /// <summary>
        /// Обновляет статус заказа
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        public ActionResult<OrdersView> UpdateStatus(OrdersView newOrder)
        {
            OrderStatus newStatus = _context.OrderStatuses.Where(s => s.Name == newOrder.status).FirstOrDefault();
            if(newStatus == null)
            {
                return NotFound();
            }

            Ordered updateOrder = _context.Ordereds.Where(o => o.Id == newOrder.id).FirstOrDefault();
            updateOrder.IdOrderStatusNavigation = newStatus;

            _context.Ordereds.Update(updateOrder);
            _context.SaveChanges();

            var result = (from order in _context.Ordereds
                          join client in _context.Clients on order.IdClient equals client.Id
                          join person in _context.PersonalInfos on client.IdPersonalInfo equals person.Id
                          join contacts in _context.Contacts on client.IdContacts equals contacts.Id
                          join status in _context.OrderStatuses on order.IdOrderStatus equals status.Id
                          join address in _context.AddressDeliveries on order.IdAddress equals address.Id
                          where order.Id == updateOrder.Id
                          select new OrdersView(order, address, person, contacts, status)).ToList();

            if(result.Count == 0)
            {
                return NotFound();
            }

            return new ObjectResult(result[0]);
        }

        /// <summary>
        /// Выводит список всех заказов клиента по логину и паролю
        /// </summary>
        [HttpGet]
        [Route("[action]/{login}/{password}")]
        public IEnumerable<OrdersView> GetOrders(string login, string password)
        {
            var clientId = _context.Clients.Where(c => c.Login == login && c.Password == SHA256.Create().ComputeHash(Encoding.Default.GetBytes(password))).Select(c => c.Id).FirstOrDefault();

            var orders = from order in _context.Ordereds
                         join client in _context.Clients on order.IdClient equals client.Id
                         join person in _context.PersonalInfos on client.IdPersonalInfo equals person.Id
                         join contacts in _context.Contacts on client.IdContacts equals contacts.Id
                         join status in _context.OrderStatuses on order.IdOrderStatus equals status.Id
                         join address in _context.AddressDeliveries on order.IdAddress equals address.Id
                         where client.Id == clientId
                         select new OrdersView(order, address, person, contacts, status);
            return orders;
        }

        /// <summary>
        /// Выводит список товаров, входящих в заказ
        /// </summary>
        [Route("[action]/{id}")]
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
            var clientId = (from order in _context.Ordereds
                            where order.Id == id
                            select order.IdClient).FirstOrDefault();

            var clientOrder = from client in _context.Clients
                              where client.Id == clientId
                              join person in _context.PersonalInfos on client.IdPersonalInfo equals person.Id
                              join contacts in _context.Contacts on client.IdContacts equals contacts.Id
                              select new ClientsView(client, person, contacts);



            if (clientOrder.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(clientOrder);
        }

        /// <summary>
        /// Регистрация нового заказа
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult<OrdersView> RegOrder(OrdersView newOrder)
        {
            DateTime nowTime = DateTime.Now;

            AddressDelivery addressDelivery = new AddressDelivery()
            {
                Address = newOrder.address,
                FrontDoor = newOrder.frontDoor,
                ApartmentNum = newOrder.apartNum,
                FloorNum = newOrder.floorNum,
                Intercom = newOrder.intercom
            };
            _context.AddressDeliveries.Add(addressDelivery);

            Ordered ordered = new Ordered()
            {
                OrderDateTime = nowTime,
                DeliveryDate = DateTime.Parse(newOrder.deliveryDate),
                DeliveryTimeFrom = TimeSpan.Parse(newOrder.timeFrom),
                DeliveryTimeTo = TimeSpan.Parse(newOrder.timeTo),
                Commentary = newOrder.commentary,
                Summ = newOrder.summ,
                Priority = 5,
                IdAddressNavigation = addressDelivery,
                IdClient = (from client in _context.Clients
                            join person in _context.PersonalInfos on client.IdPersonalInfo equals person.Id
                            where person.LastName == newOrder.lastName && person.FirstName == newOrder.firstName
                            select client.Id).FirstOrDefault(),
                IdOrderStatus = _context.OrderStatuses.Where(o => o.Name == "Обрабатывается").Select(o => o.Id).FirstOrDefault()
            };
            _context.Ordereds.Add(ordered);

            foreach(var i in newOrder.listProducts)
            {
                ProductCompos newProductCompos = new ProductCompos()
                {
                    IdOrderNavigation = ordered,
                    IdProduct = _context.Products.Where(p => p.Id == i.id).Select(p => p.Id).FirstOrDefault(),
                    Summ = i.price * i.quantity,
                    Quantity = i.quantity
                };
                _context.ProductCompos.Add(newProductCompos);
            }

            _context.SaveChanges();

            var result = (from order in _context.Ordereds
                          where order.OrderDateTime == nowTime
                          select new OrdersView() { id = order.Id }).ToList();

            if(result.Count == 0)
            {
                return NotFound();
            }

            return new ObjectResult(result[0]);
        }
    }
}
