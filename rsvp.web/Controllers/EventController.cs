using DonorGateway.Data;
using rsvp.web.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace rsvp.web.Controllers
{
    public class EventController : Controller
    {
        private readonly DataContext db;

        public EventController()
        {
            db = new DataContext();
        }

        [Route("{id}")]
        public ActionResult Index(string id)
        {
            var @event = db.Events.FirstOrDefault(x => x.Name == id);

            if (@event == null) return View("EventNotFound");

            var model = new EventViewModel()
            {
                Name = @event?.Name,
                Venue = @event?.Venue,
                Street = @event?.Street,
                City = @event?.City,
                State = @event?.State,
                Zipcode = @event?.Zipcode,
                Template = @event?.Template,
                TemplateId = @event?.TemplateId,
                EventId = @event.Id,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                Speaker = @event.Speaker,
                IsCancelled = @event.IsCancelled ?? false,
                TicketAllowance = @event.TicketAllowance ?? 0
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(EventViewModel model)
        {
            var guest = db.Guests.FirstOrDefault(g => g.FinderNumber == model.PromoCode);

            if (guest == null) ModelState.AddModelError("PromoCode", "Invalid Code");
            if (guest?.ResponseDate != null) ModelState.AddModelError("Attendance", "Already registered for event");
            if (!ModelState.IsValid)
            {
                model.Template = db.Templates.FirstOrDefault(x => x.Id == model.TemplateId);
                return View("Index", model);
            }

            var viewModel = new GuestRegisterViewModel()
            {
                Id = guest.Id,
                Name = guest?.Name,
                Email = guest?.Email,
                Phone = guest?.Phone,
                Address = guest?.Address,
                Address2 = guest?.Address2,
                Address3 = guest?.Address3,
                City = guest?.City,
                State = guest?.State,
                Zipcode = guest?.Zipcode,
                Comment = guest?.Comment,
                TicketCount = guest?.TicketCount,
                IsAttending = null,
                TicketAllowance = model.TicketAllowance,
                EventId = guest?.EventId,
                PromoCode = guest?.FinderNumber,
                EventName = guest?.Event?.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Confirm(GuestRegisterViewModel guestRegister)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", guestRegister);
            }

            var guest = db.Guests.Find(guestRegister.Id);

            guest.Name = guestRegister.Name;
            guest.Name = guestRegister?.Name;
            guest.Email = guestRegister?.Email;
            guest.Phone = guestRegister?.Phone;
            guest.Address = guestRegister?.Address;
            guest.Address2 = guestRegister?.Address2;
            guest.Address3 = guestRegister?.Address3;
            guest.City = guestRegister?.City;
            guest.State = guestRegister?.State;
            guest.Zipcode = guestRegister?.Zipcode;
            guest.Comment = guestRegister?.Comment;
            guest.TicketCount = guestRegister?.TicketCount;
            guest.IsAttending = guestRegister?.IsAttending;
            guest.ResponseDate = DateTime.Now;



            var @event = db.Events.Include(g => g.Guests).FirstOrDefault(e => e.Id == guestRegister.EventId);

            var eventViewModel = new EventViewModel()
            {
                Name = @event?.Name,
                Venue = @event?.Venue,
                Street = @event?.Street,
                City = @event?.City,
                State = @event?.State,
                Zipcode = @event?.Zipcode,
                Template = @event?.Template,
                TemplateId = @event?.TemplateId,
                EventId = @event.Id,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                Speaker = @event.Speaker,
                Capacity = @event.Capacity,
                IsCancelled = @event.IsCancelled ?? false,
                TicketAllowance = @event.TicketAllowance ?? 0,
                RegisteredGuestCount = @event.Guests.Count(x => x.IsAttending == true),
                WaitingGuestCount = @event.Guests.Count(x => x.IsWaiting == true),

            };

            if ((eventViewModel.RegisteredGuestCount + guest.TicketCount) > eventViewModel.Capacity)
            {
                guest.IsWaiting = true;
                guest.WaitingDate = DateTime.Now;
            }

            db.Guests.AddOrUpdate(guest);
            db.SaveChanges();
            var finishViewModel = new FinishViewModel()
            {
                Guest = guest,
                Event = @event
            };
            return View("Finish", finishViewModel);

        }

        public ActionResult Finish(FinishViewModel model)
        {
            return View(model);
        }

        public ActionResult EventNotFound()
        {
            return View();
        }
    }
}