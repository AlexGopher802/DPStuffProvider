using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class Ordered
    {
        public Ordered()
        {
            OrderFinisheds = new HashSet<OrderFinished>();
            ProductCompos = new HashSet<ProductCompos>();
        }

        public int Id { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime DeliveryDate { get; set; }
        public TimeSpan DeliveryTimeFrom { get; set; }
        public TimeSpan DeliveryTimeTo { get; set; }
        public string Commentary { get; set; }
        public double? Summ { get; set; }
        public int? Priority { get; set; }
        public string CodeToFinish { get; set; }
        public int IdAddress { get; set; }
        public int IdClient { get; set; }
        public int? IdCourier { get; set; }
        public int IdOrderStatus { get; set; }

        public virtual AddressDelivery IdAddressNavigation { get; set; }
        public virtual Client IdClientNavigation { get; set; }
        public virtual Courier IdCourierNavigation { get; set; }
        public virtual OrderStatus IdOrderStatusNavigation { get; set; }
        public virtual ICollection<OrderFinished> OrderFinisheds { get; set; }
        public virtual ICollection<ProductCompos> ProductCompos { get; set; }
    }
}
