using Nop.Core.Plugins;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Shipping.Ezship
{
    class EzshipPlugin : BasePlugin, IAdminMenuPlugin
    {


        public void ManageSiteMap(SiteMapNode rootNode)
        {

            //var menuItem_ListSMSText = new SiteMapNode()
            //{
            //    SystemName = "Ezship",
            //    Title = "Ezship",
            //    ControllerName = "Ezship",
            //    ActionName = "List",
            //    Visible = true,
            //    Url = "/ListSMSMessage",
            //    RouteValues = new RouteValueDictionary() { { "area", null } },
            //};


            //var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            //if (pluginNode != null)
            //{
            //    pluginNode.ChildNodes.Add(menuItem_ListSMSText);
            //}
            //else
            //{
            //    rootNode.ChildNodes.Add(menuItem_ListSMSText);
            //}
        }

        public override void Install()
        {
            //this.AddOrUpdatePluginLocaleResource("Admin.SMS.SMSList", "簡訊傳送清單");
            //this.AddOrUpdatePluginLocaleResource("Admin.SMS.OrderID", "詢價單編號");
            //this.AddOrUpdatePluginLocaleResource("Admin.SMS.CustomerName", "客戶姓名");
            //this.AddOrUpdatePluginLocaleResource("Admin.SMS.PhoneNumber", "電話");
            //this.AddOrUpdatePluginLocaleResource("Admin.SMS.MessageText", "傳送訊息");
            //this.AddOrUpdatePluginLocaleResource("Admin.SMS.MessageSendTime", "傳送時間");
            base.Install();
        }

        public override void Uninstall()
        {
            //this.DeletePluginLocaleResource("Admin.SMS.SMSList");
            //this.DeletePluginLocaleResource("Admin.SMS.OrderID");
            //this.DeletePluginLocaleResource("Admin.SMS.CustomerName");
            //this.DeletePluginLocaleResource("Admin.SMS.PhoneNumber");
            //this.DeletePluginLocaleResource("Admin.SMS.MessageText");
            //this.DeletePluginLocaleResource("Admin.SMS.MessageSendTime");
            base.Uninstall();
        }
    }
}
