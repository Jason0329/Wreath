using Nop.Web.Framework.Mvc.Routes;
using Nop.X0_Widget.Widget.WreathShippingAtCheckout.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.X0_Widget.Widget.StatisticPriceEachMonth
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {


            routes.MapRoute("Nop.X0_Widget.Widget.WreathShippingAtCheckout", "WreathShippingAtCheckout", new { controller = "WreathShipping", Action = "WreathShippingPost" },
               new[] { "Nop.X0_Widget.Widget.WreathShippingAtCheckout" });

            routes.MapRoute("Nop.X0_Widget.Widget.WreathShippingMethod", "WreathShippingMethod", new { controller = "WreathShipping", Action = "WreathShippingMethod" },
              new[] { "Nop.X0_Widget.Widget.WreathShippingAtCheckout" });

            routes.MapRoute("Nop.X0_Widget.Widget.WreathShippingMethodByStore", "WreathShippingMethodByStore", new { controller = "WreathShipping", Action = "WreathShippingMethodByStore" },
             new[] { "Nop.X0_Widget.Widget.WreathShippingAtCheckout" });

            routes.MapRoute("Nop.X0_Widget.Widget.WreathShippingGetMethodGet", "WreathShippingMethodByStore", new { controller = "WreathShipping", Action = "GetShipping" },
            new[] { "Nop.X0_Widget.Widget.WreathShippingAtCheckout" });



            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
