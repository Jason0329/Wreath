using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Domain
{
    public class GameRecord : BaseEntity
    {
        public GameRecord()
        {
        }

        public int TeamID { get; set; }
        public string  TeamName{ get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int GameID { get; set; }
        public string GameName { get; set; }
        public string GameData_Json { get; set; }
        public string TaskOrder { get; set; }
        public string LevelCurrently { get; set; }
        public int CustomerFBLoginID { get; set; }
    }
}
