using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using eStore.Utils;
using eStore.Models;
using eStore.ViewModels;
using System.Collections.Generic;
using System;

namespace eStore.Controllers
{
    public class CartController : Controller
    {
        AppDbContext _db;

        public CartController(AppDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            Dictionary<string, object> cart = HttpContext.Session.GetObject<Dictionary<string, Object>>(SessionVars.Cart);
            CartViewModel cvm = new CartViewModel(cart);
            return View(cvm);
        }

        public ActionResult ClearCart()
        {
            HttpContext.Session.Remove(SessionVars.Cart); // clear out current tray
            HttpContext.Session.SetString(SessionVars.Message, "cart Cleared"); // clear out current cart once order has been placed
            return Redirect("/Home");
        }

        public ActionResult AddOrder()
        {
            // they can't add an order if they're not logged on
            if (HttpContext.Session.GetString(SessionVars.User) == null)
            {
                return Redirect("/Login");
            }
            OrderModel model = new OrderModel(_db);
            int retVal = -1;
            string retMessage = "";
            try
            {
                Dictionary<string, object> cartItems = HttpContext.Session.GetObject<Dictionary<string, object>>(SessionVars.Cart);
                retVal = model.AddOrder(cartItems, HttpContext.Session.GetString(SessionVars.User));
                if (retVal > 0) // Order Added
                {
                    retMessage = "Order " + retVal + " Created!";
                }
                else // problem
                {
                    retMessage = "Order not added, try again later";
                }
            }
            catch (Exception ex) // big problem
            {
                retMessage = "Order was not created, try again later! - " + ex.Message;
            }
            HttpContext.Session.Remove(SessionVars.Cart); // clear out current cart/order once persisted
            HttpContext.Session.SetString(SessionVars.Message, retMessage);
            return Redirect("/Home");
        }
    }
}