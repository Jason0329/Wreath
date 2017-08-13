using Nop.Plugin.Misc.SMS_SendText.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.SMS_SendText.Data
{
    public class SMS_MessageMap : EntityTypeConfiguration<SMS_Message>
    {
        public SMS_MessageMap()
        {
            ToTable("SMS_Message");

            Property(m => m.OrderID);
            Property(m => m.CustomerName);
            Property(m => m.PhoneNumber);
            Property(m => m.MessageText);
            Property(m => m.MessageSendTime);
            Property(m => m.MessageArrangeTime);
            Property(m => m.CreateTime);
            Property(m => m.UpdateTime);
        }
    }
}
