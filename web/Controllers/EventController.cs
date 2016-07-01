using DonorGateway.Data;
using DonorGateway.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Helpers;
using System.Web.Http;
using web.Helpers;
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
            var vm = context.Events.AsQueryable().FirstOrDefault(x => x.Id == id);
            return Ok(vm);
        }

        [HttpPost, Route("{id:int}/guests")]
        public IHttpActionResult Guests(int id, GuestSearchViewModel vm)
        {
            var page = vm.Page.GetValueOrDefault(0);
            var pageSize = vm.PageSize.GetValueOrDefault(10);
            var skipRows = (page - 1) * pageSize;
            var ticketMailed = vm.IsMailed ?? false;
            var isWaiting = vm.IsWaiting ?? false;
            var isAttending = vm.IsAttending ?? false;

            var pred = PredicateBuilder.True<Guest>();
            pred = pred.And(p => p.EventId == id); 
            if (!string.IsNullOrWhiteSpace(vm.Address)) pred = pred.And(p => p.Street.Contains(vm.Address));
            if (!string.IsNullOrWhiteSpace(vm.FinderNumber)) pred = pred.And(p => p.FinderNumber.StartsWith(vm.FinderNumber));
            if (!string.IsNullOrWhiteSpace(vm.Name)) pred = pred.And(p => p.Name.Contains(vm.Name));
            if (!string.IsNullOrWhiteSpace(vm.City)) pred = pred.And(p => p.City.StartsWith(vm.City));
            if (!string.IsNullOrWhiteSpace(vm.State)) pred = pred.And(p => p.State.Equals(vm.State));
            if (!string.IsNullOrWhiteSpace(vm.ZipCode)) pred = pred.And(p => p.Zipcode.StartsWith(vm.ZipCode));
            if (!string.IsNullOrWhiteSpace(vm.Phone)) pred = pred.And(p => p.Phone.Contains(vm.Phone));
            if (!string.IsNullOrWhiteSpace(vm.Email)) pred = pred.And(p => p.Email.StartsWith(vm.Email));
            if (!string.IsNullOrWhiteSpace(vm.LookupId)) pred = pred.And(p => p.LookupId.StartsWith(vm.LookupId));
            if(vm.IsMailed != null) pred = pred.And(p => p.IsMailed == ticketMailed);
            if(vm.IsWaiting != null) pred = pred.And(p => p.IsWaiting == isWaiting);
            if(vm.IsAttending != null) pred = pred.And(p => p.IsAttending == isAttending);
            //pred = pred.And(p => p.IsWaiting == isWaiting);
            //pred = pred.And(p => p.HasResponded == hasResponded);

            List<Guest> list;
            if (vm.AllRecords)
            {
                list = context.Guests.AsQueryable()
                    .Where(pred)
                    .OrderBy(x => x.Id)
                    .ToList();
            }
            else
            {
                list = context.Guests.AsQueryable()
                             .Order(vm.OrderBy, vm.OrderDirection == "desc" ? SortDirection.Descending : SortDirection.Ascending)
                             .Where(pred)
                             .Skip(skipRows)
                             .Take(pageSize)
                             .ToList();
            }
            var totalCount = context.Guests.Count();
            var filterCount = context.Guests.Where(pred).Count();
            var totalPages = (int)Math.Ceiling((decimal)filterCount / pageSize);

            vm.TotalCount = totalCount;
            vm.FilteredCount = filterCount;
            vm.TotalPages = totalPages;

            vm.Items = list;
            return Ok(vm);

            ////var list = context.Guests.Where(x => x.EventId == id).OrderBy(x => x.Id).Skip(0).Take(10);
            //return Ok(list);
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
            context.Templates.Add(vm.Template);
            context.SaveChanges();
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