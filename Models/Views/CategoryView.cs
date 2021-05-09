using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSP_Api.Models.Views
{
    public class CategoryView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; } = "";
        public string ImageUrl { get; set; } = "";

        public CategoryView(ProductCategory category, ProductCategory parentCategory)
        {
            Id = category.Id;
            Name = category.Name;
            if(parentCategory != null)
            {
                ParentName = parentCategory.Name;
            }
            if (category.ImageUrl != null)
            {
                ImageUrl = category.ImageUrl;
            }
        }
    }
}
