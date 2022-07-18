using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BethanysPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> AllCategories => _appDbContext.Categories;

        public Category Add(Category newCategory)
        {
            _appDbContext.Add(newCategory);
            return newCategory;
        }

        public IEnumerable<Category> GetCategoriesByName(string name)
        {
            var query = from r in _appDbContext.Categories
                        where r.CategoryName.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.CategoryName
                        select r;
            return query;
        }

        public Category GetCategoryById(int categoryId) => _appDbContext.Categories.FirstOrDefault(p => p.CategoryId == categoryId);

        public Category Remove(int categoryId)
        {
            var category = GetCategoryById(categoryId);
            if (category != null)
            {
                var cat = _appDbContext.Categories.Include(p => p.Pies)
                .SingleOrDefault(p => p.CategoryId == categoryId);

                foreach (var child in cat.Pies.ToList())
                    _appDbContext.Pies.Remove(child);

                _appDbContext.Categories.Remove(category);
            }
            return category;
        }

        public Category Update(Category updatedCategory)
        {
            var entity = _appDbContext.Categories.Attach(updatedCategory);
            entity.State = EntityState.Modified;
            return updatedCategory;
        }
    }
}
