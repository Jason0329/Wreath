using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Wreath.StatisticOrder.Domain;
using Nop.Services.Events;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Wreath.StatisticOrder
{
    public class OrderPlacedStatisticOrder:IConsumer<OrderPlacedEvent>
    {
        private IOrderService _orderService;
        private IStoreContext _storeContext;
       
        private IRepository<Order> _OrderListRepository;
        private IRepository<StatisticProductBuy> _StatisticProductBuyRepository;

        public OrderPlacedStatisticOrder(IRepository<Order> OrderListRepository,
            IRepository<StatisticProductBuy> StatisticProductBuyRepository, IOrderService orderService, IStoreContext storeContext)
        {
            this._orderService = orderService;
            this._storeContext = storeContext;
            this._OrderListRepository = OrderListRepository;
            this._StatisticProductBuyRepository = StatisticProductBuyRepository;
        }

        public void HandleEvent(OrderPlacedEvent OrderPlaced)
        {
            List<StatisticProductBuy> Product;

            foreach (var Item in OrderPlaced.Order.OrderItems)
            {
                Product = _StatisticProductBuyRepository.Table.Where(s => s.ProductID == Item.ProductId && s.Name.CompareTo(Item.Product.Name + Item.AttributeDescription)==0 && s.LastUpdateTime.Month==DateTime.Now.Month).ToList();//&& s.LastUpdateTime.Month == DateTime.Now.Month
                
                if (Product.Count==0)
                {
                    Product = new List<StatisticProductBuy>();
                    Product.Add(new StatisticProductBuy(Item.ProductId,Item.Product.Name + Item.AttributeDescription,Item.Quantity));
                    _StatisticProductBuyRepository.Insert(Product[0]);
                }
                else
                {
                    Product[0].NeedBuyQuantity += Item.Quantity;
                    _StatisticProductBuyRepository.Update(Product[0]);
                    //Product[0].LastUpdateTime = DateTime.Now;
                }
            }
            
                
        }
    }
}
