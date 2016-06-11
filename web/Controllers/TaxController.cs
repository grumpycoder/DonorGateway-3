using DonorGateway.Data;
using DonorGateway.Domain;
using System.Data.Entity.Migrations;
using System.Web.Http;

namespace web.Controllers
{
    [RoutePrefix("api/tax")]
    public class TaxController : ApiController
    {
        private readonly DataContext context;

        public TaxController()
        {
            context = new DataContext();
        }

        public IHttpActionResult Put(TaxItem vm)
        {
            context.TaxItems.AddOrUpdate(vm);
            context.SaveChanges();
            return Ok(vm);
        }

        public IHttpActionResult Post(TaxItem vm)
        {
            context.TaxItems.AddOrUpdate(vm);
            context.SaveChanges();
            return Ok(vm);
        }

        public IHttpActionResult Delete(int id)
        {
            var taxItem = context.TaxItems.Find(id);
            context.TaxItems.Remove(taxItem);
            context.SaveChanges();
            return Ok($"Deleted {id} sucessfully");
        }

        //public IHttpActionResult Get()
        //{
        //    var list = context.TaxItems.OrderBy(x => x.Id).Take(10).Skip(9).ToList();
        //    return Ok(list);
        //}
    }
}