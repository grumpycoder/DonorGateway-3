using DonorGateway.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using web.ViewModels;

namespace web.Controllers
{
    [RoutePrefix("api/event")]
    public class EventController : ApiController
    {
        private readonly DataContext context;

        public EventController()
        {
            context = new DataContext();
        }

        public IHttpActionResult Get()
        {
            var list = context.Events;
            return Ok(list);
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            var vm = context.Events.AsQueryable().Where(x => x.Name == name).Include(x => x.Guests).FirstOrDefault();
            return Ok(vm);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var vm = context.Events.AsQueryable().Where(x => x.Id == id).Include(x => x.Guests).FirstOrDefault();
            return Ok(vm);
        }
    }
}