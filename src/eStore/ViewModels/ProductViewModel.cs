using eStore.Models;
using System.Collections.Generic;
namespace eStore.ViewModels
{
    public class ProductViewModel
    {
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string GraphicName { get; set; }
        public decimal CostPrice { get; set; }
        public decimal MSRP { get; set; }
        public int QtyOnHand { get; set; }
        public int QtyOnBackOrder { get; set; }
        public int Qty { get; set; }

        public string JsonData { get; set; }

        // constructors
        public ProductViewModel() { }

        // constructor to instantiate from Product object
        public ProductViewModel(Product p)
        {
            this.Qty = 0;
            this.Id = p.Id;
            this.BrandId = p.BrandId;
            this.ProductName = p.ProductName;
            this.Description = p.Description;
            this.GraphicName = p.GraphicName;
            this.MSRP = p.MSRP;
            this.CostPrice = p.CostPrice;
            this.QtyOnHand = p.QtyOnHand;
            this.QtyOnBackOrder = p.QtyOnBackOrder;
        }
    }
}