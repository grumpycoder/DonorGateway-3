using DonorGateway.Data;
using DonorGateway.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using web.Helpers;
using web.Services;
using web.ViewModels;

namespace web.Controllers
{
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        private readonly DataContext context;

        public FileController()
        {
            context = new DataContext();
        }


        [HttpPost, Route("guest/{id:int}")]
        public IHttpActionResult Guest()
        {
            var httpRequest = HttpContext.Current.Request;

            var result = new OperationResult();
            try
            {
                var postedFile = httpRequest.Files[0];
                // Fix for IE file path issue.
                var filename = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("\\") + 1);
                var filePath = HttpContext.Current.Server.MapPath(@"~\app_data\" + filename);
                postedFile.SaveAs(filePath);

                var processor = new GuestProcessor(filePath);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Success");
        }
       
    }
}