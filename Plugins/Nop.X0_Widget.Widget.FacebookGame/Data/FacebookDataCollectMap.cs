using Nop.X0_Widget.Widget.FacebookGame.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.Widget.FacebookGame.Data
{
    public class FacebookDataCollectMap : EntityTypeConfiguration<FacebookDataCollect>
    {
        public FacebookDataCollectMap()
        {
            //ToTable("UpdateProduct");
            //Property(m => m.id);
            Property(m => m.Name);
            Property(m => m.email);
            Property(m => m.Price);
            Property(m => m.IsPublished);
        }

    }
}
