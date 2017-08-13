using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.ProductBatchUpdate.Domain
{
    public class UpdateProduct : BaseEntity
    {

        public UpdateProduct()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableManufacturers = new List<SelectListItem>();
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        public int ProductID { get; set; }
        public string Name { get; set; }
        public Decimal OldPrice { get; set; }
        public Decimal Price { get; set; }
        public bool IsPublished { get; set; }

        public int SearchCategoryId { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<SelectListItem> AvailableManufacturers { get; set; }
        
    }
}
