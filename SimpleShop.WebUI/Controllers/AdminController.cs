using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Domain.Abstract;

namespace SimpleShop.WebUI.Controllers
{
    public class AdminController : Controller
    {
        // Declaring variables
        private IGameRepository repository;
        // Declaring constructors
        public AdminController(IGameRepository repo)
        {
            repository = repo;
        }
        // Declaring methods
        public ViewResult Index()
        {
            return View(repository.Games);
        }

    } // end controller

} // end namespace