using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.Widget.WreathShippingAtCheckout.Infrastructure
{
    public class CustomViewEngine:ThemeableRazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] { "~/Plugins/Widget.WreathShippingAtCheckout/Views/{0}.cshtml" };
            PartialViewLocationFormats = new[] { "~/Plugins/Widget.WreathShippingAtCheckout/Views/{0}.cshtml" };
        }
    }
}
