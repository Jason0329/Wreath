using Nop.X0_Widget.LBSGame.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Data
{
    public class TaskMap : EntityTypeConfiguration<GameTask>
    {
        public TaskMap()
        {
            ToTable("LBSGame_Task");
            Property(m => m.TaskID);
            Property(m => m.GameID);
            Property(m => m.TaskType);
            Property(m => m.TaskTitle);
            Property(m => m.TaskContent);
            Property(m => m.TaskAnswer);
            Property(m => m.TaskEndStory);
        }
    }
}
