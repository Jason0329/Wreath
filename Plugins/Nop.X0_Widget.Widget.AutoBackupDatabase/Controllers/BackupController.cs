using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.Widget.AutoBackupDatabase.Controllers
{
    public class BackupController : BaseController
    {
        private IPermissionService _PermissionService;
        private IWebHelper _webHelper;
        private IStoreContext _storeContext;
        private IMaintenanceService _maintenanceService;

        public BackupController(IPermissionService PermissionService
            , IWebHelper webHelper, IStoreContext storeContext, IMaintenanceService maintenanceService)
        {
            _PermissionService = PermissionService;
            _webHelper = webHelper;
            _storeContext = storeContext;
            _maintenanceService = maintenanceService;
          
        }
    }
}
