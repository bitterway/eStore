using Microsoft.AspNet.Mvc;
using eStore.Models;
using eStore.ViewModels;
namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext _db;
        public ProductController(AppDbContext context)
        {
            _db = context;
        }
        // GET: /<controller>/
        public IActionResult Index(BrandViewModel brand)
        {
            ProductModel model = new ProductModel(_db);
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.BrandName = brand.BrandName;
            viewModel.Products = model.GetAllByBrandName(brand.BrandName);
            return View(viewModel);
        }
    }
}