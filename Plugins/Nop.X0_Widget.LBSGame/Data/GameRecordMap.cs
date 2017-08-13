using Nop.X0_Widget.LBSGame.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Data
{
    public class GameRecordMap : EntityTypeConfiguration<GameRecord>
    {
        public GameRecordMap()
        {
            ToTable("LBSGame_GameRecord");
            Property(m => m.TeamID);
            Property(m => m.TeamName);
            Property(m => m.StartTime);
            Property(m => m.EndTime);
            Property(m => m.GameID);
            Property(m => m.GameName);
            Property(m => m.GameData_Json);
            Property(m => m.TaskOrder);
            Property(m => m.LevelCurrently);
            Property(m => m.CustomerFBLoginID);
        }
    }
}
