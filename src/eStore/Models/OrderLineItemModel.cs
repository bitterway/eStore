using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Models
{
    public class OrderLineItemModel
    {
        private AppDbContext _db;

        public OrderLineItemModel(AppDbContext context)
        {
            _db = context;
        }

        public List<OrderLineItem> GetAllByOrderId(int id)
        {
            return _db.OrderLineItems.Where(li => li.OrderId == id).ToList();
        }
    }
}
