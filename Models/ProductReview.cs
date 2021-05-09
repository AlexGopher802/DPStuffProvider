using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class ProductReview
    {
        public int Id { get; set; }
        public int ClientScore { get; set; }
        public string Commentary { get; set; }
        public int IdProduct { get; set; }
        public int IdClient { get; set; }

        public virtual Client IdClientNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
