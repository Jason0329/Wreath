using System;
using System.Text;
using System.Web.Mvc;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Shipping.Ezship.Controllers
{
    class EzshipController : BasePluginController
    {
        ActionResult EzshipResponseController()
        {
            return View("~/Plugins/Shipping.Ezship/Views/Configure.cshtml");
        }


        //[ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new object();

            return View("~/Plugins/Shipping.Ezship/Views/Configure.cshtml", model);
        }

        public ActionResult PublicInfoController()
        {
            return View("~/Plugins/Shipping.Ezship/Views/Configure.cshtml");
        }

        public ActionResult EzshipingController()
        {
            return View("~/Plugins/Shipping.Ezship/Views/Configure.cshtml");
        }
    }
}
