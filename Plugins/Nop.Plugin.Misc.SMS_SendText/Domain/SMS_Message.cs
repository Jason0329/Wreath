using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.SMS_SendText.Domain
{
    public class SMS_Message : BaseEntity
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageSendTime { get; set; }
        public DateTime MessageArrangeTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
