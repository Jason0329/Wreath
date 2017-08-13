using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Wreath.StatisticOrder.Infrastructure
{
    public class CustomViewEngine:ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { "~/Plugins/Widget.Wreath.StatisticOrder/Views/{0}.cshtml" };
            PartialViewLocationFormats = new[] { "~/Plugins/Widget.Wreath.StatisticOrder/Views/{0}.cshtml" };
        }
    }
}
