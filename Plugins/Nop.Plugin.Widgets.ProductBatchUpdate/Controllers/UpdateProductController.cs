using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Plugin.Widgets.ProductBatchUpdate.Data;
using Nop.Plugin.Widgets.ProductBatchUpdate.Domain;
using Nop.Plugin.Widgets.ProductBatchUpdate.Service;
using Nop.Services.Catalog;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.ProductBatchUpdate.Controllers
{
    public class UpdateProductController : BasePluginController
    {
        private IRepository<Product> _Product_Repository;
        private IRepository<UpdateProduct> _UpdateProduct_Repository;
        private IPermissionService _permissionService;
        private IImportManager _importManager;
        private IExportManager _exportManager;
        private ILocalizationService _localizationService;
        private IProductService _productService;

        ImportExportManagement ImportExportData;
        private IRepository<PredefinedProductAttributeValue> _predefinedProductAttrubuteValueRepository;
        private IRepository<ProductAttribute> _productAttibuteRepository;
        private IRepository<SpecificationAttributeOption> _speificationAttributeOptionRepository;
        private IRepository<SpecificationAttribute> _specificationAttributeRepository;
        private IRepository<Category> _catagoryRepository;
        private IRepository<Picture> _NopPicture;
        private IRepository<ProductPicture> _MapPicture;
        private IRepository<BlogPost> _BlogPostIRepository;
        private ICategoryService _categoryService;
        private ICacheManager _cacheManager;
        private ILogger _logger;
        private ICopyProductService _copyProductService;

        public UpdateProductController(IRepository<Product> Product_Repository, IRepository<UpdateProduct> UpdateProductProduct_Repository, 
            IPermissionService PermissionService, IImportManager importManager,IExportManager exportManager
            , ILocalizationService localizationService, IProductService productService,
           IRepository<PredefinedProductAttributeValue> predefinedProductAttributeValueRepository,
           IRepository<ProductAttribute> productAttributeRepository, IRepository<SpecificationAttributeOption> SpeificationAttributeOptionRepository
            , IRepository<SpecificationAttribute> SpecificationAttributeRepository, IRepository<Category> CatagoryRepository
            , IRepository<Picture> NopPicture, IRepository<ProductPicture> MapPicture , IRepository<BlogPost> BlogPostIRepository
            ,ICategoryService categoryService,ICacheManager cacheManager,ILogger logger,ICopyProductService copyProductService
            )
        {
            _UpdateProduct_Repository = UpdateProductProduct_Repository;
            _Product_Repository = Product_Repository;
            _permissionService = PermissionService;
            _importManager = importManager;
            _exportManager = exportManager;
            _localizationService = localizationService;
            _productService = productService;
            _predefinedProductAttrubuteValueRepository = predefinedProductAttributeValueRepository;
            _productAttibuteRepository = productAttributeRepository;


            _specificationAttributeRepository = SpecificationAttributeRepository;
            _speificationAttributeOptionRepository = SpeificationAttributeOptionRepository;

            _catagoryRepository = CatagoryRepository;
            _NopPicture = NopPicture;
            _MapPicture = MapPicture;
            _logger = logger;

            _BlogPostIRepository = BlogPostIRepository;

            _copyProductService = copyProductService;

            this._categoryService = categoryService;
            this._cacheManager = cacheManager;
            
            ImportExportData = new ImportExportManagement();
        }

        public ActionResult ProductList()
        {

            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return RedirectToAction("Login");

            var model = new UpdateProduct();

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            var categories = _categoryService.GetAllCategories(showHidden: false);
            foreach (var c in categories)
            {
                if(c.ParentCategoryId==0)
                    model.AvailableCategories.Add(new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult GetProductList(DataSourceRequest command , UpdateProduct Model)
        {
            IPagedList<Category> AllCategory = _categoryService.GetAllCategories(showHidden: true);

            List<Category> SelectedCategory = new List<Category>();
            List<Product> Product_Repository = Model.IsPublished ? _Product_Repository.Table.Where(s => s.Published == true).ToList() : _Product_Repository.Table.ToList();//
            List<UpdateProduct> ProductList = new List<UpdateProduct>();

            /***Select Category***/
            foreach (var c in AllCategory)
            {
                if (c.ParentCategoryId == Model.SearchCategoryId || Model.SearchCategoryId==0)
                {
                    SelectedCategory.Add(c);
                }
            }


            /**********Filter Product Name and Product Category**************/
            for (int i = 0; i < Product_Repository.Count; i++)
            {
                if (Model.Name != null && !Product_Repository[i].Name.ToLower().Contains(Model.Name.ToLower())
                    ||!CheckProductInCategory(SelectedCategory,Product_Repository[i]))
                {
                    Product_Repository.RemoveAt(i);
                    i--;
                }
               

            }
            /*******************************************/

            for (int i = 0; i < Product_Repository.Count; i++)
            {
                UpdateProduct __product = new UpdateProduct();

                int PageCount =(int)(i / command.PageSize) + 1;

                
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
        public ActionResult UpdateUpdateProduct(UpdateProduct _product)
        {

            var SelectProduct = _Product_Repository.GetById(_product.ProductID);
            
            SelectProduct.Name = _product.Name;
            SelectProduct.OldPrice = _product.OldPrice;
            SelectProduct.Price = _product.Price;
            SelectProduct.Published = _product.IsPublished;
            SelectProduct.UpdatedOnUtc = DateTime.Now;
            
         
            _Product_Repository.Update(SelectProduct);


            return new NullJsonResult();
        }


        [HttpPost]
        public ActionResult DeleteUpdateProduct(DataSourceRequest command, int ProductID)
        {

            var Sender = _Product_Repository.GetById(ProductID);
            Sender.Published = false;
            Sender.UpdatedOnUtc = DateTime.Now;

            _Product_Repository.Update(Sender);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult ImportCsv(FormCollection form)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return RedirectToAction("ProductList");

         

            try
            {
                var file = Request.Files["importcsvfile"];
                if (file != null && file.ContentLength > 0)
                {
                    int count = ImportExportData.Import_CSV<Product>(file.InputStream, _Product_Repository);
                    SuccessNotification(String.Format(_localizationService.GetResource("Admin.Promotions.NewsLetterSubscriptions.ImportEmailsSuccess"), count));
                    return RedirectToAction("ProductList");
                }
                ErrorNotification(_localizationService.GetResource("Admin.Common.UploadFile"));
                return RedirectToAction("ProductList");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("ProductList");
            }

            

        }

        [HttpPost]
        public ActionResult ImportSpecificationCsv(FormCollection form)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return RedirectToAction("ProductList");

            List<SpecificationAttribute> _SpecificationAttribute =  _specificationAttributeRepository.Table.ToList();
            List<SpecificationAttributeOption> _SpecificationAttributeOption = _speificationAttributeOptionRepository.Table.ToList();

            try
            {
                var file = Request.Files["importcsvfile1"];
                if (file != null && file.ContentLength > 0)
                {
                    int count = ImportExportData.Import_CSV_ProductSpecifications(file.InputStream, _Product_Repository, ref _SpecificationAttribute, ref _SpecificationAttributeOption);
                    SuccessNotification(String.Format(_localizationService.GetResource("Admin.Promotions.NewsLetterSubscriptions.ImportEmailsSuccess"), count));
                    return RedirectToAction("ProductList");
                }
                ErrorNotification(_localizationService.GetResource("Admin.Common.UploadFile"));
                return RedirectToAction("ProductList");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("ProductList");
            }


        }

        [HttpPost]
        public ActionResult ImportAttributeCsv(FormCollection form)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return RedirectToAction("ProductList");

            List<SpecificationAttribute> _SpecificationAttribute = _specificationAttributeRepository.Table.ToList();
            List<SpecificationAttributeOption> _SpecificationAttributeOption = _speificationAttributeOptionRepository.Table.ToList();

            try
            {
                var file = Request.Files["importcsvfile1"];
                if (file != null && file.ContentLength > 0)
                {
                    int count = ImportExportData.Import_BenzCSV_Attribute(file.InputStream, _Product_Repository,_productAttibuteRepository);
                    SuccessNotification(String.Format(_localizationService.GetResource("Import Benz Attribute Succeed "+count), count));
                    return RedirectToAction("ProductList");
                }
                ErrorNotification(_localizationService.GetResource("Admin.Common.UploadFile"));
                return RedirectToAction("ProductList");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("ProductList");
            }



        }

        [HttpPost, ActionName("ProductList")]
        [FormValueRequired("exportcsv")]
        public ActionResult ExportCsv(Product model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return RedirectToAction("ProductList");
            
            var products = new List<Product>();
            
            products.AddRange(_Product_Repository.Table.ToArray());


            string fileName = String.Format("Update_Product_Format_{0}_{1}.txt", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), CommonHelper.GenerateRandomDigitCode(4));
            return File(Encoding.UTF8.GetBytes(ImportExportData.Export_CSV(products)), MimeTypes.TextCsv, fileName);

        }

        [HttpPost]
        public ActionResult ExportCsvSelected(string selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return RedirectToAction("ProductList");

            var products = new List<Product>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                products.AddRange(_productService.GetProductsByIds(ids));
            }

            string fileName = String.Format("Update_Product_Format_{0}_{1}.csv", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), CommonHelper.GenerateRandomDigitCode(4));

            

            return File(Encoding.UTF8.GetBytes(ImportExportData.Export_CSV(products)), MimeTypes.TextCsv, fileName);

   
        }


        // Update SEO Product information
        [HttpPost, ActionName("ProductList")]
        [FormValueRequired("UpdateProductSettingData")]
        public ActionResult UpdateProduct(Product model)
        {
            #region Update Prodcut Information
            DateTime datetime2week = DateTime.UtcNow.AddDays(-28);
            List<Product> Product_Repository = _Product_Repository.Table.Where(m => m.Published==true).ToList();

            foreach (Product Product in Product_Repository)
            {

                Product.MetaKeywords = Product.Name + "-價格";
                Product.MetaTitle = Product.Name + "即時簡訊價格/售價查詢-GoTrueCar";
                Product.MetaDescription = "GoTrueCar查詢" + Product.Name + "價格，即時簡訊查詢成交價，讓您掌握最即時的價格訊息，幫您省去詢價、報價、比較的時間和精力";
                Product.ShortDescription = "GoTrueCar查詢" + Product.Name + "價格，即時簡訊查詢成交價，讓您掌握最即時的價格訊息，幫您省去詢價、報價、比較的時間和精力";
                Product.UpdatedOnUtc = DateTime.Now;

                try
                {
                    int CountPic = 1;
                    foreach (var pic in Product.ProductPictures)
                    {
                        pic.Picture.AltAttribute = Product.Name + "價格即時簡訊查詢-商品-圖片" + CountPic;
                        pic.Picture.TitleAttribute = Product.Name + "價格即時簡訊查詢-商品-圖片" + CountPic;
                        pic.Picture.SeoFilename = Product.Name.Replace("(", "-").Replace(")", "").Replace("/", "-") + "價格即時簡訊查詢-商品-圖片" + CountPic;
                        CountPic++;
                    }
                }
                catch (Exception ee)
                {
                    _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, "Update picuture error : " + Product.Id + " " + ee.Message + "&" + ee.InnerException.Message);
                }

                try
                {
                    
                    _Product_Repository.Update(Product);
                }
                catch (Exception ee)
                {
                    _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, "Update Product error : " + Product.Id + " " + ee.Message + "&" + ee.InnerException.Message);

                }

            }

            #endregion

            #region Update Categories Information
            try
            {
                List<Category> Categories = _catagoryRepository.Table.ToList();

                foreach (var _Category in Categories)
                {
                    _Category.PageSize = 20;
                    _Category.MetaDescription = "GoTrueCar即時簡訊查詢" + _Category.Name + "成交價，讓您掌握最即時的價格訊息，幫您省去詢價、報價、比較的時間和精力";
                    _Category.MetaTitle = _Category.Name + "價格/售價即時簡訊查詢-GoTrueCar";
                    _Category.MetaKeywords = _Category.Name + "價格";
                    _Category.AllowCustomersToSelectPageSize = false;
                    _Category.Description = "請選擇" + _Category.Name + "進行簡訊價格查詢";
                    _Category.UpdatedOnUtc = DateTime.Now;
                    if (_Category.ParentCategoryId == 0) _Category.IncludeInTopMenu = true;


                    _catagoryRepository.Update(_Category);


                    Picture pic = _NopPicture.GetById(_Category.PictureId);

                    if (pic != null)
                    {
                        pic.AltAttribute = _Category.Name + "價格即時簡訊查詢圖片-類別";
                        pic.SeoFilename = _Category.Name + "價格查詢-類別";
                        pic.TitleAttribute = _Category.Name + "價格即時簡訊查詢圖片-類別";
                        _NopPicture.Update(pic);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, "Update categories failed:"+e.Message+" "+e.InnerException.Message);
            }

            #endregion

            SuccessNotification(String.Format(_localizationService.GetResource("成功更新商品文案")));
            _logger.InsertLog(Core.Domain.Logging.LogLevel.Information, "Update Product and Categories succeed");
            return RedirectToAction("ProductList");
        }

       

        //Check Product if in category
        bool CheckProductInCategory(List<Category> CheckingCategory ,Product CheckingProduct)
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
