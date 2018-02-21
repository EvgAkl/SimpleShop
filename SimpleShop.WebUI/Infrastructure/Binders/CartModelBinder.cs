using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Domain.Entities;

namespace SimpleShop.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        // Declaring variables
        private const string sessionKey = "Cart";
        // Declaring methods
        public object BindModel (ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = null;
            // Get "Cart" object
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }
            // Create "Cart" object if it not exist
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            return cart;
        }

    } // end class

} // end namespace