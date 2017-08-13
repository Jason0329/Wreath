using Nop.Core.Plugins;
using Nop.Plugin.Wreath.StatisticOrder.Data;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Wreath.StatisticOrder
{
    public class StatisticProductBuyPlugin : BasePlugin, IAdminMenuPlugin
    {
        private StatisticProductBuyObjectContext _StatisticProductObjectContext;

        public StatisticProductBuyPlugin(StatisticProductBuyObjectContext StatisticProductObjectContext)
        {
            _StatisticProductObjectContext = StatisticProductObjectContext;
        }
          


        public void ManageSiteMap(SiteMapNode rootNode)
        {

            var menuItem = new SiteMapNode()
            {
                SystemName = "StatisticProductBuy",
                Title = "需代購商品統計",
                ControllerName = "StatisticProductBuy",
                ActionName = "WreathBuyList",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

            //var menuItem_ListSMSText = new SiteMapNode()
            //{
            //    SystemName = "SMS_Sender_ListSMSText",
            //    Title = "SMS_Sender_ListSMSText",
            //    ControllerName = "SMS_Sender",
            //    ActionName = "List",
            //    Visible = true,
            //    Url = "/SMS_Sender/List",
            //    RouteValues = new RouteValueDictionary() { { "area", null } },
            //};

            rootNode.ChildNodes.Add(new SiteMapNode());
            rootNode.ChildNodes[rootNode.ChildNodes.Count - 1].SystemName = "Wreath";
            rootNode.ChildNodes[rootNode.ChildNodes.Count - 1].Title = "Wreath";
            rootNode.ChildNodes[rootNode.ChildNodes.Count - 1].Visible = true;
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Wreath");

            if (pluginNode != null)
            {
                pluginNode.ChildNodes.Add(menuItem);
               
            }
            else
            {
                rootNode.ChildNodes.Add(menuItem);
               
            }
        }

        public override void Install()
        {
            _StatisticProductObjectContext.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            _StatisticProductObjectContext.Uninstall();
            base.Uninstall();
        }
    }
}
