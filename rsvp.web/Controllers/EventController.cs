using AutoMapper;
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
            var @event = Mapper.Map<EventViewModel>(db.Events.Include(g => g.Guests).FirstOrDefault(x => x.Name == id));

            if (@event == null) return View("EventNotFound");

            return View(@event);
        }

        [HttpPost]
        public ActionResult Register(EventViewModel model)
        {
            var guest = Mapper.Map<RegisterFormViewModel>(db.Guests.Include(e => e.Event).Include(t => t.Event.Template).FirstOrDefault(g => g.FinderNumber == model.PromoCode));

            if (guest == null) ModelState.AddModelError("PromoCode", "Invalid Reservation Code");

            if (guest != null && guest.IsRegistered) ModelState.AddModelError("Attendance", "Already registered for event");

            if (ModelState.IsValid) return View(guest);

            model.Template = db.Templates.FirstOrDefault(x => x.Id == model.TemplateId);
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Confirm(RegisterFormViewModel form)
        {
            if (!ModelState.IsValid) return View("Register", form);

            var guest = db.Guests.Include(e => e.Event).SingleOrDefault(x => x.Id == form.GuestId);

            var @event = Mapper.Map<EventViewModel>(db.Events.Include(g => g.Guests).Include(t => t.Template).FirstOrDefault(e => e.Id == form.EventId));

            if (@event.IsAtCapacity)
            {
                form.IsWaiting = true;
                form.WaitingDate = DateTime.Now;
            }

            form.ResponseDate = DateTime.Now;
            Mapper.Map(form, guest);
            db.Guests.AddOrUpdate(guest);
            //db.SaveChanges();

            var m = Mapper.Map<FinishFormViewModel>(guest);
            m.ProcessMessages();

            return View("Finish", m);

        }

        public ActionResult Finish(FinishFormViewModel model)
        {
            return View(model);
        }

        public ActionResult EventNotFound()
        {
            return View();
        }
    }
}