using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BethanysPieShop.Areas.Admin.Components
{
    public class CategorySelect : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategorySelect(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.AllCategories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}
