using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace eStore.Models
{
    public class ProductModel
    {

        private AppDbContext _db;

        //public ProductModel()
        //{
        //  _db = new AppDbContext();
        //  int x = 3;
        //}

        public ProductModel(AppDbContext context)
        {
            _db = context;
        }

        public string Decoder(string value)
        {
            Regex regex = new Regex(@"\\u(?<Value>[a-zA-Z0-9]{4})", RegexOptions.Compiled);
            return regex.Replace(value, "");
        }

        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public List<Product> GetAllByBrand(int id)
        {
            return _db.Products.Where(p => p.BrandId == id).ToList();
        }

        public List<Product> GetAllByBrandName(string brandname)
        {
            Brand brand = _db.Brands.First(b => b.Name == brandname);
            return _db.Products.Where(p => p.BrandId == brand.Id).ToList();
        }

        public string GetProductName(string id)
        {
            Product p = _db.Products.First(c => c.Id == id);
            return p.ProductName;
        }

        public Product GetById(string id)
        {
            return _db.Products.First(c => c.Id == id);
        }
    }
}
