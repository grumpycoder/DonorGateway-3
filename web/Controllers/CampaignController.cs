using DonorGateway.Data;
using System.Linq;
using System.Web.Http;

namespace web.Controllers
{
    [RoutePrefix("api/campaign")]
    public class CampaignController : ApiController
    {
        private readonly DataContext context;

        public CampaignController()
        {
            context = new DataContext();
        }

        public IHttpActionResult Get()
        {
            var list = context.Campaigns;
            return Ok(list);
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            var vm = context.Campaigns.FirstOrDefault(x => x.Name == name);
            return Ok(vm);
        }
    }
}