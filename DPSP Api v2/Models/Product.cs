using System;
using System.Collections.Generic;

#nullable disable

namespace DPSPApiV2.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
            ProductDescriptions = new HashSet<ProductDescription>();
            ProductImages = new HashSet<ProductImage>();
            ProductReviews = new HashSet<ProductReview>();
            WarehouseProducts = new HashSet<WarehouseProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float? Rating { get; set; }
        public bool? Avail { get; set; }
        public int IdCategory { get; set; }
        public int IdVendorInfo { get; set; }

        public virtual ProductCategory IdCategoryNavigation { get; set; }
        public virtual VendorInfo IdVendorInfoNavigation { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<ProductDescription> ProductDescriptions { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; }
    }
}
