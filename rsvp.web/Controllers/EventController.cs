using DonorGateway.Data;
using rsvp.web.ViewModels;
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
            var evt = context.Events.FirstOrDefault(x => x.Name == id);


            var vm = new EventViewModel()
            {
                Name = evt.Name,
                Venue = evt.Venue,
                Street = evt.Street,
                City = evt.City,
                State = evt.State,
                Zipcode = evt.Zipcode,
                Template = evt.Template
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Register(EventViewModel model)
        {

            return View(model);
        }

    }
}