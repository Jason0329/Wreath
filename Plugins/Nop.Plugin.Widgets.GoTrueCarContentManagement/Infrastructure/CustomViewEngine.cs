using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.GoTrueCarContentManagement.Infrastructure
{
    public class CustomViewEngine:ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { "~/Plugins/Widgets.GoTrueCarContentManagement/Views/{0}.cshtml" };
            PartialViewLocationFormats = new[] { "~/Plugins/Widgets.GoTrueCarContentManagement/Views/{0}.cshtml" };
        }
    }
}
