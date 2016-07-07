using DonorGateway.Data;
using DonorGateway.Domain;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;

namespace web.Controllers
{
    [RoutePrefix("api/guest")]
    public class GuestController : ApiController
    {
        private readonly DataContext context;

        public GuestController()
        {
            context = new DataContext();
        }

        public IHttpActionResult Get()
        {
            var list = context.SuppressReasons;
            return Ok(list);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var vm = context.Guests.FirstOrDefault(x => x.Id == id);
            return Ok(vm);
        }

        public IHttpActionResult Put(Guest vm)
        {
            context.Guests.AddOrUpdate(vm);
            context.SaveChanges();
            return Ok(vm);
        }
    }
}