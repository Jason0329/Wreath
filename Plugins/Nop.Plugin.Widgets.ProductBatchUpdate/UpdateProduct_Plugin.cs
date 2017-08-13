using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Plugins;
using Nop.Plugin.Widgets.ProductBatchUpdate.Data;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Widgets.ProductBatchUpdate
{
    public class UpdateProduct_Plugin : BasePlugin, IAdminMenuPlugin
    {
        private UpdateProductObjectContext _UpdateProductContext;
        private IRepository<Product> _ProductRepo;
        public UpdateProduct_Plugin(UpdateProductObjectContext UpdateProductContext, IRepository<Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
            _UpdateProductContext = UpdateProductContext;
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            
            var menuItem = new SiteMapNode()
            {
                SystemName = "ProductBatchUpdate",
                Title = "Product Batch Update",
                ControllerName = "UpdateProduct",
                ActionName = "ProductList",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

            var menuItemCopuyProduct = new SiteMapNode()
            {
                SystemName = "CopyProduct",
                Title = "Copy Product",
                ControllerName = "CopyProduct",
                ActionName = "CopyProductList",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

           

           
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
            {
                pluginNode.ChildNodes.Add(menuItem);
                pluginNode.ChildNodes.Add(menuItemCopuyProduct);
               
            }
            else
            {
                rootNode.ChildNodes.Add(menuItem);
                rootNode.ChildNodes.Add(menuItemCopuyProduct);
               
            }
        }

        public override void Install()
        {
            _UpdateProductContext.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            _UpdateProductContext.Uninstall();
            base.Uninstall();
        }
    }
}
