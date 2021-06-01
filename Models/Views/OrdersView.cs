using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using DPSP_Api.Models.Views;

namespace DPSP_Api.Models
{
    [Serializable]
    public class OrdersView
    {
        public int? id { get; set; }
        public string address { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string phone { get; set; }
        public int? frontDoor { get; set; }
        public int? apartNum { get; set; }
        public int? floorNum { get; set; }
        public string intercom { get; set; }
        public string deliveryDate { get; set; }
        public string timeFrom { get; set; }
        public string timeTo { get; set; }
        public string commentary { get; set; }
        public int? idCourier { get; set; }
        public double? summ { get; set; }
        public int? priority { get; set; }
        public string status { get; set; }
        public List<ProductView>? listProducts { get; set; }

        public OrdersView(Ordered ordered, AddressDelivery address, PersonalInfo personalInfo, Contact contact, OrderStatus orderStatus)
        {
            id = ordered.Id;
            this.address = address.Address;
            lastName = personalInfo.LastName;
            firstName = personalInfo.FirstName;
            phone = contact.Phone;
            frontDoor = address.FrontDoor;
            apartNum = address.ApartmentNum;
            floorNum = address.FloorNum;
            intercom = address.Intercom;
            deliveryDate = ordered.DeliveryDate.ToString("dd.MM.yyyy");
            timeFrom = ordered.DeliveryTimeFrom.ToString();
            timeTo = ordered.DeliveryTimeTo.ToString();
            commentary = ordered.Commentary;
            summ = ordered.Summ;
            priority = ordered.Priority;
            status = orderStatus.Name;
        }

        public OrdersView()
        {

        }
    }
}
