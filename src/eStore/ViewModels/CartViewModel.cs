using Microsoft.AspNet.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using eStore.Models;
using Newtonsoft.Json;
using System;

namespace eStore.ViewModels
{
    public class CartViewModel
    {
        public string _cart_total { get; set; }
        public string _cart_tax { get; set; }
        public string _cart_sub_total { get; set; }
        public List<CartLineItemViewModel> _cart_line_items { get; set; }

        public CartViewModel(Dictionary<string, object> cart)
        {
            if (cart != null)
            {
                createCartViewModel(cart);
            }
            else
            {
                createEmptyCartViewModel();
            }

        }

        private void createEmptyCartViewModel()
        {
            _cart_line_items = new List<CartLineItemViewModel>();
            CartLineItemViewModel cli = new CartLineItemViewModel();
            cli._line_qty = 0;
            cli._product_id = "-";
            cli._product_name = "-";
            cli._product_msrp = "-";
            cli._line_extended = "-";
            _cart_line_items.Add(cli);

            _cart_sub_total = "-";
            _cart_tax = "-";
            _cart_total = "-";
        }

        private void createCartViewModel(Dictionary<string, object> cart)
        {
            decimal extended_price = 0.0M;
            decimal sub_total = 0.0M;
            decimal tax = 0.0M;
            decimal total = 0.0M;

            _cart_line_items = new List<CartLineItemViewModel>();

            foreach (var key in cart.Keys)
            {
                ProductViewModel item = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(cart[key]));
                if (item.Qty > 0)
                {
                    CartLineItemViewModel cli = new CartLineItemViewModel();
                    cli._line_qty = item.Qty;
                    cli._product_id = item.Id;
                    cli._product_name = item.ProductName;
                    cli._product_msrp = item.MSRP.ToString("C");
                    extended_price = Math.Round(item.Qty * item.MSRP, 2);
                    cli._line_extended = extended_price.ToString("C");
                    _cart_line_items.Add(cli);

                    sub_total += extended_price;
                }
            }
            tax = Math.Round(sub_total * 0.13M, 2);
            total = sub_total + tax;
            _cart_sub_total = sub_total.ToString("C");
            _cart_tax = tax.ToString("C");
            _cart_total = total.ToString("C");
        }
    }

    public class CartLineItemViewModel
    {
        public string _product_id { get; set; }
        public string _product_name { get; set; }
        public string _product_msrp { get; set; }
        public int _line_qty { get; set; }
        public string _line_extended { get; set; }

        public CartLineItemViewModel() { }

        public CartLineItemViewModel(ProductViewModel item)
        {
            _line_qty = item.Qty;
            _product_id = item.Id;
            _product_name = item.ProductName;
            _product_msrp = item.MSRP.ToString("C");
            _line_extended = (item.Qty * item.MSRP).ToString("C");
        }
    }
}
