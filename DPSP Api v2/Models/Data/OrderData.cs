using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSPApiV2.Models.Data
{
    public class OrderData
    {
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveryTimeFrom { get; set; }
        public string DeliveryTimeTo { get; set; }
        public string Commentary { get; set; }
        public float Summ { get; set; }
        public string CodeToFinish { get; set; }
        public string Status { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int CourierId { get; set; }
        public string CourierName { get; set; }
        public int? AddressId { get; set; }
        public string Address { get; set; }
        public int FrontDoor { get; set; }
        public int ApartmentNum { get; set; }
        public int FloorNum { get; set; }
        public string Intercom { get; set; }
        public List<OrderComposData> OrderProducts { get; set; }
    }
}
