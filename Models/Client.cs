using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class Client
    {
        public Client()
        {
            ClientAddresses = new HashSet<ClientAddress>();
            Ordereds = new HashSet<Ordered>();
            ProductReviews = new HashSet<ProductReview>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public int IdPersonalInfo { get; set; }
        public int IdContacts { get; set; }

        public virtual Contact IdContactsNavigation { get; set; }
        public virtual PersonalInfo IdPersonalInfoNavigation { get; set; }
        public virtual ICollection<ClientAddress> ClientAddresses { get; set; }
        public virtual ICollection<Ordered> Ordereds { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
