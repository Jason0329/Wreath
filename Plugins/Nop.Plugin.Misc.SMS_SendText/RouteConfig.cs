using Nop.Plugin.Misc.SMS_Send.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.SMS_Send
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 1; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute("Plugin.Misc.SMS_SendOrderBuyOrRent", "BuyOrRent", new { controller = "SMS_Sender", Action = "BuyOrRent" },
               new[] { "Nop.Plugin.Misc.SMS_SendText.Controllers" });

            routes.MapRoute("Plugin.Misc.SMS_SendOrderForThanks", "Thanks", new { controller = "SMS_Sender", Action = "Thanks" },
                new[] { "Nop.Plugin.Misc.SMS_SendText.Controllers" });

            routes.MapRoute("Plugin.Misc.SMS_SendOrderAsk", "AskRequire", new { controller = "SMS_Sender", Action = "Ask" },
                new[] { "Nop.Plugin.Misc.SMS_SendText.Controllers" });

            routes.MapRoute("Plugin.Misc.SMS_SendOrderConsidering", "Considering", new { controller = "SMS_Sender", Action = "Considering" },
                new[] { "Nop.Plugin.Misc.SMS_SendText.Controllers" });

            routes.MapRoute("Plugin.Misc.SMS_SendOrderList", "ListSMSMessage", new { controller = "SMS_Sender", Action = "List" },
               new[] { "Nop.Plugin.Misc.SMS_SendText.Controllers" });

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
