using Nop.X0_Widget.LBSGame.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSStory.Data
{
    public class StoryMap : EntityTypeConfiguration<Story>
    {
        public StoryMap()
        {
            ToTable("LBSGame_Story");
            Property(m => m.StoryID);
            Property(m => m.GameID);
            Property(m => m.StoryOrder);
            Property(m => m.StoryTitle);
            Property(m => m.StoryContent);
            Property(m => m.StoryMusic);
        }
    }
}
