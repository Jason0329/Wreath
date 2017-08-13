using Nop.Plugin.Wreath.StatisticOrder.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Wreath.StatisticOrder.Data
{
    public class StatisticProductBuyMap: EntityTypeConfiguration<StatisticProductBuy>
    {
        public StatisticProductBuyMap()
        {
            ToTable("StatisticProductBuy");

            Property(m => m.ProductID);
            Property(m => m.Name);
            Property(m => m.NeedBuyQuantity);
            Property(m => m.HadBuyQuantity);
            Property(m => m.ProductBuyConfirm);
            Property(m => m.LastUpdateTime);
        }
      
    }
}
