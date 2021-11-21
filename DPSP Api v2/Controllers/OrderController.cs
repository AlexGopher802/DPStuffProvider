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
    public class OrderController : ControllerBase
    {
        private DPSPdbV2Context _context;

        public OrderController(DPSPdbV2Context context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Регистрация нового заказа
        /// </summary>        
        [Route("[action]")]
        [HttpPost]
        public ActionResult Registration(OrderData orderData)
        {
            try
            {
                ClientInfo client = _context.ClientInfos.Where(c => c.Id == orderData.ClientId).FirstOrDefault();
                if (client == null)
                {
                    throw new Exception("That client was not finded");
                }

                AddressDelivery addressDelivery = new AddressDelivery();
                if (orderData.AddressId != null)
                {
                    addressDelivery = _context.AddressDeliveries.Where(a => a.Id == orderData.AddressId).FirstOrDefault();
                }
                else
                {
                    AddressDelivery address = new AddressDelivery()
                    {
                        Address = orderData.Address,
                        FrontDoor = orderData.FrontDoor,
                        ApartmentNum = orderData.ApartmentNum,
                        FloorNum = orderData.FloorNum,
                        Intercom = orderData.Intercom
                    };
                    _context.AddressDeliveries.Add(address);
                    addressDelivery = address;
                }

                Ordered order = new Ordered()
                {
                    OrderDateTime = DateTime.Now,
                    DeliveryDate = DateTime.Parse(orderData.DeliveryDate),
                    DeliveryTimeFrom = TimeSpan.Parse(orderData.DeliveryTimeFrom),
                    DeliveryTimeTo = TimeSpan.Parse(orderData.DeliveryTimeTo),
                    Commentary = orderData.Commentary,
                    Summ = orderData.Summ,
                    CodeToFinish = new Random().Next(1000, 9999).ToString(),
                    IdAddressNavigation = addressDelivery,
                    IdClientNavigation = client,
                    IdOrderStatusNavigation = _context.OrderStatuses.Where(s => s.Name == "Обрабатывается").FirstOrDefault()
                };
                _context.Ordereds.Add(order);

                foreach (var i in orderData.OrderProducts)
                {
                    OrderProduct orderProduct = new OrderProduct()
                    {
                        IdProduct = i.ProductId,
                        Quantity = i.Quantity,
                        Summ = i.Quantity * _context.Products.Where(p => p.Id == i.ProductId).Select(p => p.Price).FirstOrDefault(),
                        IdOrderNavigation = order
                    };
                    _context.OrderProducts.Add(orderProduct);
                }
                _context.SaveChanges();

                var result = (from orderInfo in _context.Ordereds
                              where orderInfo.IdClient == orderData.ClientId && orderInfo.CodeToFinish == order.CodeToFinish
                              select new
                              {
                                  Id = orderInfo.Id,
                                  DeliveryDate = orderInfo.DeliveryDate,
                                  Summ = orderInfo.Summ,
                                  ClientName = $"{orderInfo.IdClientNavigation.IdPersonalInfoNavigation.LastName} {orderInfo.IdClientNavigation.IdPersonalInfoNavigation.FirstName} {orderInfo.IdClientNavigation.IdPersonalInfoNavigation.Patronymic}",
                                  ClientPhone = orderInfo.IdClientNavigation.IdContactsNavigation.Phone
                              }).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("Order was not added");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Возвращает список свободных заказов (у которых еще нет курьера)
        /// </summary>        
        [Route("[action]")]
        [HttpGet]
        public ActionResult FreeList()
        {
            try
            {
                var result = (from order in _context.Ordereds
                              where order.IdOrderStatusNavigation.Name == "Обрабатывается"
                              select new OrderData()
                              {
                                  OrderId = order.Id,
                                  OrderDateTime = order.OrderDateTime,
                                  DeliveryDate = order.DeliveryDate.ToString("dd.MM.yyyy"),
                                  DeliveryTimeFrom = order.DeliveryTimeFrom.ToString(),
                                  DeliveryTimeTo = order.DeliveryTimeTo.ToString(),
                                  Commentary = order.Commentary,
                                  Summ = order.Summ.Value,
                                  CodeToFinish = order.CodeToFinish,
                                  Status = order.IdOrderStatusNavigation.Name,
                                  ClientId = order.IdClient,
                                  ClientName = $"{order.IdClientNavigation.IdPersonalInfoNavigation.LastName} {order.IdClientNavigation.IdPersonalInfoNavigation.FirstName} {order.IdClientNavigation.IdPersonalInfoNavigation.Patronymic}",
                                  AddressId = order.IdAddress,
                                  Address = order.IdAddressNavigation.Address,
                                  FrontDoor = order.IdAddressNavigation.FrontDoor.Value,
                                  ApartmentNum = order.IdAddressNavigation.ApartmentNum.Value,
                                  FloorNum = order.IdAddressNavigation.FloorNum.Value,
                                  Intercom = order.IdAddressNavigation.Intercom,
                                  OrderProducts = (from orderProduct in _context.OrderProducts
                                                   where orderProduct.IdOrder == order.Id
                                                   select new OrderComposData()
                                                   {
                                                       ProductId = orderProduct.IdProduct,
                                                       Name = orderProduct.IdProductNavigation.Name,
                                                       Price = orderProduct.IdProductNavigation.Price,
                                                       Vendor = orderProduct.IdProductNavigation.IdVendorInfoNavigation.Name,
                                                       Quantity = orderProduct.Quantity
                                                   }).ToList()
                              }).ToList();

                if (result.Count == 0)
                {
                    throw new Exception("Free orders not exist");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Возвращает список заказов клиента
        /// </summary>        
        [Route("[action]")]
        [HttpGet]
        public ActionResult ClientList(int? clientId)
        {
            try
            {
                var result = (from order in _context.Ordereds
                              where order.IdClientNavigation.Id == ((clientId == null) ? 0 : clientId)
                              select new OrderData()
                              {
                                  OrderId = order.Id,
                                  OrderDateTime = order.OrderDateTime,
                                  DeliveryDate = order.DeliveryDate.ToString("dd.MM.yyyy"),
                                  DeliveryTimeFrom = order.DeliveryTimeFrom.ToString(),
                                  DeliveryTimeTo = order.DeliveryTimeTo.ToString(),
                                  Commentary = order.Commentary,
                                  Summ = order.Summ.Value,
                                  CodeToFinish = order.CodeToFinish,
                                  Status = order.IdOrderStatusNavigation.Name,
                                  ClientId = order.IdClient,
                                  ClientName = $"{order.IdClientNavigation.IdPersonalInfoNavigation.LastName} {order.IdClientNavigation.IdPersonalInfoNavigation.FirstName} {order.IdClientNavigation.IdPersonalInfoNavigation.Patronymic}",
                                  AddressId = order.IdAddress,
                                  Address = order.IdAddressNavigation.Address,
                                  FrontDoor = order.IdAddressNavigation.FrontDoor.Value,
                                  ApartmentNum = order.IdAddressNavigation.ApartmentNum.Value,
                                  FloorNum = order.IdAddressNavigation.FloorNum.Value,
                                  Intercom = order.IdAddressNavigation.Intercom,
                                  OrderProducts = (from orderProduct in _context.OrderProducts
                                                   where orderProduct.IdOrder == order.Id
                                                   select new OrderComposData()
                                                   {
                                                       ProductId = orderProduct.IdProduct,
                                                       Name = orderProduct.IdProductNavigation.Name,
                                                       Price = orderProduct.IdProductNavigation.Price,
                                                       Vendor = orderProduct.IdProductNavigation.IdVendorInfoNavigation.Name,
                                                       Quantity = orderProduct.Quantity
                                                   }).ToList()
                              }).ToList();

                if (result.Count == 0)
                {
                    throw new Exception("Orders not found");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Выдача заказа курьеру
        /// </summary>        
        [Route("[action]")]
        [HttpPost]
        public ActionResult SetCourier(OrderData orderData)
        {
            try
            {
                Ordered order = _context.Ordereds.Where(o => o.Id == orderData.OrderId).FirstOrDefault();
                if (order == null)
                {
                    throw new Exception("That orderId invalid");
                }

                CourierInfo courier = _context.CourierInfos.Where(c => c.Id == orderData.CourierId).FirstOrDefault();
                if (courier == null)
                {
                    throw new Exception("That courierId invalid");
                }

                order.IdCourier = courier.Id;
                order.IdOrderStatusNavigation = _context.OrderStatuses.Where(s => s.Name == "Выдан курьеру").FirstOrDefault();
                _context.Ordereds.Update(order);
                _context.SaveChanges();

                return new ObjectResult(new { message = "Order update success" }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Завершение заказа
        /// </summary>        
        [Route("[action]")]
        [HttpPost]
        public ActionResult Finish(OrderData orderData)
        {
            try
            {
                Ordered order = _context.Ordereds.Where(o => o.Id == orderData.OrderId).FirstOrDefault();
                if (order == null)
                {
                    throw new Exception("That orderId invalid");
                }

                CourierInfo courier = _context.CourierInfos.Where(c => c.Id == orderData.CourierId).FirstOrDefault();
                if (courier == null)
                {
                    throw new Exception("That courierId invalid");
                }

                if (order.CodeToFinish != orderData.CodeToFinish)
                {
                    throw new Exception("Code invalid");
                }

                order.IdOrderStatusNavigation = _context.OrderStatuses.Where(s => s.Name == "Завершен").FirstOrDefault();
                _context.Ordereds.Update(order);
                _context.SaveChanges();

                return new ObjectResult(new { message = "Order update success" }) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }
    }
}
