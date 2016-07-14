using DonorGateway.Data;
using DonorGateway.Domain;
using rsvp.web.ViewModels;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace rsvp.web.Controllers
{
    public class EventController : Controller
    {
        private readonly DataContext context;

        public EventController()
        {
            context = new DataContext();
        }

        [Route("{id}")]
        public ActionResult Index(string id)
        {
            var @event = context.Events.FirstOrDefault(x => x.Name == id);

            if (@event == null) RedirectToAction("EventNotFound");

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
                EventId = @event.Id
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(EventViewModel model)
        {
            var guest = context.Guests.FirstOrDefault(g => g.FinderNumber == model.PromoCode);

            if (ModelState.IsValid && guest != null) return View(guest);

            model.Template = context.Templates.FirstOrDefault(x => x.Id == model.TemplateId);
            if (guest == null) ModelState.AddModelError("PromoCode", "Invalid Code");
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult RegisterConfirm(Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", guest);
            }

            context.Guests.AddOrUpdate(guest);
            context.SaveChanges();

            return View("RegisterComplete", guest);

        }

        public ActionResult RegisterComplete(Guest guest)
        {
            return View(guest);
        }

        public ActionResult EventNotFound()
        {
            return View();
        }
    }
}