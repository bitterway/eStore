using Microsoft.AspNet.Mvc;
using eStore.Models;
using eStore.ViewModels;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System;
using eStore.Utils;

namespace eStore.Controllers
{
    public class BrandController : Controller
    {
        AppDbContext _db;

        public BrandController(AppDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            MenuViewModel vm = new MenuViewModel();
            // only build the catalogue once
            if (HttpContext.Session.GetObject<List<Brand>>(SessionVars.Brands) == null)
            {
                try
                {
                    BrandModel brModel = new BrandModel(_db);
                    // now load the categories
                    List<Brand> brands = brModel.GetAll();
                    HttpContext.Session.SetObject(SessionVars.Brands, brands);
                    vm.SetBrands(HttpContext.Session.GetObject<List<Brand>>(SessionVars.Brands));
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Catalogue Problem - " + ex.Message;
                }
            }
            else
            {
                vm.SetBrands(HttpContext.Session.GetObject<List<Brand>>(SessionVars.Brands));
            }
            return View(vm);
        }

        public IActionResult SelectBrand(MenuViewModel vm)
        {
            BrandModel brModel = new BrandModel(_db);
            ProductModel prModel = new ProductModel(_db);
            List<Product> products = prModel.GetAllByBrand(vm.BrandId);
            List<ProductViewModel> vms = new List<ProductViewModel>();
            if (products.Count > 0)
            {
                foreach (Product p in products)
                {
                    ProductViewModel pvm = new ProductViewModel();
                    pvm.Qty = 0;
                    pvm.BrandId = p.BrandId;
                    pvm.BrandName = brModel.GetName(p.BrandId);
                    pvm.Description = p.Description;
                    pvm.Id = p.Id;
                    pvm.ProductName = p.ProductName;
                    pvm.GraphicName = p.GraphicName;
                    pvm.CostPrice = p.CostPrice;
                    pvm.MSRP = p.MSRP;
                    pvm.QtyOnHand = p.QtyOnHand;
                    pvm.QtyOnBackOrder = p.QtyOnBackOrder;
                    vms.Add(pvm);
                }
                ProductViewModel[] myMenu = vms.ToArray();
                HttpContext.Session.SetObject(SessionVars.Menu, myMenu);
            }
            vm.SetBrands(HttpContext.Session.GetObject<List<Brand>>(SessionVars.Brands));
            return View("Index", vm);
        }

        [HttpPost]
        public ActionResult SelectItem(MenuViewModel vm)
        {
            Dictionary<int, object> cart;
            if (HttpContext.Session.GetObject<Dictionary<String, Object>>(SessionVars.Cart) == null)
            {
                cart = new Dictionary<int, object>();
            }
            else
            {
                cart = HttpContext.Session.GetObject<Dictionary<int, object>>(SessionVars.Cart);
            }
            ProductViewModel[] menu = HttpContext.Session.GetObject<ProductViewModel[]>(SessionVars.Menu);
            String retMsg = "";
            foreach (ProductViewModel item in menu)
            {
                if (item.Id == vm.Id)
                {
                    if (vm.Qty > 0) // update only selected item
                    {
                        item.Qty = vm.Qty;
                        retMsg = vm.Qty + " - item(s) Added!";
                        cart[Int32.Parse(item.Id)] = item;
                    }
                    else
                    {
                        item.Qty = 0;
                        cart.Remove(Int32.Parse(item.Id));
                        retMsg = "item(s) Removed!";
                    }
                    vm.BrandId = item.BrandId;
                    break;
                }
            }
            ViewBag.AddMessage = retMsg;
            HttpContext.Session.SetObject(SessionVars.Cart, cart);
            vm.SetBrands(HttpContext.Session.GetObject<List<Brand>>(SessionVars.Brands));
            return View("Index", vm);
        }
    }
}



//using Microsoft.AspNet.Mvc;
//using eStore.Models;
//using eStore.ViewModels;

//namespace eStore.Controllers
//{
//    public class BrandController : Controller
//    {
//        AppDbContext _db;
//        public BrandController(AppDbContext context)
//        {
//            _db = context;
//        }

//        public IActionResult Index(BrandViewModel vm)
//        {
//            BrandViewModel viewModel;
//            if (vm.BrandName == null) // 1st time
//            {
//                viewModel = new BrandViewModel();
//            }
//            else
//            {
//                viewModel = vm;
//                ProductModel itemModel = new ProductModel(_db);
//                viewModel.Products = itemModel.GetAllByBrandName(viewModel.BrandName);
//            }
//            BrandModel brModel = new BrandModel(_db);
//            viewModel.Brands = brModel.GetAll();
//            return View(viewModel);
//        }
//    }
//}