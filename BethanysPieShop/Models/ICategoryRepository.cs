using System.Collections.Generic;

namespace BethanysPieShop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
        IEnumerable<Category> GetCategoriesByName(string name);
        Category GetCategoryById(int categoryId);
        Category Add(Category newCategory);
        Category Update(Category updatedCategory);
        Category Remove(int categoryId);
    }
}
