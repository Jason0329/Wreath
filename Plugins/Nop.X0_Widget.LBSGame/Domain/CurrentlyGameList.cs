using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Domain
{
    public class CurrentlyGameList
    {
        public CurrentlyGameList()
        {
            _CurrentlyGameData = new List<CurrentlyGameData>();
        }

        public int GameID { get; set; }
        public string GameName { get; set; }
        public string GameStartStory { get; set; }
        public string GameEndStory { get; set; }
        public string GameStartMusic { get; set; }
        public string GmaeEndMusic { get; set; }
        public string TeamName { get; set; }
        public int LevelCurrently { get; set; }

        public List<CurrentlyGameData> _CurrentlyGameData;
        
    }

    public class CurrentlyGameData
    {
        public string StoryTitle { get; set; }
        public string StoryContent { get; set; }
        public string StoryMusic { get; set; }

        public string TaskType { get; set; }
        public string TaskTitle { get; set; }
        public string TaskContent { get; set; }
        public string TaskAnswer { get; set; }
        public string TaskEndStory { get; set; }

        public string LocationDescription { get; set; }
        public string LocationName { get; set; }
        public Decimal LocationX { get; set; }
        public Decimal LocationY { get; set; }
    }
}
