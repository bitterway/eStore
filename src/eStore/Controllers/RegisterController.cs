using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using eStore.Models;
using Microsoft.AspNet.Http;
using eStore.ViewModels;
using eStore.Utils;

namespace eStore.Controllers
{
    public class RegisterController : Controller
    {
        UserManager<ApplicationUser> _usrMgr;
        SignInManager<ApplicationUser> _signInMgr;

        public RegisterController(UserManager<ApplicationUser> userManager,

        SignInManager<ApplicationUser> signInManager)
        {
            _usrMgr = userManager;
            _signInMgr = signInManager;
        }
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        //
        // POST:/Register/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register/Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = model.Email,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Age = model.Age,
                    Address1 = model.Address1,
                    City = model.City,
                    Mailcode = model.Mailcode,
                    Country = model.Country,
                    CreditcardType = model.CreditcardType,
                    Region = model.Region,
                    Email = model.Email
                };
                var result = await _usrMgr.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInMgr.SignInAsync(user, isPersistent: false);
                    HttpContext.Session.SetString(SessionVars.User, model.Email);
                    HttpContext.Session.SetString(SessionVars.LoginStatus, "logged on as " + model.Email);
                    HttpContext.Session.SetString(SessionVars.Message, "Registered, as " + model.Email);
                }
                else
                {
                    ViewBag.Message = "registration failed - " + result.Errors.First().Description;
                    return View("Index");
                }
            }
            return Redirect("/Home");
        }
    }
}