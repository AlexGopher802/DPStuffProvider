using System;
using System.Collections.Generic;

#nullable disable

namespace DPSP_Api
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            InverseIdParentCategoryNavigation = new HashSet<ProductCategory>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? IdParentCategory { get; set; }

        public virtual ProductCategory IdParentCategoryNavigation { get; set; }
        public virtual ICollection<ProductCategory> InverseIdParentCategoryNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
