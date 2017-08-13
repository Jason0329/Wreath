using System;
using System.Text;
using System.Web.Mvc;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Core.Domain.Orders;
using Nop.Web.Framework.Controllers;
using Nop.Services.Shipping;
using Nop.Services.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Common;
using Nop.Services.Events;
using Nop.Core.Plugins;
using Nop.Services.Shipping.Tracking;
using System.Web.Routing;

namespace Nop.Plugin.Shipping.Ezship.Services
{
    class EzshipService : IConsumer<ShipmentDeliveredEvent>, IConsumer<OrderPlacedEvent>,IConsumer<ShipmentSentEvent>, IShippingRateComputationMethod
    {
        public EzshipService(IShippingService shippingService, IOrderProcessingService dd)
        {
            
            
        }

        public ShippingRateComputationMethodType ShippingRateComputationMethodType => throw new NotImplementedException();

        public IShipmentTracker ShipmentTracker => throw new NotImplementedException();

        public PluginDescriptor PluginDescriptor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            throw new NotImplementedException();
        }

        public decimal? GetFixedRate(GetShippingOptionRequest getShippingOptionRequest)
        {
            throw new NotImplementedException();
        }

        public GetShippingOptionResponse GetShippingOptions(GetShippingOptionRequest getShippingOptionRequest)
        {
            throw new NotImplementedException();
        }

        public void HandleEvent(OrderProcessingService eventMessage)
        {
            throw new NotImplementedException();
        }

        public void HandleEvent(OrderPlacedEvent eventMessage)
        {
            throw new NotImplementedException();
        }

        public void HandleEvent(ShipmentDeliveredEvent eventMessage)
        {
            throw new NotImplementedException();
        }

        public void HandleEvent(ShipmentSentEvent eventMessage)
        {
            throw new NotImplementedException();
        }

        public void Install()
        {
            throw new NotImplementedException();
        }

        public void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}
