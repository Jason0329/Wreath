using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Domain
{
    public class TaskLocationMapping: BaseEntity
    {
        public TaskLocationMapping()
        {
        }

        public int TaskID { get; set; }
        public int LocationID { get; set; }
    }
}
