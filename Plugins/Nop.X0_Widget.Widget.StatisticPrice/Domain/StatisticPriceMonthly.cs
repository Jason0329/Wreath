using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.Widget.StatisticPrice.Domain
{
    public class StatisticPriceMonthly : BaseEntity
    {
        public StatisticPriceMonthly()
        {
            StatisticPriceMontlyList = new List<StatisticPriceMonthly>();
            MSRPList = new List<Decimal>();
            MonthlyAveragePriceList = new List<Decimal>();
            MonthlyGotruecarPriceList = new List<Decimal>();
            MonthList = new List<string>();
        }


        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Month { get; set; }
        public Decimal MSRP { get; set; }    
        public Decimal MonthlyAveragePrice { get; set; }
        public Decimal MonthlyGotruecarPrice { get; set; }    
        public bool IsPublished { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public IList<StatisticPriceMonthly> StatisticPriceMontlyList { get; set; }
        public IList<Decimal> MSRPList { get; set; }
        public IList<Decimal> MonthlyAveragePriceList { get; set; }
        public IList<Decimal> MonthlyGotruecarPriceList { get; set; }
        public IList<string> MonthList { get; set; }

        public string MSRP_Json;
        public string MonthlyAveragePrice_Json;
        public string MonthlyGotruecarPrice_Json;
        public string Month_Json;

    }
}
