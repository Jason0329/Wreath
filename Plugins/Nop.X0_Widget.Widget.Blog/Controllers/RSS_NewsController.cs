using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Blogs;
using Nop.Services.Blogs;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using Nop.Services.Seo;
using Nop.Web.Framework;
using Nop.Services.Customers;

namespace Nop.X0_Widget.Widget.Blog.Controllers
{
    public class RSS_NewsController : BaseController
    {
        private IPermissionService _PermissionService;
        private ILocalizationService _localizationService;
        private IRepository<BlogPost> _BlogPostIRepository;
        private IWebHelper _webHelper;
        private IStoreContext _storeContext;
        private IBlogService _blogService;
        private ICustomerService _CustomerService;

        public RSS_NewsController(IPermissionService PermissionService
            , ILocalizationService localizationService,
            IRepository<BlogPost> BlogPostIRepository,
            IWebHelper webHelper, IStoreContext storeContext, 
            IBlogService blogService,ICustomerService CustomerService
           
            )
        {
            _PermissionService = PermissionService;
            _localizationService = localizationService;
            _BlogPostIRepository = BlogPostIRepository;
            _webHelper = webHelper;
            _storeContext = storeContext;
            _blogService = blogService;
            _CustomerService = CustomerService;
          
        }

        public ActionResult RssOutput()
        {
            ////RssStructureMethod RssMethod = new RssStructureMethod(_webHelper, _storeContext, _blogService
            ////    , "GoTRUECAR 車價查詢網", "http://gotruecar.com/", "GoTrueCar車價查詢網，買車的新方法，GoTrueCar致力於讓大家可以簡單的動作查詢到市場上的車價，透明的資訊提供給消費者");

            ////string Feed = RssMethod.RssFeed(Request.RequestContext);


            ////context.HttpContext.Response.ContentType = MimeTypes.ApplicationRssXml;

            ////var rssFormatter = new Rss20FeedFormatter(Feed);
            ////using (var writer = XmlWriter.Create(context.HttpContext.Response.Output))
            ////{
            ////    rssFormatter.WriteTo(writer);
            ////}



            ////return null;
            
            //string GuidCode = Request.Cookies["Nop.customer"].Value;
            //var aa = _CustomerService.GetCustomerByGuid(Guid.Parse(GuidCode));

            var feed = new SyndicationFeed(
                                    string.Format("{0}: Blog", _storeContext.CurrentStore.GetLocalized(x => x.Name)),
                                    "Blog",
                                    new Uri(_webHelper.GetStoreLocation(false)),
                                    string.Format("urn:store:{0}:blog", _storeContext.CurrentStore.Id),
                                    DateTime.UtcNow);



            var items = new List<SyndicationItem>();
            var blogPosts = _blogService.GetAllBlogPosts(_storeContext.CurrentStore.Id, 2).Where( m=>m.Tags!=null && m.Tags.Contains("汽車新聞"));
            foreach (var blogPost in blogPosts)
            {
                string blogPostUrl = Url.RouteUrl("BlogPost", new { SeName = blogPost.GetSeName(blogPost.LanguageId, ensureTwoPublishedLanguages: false) }, _webHelper.IsCurrentConnectionSecured() ? "https" : "http");

                SyndicationItem item = new SyndicationItem(blogPost.Title, blogPost.Body.Replace("<h3>", " ").Replace("</h3>", " ").Replace("<p>", " ").Replace("</p>", " "), new Uri(blogPostUrl), String.Format("urn:store:{0}:blog:post:{1}", _storeContext.CurrentStore.Id, blogPost.Id), blogPost.CreatedOnUtc);
                item.PublishDate = blogPost.CreatedOnUtc;

                items.Add(item);
            }
            feed.Items = items;



            HttpContext.Response.ContentType = "application/xml";
            HttpContext.Response.ContentEncoding = Encoding.UTF8;
            var rssFormatter = new Rss20FeedFormatter(feed);

            using (var writer = XmlWriter.Create(HttpContext.Response.Output, new XmlWriterSettings { Indent = true }))
            {
                rssFormatter.WriteTo(writer);
                writer.Flush();
                
            }
            return null;
        }


    }
}
