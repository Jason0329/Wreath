using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Orders;
using Nop.Services.Events;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Wreath.MergeOrder
{
    public class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
    {
        private IOrderService _orderService;
        private IStoreContext _storeContext;
       
        private IRepository<Order> _OrderListRepository;
        public OrderPlacedEventConsumer(IRepository<Order> OrderListRepository, IOrderService orderService, IStoreContext storeContext)
        {
            this._orderService = orderService;
            this._storeContext = storeContext;
            this._OrderListRepository = OrderListRepository;
        }

        public void HandleEvent(OrderPlacedEvent OrderPlaced)
        {
            
            try
            {
                //_OrderListRepository.Insert(OrderPlaced.Order);

                var GetAllOrder = from orders in _OrderListRepository.Table.ToList() where 
                                      orders.CustomerId == OrderPlaced.Order.CustomerId &&
                                      orders.Id!=OrderPlaced.Order.Id&&
                                      orders.Deleted ==false &&
                                      orders.CreatedOnUtc.Month == OrderPlaced.Order.CreatedOnUtc.Month &&
                                      orders.OrderStatus == OrderStatus.Pending     
                                      orderby orders.Id descending
                                  select orders ;

                

                foreach (var Order in GetAllOrder)
                {
                    foreach (var OrderItem in Order.OrderItems)
                    {
                        OrderItem _items = OrderPlaced.Order.OrderItems.Where(item => item.ProductId == OrderItem.ProductId && item.AttributeDescription.CompareTo(OrderItem.AttributeDescription) == 0).FirstOrDefault();

                        if (_items == null)
                            OrderPlaced.Order.OrderItems.Add(OrderItem);
                        else
                        {
                            _items.Quantity += OrderItem.Quantity;
                            _items.PriceExclTax = _items.Quantity * _items.UnitPriceExclTax;
                            _items.PriceInclTax = _items.Quantity * _items.UnitPriceInclTax;
                        }
                            
                    }

                    OrderPlaced.Order.OrderSubtotalExclTax += Order.OrderSubtotalExclTax;
                    OrderPlaced.Order.OrderSubtotalInclTax += Order.OrderSubtotalInclTax;

                    OrderPlaced.Order.OrderTotal += Order.OrderTotal - Order.OrderShippingExclTax;

                    if (OrderPlaced.Order.OrderTotal >= 2500)
                    {
                        OrderPlaced.Order.OrderTotal -= OrderPlaced.Order.OrderShippingExclTax;
                        OrderPlaced.Order.OrderShippingExclTax = 0;
                        OrderPlaced.Order.OrderShippingInclTax = 0;
                    }

                    
                    Order.Deleted = true;
                    _OrderListRepository.Update(Order);
                }

                _OrderListRepository.Update(OrderPlaced.Order);
            }
            catch (Exception eee)
            {
                Console.WriteLine(eee.Message);
            }
        }
    }
}
