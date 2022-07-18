using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace BethanysPieShop.Models
{
    public class Pie
    {
        
        public int PieId { get; set; }

        [Required(ErrorMessage = "Please enter pie name")]
        [Display(Name = "Pie name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter short description")]
        [Display(Name = "Short Description")]
        [StringLength(100)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Please enter long description")]
        [Display(Name = "Long Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "Please enter pie price")]
        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Display(Name = "Image Url")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Display(Name = "Image Thumbnail Url")]
        [DataType(DataType.ImageUrl)]
        public string ImageThumbnailUrl { get; set; }

        [Required(ErrorMessage = "Please enter correct")]
        [Display(Name = "Is Pie of the week")]
        public bool IsPieOfTheWeek { get; set; }

        [Required(ErrorMessage = "Please enter correct")]
        [Display(Name = "In Stock")]
        public bool InStock { get; set; }

       
        public int CategoryId { get; set; }

        [BindNever]
        public Category Category { get; set; }
    }
}
