using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Domain
{
    public class Game : BaseEntity
    {
        public Game()
        {
        }

        public int GameID { get; set; }
        public string GameName { get; set; }
        public string GameStartStory { get; set; }
        public string GameEndStory { get; set; }
        public string GameStartMusic { get; set; }
        public string GmaeEndMusic { get; set; }
    }
}
