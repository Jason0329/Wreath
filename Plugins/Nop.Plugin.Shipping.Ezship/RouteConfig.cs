using Nop.Plugin.Shipping.Ezship.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Shipping.Ezship
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute("Plugin.Shipping.Ezship", "Plugins/Ezship/BuyOrRent", 
                new { controller = "Ezship", Action = "EzshipResponse" },
               new[] { "Nop.Plugin.Shipping.Ezship.Controllers" });

            routes.MapRoute("Plugin.Shipping.Ezship.Configure",
                "Plugins/Ezship/Configure",
                new { controller = "Ezship", action = "Configure", },
                new[] { "Nop.Plugin.Shipping.Ezship.Controllers" });


            routes.MapRoute("Plugin.Shipping.Ezship.PublicInfo",
               "Plugins/Ezship/PublicInfo",
               new { controller = "Ezship", action = "PublicInfo", },
               new[] { "Nop.Plugin.Shipping.Ezship.Controllers" });

            routes.MapRoute("Plugin.Shipping.Ezship.Ezshipping",
              "Ezshipping",
              new { controller = "Ezship", action = "Ezshiping", },
              new[] { "Nop.Plugin.Shipping.Ezship.Controllers" });

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
