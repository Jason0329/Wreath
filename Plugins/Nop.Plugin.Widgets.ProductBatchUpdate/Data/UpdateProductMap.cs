using Nop.Plugin.Widgets.ProductBatchUpdate.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductBatchUpdate.Data
{
    public class UpdateProductMap : EntityTypeConfiguration<UpdateProduct>
    {

        public UpdateProductMap()
        {
            //ToTable("UpdateProduct");
            Property(m => m.ProductID);
            Property(m => m.Name);
            Property(m => m.OldPrice);
            Property(m => m.Price);
            Property(m => m.IsPublished);
        }

    }
}
