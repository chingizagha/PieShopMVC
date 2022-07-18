using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _appDbContext;

        public CategoryController(ICategoryRepository categoryRepository, AppDbContext appDbContext)
        {
            _categoryRepository = categoryRepository;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult List(string categoryName)
        {
            IEnumerable<Category> category;

            if(string.IsNullOrEmpty(categoryName))
                category = _categoryRepository.AllCategories.OrderBy(c => c.CategoryId);
            else
                category = _categoryRepository.GetCategoriesByName(categoryName);

            return View(new AdminCategoriesListViewModel
            {
                Categories = category
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int categoryId)
        {
            _categoryRepository.Remove(categoryId);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Detail(int categoryId)
        {
            var category = _categoryRepository.GetCategoryById(categoryId);
            if (category == null)
                return RedirectToAction(nameof(List));
            return View(category);
        }

        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind(include: "CategoryName, Description")]Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoryRepository.Add(category);
                    await _appDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int CategoryId)
        {
            var category = _categoryRepository.GetCategoryById(CategoryId);
            if (category == null)
                return RedirectToAction(nameof(List));
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CategoryId, CategoryName, Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.Update(category);
                    await _appDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(category);
        }
    }
}
