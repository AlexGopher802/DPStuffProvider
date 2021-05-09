using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSP_Api.Models
{
    public class OrdersView
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public int? FrontDoor { get; set; }
        public int? ApartNum { get; set; }
        public int? FloorNum { get; set; }
        public string Intercom { get; set; }
        public string DeliveryDate { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Commentary { get; set; }
        public double? Summ { get; set; }
        public int? Priority { get; set; }
        public string Status { get; set; }

        public OrdersView(Ordered ordered, AddressDelivery address, PersonalInfo personalInfo, Contact contact, OrderStatus orderStatus)
        {
            Id = ordered.Id;
            Address = address.Address;
            LastName = personalInfo.LastName;
            FirstName = personalInfo.FirstName;
            Phone = contact.Phone;
            FrontDoor = address.FrontDoor;
            ApartNum = address.ApartmentNum;
            FloorNum = address.FloorNum;
            Intercom = address.Intercom;
            DeliveryDate = ordered.DeliveryDate.ToString("dd.MM.yyyy");
            TimeFrom = ordered.DeliveryTimeFrom.ToString();
            TimeTo = ordered.DeliveryTimeTo.ToString();
            Commentary = ordered.Commentary;
            Summ = ordered.Summ;
            Priority = ordered.Priority;
            Status = orderStatus.Name;
        }
    }
}
