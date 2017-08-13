using Nop.Plugin.Widgets.GoTrueCarContentManagement.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.GoTrueCarContentManagement
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {


            routes.MapRoute("Plugin.Widgets.GoTrueCarContentManagement", "Product_List", new { controller = "UpdateProduct", Action = "ProductList" },
               new[] { "Nop.Plugin.Widgets.GoTrueCarContentManagement" });

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
