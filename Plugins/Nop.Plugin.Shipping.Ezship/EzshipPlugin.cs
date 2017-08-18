using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Shipping.Ezship
{
    class EzshipPlugin : BasePlugin, IAdminMenuPlugin, IWidgetPlugin
    {
        private IWebHelper _webHelper;
        private ISettingService _settingService;

        public EzshipPlugin(ISettingService settingService, IWebHelper webHelper)
        {
            this._settingService = settingService;
            this._webHelper = webHelper;

        }
        public void ManageSiteMap(SiteMapNode rootNode)
        {

            var menuItem_ListSMSText = new SiteMapNode()
            {
                SystemName = "Ezship",
                Title = "Ezship",
                ControllerName = "Ezship",
                ActionName = "Ezshiping",
                Visible = true,
                Url = "/EzshipTest",
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };


            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
            {
                pluginNode.ChildNodes.Add(menuItem_ListSMSText);
            }
            else
            {
                rootNode.ChildNodes.Add(menuItem_ListSMSText);
            }
        }

        public override void Install()
        {
            PluginManager.MarkPluginAsInstalled(this.PluginDescriptor.SystemName);

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
            PluginManager.MarkPluginAsUninstalled(this.PluginDescriptor.SystemName);
            //this.DeletePluginLocaleResource("Admin.SMS.SMSList");
            //this.DeletePluginLocaleResource("Admin.SMS.OrderID");
            //this.DeletePluginLocaleResource("Admin.SMS.CustomerName");
            //this.DeletePluginLocaleResource("Admin.SMS.PhoneNumber");
            //this.DeletePluginLocaleResource("Admin.SMS.MessageText");
            //this.DeletePluginLocaleResource("Admin.SMS.MessageSendTime");
            base.Uninstall();
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string>() { "checkout_billing_address_bottom" };
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "Ezship";
            routeValues = new RouteValueDictionary()
           {
               { "Namespaces", "Nop.Plugin.Shipping.Ezship.Controllers" },
               { "area", null }
           };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "Ezship";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Shipping.Ezship.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }
    }
}
