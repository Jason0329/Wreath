using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Shipping.Ezship.Infrastructure
{
    public class CustomViewEngine:ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { "~/Plugins/Shipping.Ezship/Views/{0}.cshtml" };
            PartialViewLocationFormats = new[] { "~/Plugins/Shipping.Ezship/Views/{0}.cshtml" };
        }
    }
}
