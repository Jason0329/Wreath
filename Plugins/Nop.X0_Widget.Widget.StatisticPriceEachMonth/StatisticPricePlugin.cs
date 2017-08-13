using Nop.Core.Data;
using Nop.Core.Domain.Localization;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Web.Framework.Menu;
using Nop.X0_Widget.Widget.StatisticPriceEachMonth.Data;
using Nop.X0_Widget.Widget.StatisticPriceEachMonth.Domain;
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
        private StatisticPriceMonthlyObjectContext _StatisticPriceMonthlyContext;
        private IRepository<StatisticPriceMonthly> _StatisticPriceMonthlyIRepository;
        private ILocalizationService _LocalizationService;
        public StatisticPricePlugin(StatisticPriceMonthlyObjectContext StatisticPriceMonthlyContext, 
            IRepository<StatisticPriceMonthly> StatisticPriceMonthlyIRepository,
            ILocalizationService LocalizationService)
        {
            _StatisticPriceMonthlyContext = StatisticPriceMonthlyContext;
            _StatisticPriceMonthlyIRepository = StatisticPriceMonthlyIRepository;
            _LocalizationService = LocalizationService;
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName,
           out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "StatisticPriceMonthly";
            routeValues = new RouteValueDictionary()
           {
               { "Namespaces", "Nop.X0_Widget.Widget.StatisticPriceEachMonth.Controllers" },
               { "area", null }
           };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName,
            out RouteValueDictionary routeValues)
        {

            //if (widgetZone == "productlist_page_last_month_price")
            //{
                actionName = "LastMonthCarPrice";
                controllerName = "StatisticPriceMonthly";
                routeValues = new RouteValueDictionary
                {
                    {"Namespaces", "Nop.X0_Widget.Widget.StatisticPriceEachMonth.Controllers"},
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
            return new List<string>() { "productlist_page_last_month_price" };//, "product_page_statistic_price"};//, "productlist_page_last_month_price" };
        }
        
        public void ManageSiteMap(SiteMapNode rootNode)
        {

            var menuItem = new SiteMapNode()
            {
                SystemName = "StatisticPriceListMonthly",
                Title = "Statistic Price List Monthly",
                ControllerName = "StatisticPriceMonthly",
                ActionName = "StatisticPriceListMonthly",
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
            _StatisticPriceMonthlyContext.Install();
            PluginManager.MarkPluginAsInstalled(this.PluginDescriptor.SystemName);

           // _LocalizationService.InsertLocaleStringResource(new LocaleStringResource { ResourceName = "LastMonth.PriceTrend", ResourceValue = "至上月止車價成交趨勢" });
            //StatisticPriceMonthly _StatisticPriceMonthly;
            //DateTime datetime1 = new DateTime(2016, 01, 01);
            //DateTime datetime2 = new DateTime(2016, 01, 15);
            //for (int i = 0; i < 13; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        _StatisticPriceMonthly = new StatisticPriceMonthly();
            //        _StatisticPriceMonthly.ProductID = 4350 + j;
            //        _StatisticPriceMonthly.IsPublished = true;
            //        _StatisticPriceMonthly.MSRP = 6430000;
            //        _StatisticPriceMonthly.MonthlyAveragePrice = 6430000 - 100000 * i - _StatisticPriceMonthly.ProductID;
            //        _StatisticPriceMonthly.MonthlyGotruecarPrice = 6430000 - 200000 * i;
            //        _StatisticPriceMonthly.Name = " Lexus LS 600hL 頂級(16/17)-1" + i * j;
            //        _StatisticPriceMonthly.CreateTime = DateTime.Now;
            //        _StatisticPriceMonthly.UpdateTime = DateTime.Now;
            //        _StatisticPriceMonthly.Month = (i % 2 == 0) ? datetime1.AddMonths(i / 2).ToString("yyyyMMdd") : datetime2.AddMonths(i / 2).ToString("yyyyMMdd");

            //        _StatisticPriceMonthlyIRepository.Insert(_StatisticPriceMonthly);
            //    }
            //}


        }
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            _StatisticPriceMonthlyContext.Uninstall();
            PluginManager.MarkPluginAsUninstalled(this.PluginDescriptor.SystemName);
        }

    }
}
