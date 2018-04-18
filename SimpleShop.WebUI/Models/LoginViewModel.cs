using System.ComponentModel.DataAnnotations;

namespace SimpleShop.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    } // end class

} // end namespace