using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.X0_Widget.Widget.StatisticPriceEachMonth.Domain;
using Nop.X0_Widget.Widget.StatisticPriceEachMonth.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Nop.X0_Widget.Widget.StatisticPriceEachMonth.Controllers
{
    public class StatisticPriceMonthlyController : BasePluginController
    {
        private IRepository<StatisticPriceMonthly> _StatisticPriceMontlyRepository;
        private ILogger _logger;
        private IPermissionService _PermissionService;    
        private ILocalizationService _localizationService;

        ImportExportManagement ImportExportData;
        private Product _product;
        private IPriceFormatter _priceFormatter;

        public StatisticPriceMonthlyController(IRepository<StatisticPriceMonthly> StatisticPriceMontlyRepository,ILogger logger
            , IPermissionService PermissionService, ILocalizationService localizationService, IPriceFormatter priceFormatter
          
            )
        {
            _StatisticPriceMontlyRepository = StatisticPriceMontlyRepository;
            _logger = logger;
            _PermissionService = PermissionService;
            _localizationService = localizationService;
            _priceFormatter = priceFormatter;
 
            ImportExportData = new ImportExportManagement();
            
        }

        public ActionResult Configure()
        {
            var model = new object();
            //..\..\Presentation\Nop.Web\Plugins\BadPayBad.HelloWorld\
            //the view file .cshml depend your output path
            return View("~/Plugins/Widget.StatisticPriceEachMonth/Views/Admin/Configure.cshtml", model);
        }


        public ActionResult StatisticCarPrice(int ProductID)
        {
            var model = new StatisticPriceMonthly(); 
            model.StatisticPriceMontlyList=_StatisticPriceMontlyRepository.Table.Where(m=>m.ProductID==ProductID).OrderBy(m=>m.Month).ToList();

            int i = model.StatisticPriceMontlyList.Count >= 9 ? model.StatisticPriceMontlyList.Count - 9 : 0;
            for (; i < model.StatisticPriceMontlyList.Count; i++)
            {
                model.MSRPList.Add(model.StatisticPriceMontlyList[i].MSRP);
                model.MonthlyAveragePriceList.Add(model.StatisticPriceMontlyList[i].MonthlyAveragePrice);
                model.MonthlyGotruecarPriceList.Add(model.StatisticPriceMontlyList[i].MonthlyGotruecarPrice);
                model.MonthList.Add(model.StatisticPriceMontlyList[i].Month);
                model.Name = model.StatisticPriceMontlyList[0].Name;
            }

            var jsonSerialiser = new JavaScriptSerializer();
            
            model.MSRP_Json = jsonSerialiser.Serialize(model.MSRPList);
            model.MonthlyAveragePrice_Json = jsonSerialiser.Serialize(model.MonthlyAveragePriceList);
            model.MonthlyGotruecarPrice_Json = jsonSerialiser.Serialize(model.MonthlyGotruecarPriceList);

            //..\..\Presentation\Nop.Web\Plugins\BadPayBad.HelloWorld\
            //the view file .cshml depend your output path
            return View("~/Plugins/Widget.StatisticPriceEachMonth/Views/_StatisticCarPrice.cshtml", model);
        }

        public ActionResult LastMonthCarPrice(object additionalData=null)
        {
            var model_Select = new StatisticPriceMonthly();
            int productid = (int)additionalData;
            model_Select.StatisticPriceMontlyList = _StatisticPriceMontlyRepository.Table.Where(m => m.ProductID == productid).OrderBy(m => m.Month).ToList();

            if (model_Select.StatisticPriceMontlyList.Count == 0)
                return null;

            decimal price = model_Select.StatisticPriceMontlyList[model_Select.StatisticPriceMontlyList.Count - 1].MonthlyAveragePrice;
            var model = _priceFormatter.FormatPrice(price);
           

            return View("~/Plugins/Widget.StatisticPriceEachMonth/Views/_LastMonthCarPrice.cshtml", (object)model);
        }

        public ActionResult StatisticPriceProductList()
        {
            return View();
        }

        public ActionResult StatisticPriceListMonthly()
        {

            if (!_PermissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return RedirectToAction("Login");



            return View();
        }

        [HttpPost]
        public ActionResult GetStatisticPrice(DataSourceRequest command, StatisticPriceMonthly Model)
        {

            
            List<StatisticPriceMonthly> StatisticPriceMontlyList = Model.IsPublished ? _StatisticPriceMontlyRepository.Table.Where(s => s.IsPublished == true).ToList() : _StatisticPriceMontlyRepository.Table.ToList();

            List<StatisticPriceMonthly> OutputData = new List<StatisticPriceMonthly>();

            for (int i = 0; i < StatisticPriceMontlyList.Count; i++)
            {
                //int PageCount = (int)(i / command.PageSize) + 1;

                //if (PageCount < command.Page) continue;
                //if (PageCount > command.Page) break;

                if(Model.Name==null||StatisticPriceMontlyList[i].Name.Contains(Model.Name))
                {
                    OutputData.Add(StatisticPriceMontlyList[i]);
                }
            }


            OutputData = OutputData.OrderBy(m => m.Name).ToList();
            //OutputData.Sort(delegate(StatisticPriceMonthly p1, StatisticPriceMonthly p2)
            //{
            //    return p1.Name.CompareTo(p2.Name);
            //});

            var gridModel = new DataSourceResult
            {
                Data = OutputData,
                Total = OutputData.Count
            };

            return Json(gridModel);
        }


        [HttpPost]
        public ActionResult UpdateStatisticPrice(StatisticPriceMonthly _StatisticPrice)
        {

            var SelectProduct = _StatisticPriceMontlyRepository.GetById(_StatisticPrice.Id);

            SelectProduct.Name = _StatisticPrice.Name;
            SelectProduct.Month = _StatisticPrice.Month;
            SelectProduct.IsPublished = _StatisticPrice.IsPublished;
            SelectProduct.MSRP = _StatisticPrice.MSRP;
            SelectProduct.MonthlyAveragePrice = _StatisticPrice.MonthlyAveragePrice;
            SelectProduct.MonthlyGotruecarPrice = _StatisticPrice.MonthlyGotruecarPrice;
            SelectProduct.UpdateTime = DateTime.Now;


            _StatisticPriceMontlyRepository.Update(SelectProduct);


            return new NullJsonResult();
        }


        [HttpPost]
        public ActionResult DeleteStatisticPrice(DataSourceRequest command, int ID)
        {

            var Sender = _StatisticPriceMontlyRepository.GetById(ID);
            Sender.IsPublished = false;
            Sender.UpdateTime = DateTime.Now;

            _StatisticPriceMontlyRepository.Update(Sender);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult ImportCsv(FormCollection form)
        {
            if (!_PermissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return RedirectToAction("Home");



            try
            {
                var file = Request.Files["importcsvfile"];
                if (file != null && file.ContentLength > 0)
                {
                    int count = ImportExportData.Import_CSV(file.InputStream, _StatisticPriceMontlyRepository);
                    SuccessNotification(String.Format(_localizationService.GetResource("Admin.Promotions.NewsLetterSubscriptions.ImportEmailsSuccess"), count));
                    return RedirectToAction("StatisticPriceList");
                }
                ErrorNotification(_localizationService.GetResource("Admin.Common.UploadFile"));
                return RedirectToAction("StatisticPriceList");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("StatisticPriceList");
            }



        }
    }
}
