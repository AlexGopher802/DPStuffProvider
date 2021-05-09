using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSP_Api.Models
{
    public class OrderComposView
    {
        public string Name { get; set; }
        public string ShopName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public OrderComposView(ProductCompos productCompos, Product product, StoreInfo storeInfo)
        {
            Name = product.Name;
            ShopName = storeInfo.Name;
            Quantity = productCompos.Quantity;
            Price = product.Cost;
        }
    }
}
