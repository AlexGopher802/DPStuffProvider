using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPSP_Api.Models.Views;

namespace DPSP_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DPSPDBContext _context;

        public CategoryController(DPSPDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получаем список всех категорий
        /// </summary>
        [HttpGet]
        public IEnumerable<CategoryView> GetCategories()
        {
            var result = (from category in _context.ProductCategories
                         where category.IdParentCategoryNavigation == null
                         select new CategoryView(category, null)).ToList();

            result.AddRange((from category in _context.ProductCategories
                     join parentCategory in _context.ProductCategories on category.IdParentCategoryNavigation equals parentCategory
                     select new CategoryView(category, parentCategory)).ToList());
                     
            return result;
        }

        /// <summary>
        /// Получаем список категорий, не имеющих родительской категории
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<CategoryView> GetMainCategories()
        {
            var result = from category in _context.ProductCategories
                         where category.IdParentCategoryNavigation == null
                         select new CategoryView(category, null);

            return result;
        }

        /// <summary>
        /// Получаем список дочерних категорий
        /// </summary>
        [HttpGet]
        [Route("{categoryName}")]
        public ActionResult<IEnumerable<CategoryView>> GetChildCategories(string categoryName)
        {
            var result = ChildCategories(categoryName);

            if(result.Count() == 0)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }

        /// <summary>
        /// Получаем список дочерних категорий
        /// </summary>
        public List<CategoryView> ChildCategories(string categoryName)
        {
            var result = (from category in _context.ProductCategories
                         join parentCategory in _context.ProductCategories on category.IdParentCategory equals parentCategory.Id
                         where parentCategory.Name == categoryName
                         select new CategoryView(category, parentCategory)).ToList();

            return result;
        }
    }
}
