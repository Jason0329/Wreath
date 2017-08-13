﻿using Nop.X0_Widget.LBSGame.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Data
{
    public class TaskLocationMappingMap : EntityTypeConfiguration<TaskLocationMapping>
    {
        public TaskLocationMappingMap()
        {
            ToTable("LBSGame_TaskLocationMapping");
            Property(m => m.LocationID);
            Property(m => m.TaskID);

        }
    }
}