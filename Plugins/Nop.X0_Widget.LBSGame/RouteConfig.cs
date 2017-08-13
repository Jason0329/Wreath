using Nop.X0_Widget.LBSGame.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.X0_Widget.LBSGame
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {


            routes.MapRoute("Nop.X0_Widget.LBSGame.StartGame", "StartGame", new { controller = "LBSGame", Action = "StartGame" },
               new[] { "Nop.X0_Widget.LBSGame" });

            routes.MapRoute("Nop.X0_Widget.LBSGame.Game", "Game", new { controller = "LBSGame", Action = "Game" },
               new[] { "Nop.X0_Widget.LBSGame" });


            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
