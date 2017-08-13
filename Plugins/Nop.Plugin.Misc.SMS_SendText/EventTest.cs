using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Services.Events;
using Nop.Services.Orders;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.SMS_SendText
{
    public class EventTest : BasePluginController, IConsumer<EntityInserted<ShoppingCartItem>>
    {
        private IShoppingCartService _ShoppingCartService;
        public EventTest(IShoppingCartService ShoppingCartService)
        {
            _ShoppingCartService = ShoppingCartService;
        }
        public void HandleEvent(EntityInserted<ShoppingCartItem> eventMessage)
        {
            var shoppingCartItem = eventMessage.Entity; // Entity is null

            Url.RouteUrl("HomePage");
            //do some more stuff here
        }
    }
}
