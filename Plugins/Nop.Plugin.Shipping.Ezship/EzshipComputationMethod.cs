using Nop.Core.Plugins;
using Nop.Services.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Services.Shipping.Tracking;
using System.Web.Routing;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Core.Domain.Directory;
using Nop.Services.Directory;
using Nop.Services.Configuration;
using Nop.Services.Localization;

namespace Nop.Plugin.Shipping.Ezship
{
    public class EzshipComputationMethod : BasePlugin, IShippingRateComputationMethod
    {
        private IMeasureService _measureService;
        private ISettingService _settingService;
        private IShippingService _shippingService;
        private ICountryService _countryService;
        private ICurrencyService _currencyService;
        private CurrencySettings _currencySettings;
        private IOrderTotalCalculationService _orderTotalCalculationService;
        private ILogger _logger;
        private ILocalizationService _localizationService;
        private StringBuilder _traceMessages;

        public EzshipComputationMethod(IMeasureService measureService,
            IShippingService shippingService,
            ISettingService settingService,
           // UPSSettings upsSettings, 
            ICountryService countryService,
            ICurrencyService currencyService,
            CurrencySettings currencySettings,
            IOrderTotalCalculationService orderTotalCalculationService,
            ILogger logger,
            ILocalizationService localizationService)
        {
            this._measureService = measureService;
            this._shippingService = shippingService;
            this._settingService = settingService;
           // this._upsSettings = upsSettings;
            this._countryService = countryService;
            this._currencyService = currencyService;
            this._currencySettings = currencySettings;
            this._orderTotalCalculationService = orderTotalCalculationService;
            this._logger = logger;
            this._localizationService = localizationService;

            this._traceMessages = new StringBuilder();
        }
        public ShippingRateComputationMethodType ShippingRateComputationMethodType => throw new NotImplementedException();

        public IShipmentTracker ShipmentTracker => throw new NotImplementedException();

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "Ezship";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Shipping.Ezship.Controllers" }, { "area", null } };
        }

        public decimal? GetFixedRate(GetShippingOptionRequest getShippingOptionRequest)
        {
            throw new NotImplementedException();
        }

        public GetShippingOptionResponse GetShippingOptions(GetShippingOptionRequest getShippingOptionRequest)
        {
            throw new NotImplementedException();
        }
    }
}
