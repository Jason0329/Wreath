using Nop.Core;
using Nop.Core.Domain.Blogs;
using Nop.Core.Plugins;
using Nop.Services.Blogs;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Controllers;
using Nop.Services.Seo;
using System.Web.Routing;

namespace Nop.X0_Widget.Widget.Blog
{
    public class RssStructureMethod : BaseController
    {
        private IBlogService _blogService;
        private IStoreContext _storeContext;
        private IWebHelper _webHelper;
        //private UrlHelper Url { get; set; }
       
       
        #region Fields

        //private readonly ILocalizationService _localizationService;

        //private readonly StringBuilder _traceMessages;
        string RssWebsiteTitle { get; set; }
        string Link { get; set; }
        string Description { get; set; }



        #endregion

        #region Ctor
        public RssStructureMethod(IWebHelper webHelper,IStoreContext storeContext, IBlogService blogService, string RssTitle, string Link, string Description)
        {
            this.RssWebsiteTitle = RssTitle;
            this.Link = Link;
            this.Description = Description;
            _blogService = blogService;
            _storeContext = storeContext;
            _webHelper = webHelper;
           
        }
        #endregion

        #region Utilities


        #endregion

        #region Methods

        public string RssFeed(RequestContext Request)
        {
            return SetHeader() + SetBody(Request) + SetFooter();
        }

        string SetHeader()
        {
            string _VersionEncoding = "<?xml version=\"1.0\" encoding=\"utf-8\"?><rss version=\"2.0\" xmlns:content=\"http://purl.org/rss/1.0/modules/content/\" xmlns:media=\"" + this.Link + "\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\">";
            string _Title = "<channel><title>"+RssWebsiteTitle+"</title><link>"+this.Link+"</link>";
            string _Description = "<description>" + Description + "</description>";
            string _PubDate = "<pubDate>" + String.Format("{0:R}", DateTime.Now).Replace("GMT", "+0800") + "</pubDate>";

            return _VersionEncoding+_Title+_Description+_PubDate;
        }

        string SetFooter()
        {
            return @"</channel></rss>";
        }

        string SetBody(RequestContext Request)
        {

            string RssBody = "";
            var blogPosts = _blogService.GetAllBlogPosts(_storeContext.CurrentStore.Id).OrderByDescending(m=>m.CreatedOnUtc);
            Url = new UrlHelper(Request);
            foreach (var _blogPost in blogPosts)
            {
                string blogPostUrl = Url.RouteUrl("BlogPost", new { SeName = _blogPost.GetSeName(_blogPost.LanguageId, ensureTwoPublishedLanguages: false) }, _webHelper.IsCurrentConnectionSecured() ? "https" : "http");


                RssBody += "<item>";
                RssBody += "<title>" + _blogPost.Title + "</title>";
                RssBody += "<description>" + _blogPost.Body + "</description>";
                RssBody += "<link>" + blogPostUrl + "</link>";
                RssBody += "<guid>" + blogPostUrl + "</guid>";
                RssBody += "<pubDate>" + String.Format("{0:R}", _blogPost.CreatedOnUtc).Replace("GMT", "+0800") + "</pubDate>";


                RssBody += "</item>";
            }
            return RssBody;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a shipping rate computation method type
        /// </summary>
        public string Header
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// Gets a shipment tracker
        /// </summary>
        public string Footer
        {
            get { return ""; }
        }

        #endregion
    }
}
