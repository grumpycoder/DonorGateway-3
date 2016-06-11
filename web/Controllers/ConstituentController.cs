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
    [RoutePrefix("api/constituent")]
    public class ConstituentController : ApiController
    {
        private readonly DataContext context;

        public ConstituentController()
        {
            context = new DataContext();
        }

        [Route("search")]
        public IHttpActionResult Search(ConsituentSearchViewModel vm)
        {
            var page = vm.Page.GetValueOrDefault(0);
            var pageSize = vm.PageSize.GetValueOrDefault(10);
            var skipRows = (page - 1) * pageSize;

            var pred = PredicateBuilder.True<Constituent>();
            if (vm.UpdateStatus != null) pred = pred.And(p => p.UpdateStatus == vm.UpdateStatus);
            if (!string.IsNullOrWhiteSpace(vm.Name)) pred = pred.And(p => p.Name.Contains(vm.Name));
            if (!string.IsNullOrWhiteSpace(vm.FinderNumber)) pred = pred.And(p => p.FinderNumber.Contains(vm.FinderNumber));
            if (!string.IsNullOrWhiteSpace(vm.LookupId)) pred = pred.And(p => p.LookupId.Contains(vm.LookupId));
            if (!string.IsNullOrWhiteSpace(vm.Zipcode)) pred = pred.And(p => p.Zipcode.StartsWith(vm.Zipcode));
            if (!string.IsNullOrWhiteSpace(vm.Email)) pred = pred.And(p => p.Email.Contains(vm.Email));
            if (!string.IsNullOrWhiteSpace(vm.Phone)) pred = pred.And(p => p.Phone.Contains(vm.Phone));

            List<Constituent> list;
            if (vm.AllRecords)
            {
                list = context.Constituents.AsQueryable()
                    .Where(pred)
                    .OrderBy(x => x.Id)
                    //.ProjectTo<ConstituentViewModel>()
                    .ToList();
            }
            else
            {
                list = context.Constituents.AsQueryable()
                             .Order(vm.OrderBy, vm.OrderDirection == "desc" ? SortDirection.Descending : SortDirection.Ascending)
                             .Where(pred)
                             .Include(x => x.TaxItems)
                             .Skip(skipRows)
                             .Take(pageSize)
                             .ToList();
            }
            var totalCount = context.Constituents.Count();
            var filterCount = context.Constituents.Where(pred).Count();
            var totalPages = (int)Math.Ceiling((decimal)filterCount / pageSize);

            vm.TotalCount = totalCount;
            vm.FilteredCount = filterCount;
            vm.TotalPages = totalPages;

            vm.Items = list;
            return Ok(vm);
        }

        public IHttpActionResult Put(Constituent vm)
        {
            context.Constituents.AddOrUpdate(vm);
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