using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace HolographicShop.DataObjects
{
    public class Cart : EntityData
    {
        public string UserKey { get; set; }
        public bool Checkout { get; set; }
        public double CartSum { get; set; }
        public int CartNoOfItems { get; set; }
        public virtual List<ProductInCart> ProductsInCart { get; set; }
    }
}