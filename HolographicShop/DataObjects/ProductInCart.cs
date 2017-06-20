using Microsoft.Azure.Mobile.Server;

namespace HolographicShop.DataObjects
{
    public class ProductInCart : EntityData
    {
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}