using Nop.Core.Plugins;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Wreath.MergeOrder
{
    public class MergeOrderPlugin : BasePlugin, IAdminMenuPlugin
    {
        public MergeOrderPlugin()
        {
        }


        public void ManageSiteMap(SiteMapNode rootNode)
        {

            //var menuItem = new SiteMapNode()
            //{
            //    SystemName = "OrderMerge",
            //    Title = "WreathOrderMerge",
            //    ControllerName = "SMS_Sender",
            //    ActionName = "Merge",
            //    Visible = true,
            //    RouteValues = new RouteValueDictionary() { { "area", null } },
            //};

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


            //var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            //if (pluginNode != null)
            //{
            //    pluginNode.ChildNodes.Add(menuItem);
            //    pluginNode.ChildNodes.Add(menuItem_ListSMSText);
            //}
            //else
            //{
            //    rootNode.ChildNodes.Add(menuItem);
            //    rootNode.ChildNodes.Add(menuItem_ListSMSText);
            //}
        }

        public override void Install()
        {
            base.Install();
        }

        public override void Uninstall()
        {    
            base.Uninstall();
        }
    }
}
