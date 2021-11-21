using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class ClientInfo
    {
        public ClientInfo()
        {
            ClientAddresses = new HashSet<ClientAddress>();
            Ordereds = new HashSet<Ordered>();
            ProductReviews = new HashSet<ProductReview>();
        }

        public int Id { get; set; }
        public int IdPersonalInfo { get; set; }
        public int IdContacts { get; set; }
        public int IdUser { get; set; }

        public virtual Contact IdContactsNavigation { get; set; }
        public virtual PersonalInfo IdPersonalInfoNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<ClientAddress> ClientAddresses { get; set; }
        public virtual ICollection<Ordered> Ordereds { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
