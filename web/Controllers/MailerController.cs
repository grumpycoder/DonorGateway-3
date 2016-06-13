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
    [RoutePrefix("api/mailer")]
    public class MailerController : ApiController
    {
        private readonly DataContext context;

        public MailerController()
        {
            context = new DataContext();
        }

        [Route("search")]
        public IHttpActionResult Search(MailerSearchViewModel vm)
        {
            var page = vm.Page.GetValueOrDefault(0);
            var pageSize = vm.PageSize.GetValueOrDefault(10);
            var skipRows = (page - 1) * pageSize;
            var suppress = vm.Suppress ?? false;

            var pred = PredicateBuilder.True<Mailer>();
            if (suppress) pred = pred.And(p => p.Suppress == suppress);
            if (!string.IsNullOrWhiteSpace(vm.FinderNumber)) pred = pred.And(p => p.FinderNumber.StartsWith(vm.FinderNumber));
            if (!string.IsNullOrWhiteSpace(vm.FirstName)) pred = pred.And(p => p.FirstName.Contains(vm.FirstName));
            if (!string.IsNullOrWhiteSpace(vm.LastName)) pred = pred.And(p => p.LastName.Contains(vm.LastName));
            if (!string.IsNullOrWhiteSpace(vm.Address)) pred = pred.And(p => p.Address.Contains(vm.Address));
            if (!string.IsNullOrWhiteSpace(vm.City)) pred = pred.And(p => p.City.StartsWith(vm.City));
            if (!string.IsNullOrWhiteSpace(vm.State)) pred = pred.And(p => p.State.Equals(vm.State));
            if (!string.IsNullOrWhiteSpace(vm.ZipCode)) pred = pred.And(p => p.ZipCode.StartsWith(vm.ZipCode));
            if (!string.IsNullOrWhiteSpace(vm.SourceCode)) pred = pred.And(p => p.SourceCode.StartsWith(vm.SourceCode));
            if (vm.CampaignId != 0) pred = pred.And(p => p.CampaignId == vm.CampaignId);

            List<Mailer> list;
            if (vm.AllRecords)
            {
                list = context.Mailers.AsQueryable()
                    .Where(pred)
                    .OrderBy(x => x.Id)
                    //.ProjectTo<ConstituentViewModel>()
                    .ToList();
            }
            else
            {
                list = context.Mailers.AsQueryable()
                             .Order(vm.OrderBy, vm.OrderDirection == "desc" ? SortDirection.Descending : SortDirection.Ascending)
                             .Where(pred)
                             .Include(x => x.Campaign)
                             .Skip(skipRows)
                             .Take(pageSize)
                             .ToList();
            }
            var totalCount = context.Mailers.Count();
            var filterCount = context.Mailers.Where(pred).Count();
            var totalPages = (int)Math.Ceiling((decimal)filterCount / pageSize);

            vm.TotalCount = totalCount;
            vm.FilteredCount = filterCount;
            vm.TotalPages = totalPages;

            vm.Items = list;
            return Ok(vm);
        }

        public IHttpActionResult Put(Mailer vm)
        {
            context.Mailers.AddOrUpdate(vm);
            context.SaveChanges();
            return Ok(vm);
        }

        //public IHttpActionResult Get()
        //{
        //    var list = context.Constituents.OrderBy(x => x.Id).Take(10).Skip(0).ToList();
        //    return Ok(list);
        //}

        //[Route("{id:int}")]
        //public IHttpActionResult Get(int id)
        //{
        //    var vm = context.Constituents.FirstOrDefault(x => x.Id == id);
        //    return Ok(vm);
        //}
        //public IHttpActionResult Post(Constituent vm)
        //{
        //    context.Constituents.Add(vm);
        //    context.SaveChanges();
        //    return Ok(vm);
        //}

        //public IHttpActionResult Delete(int id)
        //{
        //    var c = context.Constituents.Find(id);
        //    context.Constituents.Remove(c);
        //    return Ok($"Deleted {id}");
        //}
    }
}