using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Tasks;
using Nop.Core.Plugins;
using Nop.Data;
using Nop.Services.Common;
using Nop.Services.Logging;
using Nop.Services.Tasks;
using Nop.X0_Widget.Widget.AutoBackupDatabase.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nop.X0_Widget.Widget.AutoBackupDatabase
{
    public class AutoBackupDatabasePlugin : BasePlugin, ITask
    {
        private ILogger _logger;
        private IScheduleTaskService _scheduleTaskService;
        private IMaintenanceService _maintenanceService;

        public AutoBackupDatabasePlugin(IScheduleTaskService scheduleTaskService, ILogger logger, IMaintenanceService maintenanceService,
            IDataProvider dataProvider, IDbContext dbContext,
           CommonSettings commonSettings, HttpContextBase httpContext)
        {
            _scheduleTaskService = scheduleTaskService;
            _logger = logger;
            _maintenanceService = new AutoBackup_Database(dataProvider, dbContext,
                commonSettings, httpContext);
        }

        public override void Install()
        {
            _scheduleTaskService.InsertTask(new ScheduleTask()
            {
                Enabled = true,
                Name = "Backup Task",
                Seconds = 3600,
                StopOnError = false,
                Type = "Nop.X0_Widget.Widget.AutoBackupDatabase.AutoBackupDatabasePlugin, Nop.X0_Widget.Widget.AutoBackupDatabase"
            });

            base.Install();
        }

        public override void Uninstall()
        {
            ScheduleTask task = _scheduleTaskService.GetTaskByType("Nop.X0_Widget.Widget.AutoBackupDatabase.AutoBackupDatabasePlugin, Nop.X0_Widget.Widget.AutoBackupDatabase");

            if (task != null)
            {
                _scheduleTaskService.DeleteTask(task);
            }

            base.Uninstall();
        }

        public void Execute()
        {

            List<FileInfo> AllBackupFile = _maintenanceService.GetAllBackupFiles().OrderBy(m => m.CreationTimeUtc).ToList();

            if (DateTime.UtcNow.Hour != 20) return;

            try
            {

                _maintenanceService.BackupDatabase();

                if (AllBackupFile.Count >= 5)
                {
                    var backupPath = AllBackupFile[0].FullName;
                    System.IO.File.Delete(backupPath);
                }


                _logger.InsertLog(Core.Domain.Logging.LogLevel.Information, "Backup Database Succeed");
            }
            catch (Exception exc)
            {
               _logger.InsertLog(Core.Domain.Logging.LogLevel.Error,"failed to backup database:"+exc.Message+ " "+exc.InnerException.Message);
            }
           
        }
    }
}
