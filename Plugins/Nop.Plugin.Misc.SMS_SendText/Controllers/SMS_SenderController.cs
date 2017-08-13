using Nop.Core.Data;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.SMS_SendText.Domain;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.SMS_SendText.Controllers
{
    public class SMS_SenderController :BasePluginController
    {
        private IRepository<SMS_Message> _SMS_SenderRepository;
        private IRepository<Order> _OrderRepository;
        private ILogger _logger;
        //private IRepository<IOrderService>

        public SMS_SenderController(IRepository<SMS_Message> SMS_SenderRepository,IRepository<Order> OrderRepository,ILogger logger )
        {
            _SMS_SenderRepository = SMS_SenderRepository;
            _OrderRepository = OrderRepository;
            _logger = logger;
        }

        public ActionResult Thanks()
        {
            SendTextSMS SMSFunction = new SendTextSMS();
            OrderNote Note = new OrderNote();
            string id = Request.QueryString["id"];
            string BuyOrRent = Request.QueryString["buy"];
            int OrderID =-1;

            try
            {
                id = SMSFunction.DecryptString(id.Replace("99999","+"));
                OrderID = int.Parse(id);
                Order CustomerOrder = _OrderRepository.GetById(OrderID);

                Note.Note = BuyOrRent;
                Note.CreatedOnUtc = DateTime.Now;

                CustomerOrder.PaymentMethodSystemName = BuyOrRent;
                CustomerOrder.OrderNotes.Add(Note);
                
                _OrderRepository.Update(CustomerOrder);
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Information, "Order Status Update: " + OrderID );
            }
            catch (Exception ee)
            {
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, "Order Update Failed: " + OrderID + " " + ee.Message);
            }
            
            return View();
        }

        public ActionResult BuyOrRent()
        {
            string id = Request.QueryString["id"];
            SendTextSMS SMSFunction = new SendTextSMS();
            int OrderID = -1;

            try
            {
                id = SMSFunction.DecryptString(id.Replace("99999", "+"));
                OrderID = int.Parse(id);
                
                Order CustomerOrder = _OrderRepository.GetById(OrderID);
                CustomerOrder.OrderStatus = OrderStatus.Pending;
                _OrderRepository.Update(CustomerOrder);
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Information, "Order Status Change: " + OrderID);
            }
            catch (Exception ee)
            {
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, "Order Update Failed: " + OrderID + " " + ee.Message);
            }


            return View();
        }

        public ActionResult Ask()
        {
            string id = Request.QueryString["id"];
            SendTextSMS SMSFunction = new SendTextSMS();
            int OrderID = -1;

            try
            {
                id = SMSFunction.DecryptString(id.Replace("99999", "+"));
                OrderID = int.Parse(id);

                _logger.InsertLog(Core.Domain.Logging.LogLevel.Information, "Order Ask Required: " + OrderID);
            }
            catch (Exception ee)
            {
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, "Order Update Failed: " + OrderID + " " + ee.Message);
            }


            return View();
        }

        public ActionResult Considering()
        {
            return View();
        }
      
        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult List()
        {

            //var SMSList = _SMS_SenderRepository.Table.ToList();

            //var gridModel = new DataSourceResult
            //{
            //    Data = SMSList,
            //    Total = SMSList.Count
            //};

            //return View(gridModel);

            var Message = new SMS_Message();

            return View(Message);
            
        }

        [HttpPost]
        public ActionResult GetSMS_Sender(DataSourceRequest command)
        {
            var SMSList = _SMS_SenderRepository.Table.OrderByDescending(m => m.MessageSendTime).ToList();

            var gridModel = new DataSourceResult
            {
                Data = SMSList,
                Total = SMSList.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult UpdateSMS_Sender(SMS_Message SenderUpdate)
        {
            //var Sender = _SMS_SenderRepository.GetById(SenderUpdate.O);

            //Sender.CustomerName = SenderUpdate.CustomerName;
            //Sender.Need_Buy_Car = SenderUpdate.Need_Buy_Car;
            //Sender.OrderID = SenderUpdate.OrderID;
            //Sender.PhoneNumber = SenderUpdate.PhoneNumber;
            //Sender.SendText = SenderUpdate.SendText;

            //_SMS_SenderRepository.Update(Sender);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult DeleteSMS_Sender(DataSourceRequest command, int SMS_ID)
        {
            var Sender = _SMS_SenderRepository.GetById(SMS_ID);
            _SMS_SenderRepository.Delete(Sender);

            return new NullJsonResult();
        }


    }
}
