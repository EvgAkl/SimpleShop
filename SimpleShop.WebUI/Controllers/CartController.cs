using System.Linq;
using System.Web.Mvc;
using SimpleShop.Domain.Entities;
using SimpleShop.Domain.Abstract;
using SimpleShop.WebUI.Models;

namespace SimpleShop.WebUI.Controllers
{
    public class CartController : Controller
    {
        // Declaring variable
        private IGameRepository repository;
        // Declaring constructors
        public CartController(IGameRepository repo)
        {
            repository = repo;
        }
        // Declaring methods
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        } // end Index()

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        } // end Summary()

        public ViewResult Checkout(Cart cart, ShipingDetails shipingDetails)
        {
            return View(new ShipingDetails());
        } // end Checkout()

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            Game game = repository.Games.FirstOrDefault(s => s.Id == Id);

            if (game != null)
            {
                cart.AddItem(game, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        } // end AddToCart()

        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            Game game = repository.Games.FirstOrDefault(s => s.Id == Id);

            if (game != null)
            {
                cart.RemoveLine(game);
            }
            return RedirectToAction("Index", new { returnUrl });
        } // end RemoveFromCart()

    } // end controller

} // end namrspace