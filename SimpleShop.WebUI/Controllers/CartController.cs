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
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        } // end Index()

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        } // end GetCart()

        public RedirectToRouteResult AddToCart(int Id, string returnUrl)
        {
            Game game = repository.Games.FirstOrDefault(s => s.Id == Id);

            if (game != null)
            {
                GetCart().AddItem(game, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        } // end AddToCart()

        public RedirectToRouteResult RemoveFromCart(int Id, string returnUrl)
        {
            Game game = repository.Games.FirstOrDefault(s => s.Id == Id);

            if (game != null)
            {
                GetCart().RemoveLine(game);
            }
            return RedirectToAction("Index", new { returnUrl });
        } // end RemoveFromCart()

    } // end controller

} // end namrspace