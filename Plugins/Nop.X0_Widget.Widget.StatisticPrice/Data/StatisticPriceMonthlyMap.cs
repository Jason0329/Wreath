using Nop.X0_Widget.Widget.StatisticPrice.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.Widget.StatisticPrice.Data
{
    public class StatisticPriceMonthlyMap : EntityTypeConfiguration<StatisticPriceMonthly>
    {
        public StatisticPriceMonthlyMap()
        {
            ToTable("StatisticPriceMonthly");
            Property(m => m.ProductID);
            Property(m => m.Name);
            Property(m => m.Month);
            Property(m => m.MSRP);
            Property(m => m.MonthlyAveragePrice);
            Property(m => m.MonthlyGotruecarPrice);
            Property(m => m.IsPublished);
            Property(m => m.CreateTime);
            Property(m => m.UpdateTime);
        }
    }
}
