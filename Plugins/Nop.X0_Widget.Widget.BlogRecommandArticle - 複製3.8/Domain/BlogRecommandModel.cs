using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.X0_Widget.Widget.BlogRecommandArticle.Domain
{
    public class BlogRecommandModel : BaseEntity
    {
        public List<BlogRecommandArticleClass> RecentlyArticle { get; set; }
        public List<BlogRecommandArticleClass> BrandRelated { get; set; }

        public string Brand { get; set; }
        public string BrandImageUrl { get; set; }
        public string BrandLogoUrl { get; set; }
        public string BrandUrl { get; set; }
        public string BrandContent { get; set; }
        public string AllTag { get; set; }

        public BlogRecommandModel()
        {
            RecentlyArticle = new List<BlogRecommandArticleClass>();
            BrandRelated = new List<BlogRecommandArticleClass>();
        }
    }

    public class BlogRecommandArticleClass
    {
        public string _Title { get; set; }
        public string _ImageLink { get; set; }
        public string Description { get; set; }
        public string _BlogLink { get; set; }

        public BlogRecommandArticleClass(string Title, string BodyOverview , string BlogLink)
        {
            
            this._Title = Title;
            this._BlogLink = BlogLink;
            this._ImageLink = BodyOverview.Split(new string[] { "src=\"","\">" },StringSplitOptions.RemoveEmptyEntries)[1];
            this.Description = BodyOverview.Split(new string[] { "<p>","</p>" }, StringSplitOptions.RemoveEmptyEntries)[1];
        }
    }
}
