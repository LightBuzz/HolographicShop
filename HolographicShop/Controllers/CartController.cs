using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using HolographicShop.DataObjects;
using HolographicShop.Models;

namespace HolographicShop.Controllers
{
    public class CartController : TableController<Cart>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Cart>(context, Request);
        }

        // GET tables/Cart
        public IQueryable<Cart> GetAllCart()
        {
            return Query(); 
        }

        // GET tables/Cart/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Cart> GetCart(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Cart/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Cart> PatchCart(string id, Delta<Cart> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Cart
        public async Task<IHttpActionResult> PostCart(Cart item)
        {
            Cart current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Cart/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCart(string id)
        {
             return DeleteAsync(id);
        }
    }
}
