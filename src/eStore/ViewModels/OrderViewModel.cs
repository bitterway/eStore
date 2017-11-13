using System.Collections.Generic;
using eStore.Models;
using eStore.ViewModels;
using System;

namespace eStore.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string DateCreated { get; set; }
        public List<OrderLineItemViewModel> _line_items { get; set; }
        public string _order_total { get; set; }
        public string _order_tax { get; set; }
        public string _order_sub_total { get; set; }

        public OrderViewModel() { }

        public OrderViewModel(Order order, List<OrderLineItemViewModel> lineItems)
        {
            this._line_items = lineItems;
            this.OrderId = order.Id;
            this.UserId = order.UserId;
            this.DateCreated = order.DateCreated.ToString("yyyy/MM/dd - hh:mm tt");

            decimal sub_total = 0.0M;
            decimal tax = 0.0M;
            decimal total = 0.0M;

            this._line_items = lineItems;

            foreach (var li in _line_items)
            {
                sub_total += li._extended_price;
            }

            tax = Math.Round(sub_total * 0.13M, 2);
            total = sub_total + tax;

            // currency format
            this._order_sub_total = sub_total.ToString("C");
            this._order_tax = tax.ToString("C");
            this._order_total = total.ToString("C");
        }

    }
}
