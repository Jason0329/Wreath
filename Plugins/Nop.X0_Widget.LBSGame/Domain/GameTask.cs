using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Domain
{
    public class GameTask : BaseEntity
    {
        public GameTask()
        {
        }

        public int TaskID { get; set; }
        public int GameID { get; set; }
        public string TaskType { get; set; }
        public string TaskTitle { get; set; }
        public string TaskContent { get; set; }
        public string TaskAnswer { get; set; }
        public string TaskEndStory { get; set; }

    }
}
