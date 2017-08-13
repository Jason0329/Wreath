using Nop.X0_Widget.LBSGame.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Data
{
    public class GameMap : EntityTypeConfiguration<Game>
    {
        public GameMap()
        {
            ToTable("LBSGame_Game");
            Property(m => m.GameID);
            Property(m => m.GameName);
            Property(m => m.GameStartStory);
            Property(m => m.GameEndStory);
            Property(m => m.GameStartMusic);
            Property(m => m.GmaeEndMusic);
        }
    }
}
