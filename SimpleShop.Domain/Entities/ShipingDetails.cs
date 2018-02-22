using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Domain.Entities
{
    public class ShipingDetails
    {
        [Required(ErrorMessage = "Укажите своё имя")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Укажите первый адрес доставки")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }

    } // end ShipingDetails()

} // end namespace
