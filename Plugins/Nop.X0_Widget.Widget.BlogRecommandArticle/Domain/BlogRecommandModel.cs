using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.X0_Widget.Widget.WreathShippingCheckout.Domain
{
    public class WreathShippingCheckoutModel : BaseEntity
    {
        public List<WreathShippingCheckoutClass> RecentlyArticle { get; set; }
        public List<WreathShippingCheckoutClass> BrandRelated { get; set; }

        public string Brand { get; set; }
        public string BrandImageUrl { get; set; }
        public string BrandLogoUrl { get; set; }
        public string BrandUrl { get; set; }
        public string BrandContent { get; set; }
        public string AllTag { get; set; }

        public WreathShippingCheckoutModel()
        {
            RecentlyArticle = new List<WreathShippingCheckoutClass>();
            BrandRelated = new List<WreathShippingCheckoutClass>();
        }
    }

    public class WreathShippingCheckoutClass
    {
        public string _Title { get; set; }
        public string _ImageLink { get; set; }
        public string Description { get; set; }
        public string _BlogLink { get; set; }

        public WreathShippingCheckoutClass(string Title, string BodyOverview , string BlogLink)
        {
            
            this._Title = Title;
            this._BlogLink = BlogLink;
            this._ImageLink = BodyOverview.Split(new string[] { "src=\"","\">" },StringSplitOptions.RemoveEmptyEntries)[1];
            this.Description = BodyOverview.Split(new string[] { "<p>","</p>" }, StringSplitOptions.RemoveEmptyEntries)[1];
        }
    }
}
