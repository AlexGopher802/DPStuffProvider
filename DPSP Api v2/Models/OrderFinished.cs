using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class OrderFinished
    {
        public int Id { get; set; }
        public int? ClientScore { get; set; }
        public string Commentary { get; set; }
        public int IdOrder { get; set; }

        public virtual Ordered IdOrderNavigation { get; set; }
    }
}
