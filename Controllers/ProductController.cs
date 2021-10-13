using DPSP_Api.Models.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPSP_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DPSPDBContext _context;

        public ProductController(DPSPDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получаем список всех товаров
        /// </summary>
        [HttpGet]
        public IEnumerable<object> GetProducts()
        {
            var result = (from product in _context.Products
                          join store in _context.StoreInfos on product.IdStoreInfo equals store.Id
                          join category in _context.ProductCategories on product.IdCategory equals category.Id
                          select new ProductView()
                          {
                              id = product.Id,
                              name = product.Name,
                              price = product.Cost,
                              rating = product.Rating,
                              avail = Convert.ToBoolean(product.Avail),
                              category = category.Name,
                              store = store.Name
                          }).ToList();

            return result;
        }

        /// <summary>
        /// Получаем и выводим список товаров категории и всех дочерних категорий
        /// </summary>
        [HttpGet]
        [Route("{categoryName}")]
        public ActionResult<IEnumerable<object>> GetProducts(string categoryName)
        {
            var result = ProductsByCategory(categoryName);

            if(result.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }

        /// <summary>
        /// Получаем список товаров категории и всех дочерних категорий
        /// </summary>
        public List<object> ProductsByCategory(string categoryName)
        {
            var result = (from product in _context.Products
                          join store in _context.StoreInfos on product.IdStoreInfo equals store.Id
                          join category in _context.ProductCategories on product.IdCategory equals category.Id
                          where category.Name == categoryName
                          select new
                          {
                              id = product.Id,
                              name = product.Name,
                              price = product.Cost,
                              rating = product.Rating,
                              avail = product.Avail,
                              category = category.Name,
                              store = store.Name
                          }).ToList<object>();

            var listChilds = new CategoryController(_context).ChildCategories(categoryName);
            if (listChilds.Count != 0)
            {
                foreach(var i in listChilds)
                {
                    result.AddRange(ProductsByCategory(i.Name));
                }
            }

            return result;
        }

        /// <summary>
        /// Получаем список товаров по наименованию продукта
        /// </summary>
        [HttpGet]
        [Route("[action]/{productName}")]
        public List<ProductView> GetByName(string productName)
        {
            List<ProductView> result = new List<ProductView>();
            var productsList = (from product in _context.Products
                         join store in _context.StoreInfos on product.IdStoreInfo equals store.Id
                         join category in _context.ProductCategories on product.IdCategory equals category.Id
                         select new ProductView()
                         {
                             id = product.Id,
                             name = product.Name,
                             price = product.Cost,
                             rating = product.Rating,
                             avail = Convert.ToBoolean(product.Avail),
                             category = category.Name,
                             store = store.Name
                         }).ToList();

            foreach(var i in productsList)
            {
                if (i.name.ToLower().Contains(productName.ToLower().Trim()))
                {
                    result.Add(i);
                }
            }

            return result;
        }

        /// <summary>
        /// Получаем подробную информацию о товаре (характеристики)
        /// </summary>
        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult<IEnumerable<object>> GetMoreInfo(int id)
        {
            var result = from productDesc in _context.ProductDescriptions
                         join attribute in _context.ProductAttributes on productDesc.IdProductAttribute equals attribute.Id
                         where productDesc.IdProduct == id
                         select new
                         {
                             attribute = attribute.Name,
                             value = productDesc.AttrValue
                         };

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }

        /// <summary>
        /// Получаем список адресов изображений товара
        /// </summary>
        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult<IEnumerable<object>> GetImages(int id)
        {
            var result = from productImage in _context.ProductImages
                         //join product in _context.Products on productImage.IdProduct equals product.Id
                         where productImage.IdProduct == id
                         select new
                         {
                             id = productImage.Id,
                             idProduct = productImage.IdProduct,
                             imageUrl = productImage.ImageUrl
                         };

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }
    }
}
