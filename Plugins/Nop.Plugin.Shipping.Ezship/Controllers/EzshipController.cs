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
            return null;
        }


        //[ChildActionOnly]
        public ActionResult Configure()
        {
            var model=1;

            return View("~/Plugins/Shipping.Ezship/Views/Configure.cshtml", model);
        }
    }
}
