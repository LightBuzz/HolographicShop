using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace HolographicShop.DataObjects
{
    public class Category : EntityData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public string ModelURL { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}