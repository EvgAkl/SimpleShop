using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Domain.Abstract;

namespace SimpleShop.WebUI.Controllers
{
    public class NavController : Controller
    {
        // Declaring variables
        private IGameRepository repository;
        // Declaring constructors
        public NavController(IGameRepository repo)
        {
            repository = repo;
        }
        // Declaring methods
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Games.Select(s => s.Category).Distinct().OrderBy(x => x);

            return PartialView("FlexMenu", categories);
        } // end Menu()

    } // end controller

} // end namespace