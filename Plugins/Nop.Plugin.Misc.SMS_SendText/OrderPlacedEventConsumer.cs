using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.SMS_SendText.Domain;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.SMS_SendText
{
    public class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
    {
        private IOrderService _orderService;
        private IStoreContext _storeContext;
        private IRepository<SMS_Message> _SMS_SenderData;
        private IRepository<Order> _OrderListRepository;
        private ILogger _logger;
        public OrderPlacedEventConsumer(IRepository<SMS_Message> SMS_SenderData
            , IRepository<Order> OrderListRepository, IOrderService orderService, IStoreContext storeContext,ILogger logger)
        {
            this._orderService = orderService;
            this._storeContext = storeContext;
            this._SMS_SenderData = SMS_SenderData;
            this._OrderListRepository = OrderListRepository;
            this._logger = logger;
        }

        public void HandleEvent(OrderPlacedEvent eventMessage)
        {
            SendTextSMS SendTextPrepare = new SendTextSMS();
            SMS_Message MessageRecord = new SMS_Message();

            string PrepareSendForPrice = SendTextPrepare.PrepareSendText(eventMessage.Order);

            eventMessage.Order.OrderStatus = OrderStatus.Complete;
            _OrderListRepository.Update(eventMessage.Order);

            if (!PrepareSendForPrice.Contains("官方售價")) return;

            try
            {
                int SendMessgeReturn = SendTextPrepare.SendMessage(PrepareSendForPrice);

                MessageRecord.OrderID = eventMessage.Order.Id;
                MessageRecord.CustomerName = eventMessage.Order.Customer.BillingAddress.FirstName + eventMessage.Order.Customer.BillingAddress.LastName;
                MessageRecord.PhoneNumber = eventMessage.Order.Customer.BillingAddress.PhoneNumber;
                MessageRecord.MessageText = PrepareSendForPrice.Split(new string[1]{"smbody="},StringSplitOptions.None)[1].Split('&')[0];
                MessageRecord.CreateTime = DateTime.Now;
                MessageRecord.MessageArrangeTime = DateTime.Now;
                MessageRecord.MessageSendTime = DateTime.Now;
                MessageRecord.UpdateTime = DateTime.Now;

                _SMS_SenderData.Insert(MessageRecord);
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Information, "Send Message Succeed");
            }
            catch (Exception e)
            {
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, "Send Message Failed: "+e.Message + " "+e.InnerException.Message);
            }

        }
    }
}
