using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPSPApiV2.Logics;
using DPSPApiV2.Models;
using DPSPApiV2.Models.Data;

namespace DPSPApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private DPSPdbV2Context _context;

        public ProductController(DPSPdbV2Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление одного товара
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AddOne(ProductData productData)
        {
            try
            {
                ProductCategory category = _context.ProductCategories.Where(c => c.Name == productData.Category).FirstOrDefault();
                if (category == null)
                {
                    throw new Exception($"Category \"{productData.Category}\" is not exist");
                }

                VendorInfo vendor = _context.VendorInfos.Where(v => v.Name == productData.Vendor).FirstOrDefault();
                if (vendor == null)
                {
                    throw new Exception($"Vendor \"{productData.Vendor}\" is not exist");
                }

                Product product = new Product()
                {
                    Name = productData.Name,
                    Price = productData.Price,
                    Rating = 0.0f,
                    Avail = true,
                    IdCategoryNavigation = category,
                    IdVendorInfoNavigation = vendor
                };
                _context.Products.Add(product);

                if (productData.Images != null)
                {
                    foreach (var i in productData.Images)
                    {
                        ProductImage productImage = new ProductImage()
                        {
                            IdProductNavigation = product,
                            ImageUrl = i.ImageUrl
                        };
                        _context.ProductImages.Add(productImage);
                    }
                }

                if (productData.Attributes != null)
                {
                    foreach (var i in productData.Attributes)
                    {
                        ProductAttribute checkProductAttribute = (from attribute in _context.ProductAttributes
                                                                  where attribute.Name == i.Name
                                                                  select attribute).FirstOrDefault();
                        if (checkProductAttribute == null)
                        {
                            _context.ProductAttributes.Add(new ProductAttribute()
                            {
                                Name = i.Name
                            });
                        }

                        ProductAttribute productAttribute = (from attribute in _context.ProductAttributes
                                                              where attribute.Name == i.Name
                                                              select attribute).FirstOrDefault();

                        ProductDescription productDescription = new ProductDescription()
                        {
                            IdProductNavigation = product,
                            IdProductAttributeNavigation = productAttribute,
                            Value = i.Value
                        };
                        _context.ProductDescriptions.Add(productDescription);
                    }
                }

                _context.SaveChanges();

                var result = (from productDb in _context.Products
                              where productDb.Name == productData.Name && productDb.Price == productData.Price &&
                                  productDb.IdVendorInfoNavigation.Name == productData.Vendor
                              select new
                              {
                                  Id = productDb.Id,
                                  Name = productDb.Name,
                                  Vendor = productDb.IdVendorInfoNavigation.Name
                              }).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("Product was not added");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"{ex.Message} {ex.InnerException.Message}" }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Добавление списка товаров
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AddMany(List<ProductData> productsData)
        {
            try
            {
                foreach (var productData in productsData)
                {
                    ProductCategory category = _context.ProductCategories.Where(c => c.Name == productData.Category).FirstOrDefault();
                    if (category == null)
                    {
                        throw new Exception($"Category \"{productData.Category}\" is not exist");
                    }

                    VendorInfo vendor = _context.VendorInfos.Where(v => v.Name == productData.Vendor).FirstOrDefault();
                    if (vendor == null)
                    {
                        throw new Exception($"Vendor \"{productData.Vendor}\" is not exist");
                    }

                    Product product = new Product()
                    {
                        Name = productData.Name,
                        Price = productData.Price,
                        Rating = 0.0f,
                        Avail = true,
                        IdCategoryNavigation = category,
                        IdVendorInfoNavigation = vendor
                    };
                    _context.Products.Add(product);

                    if (productData.Images != null)
                    {
                        foreach (var i in productData.Images)
                        {
                            ProductImage productImage = new ProductImage()
                            {
                                IdProductNavigation = product,
                                ImageUrl = i.ImageUrl
                            };
                            _context.ProductImages.Add(productImage);
                        }
                    }

                    if (productData.Attributes != null)
                    {
                        foreach (var i in productData.Attributes)
                        {
                            ProductAttribute checkProductAttribute = (from attribute in _context.ProductAttributes
                                                                      where attribute.Name == i.Name
                                                                      select attribute).FirstOrDefault();
                            if (checkProductAttribute == null)
                            {
                                _context.ProductAttributes.Add(new ProductAttribute()
                                {
                                    Name = i.Name
                                });
                            }

                            ProductAttribute productAttribute = (from attribute in _context.ProductAttributes
                                                                 where attribute.Name == i.Name
                                                                 select attribute).FirstOrDefault();

                            ProductDescription productDescription = new ProductDescription()
                            {
                                IdProductNavigation = product,
                                IdProductAttributeNavigation = productAttribute,
                                Value = i.Value
                            };
                            _context.ProductDescriptions.Add(productDescription);
                        }
                    }
                }
                _context.SaveChanges();

                List<object> result = new List<object>();
                foreach (var productData in productsData)
                {
                    var i = (from productDb in _context.Products
                                  where productDb.Name == productData.Name && productDb.Price == productData.Price &&
                                    productDb.IdVendorInfoNavigation.Name == productData.Vendor
                                  select new
                                  {
                                      Id = productDb.Id,
                                      Name = productDb.Name,
                                      Vendor = productDb.IdVendorInfoNavigation.Name
                                  }).FirstOrDefault();
                    result.Add(i);
                }

                if (result.Count == 0)
                {
                    throw new Exception("Products was not added");
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"{ex.Message} {ex.InnerException.Message}" }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Возвращает список всех товаров
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult AllList()
        {
            try
            {
                var result = (from product in _context.Products
                              select new ProductData()
                              {
                                  Id = product.Id,
                                  Name = product.Name,
                                  Price = product.Price,
                                  Rating = product.Rating.Value,
                                  Avail = product.Avail.Value,
                                  Category = product.IdCategoryNavigation.Name,
                                  Vendor = product.IdVendorInfoNavigation.Name,
                                  Images = (from image in _context.ProductImages
                                            where image.IdProduct == product.Id
                                            select new ProductImageData()
                                            {
                                                ImageUrl = image.ImageUrl
                                            }).ToList(),
                                  Attributes = (from attribute in _context.ProductDescriptions
                                                where attribute.IdProduct == product.Id
                                                select new ProductAttributeData()
                                                {
                                                    Name = attribute.IdProductAttributeNavigation.Name,
                                                    Value = attribute.Value
                                                }).ToList()
                              }).ToList();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }

        /// <summary>
        /// Возвращает список дочерних категорий (если ничего не передавать - вернет список корневых категорий)
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ChildCategory(string categoryName)
        {
            try
            {
                var result = (from category in _context.ProductCategories
                              where category.IdParentCategoryNavigation.Name == categoryName
                              select category).ToList();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 501 };
            }
        }
    }
}
