using eStore.Models;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace eStore.ViewModels
{
    public class MenuViewModel
    {
        private List<Brand> _brands;
        private List<ProductViewModel> _products;
        public int Qty { get; set; }
        public string Id { get; set; }
        public int BrandId { get; set; }

        public IEnumerable<SelectListItem> GetBrands()
        {
            return _brands.Select(brand => new SelectListItem { Text = brand.Name, Value = Convert.ToString(brand.Id) });
        }

        public void SetBrands(List<Brand> brandl)
        {
            _brands = brandl;
        }

        public IEnumerable<ProductViewModel> GetProducts()
        {
            return _products;
        }

        public void SetProducts(List<ProductViewModel> productl)
        {
            _products = productl;
        }

        public void SetProducts(ProductViewModel[] catalogue)
        {
            _products = catalogue.ToList<ProductViewModel>();
        }
    }
}