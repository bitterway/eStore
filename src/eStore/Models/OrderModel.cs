using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using eStore.ViewModels;

namespace eStore.Models
{
    public class OrderModel
    {
        private AppDbContext _db;
        public OrderModel(AppDbContext ctx)
        {
            _db = ctx;
        }

        // Add Order to db
        public int AddOrder(Dictionary<string, object> items, string userId)
        {
            int orderId = -1;
            using (_db)
            {
                // we need a transaction as multiple entities involved
                using (var _trans = _db.Database.BeginTransaction())
                {
                    try
                    {
                        Order order = new Order();
                        order.UserId = userId;

                        foreach (var key in items.Keys)
                        {
                            ProductViewModel item = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            if (item.Qty > 0)
                            {
                                order.DateCreated = System.DateTime.Now;
                            }
                        }
                        _db.Orders.Add(order);
                        _db.SaveChanges();

                        // then add each item to the OrderLineItems table and update product table and orderTotal
                        //decimal order_total = 0.0M;
                        foreach (var key in items.Keys)
                        {
                            ProductViewModel item = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            if (item.Qty > 0)
                            {
                                OrderLineItem lineItem = new OrderLineItem();
                                lineItem.Product = _db.Products.FirstOrDefault(p => p.Id == item.Id);
                                lineItem.ProductId = item.Id;
                                lineItem.OrderId = order.Id;
                                lineItem.QtyOrdered = item.Qty;
                                if (item.Qty > item.QtyOnHand)
                                {
                                    lineItem.QtyBackOrdered = item.Qty - item.QtyOnHand;
                                    lineItem.Product.QtyOnBackOrder += lineItem.QtyBackOrdered;
                                    lineItem.Product.QtyOnHand = 0;
                                }
                                else
                                {
                                    lineItem.QtyBackOrdered = 0;
                                    lineItem.Product.QtyOnBackOrder = 0;
                                    lineItem.Product.QtyOnHand = item.QtyOnHand - item.Qty;
                                }
                                lineItem.QtySold = lineItem.QtyOrdered - lineItem.QtyBackOrdered;

                                _db.Products.Update(lineItem.Product);
                                _db.OrderLineItems.Add(lineItem);
                                _db.SaveChanges();
                            }
                        }
                        // test trans by uncommenting out these 3 lines
                        //int x = 1;
                        //int y = 0;
                        //x = x / y;
                        _trans.Commit();
                        orderId = order.Id;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _trans.Rollback();
                    }
                }
            }
            return orderId;
        }

        public List<OrderViewModel> GetOrderViewModels(string uid)
        {
            List<OrderViewModel> ovm_list = new List<OrderViewModel>();

            try
            {
                List<Order> orders = _db.Orders.Where(o => o.UserId == uid).ToList();
                foreach (var o in orders)
                {
                    List<OrderLineItemViewModel> olvm_list = GetOrderLineItems(o.Id);
                    ovm_list.Add(new OrderViewModel(o, olvm_list));
                }

                return ovm_list;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }

        }

        public List<OrderLineItemViewModel> GetOrderLineItems(int oid)
        {
            ProductModel pm = new ProductModel(_db);
            List<OrderLineItemViewModel> vm_list = new List<OrderLineItemViewModel>();

            try
            {
                List<OrderLineItem> line_items = new OrderLineItemModel(_db).GetAllByOrderId(oid);
                foreach (var li in line_items)
                {
                    vm_list.Add(new OrderLineItemViewModel(li, pm));
                }

                return vm_list;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
    }
}