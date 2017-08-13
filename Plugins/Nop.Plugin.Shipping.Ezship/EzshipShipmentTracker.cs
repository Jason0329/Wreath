using Nop.Services.Shipping.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Shipping.Ezship
{
    public class EzshipShipmentTracker : IShipmentTracker
    {
        public IList<ShipmentStatusEvent> GetShipmentEvents(string trackingNumber)
        {
            throw new NotImplementedException();
        }

        public string GetUrl(string trackingNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsMatch(string trackingNumber)
        {
            throw new NotImplementedException();
        }
    }
}
