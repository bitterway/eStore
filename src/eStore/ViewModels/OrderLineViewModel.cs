using eStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.ViewModels
{
    public class OrderLineItemViewModel
    {
        public int _id { get; set; }
        public int _orderId { get; set; }
        public string _product_id { get; set; }
        public string _product_name { get; set; }
        public int _qtySold { get; set; }
        public int _qtyOrdered { get; set; }
        public int _qtyBackOrdered { get; set; }
        public decimal _product_msrp { get; set; }
        public decimal _extended_price { get; set; }

        // currency formatted values
        public string _str_product_msrp { get; set; }
        public string _str_extended_price { get; set; }

        public OrderLineItemViewModel() { }

        public OrderLineItemViewModel(OrderLineItem li, ProductModel pm)
        {
            this._id = li.Id;
            this._orderId = li.OrderId;
            this._product_id = li.ProductId;
            this._qtySold = li.QtySold;
            this._qtyOrdered = li.QtyOrdered;
            this._qtyBackOrdered = li.QtyBackOrdered;
            Product pr = pm.GetById(_product_id);
            this._product_name = pr.ProductName;
            this._product_msrp = pr.MSRP;
            this._extended_price = Math.Round(_qtySold * _product_msrp, 2);

            // currency format
            this._str_product_msrp = _product_msrp.ToString("C");
            this._str_extended_price = _extended_price.ToString("C");
        }

    }
}
