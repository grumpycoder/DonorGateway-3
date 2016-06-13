using DonorGateway.Data;
using System.Linq;
using System.Web.Http;

namespace web.Controllers
{
    [RoutePrefix("api/reason")]
    public class ReasonController : ApiController
    {
        private readonly DataContext context;

        public ReasonController()
        {
            context = new DataContext();
        }

        public IHttpActionResult Get()
        {
            var list = context.SuppressReasons;
            return Ok(list);
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            var vm = context.SuppressReasons.FirstOrDefault(x => x.Reason == name);
            return Ok(vm);
        }
    }
}