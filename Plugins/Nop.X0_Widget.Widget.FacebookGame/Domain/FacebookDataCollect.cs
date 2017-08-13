using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.Widget.FacebookGame.Domain
{
    public class FacebookDataCollect : BaseEntity
    {
        
        public string Name { get; set; }
        public string email { get; set; }
        public Decimal Price { get; set; }
        public bool IsPublished { get; set; }
    }
}
