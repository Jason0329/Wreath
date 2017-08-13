using Nop.Plugin.Widgets.ProductBatchUpdate.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using Nop.X0_Widget.Widget.StatisticPrice.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.ProductBatchUpdate
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {


            routes.MapRoute("X0_Widget.Widget.AutoBackupDatabase", "Rss/News", new { controller = "Rss_News", Action = "RssOutput" },
               new[] { "Nop.X0_Widget.Widget.AutoBackupDatabase" });

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
