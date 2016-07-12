using DonorGateway.Data;
using DonorGateway.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CsvHelper;
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
            var e = context.Events.Include(x => x.Guests).AsQueryable().FirstOrDefault(x => x.Id == id);
            if (e == null) return NotFound();
            //TODO: Automapper Here
            var model = new EventViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Speaker = e.Speaker,
                Venue = e.Venue,
                Street = e.Street,
                City = e.City,
                State = e.State,
                Zipcode = e.Zipcode,
                Capacity = e.Capacity,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                VenueOpenDate = e.VenueOpenDate,
                RegistrationCloseDate = e.RegistrationCloseDate,
                TicketsAllowance = e.TicketAllowance,
                IsCancelled = e.IsCancelled,
                Template = e.Template,
                RegisteredGuestCount = e.Guests.Count(x => x.IsAttending == true),
                WaitingGuestCount = e.Guests.Count(x => x.IsWaiting == true),
                TicketMailedCount = e.Guests.Count(x => x.IsMailed == true),
                TicketMailedQueueCount =
                    e.Guests.Count(x => x.IsAttending == true && x.IsWaiting == false && x.IsMailed == false)
            };

            model.TicketRemainingCount = e.Capacity - (model.RegisteredGuestCount - model.WaitingGuestCount);
            return Ok(model);
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
            if (!string.IsNullOrWhiteSpace(vm.Address)) pred = pred.And(p => p.Address.Contains(vm.Address));
            if (!string.IsNullOrWhiteSpace(vm.FinderNumber)) pred = pred.And(p => p.FinderNumber.StartsWith(vm.FinderNumber));
            if (!string.IsNullOrWhiteSpace(vm.Name)) pred = pred.And(p => p.Name.Contains(vm.Name));
            if (!string.IsNullOrWhiteSpace(vm.City)) pred = pred.And(p => p.City.StartsWith(vm.City));
            if (!string.IsNullOrWhiteSpace(vm.State)) pred = pred.And(p => p.State.Equals(vm.State));
            if (!string.IsNullOrWhiteSpace(vm.ZipCode)) pred = pred.And(p => p.Zipcode.StartsWith(vm.ZipCode));
            if (!string.IsNullOrWhiteSpace(vm.Phone)) pred = pred.And(p => p.Phone.Contains(vm.Phone));
            if (!string.IsNullOrWhiteSpace(vm.Email)) pred = pred.And(p => p.Email.StartsWith(vm.Email));
            if (!string.IsNullOrWhiteSpace(vm.LookupId)) pred = pred.And(p => p.LookupId.StartsWith(vm.LookupId));
            if (vm.IsMailed != null) pred = pred.And(p => p.IsMailed == ticketMailed);
            if (vm.IsWaiting != null) pred = pred.And(p => p.IsWaiting == isWaiting);
            if (vm.IsAttending != null) pred = pred.And(p => p.IsAttending == isAttending);

            var list = context.Guests.AsQueryable()
                .Order(vm.OrderBy, vm.OrderDirection == "desc" ? SortDirection.Descending : SortDirection.Ascending)
                .Where(pred)
                .Skip(skipRows)
                .Take(pageSize)
                .ToList();

            var totalCount = context.Guests.Count();
            var filterCount = context.Guests.Where(pred).Count();
            var totalPages = (int)Math.Ceiling((decimal)filterCount / pageSize);

            vm.TotalCount = totalCount;
            vm.FilteredCount = filterCount;
            vm.TotalPages = totalPages;

            vm.Items = list;
            return Ok(vm);
            
        }

        [HttpPost, Route("{id:int}/guests/export")]
        public IHttpActionResult Export(int id, GuestSearchViewModel vm)
        {
            var ticketMailed = vm.IsMailed ?? false;
            var isWaiting = vm.IsWaiting ?? false;
            var isAttending = vm.IsAttending ?? false;

            var pred = PredicateBuilder.True<Guest>();
            pred = pred.And(p => p.EventId == id);
            if (!string.IsNullOrWhiteSpace(vm.Address)) pred = pred.And(p => p.Address.Contains(vm.Address));
            if (!string.IsNullOrWhiteSpace(vm.FinderNumber)) pred = pred.And(p => p.FinderNumber.StartsWith(vm.FinderNumber));
            if (!string.IsNullOrWhiteSpace(vm.Name)) pred = pred.And(p => p.Name.Contains(vm.Name));
            if (!string.IsNullOrWhiteSpace(vm.City)) pred = pred.And(p => p.City.StartsWith(vm.City));
            if (!string.IsNullOrWhiteSpace(vm.State)) pred = pred.And(p => p.State.Equals(vm.State));
            if (!string.IsNullOrWhiteSpace(vm.ZipCode)) pred = pred.And(p => p.Zipcode.StartsWith(vm.ZipCode));
            if (!string.IsNullOrWhiteSpace(vm.Phone)) pred = pred.And(p => p.Phone.Contains(vm.Phone));
            if (!string.IsNullOrWhiteSpace(vm.Email)) pred = pred.And(p => p.Email.StartsWith(vm.Email));
            if (!string.IsNullOrWhiteSpace(vm.LookupId)) pred = pred.And(p => p.LookupId.StartsWith(vm.LookupId));
            if (vm.IsMailed != null) pred = pred.And(p => p.IsMailed == ticketMailed);
            if (vm.IsWaiting != null) pred = pred.And(p => p.IsWaiting == isWaiting);
            if (vm.IsAttending != null) pred = pred.And(p => p.IsAttending == isAttending);

            var list = context.Guests.AsQueryable()
                    .Where(pred)
                    .ProjectTo<GuestExportViewModel>();

            var path = HttpContext.Current.Server.MapPath(@"~\app_data\guestlist.csv");
            using (var csv = new CsvWriter(new StreamWriter(File.Create(path))))
            {
                csv.Configuration.RegisterClassMap<GuestMap>();
                csv.WriteHeader<GuestExportViewModel>();
                csv.WriteRecords(list);
            }
            return Ok(list);
        }


        [HttpDelete, Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var vm = context.Events.Find(id);
            var message = "Deleted Event";

            if (vm != null)
            {
                try
                {
                    context.Events.Remove(vm);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
            else
            {
                return NotFound();
            }
            return Ok(message);
        }

        public IHttpActionResult Post(Event vm)
        {
            context.Templates.Add(vm.Template);
            context.SaveChanges();
            context.Events.Add(vm);
            context.SaveChanges();

            var model = new EventViewModel
            {
                Id = vm.Id,
                Name = vm.Name,
                Speaker = vm.Speaker,
                Venue = vm.Venue,
                Street = vm.Street,
                City = vm.City,
                State = vm.State,
                Zipcode = vm.Zipcode,
                Capacity = vm.Capacity,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                VenueOpenDate = vm.VenueOpenDate,
                RegistrationCloseDate = vm.RegistrationCloseDate,
                TicketsAllowance = vm.TicketAllowance,
                IsCancelled = vm.IsCancelled,
                Template = vm.Template,
                RegisteredGuestCount = vm.Guests.Count(x => x.IsAttending == true),
                WaitingGuestCount = vm.Guests.Count(x => x.IsWaiting == true),
                TicketMailedCount = vm.Guests.Count(x => x.IsMailed == true),
                TicketMailedQueueCount =
                  vm.Guests.Count(x => x.IsAttending == true && x.IsWaiting == false && x.IsMailed == false)
            };
            model.TicketRemainingCount = vm.Capacity - (model.RegisteredGuestCount - model.WaitingGuestCount);

            return Ok(vm);
        }

        public IHttpActionResult Put(Event vm)
        {
            context.Events.AddOrUpdate(vm);
            context.SaveChanges();
            var e = context.Events.Include(x => x.Guests).AsQueryable().FirstOrDefault(x => x.Id == vm.Id);
            if (e == null) return NotFound();
            var model = new EventViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Speaker = e.Speaker,
                Venue = e.Venue,
                Street = e.Street,
                City = e.City,
                State = e.State,
                Zipcode = e.Zipcode,
                Capacity = e.Capacity,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                VenueOpenDate = e.VenueOpenDate,
                RegistrationCloseDate = e.RegistrationCloseDate,
                TicketsAllowance = e.TicketAllowance,
                IsCancelled = e.IsCancelled,
                Template = e.Template,
                RegisteredGuestCount = e.Guests.Count(x => x.IsAttending == true),
                WaitingGuestCount = e.Guests.Count(x => x.IsWaiting == true),
                TicketMailedCount = e.Guests.Count(x => x.IsMailed == true),
                TicketMailedQueueCount =
                   e.Guests.Count(x => x.IsAttending == true && x.IsWaiting == false && x.IsMailed == false)
            };
            model.TicketRemainingCount = e.Capacity - (model.RegisteredGuestCount - model.WaitingGuestCount);
            return Ok(model);
        }
    }
}