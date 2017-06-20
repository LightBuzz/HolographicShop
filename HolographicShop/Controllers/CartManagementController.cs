using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HolographicShop.DataObjects;
using HolographicShop.Models;
using Microsoft.Azure.Mobile.Server.Config;

namespace HolographicShop.Controllers
{
    [MobileAppController]
    public class CartManagementController : ApiController
    {
        [HttpPost, Route("api/AddToCart")]
        public HttpResponseMessage Post(string productId, string cartId)
        {
            MobileServiceContext context = new MobileServiceContext();
            Product product = context.Products.SingleOrDefault(a => a.Id == productId);
            Cart cart = context.Carts.SingleOrDefault(a => a.Id == cartId);
            ProductInCart productInCart =
                context.ProductsInCarts.SingleOrDefault(a => a.Product.Id == productId && a.Cart.Id == cartId);
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found");
            }
            if (cart == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cart not found");
            }
            if (productInCart != null)
            {
                productInCart.Quantity += 1;
                cart.CartNoOfItems += 1;
                cart.CartSum += product.Price;
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Cart updated");
            }
            productInCart = new ProductInCart
            {
                Id = Guid.NewGuid().ToString("N"),
                ProductId = productId,
                Product = product,
                CartId = cartId,
                Cart = cart,
                Quantity = 1
            };
            context.ProductsInCarts.Add(productInCart);
            cart.CartNoOfItems += 1;
            cart.CartSum += product.Price;
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Configuration.Services.GetTraceWriter().Error("Exception: " + e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Product added to cart");
        }

        [HttpDelete, Route("api/RemoveFromCart")]
        public HttpResponseMessage Delete(string productId, string cartId)
        {
            MobileServiceContext context = new MobileServiceContext();
            Product product = context.Products.SingleOrDefault(a => a.Id == productId);
            Cart cart = context.Carts.SingleOrDefault(a => a.Id == cartId);
            ProductInCart productInCart =
                context.ProductsInCarts.SingleOrDefault(a => a.Product.Id == productId && a.Cart.Id == cartId);
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found");
            }
            if (cart == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cart not found");
            }
            if (productInCart == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found in the cart");
            }
            productInCart.Quantity -= 1;
            cart.CartNoOfItems -= 1;
            cart.CartSum -= product.Price;
            context.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Cart updated");
        }
    }
}
