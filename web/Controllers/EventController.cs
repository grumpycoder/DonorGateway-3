using DonorGateway.Data;
using DonorGateway.Domain;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var vm = context.Events.AsQueryable().Where(x => x.Id == id)
                                                 .Include(x => x.Guests)
                                                 .FirstOrDefault();
            return Ok(vm);
        }

        [HttpDelete, Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var vm = context.Events.Find(id);
            if (vm != null)
            {
                context.Events.Remove(vm);
                context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        public IHttpActionResult Post(Event vm)
        {
            context.Events.Add(vm);
            context.SaveChanges();
            return Ok(vm);
        }

        public IHttpActionResult Put(Event vm)
        {
            context.Events.AddOrUpdate(vm);
            context.SaveChanges();
            return Ok(vm);
        }
    }
}