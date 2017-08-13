using Nop.Plugin.Widgets.ProductBatchUpdate.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
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


            routes.MapRoute("Plugin.Widgets.ProductBatchUpdageList", "Product_List", new { controller = "UpdateProduct", Action = "ProductList" },
               new[] { "Nop.Plugin.Widgets.ProductBatchUpdate" });

            //routes.MapRoute("Plugin.Widgets.CopyProduct", "CopyProduct", new { controller = "CopyProduct", Action = "CopyProductList" },
            //  new[] { "Nop.Plugin.Widgets.ProductBatchUpdate" });

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
