﻿using DonorGateway.Data;
using rsvp.web.ViewModels;
using System;
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
                IsCancelled = @event.IsCancelled ?? false
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(EventViewModel model)
        {
            var guest = db.Guests.FirstOrDefault(g => g.FinderNumber == model.PromoCode);

            if (guest == null || !ModelState.IsValid)
            {
                if (guest == null) ModelState.AddModelError("PromoCode", "Invalid Code");
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
                IsAttending = guest?.IsAttending,
                EventId = guest?.EventId,
                PromoCode = guest?.FinderNumber,
                EventName = guest?.Event.Name
            };

            viewModel.Validate();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Confirm(GuestRegisterViewModel guestRegister)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", guestRegister);
            }

            var g = db.Guests.Find(guestRegister.Id);

            g.Name = guestRegister.Name;
            g.Name = guestRegister?.Name;
            g.Email = guestRegister?.Email;
            g.Phone = guestRegister?.Phone;
            g.Address = guestRegister?.Address;
            g.Address2 = guestRegister?.Address2;
            g.Address3 = guestRegister?.Address3;
            g.City = guestRegister?.City;
            g.State = guestRegister?.State;
            g.Zipcode = guestRegister?.Zipcode;
            g.Comment = guestRegister?.Comment;
            g.TicketCount = guestRegister?.TicketCount;
            g.IsAttending = guestRegister?.IsAttending;
            g.ResponseDate = DateTime.Now;

            db.Guests.AddOrUpdate(g);
            db.SaveChanges();

            var @event = db.Events.Find(guestRegister.EventId);

            var finishViewModel = new FinishViewModel()
            {
                Guest = g,
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