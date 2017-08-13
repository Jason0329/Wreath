using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Data;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nop.X0_Widget.Widget.AutoBackupDatabase.Service
{
    public class AutoBackup_Database : MaintenanceService
    {

        public AutoBackup_Database(IDataProvider dataProvider, IDbContext dbContext,
           CommonSettings commonSettings, HttpContextBase httpContext)
            : base(dataProvider, dbContext,
                commonSettings, httpContext)
        {

        }

         protected override string GetBackupDirectoryPath()
         {
             return HttpRuntime.AppDomainAppPath + "Administration\\db_backups\\";
         }
      
    }
}
