using Nop.Plugin.Widgets.AutoBackupDatabase.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.AutoBackupDatabase
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {


            //routes.MapRoute("X0_Widget.Widget.Blog", "Rss/News", new { controller = "Rss_News", Action = "RssOutput" },
            //   new[] { "Nop.X0_Widget.Widget.Blog" });

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
