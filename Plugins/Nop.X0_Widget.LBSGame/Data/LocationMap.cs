using Nop.X0_Widget.LBSGame.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Data
{
    public class LocationMap: EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            ToTable("LBSGame_Location");
            Property(m => m.LocationID);
            Property(m => m.LocationX);
            Property(m => m.LocationY);
            Property(m => m.LocationName);
            Property(m => m.LocationType);
            Property(m => m.LocationDescription);
        }
    }
}
