using BethanysPieShop.Models;
using System.Collections.Generic;

namespace BethanysPieShop.ViewModels
{
    public class AdminCategoriesListViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
