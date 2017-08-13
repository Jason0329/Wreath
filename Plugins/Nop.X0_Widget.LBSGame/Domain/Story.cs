using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Domain
{
    public class Story : BaseEntity
    {
        public Story()
        {
        }

        public int StoryID { get; set; }
        public int GameID { get; set; }
        public int StoryOrder { get; set; }
        public string StoryTitle { get; set; }
        public string StoryContent { get; set; }
        public string StoryMusic { get; set; }
    }
}
