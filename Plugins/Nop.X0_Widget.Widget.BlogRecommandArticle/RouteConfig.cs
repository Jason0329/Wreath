using Nop.Web.Framework.Mvc.Routes;
using Nop.X0_Widget.Widget.WreathShippingCheckout.Infrastructure;
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


            routes.MapRoute("Nop.X0_Widget.Widget.WreathShippingCheckout", "WreathShippingCheckout", new { controller = "WreathShippingCheckout", Action = "WreathShippingCheckoutPost" },
               new[] { "Nop.X0_Widget.Widget.WreathShippingCheckout" });



            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
