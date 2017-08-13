using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Shipping.Ezship
{
    public class EzshipSetting : ISettings
    {
        public string Url { get; set; }
        public string Username { get; set; }
    }
}
