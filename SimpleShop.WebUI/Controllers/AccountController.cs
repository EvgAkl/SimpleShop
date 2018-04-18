using System.Web.Mvc;
using SimpleShop.WebUI.Models;
using SimpleShop.WebUI.Infrastructure.Abstract;


namespace SimpleShop.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        } // end constructor

        public ViewResult login()
        {
            return View();
        } // end login()

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View();
                }
            } // end main if
            else
            {
                return View();
            }

        } // end Index()

    } // end class

} // end namespace