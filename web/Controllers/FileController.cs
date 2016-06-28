using DonorGateway.Data;
using DonorGateway.Domain;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using CsvHelper;
using CsvHelper.Configuration;
using EntityFramework.Utilities;
using web.Services;

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
        public IHttpActionResult Guest(int id)
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

                var configuration = new CsvConfiguration()
                {
                    IsHeaderCaseSensitive = false,
                    WillThrowOnMissingField = false,
                    IgnoreReadingExceptions = true,
                    ThrowOnBadData = false,
                    SkipEmptyRecords = true
                };
                var csv = new CsvReader(new StreamReader(filePath, Encoding.Default, true), configuration);
                csv.Configuration.RegisterClassMap<GuestMap>();
                var list = csv.GetRecords<Guest>().ToList();
                foreach (var guest in list)
                {
                    guest.EventId = id;
                }
                using (context)
                {
                    EFBatchOperation.For(context, context.Guests).InsertAll(list);
                }
                csv.Dispose();
                return Ok(list);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }

    public sealed class GuestMap : CsvClassMap<Guest>
    {
        public GuestMap()
        {
            Map(m => m.AccountId).Name("Account ID");
            Map(m => m.Name).Name("Account Name", "account name", "ACCOUNT NAME");
            Map(m => m.GuestType).Name("Account Type", "account type", "ACCOUNT TYPE", "accounttype", "ACCOUNTTYPE");
            Map(m => m.Phone).Name("Phone", "PHONE", "phone", "Phone Number", "phone number", "PHONE NUMBER", "phone number", "phonenumber", "PHONENUMBER");
            Map(m => m.Email).Name("EmailAddress", "EMAILADDRESS", "Email Address", "emailaddress", "email address", "Email", "email");
            Map(m => m.AccountId)
                .Name("Account ID", "AccountID", "AccountId", "Account Id", "LookupId", "LookupID", "Lookup Id", "Lookup ID", "lookupid", "lookup id", "lookup Id", "lookup ID");
            Map(m => m.FinderNumber)
                .Name("FinderNumber", "Finder Number", "finder number", "findernumber", "FinderNumber", "Findernumber");
            Map(m => m.Street).Name("Address line1", "Address line 1", "Addressline1", "address line1", "address line 1", "addressline1", "address line1", "Address Line 1", "AddressLine1", "Address Line1");
            Map(m => m.Street2).Name("Address line2", "Address line 2", "Addressline2", "address line2", "address line 2", "addressline2", "address line2", "Address Line 2", "AddressLine2", "Address Line2");
            Map(m => m.Street3).Name("Address line3", "Address line 3", "Addressline3", "address line3", "address line 3", "addressline3", "address line3", "Address Line 3", "AddressLine3", "Address Line3");
            Map(m => m.City).Name("City", "city", "CITY");
            Map(m => m.State).Name("State", "state", "STATE");
            Map(m => m.Zipcode).Name("ZIP", "Zip", "zip", "ZipCode", "Zipcode", "zipcode", "POSTCODE", "PostCode", "Post Code", "post code");

        }
    }
}