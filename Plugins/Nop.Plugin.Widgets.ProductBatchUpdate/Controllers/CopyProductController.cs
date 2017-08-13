using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Plugin.Widgets.ProductBatchUpdate.Domain;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.ProductBatchUpdate.Controllers
{
    public class CopyProductController : BasePluginController
    {
        private ILogger _logger;
        private IPermissionService _permissionService;
        private IRepository<Product> _Product_Repository;
        private IRepository<Category> _catagoryRepository;
        private IRepository<Picture> _NopPicture;
        private IRepository<ProductPicture> _MapPicture;
        private ICopyProductService _copyProductService;
        private ICategoryService _categoryService;
        private ILocalizationService _localizationService;

        public CopyProductController(IRepository<Product> Product_Repository, 
            IPermissionService PermissionService, IRepository<Category> CatagoryRepository
            , IRepository<Picture> NopPicture, IRepository<ProductPicture> MapPicture 
            ,ICategoryService categoryService,ILogger logger,ICopyProductService copyProductService
            , ILocalizationService localizationService
            )
        {
            _localizationService = localizationService;
            _Product_Repository = Product_Repository;
            _permissionService = PermissionService;

            _catagoryRepository = CatagoryRepository;
            _NopPicture = NopPicture;
            _MapPicture = MapPicture;
            _logger = logger;


            _copyProductService = copyProductService;

            _categoryService = categoryService;
            
        }

        public ActionResult CopyProductList()
        {

            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return RedirectToAction("Login");

            var model = new UpdateProduct();

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            var categories = _categoryService.GetAllCategories(showHidden: false);
            foreach (var c in categories)
            {
                if (c.ParentCategoryId == 0)
                    model.AvailableCategories.Add(new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult GetCopyProductList(DataSourceRequest command, UpdateProduct Model)
        {
            IPagedList<Category> AllCategory = _categoryService.GetAllCategories(showHidden: true);

            List<Category> SelectedCategory = new List<Category>();
            List<Product> Product_Repository = Model.IsPublished ? _Product_Repository.Table.Where(s => s.Published == true).ToList() : _Product_Repository.Table.ToList();//
            List<UpdateProduct> ProductList = new List<UpdateProduct>();

            /***Select Category***/
            foreach (var c in AllCategory)
            {
                if (c.ParentCategoryId == Model.SearchCategoryId || Model.SearchCategoryId == 0)
                {
                    SelectedCategory.Add(c);
                }
            }


            /**********Filter Product Name and Product Category**************/
            for (int i = 0; i < Product_Repository.Count; i++)
            {
                if (Model.Name != null && !Product_Repository[i].Name.ToLower().Contains(Model.Name.ToLower())
                    || !CheckProductInCategory(SelectedCategory, Product_Repository[i]))
                {
                    Product_Repository.RemoveAt(i);
                    i--;
                }


            }
            /*******************************************/

            for (int i = 0; i < Product_Repository.Count; i++)
            {
                UpdateProduct __product = new UpdateProduct();

                int PageCount = (int)(i / command.PageSize) + 1;


                if (PageCount < command.Page) continue;
                if (PageCount > command.Page) break;


                __product.ProductID = Product_Repository[i].Id;
                __product.Name = Product_Repository[i].Name;
                __product.OldPrice = Product_Repository[i].OldPrice;
                __product.Price = Product_Repository[i].Price;
                __product.IsPublished = Product_Repository[i].Published;
                ProductList.Add(__product);
            }



            var gridModel = new DataSourceResult
            {
                Data = ProductList,
                Total = Product_Repository.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult UpdateCopyProductList(UpdateProduct _product)
        {

            var SelectProduct = _Product_Repository.GetById(_product.ProductID);

            SelectProduct.OldPrice = _product.OldPrice;
            SelectProduct.Price = _product.Price;
            SelectProduct.Published = _product.IsPublished;
            SelectProduct.UpdatedOnUtc = DateTime.Now;


            _copyProductService.CopyProduct(SelectProduct, _product.Name, _product.IsPublished);


            return new NullJsonResult();
        }

        // Update SEO Product information
        [HttpPost, ActionName("CopyProductList")]
        [FormValueRequired("UpdateProductSettingData")]
        public ActionResult UpdateProduct(Product model)
        {
            UpdateProductFromAnother(5286, 5642);

            SuccessNotification(String.Format(_localizationService.GetResource("成功更新")));
            _logger.InsertLog(Core.Domain.Logging.LogLevel.Information, "Update Product and Categories succeed");
            return RedirectToAction("ProductList");
        }

        public void UpdateProductFromAnother(int RefProduct, int UpdateProduct)
        {
            Product Ref_Product = _Product_Repository.GetById(RefProduct);
            Product Update_Product = _Product_Repository.GetById(UpdateProduct);

            #region
           
            #endregion
            foreach (var Pic in Ref_Product.ProductPictures)
            {
                Update_Product.ProductPictures.Add(Pic);
            }

            _Product_Repository.Update(Update_Product);
            

        }

    


        //Check Product if in category
        bool CheckProductInCategory(List<Category> CheckingCategory, Product CheckingProduct)
        {
            foreach (var _productCategory in CheckingProduct.ProductCategories)
            {
                foreach (var _category in CheckingCategory)
                {
                    if (_productCategory.CategoryId == _category.Id)
                        return true;
                }
            }

            return false;
        }
    }
}
