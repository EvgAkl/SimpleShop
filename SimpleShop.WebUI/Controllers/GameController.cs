using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Domain.Abstract;
using SimpleShop.Domain.Entities;


namespace SimpleShop.WebUI.Controllers
{
    public class GameController : Controller
    {
        // Declaretion variables
        private IGameRepository repository;
        public int pageSize = 4;
        // Decalretion constructors
        public GameController(IGameRepository repo)
        {
            repository = repo;
        }
        // Declaretion methods
        public ViewResult List(int page = 1)
        {
            return View(repository.Games.OrderBy(o => o.Id).Skip((page - 1) * pageSize).Take(pageSize));
        }



    } // end controller

} // end namespace