using Nop.Plugin.Wreath.StatisticOrder.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Wreath.StatisticOrder
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute("Plugin.Wreath.StatisticOrder", "WreathBuyList", new { controller = "StatisticProductBuy", Action = "WreathBuyList" },
               new[] { "Nop.Plugin.Wreath.StatisticOrder" });

            //routes.MapRoute("Plugin.Wreath.StatisticOrder.UpdateStatisticProductBuy", "Plugins/WreathStatisticOrder/UpdateStatisticProductBuy", new { controller = "StatisticProductBuy", Action = "UpdateStatisticProductBuy" },
            //  new[] { "Nop.Plugin.Wreath.StatisticOrder" });

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
