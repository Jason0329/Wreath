using Nop.Core.Data;
using Nop.Core.Domain.Blogs;
using Nop.Services.Blogs;
using Nop.Services.Logging;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Configuration;
using Nop.X0_Widget.Widget.WreathShippingCheckout.Domain;
using Nop.Core.Domain.Seo;

namespace Nop.X0_Widget.Widget.WreathShippingCheckout.Controllers
{
    public class WreathShippingCheckoutController : BasePluginController
    {
        private ILogger _logger;
        private IBlogService _blogService;
        private IRepository<UrlRecord> _blogUrl;

        public WreathShippingCheckoutController(ILogger logger, IBlogService blogService , IRepository<UrlRecord> blogUrl)
        {
            _logger = logger;
            _blogService = blogService;
            _blogUrl = blogUrl;
        }

        public ActionResult WreathShippingCheckoutPost(object additionalData = null)
        {
            return View("~/Plugins/Widget.WreathShippingCheckout/Views/_WreathShippingCheckoutTest.cshtml");
            BlogPost Blog = _blogService.GetBlogPostById((int)additionalData);
            WreathShippingCheckoutModel RecommandArticle = new WreathShippingCheckoutModel();
            
            string[] Brands = ConfigurationManager.AppSettings["brand"].Split(',');
            string[] BrandImages = ConfigurationManager.AppSettings["brand-image"].Split(',');
            string[] BrandLinks = ConfigurationManager.AppSettings["brand-link"].Split(',');
            string[] BrandContent = ConfigurationManager.AppSettings["brand-content"].Split(',');

            int BrandNumber = Brand(Brands, Blog.Tags);

            
            var BrandBlog = _blogService.GetAllBlogPostsByTag(tag: Brands[BrandNumber]).Where(m => m.Id != Blog.Id).OrderByDescending(e => e.CreatedOnUtc).Take(4);

            foreach (var _blog in BrandBlog)
            {
                string BlogLink = _blogUrl.Table.Where(m => m.EntityId == _blog.Id).Select(m => m.Slug).FirstOrDefault().ToString();
                RecommandArticle.BrandRelated.Add(new WreathShippingCheckoutClass(_blog.Title, _blog.BodyOverview, BlogLink));
            }

            var RecentlyBlog = _blogService.GetAllBlogPosts().Where(m => m.Id != Blog.Id && (m.Tags==null||!m.Tags.Contains(Brands[BrandNumber]))).OrderByDescending(e => e.CreatedOnUtc).Take(4+4-RecommandArticle.BrandRelated.Count);

            foreach (var _blog in RecentlyBlog)
            {
                string BlogLink = _blogUrl.Table.Where(m => m.EntityId == _blog.Id).Select(m => m.Slug).FirstOrDefault().ToString();
                RecommandArticle.RecentlyArticle.Add(new WreathShippingCheckoutClass(_blog.Title, _blog.BodyOverview, BlogLink));
            }



            RecommandArticle.AllTag = Blog.Tags;
            RecommandArticle.Brand = Brands[BrandNumber];
            RecommandArticle.BrandImageUrl = BrandImages[BrandNumber];
            RecommandArticle.BrandUrl = BrandLinks[BrandNumber];
            RecommandArticle.BrandContent = BrandContent[BrandNumber];


            return View("~/Plugins/Widget.WreathShippingCheckout/Views/_WreathShippingCheckout.cshtml",(object)RecommandArticle);//, (object)model);
        }

        public ActionResult Configure()
        {
            var model = new object();
            //..\..\Presentation\Nop.Web\Plugins\BadPayBad.HelloWorld\
            //the view file .cshml depend your output path
            return View("~/Plugins/Widget.WreathShippingCheckout/Views/Admin/Configure.cshtml", model);
        }

        public int Brand(string [] AllBrand,string Tag)
        {
            for (int i = 0; i < AllBrand.Length; i++)
            {
                if (Tag.ToLower().Contains(AllBrand[i].ToLower()))
                {
                    return i;
                }
            }

            return 0;
        }

    }
}
