using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Wreath.StatisticOrder.Domain
{
    public class StatisticProductBuy : BaseEntity
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int NeedBuyQuantity { get; set; }
        public int HadBuyQuantity { get; set; }
        public bool ProductBuyConfirm { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public StatisticProductBuy()
        {
        }

        public StatisticProductBuy(int productID,string name, int needBuy=1)
        {
            this.ProductID = productID;
            this.Name = name;
            this.NeedBuyQuantity = needBuy;
            this.HadBuyQuantity = 0;
            this.ProductBuyConfirm = false;
            this.LastUpdateTime = DateTime.Now;
        }
    }
}
