using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Domain.Entities
{
    public class Game
    {   
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название игры")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание игры")]
        [DataType(DataType.MultilineText)]
        public  string Description { get; set; }
        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию для игры")]
        public string Category { get; set; }
        [Display(Name = "Цена (руб)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }
    } // end class

} // end namespace
