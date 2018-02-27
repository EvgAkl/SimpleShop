using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Domain.Entities
{
    public class Game
    {   
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public  string Description { get; set; }
        [Display(Name = "Категория")]
        public string Category { get; set; }
        [Display(Name = "Цена (руб)")]
        public decimal Price { get; set; }
    } // end class

} // end namespace
