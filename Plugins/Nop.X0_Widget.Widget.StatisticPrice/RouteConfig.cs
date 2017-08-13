using Nop.Web.Framework.Mvc.Routes;
using Nop.X0_Widget.Widget.StatisticPrice.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.X0_Widget.Widget.StatisticPrice
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {


            routes.MapRoute("Nop.X0_Widget.Widget.StatisticPrice", "StatisticPrice", new { controller = "StatisticPrice", Action = "StatisticPrice" },
               new[] { "Nop.X0_Widget.Widget.StatisticPrice" });

            routes.MapRoute("Nop.X0_Widget.Widget.StatisticPriceList", "StatisticPriceList", new { controller = "StatisticPrice", Action = "StatisticPriceList" },
               new[] { "Nop.X0_Widget.Widget.StatisticPrice" });

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
