using Nop.Core.Data;
using Nop.Core.Domain.Localization;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.X0_Widget.Widget.StatisticPriceEachMonth
{
    public class StatisticPricePlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
       
        public StatisticPricePlugin()
        {

        }

        public void GetConfigurationRoute(out string actionName, out string controllerName,
           out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WreathShipping";
            routeValues = new RouteValueDictionary()
           {
               { "Namespaces", "Nop.X0_Widget.Widget.WreathShippingAtCheckout.Controllers" },
               { "area", null }
           };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName,
            out RouteValueDictionary routeValues)
        {

            //if (widgetZone == "productlist_page_last_month_price")
            //{
                actionName = "WreathShippingPost";
                controllerName = "WreathShipping";
                routeValues = new RouteValueDictionary
                {
                    {"Namespaces", "Nop.X0_Widget.Widget.WreathShippingAtCheckout.Controllers"},
                    {"area", null},
                    {"widgetZone", widgetZone}
                };
            //}
            //else //if (widgetZone == "product_page_statistic_price")
            //{
            //    actionName = "StatisticCarPrice";
            //    controllerName = "StatisticPriceMonthly";
            //    routeValues = new RouteValueDictionary
            //    {
            //        {"Namespaces", "Nop.X0_Widget.Widget.StatisticPriceEachMonth.Controllers"},
            //        {"area", null},
            //        {"widgetZone", widgetZone}
            //    };
            //}

        }

        public IList<string> GetWidgetZones()
        {
            return new List<string>() { "checkout_billing_address_field", "blogpost_page_before_comments" };//, "product_page_statistic_price"};//, "productlist_page_last_month_price" };
        }
        
        public void ManageSiteMap(SiteMapNode rootNode)
        {

            var menuItem = new SiteMapNode()
            {
                SystemName = "WreathShipping",
                Title = "Blog Recommand",
                ControllerName = "WreathShipping",
                ActionName = "WreathShippingPost",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };




            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
            {
                pluginNode.ChildNodes.Add(menuItem);

            }
            else
            {
                rootNode.ChildNodes.Add(menuItem);

            }
        }
        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //_StatisticPriceMonthlyContext.Install();
            PluginManager.MarkPluginAsInstalled(this.PluginDescriptor.SystemName);

         

        }
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            PluginManager.MarkPluginAsUninstalled(this.PluginDescriptor.SystemName);
        }

    }
}
