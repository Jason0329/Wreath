using Nop.Core.Data;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Wreath.StatisticOrder.Data;
using Nop.Plugin.Wreath.StatisticOrder.Domain;
using Nop.Services.Localization;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Wreath.StatisticOrder.Controllers
{
    public class StatisticProductBuyController : BasePluginController
    {
        private IRepository<StatisticProductBuy> _StatisticProductBuyRepository;
        private IRepository<Order> _OrderRepository;
        private ILocalizationService _localizationService;

        public StatisticProductBuyController(IRepository<StatisticProductBuy> StatisticProductBuyRepository,IRepository<Order>OrderRepository, ILocalizationService localizationService)
        {
            _StatisticProductBuyRepository = StatisticProductBuyRepository;
            _OrderRepository = OrderRepository;
            _localizationService = localizationService;
        }

        public ActionResult WreathBuyList()
        {
          
            return View();
        }

        [HttpPost]
        public ActionResult GetStatisticProductBuy(DataSourceRequest command, StatisticProductBuy Model)
        {
            List<StatisticProductBuy> NeedBuyProducts = Model.ProductBuyConfirm ? _StatisticProductBuyRepository.Table.Where(s => s.ProductBuyConfirm == false && s.LastUpdateTime.Month==DateTime.Now.Month).ToList() : _StatisticProductBuyRepository.Table.ToList();//_StatisticProductBuyRepository.Table.ToList();


            var gridModel = new DataSourceResult
            {
                Data = NeedBuyProducts,
                Total = NeedBuyProducts.Count
            };

            return Json(gridModel);
        }

       
        [HttpPost]
        public ActionResult UpdateStatisticProduct(StatisticProductBuy ProductBuy)
        {
            var _ProductBuy = _StatisticProductBuyRepository.GetById(ProductBuy.Id);

            _ProductBuy.HadBuyQuantity = ProductBuy.HadBuyQuantity;
            _ProductBuy.NeedBuyQuantity = ProductBuy.NeedBuyQuantity;
            _ProductBuy.ProductBuyConfirm = ProductBuy.ProductBuyConfirm;
            _ProductBuy.LastUpdateTime = DateTime.Now;


            _StatisticProductBuyRepository.Update(_ProductBuy);

            return new NullJsonResult();
        }

    

       

        [HttpPost]
        public ActionResult DeleteStatisticProductBuy(DataSourceRequest command, int ID)
        {
            var Sender = _StatisticProductBuyRepository.GetById(ID);
            _StatisticProductBuyRepository.Delete(Sender);

            return new NullJsonResult();
        }

        // Update SEO Product information
        [HttpPost, ActionName("StatisticProdcutBuy")]
        [FormValueRequired("UpdateProductStatisticData")]
        public ActionResult UpdateProduct(StatisticProductBuy model)
        {
            List<Order> OrderList = _OrderRepository.Table.Where(s => s.CreatedOnUtc.Month == DateTime.Now.Month).ToList();
            List<StatisticProductBuy> StatisticProductBuyList = _StatisticProductBuyRepository.Table.ToList();
            SortedList<int, int> CollectionOrderItemCount = new SortedList<int, int>();

            foreach (var Orders in OrderList)
            {
                foreach (var _OrderItem in Orders.OrderItems)
                {
                   if(!CollectionOrderItemCount.ContainsKey(_OrderItem.ProductId))
                   {
                       CollectionOrderItemCount.Add(_OrderItem.ProductId,1);
                   }
                   else
                   {
                       int key = CollectionOrderItemCount.IndexOfKey(_OrderItem.ProductId);
                       CollectionOrderItemCount[key] = CollectionOrderItemCount[key]++;
                   }
                }
            }//Collection All order item to calcualte


            foreach (var CountProduct in CollectionOrderItemCount)
            {
                
            }

           

            SuccessNotification(String.Format(_localizationService.GetResource("成功更新")));
            return RedirectToAction("WreathBuyList");
        }
    }
}
