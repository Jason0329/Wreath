using Nop.Plugin.Widgets.FacebookGame.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.FacebookGame
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {


            routes.MapRoute("X0_Widget.Widget.FacebookGame", "TestGame", new { controller = "Games", Action = "FacebookGame" },
               new[] { "Nop.X0_Widget.Widget.FacebookGame" });

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
