using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class Product
    {
        public Product()
        {
            ProductCompos = new HashSet<ProductCompos>();
            ProductDescriptions = new HashSet<ProductDescription>();
            ProductImages = new HashSet<ProductImage>();
            ProductReviews = new HashSet<ProductReview>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public double? Rating { get; set; }
        public bool? Avail { get; set; }
        public int IdCategory { get; set; }
        public int IdStoreInfo { get; set; }

        public virtual ProductCategory IdCategoryNavigation { get; set; }
        public virtual StoreInfo IdStoreInfoNavigation { get; set; }
        public virtual ICollection<ProductCompos> ProductCompos { get; set; }
        public virtual ICollection<ProductDescription> ProductDescriptions { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
