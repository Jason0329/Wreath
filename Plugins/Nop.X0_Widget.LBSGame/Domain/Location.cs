using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Domain
{
    public class Location : BaseEntity
    {
        public Location()
        {
        }

        public int LocationID { get; set; }
        public Decimal LocationX { get; set; }
        public Decimal LocationY { get; set; }
        public string LocationName { get; set; }
        public string LocationType { get; set; }
        public string LocationDescription { get; set; }

    }
}
