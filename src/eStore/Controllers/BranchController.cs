using Microsoft.AspNet.Mvc;
using eStore.Models;
using eStore.Utils;
using Microsoft.AspNet.Http;
using estore.Models;

namespace eStore.Controllers
{
    public class BranchController : Controller
    {
        AppDbContext _db;

        //constructor
        public BranchController(AppDbContext context)
        {
            _db = context;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionVars.Message) != null)
            {
                ViewBag.Message = HttpContext.Session.GetString(SessionVars.Message);
            }
            return View();
        }

        //get 3 branches
        [Route("[action]/{lat:double}/{lng:double}")]
        public IActionResult GetBranches(float lat, float lng)
        {
            BranchModel model = new BranchModel(_db);
            return Ok(model.GetThreeClosetBranches(lat, lng));
        }
    }
}