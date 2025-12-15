using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using MyRepository = AdvanceCRM. Administration.Repositories.CompanyDetailsRepository;
using MyRow = AdvanceCRM.Administration.CompanyDetailsRow;

namespace AdvanceCRM.Administration.Endpoints
{
    [Route("Services/Administration/CompanyDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class CompanyDetailsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public CompanyDetailsController(ISqlConnections connections, IConfiguration configuration, IWebHostEnvironment env)
        {
            _connections = connections;
            _configuration = configuration;
            _env = env;
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MyRepository(Context).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MyRepository(Context).Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyRepository(Context).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRepository(Context).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context).List(connection, request);
        }

        // Bulk Data Transfer
        [HttpPost]
        [ServiceAuthorize(PermissionKeys.BulkTransfer)]
        public StandardResponse BulkTransfer(IUnitOfWork uow, TransferRequest request)
        {
            var response = new StandardResponse();
            var connection = _connections.NewByKey("Default");

            try
            {
                string fromId = request.FromID.ToString();
                string toId = request.ToID.ToString();

                void ExecuteTransfer(string table, string column)
                {
                    new SqlUpdate(table)
                        .Set(column, toId)
                        .Where($"{column} = {fromId}")
                        .Execute(connection, ExpectedRows.Ignore);
                }

                switch (request.Type)
                {
                    case "1": // Enquiry
                        ExecuteTransfer("dbo.Enquiry", "AssignedId");
                        ExecuteTransfer("dbo.EnquiryAppointments", "RepresentativeId");
                        break;
                    case "2": // Quotation
                        ExecuteTransfer("dbo.Quotation", "AssignedId");
                        ExecuteTransfer("dbo.QuotationAppointments", "RepresentativeId");
                        break;
                    case "3": // Proforma
                        ExecuteTransfer("dbo.Invoice", "AssignedId");
                        ExecuteTransfer("dbo.InvoiceAppointments", "RepresentativeId");
                        break;
                    case "4": // Sales
                        ExecuteTransfer("dbo.Sales", "AssignedId");
                        ExecuteTransfer("dbo.SalesAppointments", "RepresentativeId");
                        break;
                    case "5": // Contacts
                        ExecuteTransfer("dbo.Contacts", "AssignedId");
                        break;
                    case "6": // CMS
                        ExecuteTransfer("dbo.CMS", "AssignedTo");
                        break;
                    case "7": // AMC
                        ExecuteTransfer("dbo.AMC", "AssignedId");
                        ExecuteTransfer("dbo.AMCVisitPlanner", "RepresentativeId");
                        break;
                    default:
                        response.Status = "Invalid transfer type!";
                        return response;
                }

                response.Status = "Bulk transfer completed successfully!";
            }
            catch (Exception ex)
            {
                response.Status = "Error transferring data: " + ex.Message;
            }

            return response;
        }

        // Database Backup
        [HttpPost]
        [ServiceAuthorize("Administration:Database Backup")]
        public FileContentResult Backup()
        {
            string backupDir = Path.Combine(_env.ContentRootPath, "App_Data", "DBBackups");
            string fileName = DateTime.Now.ToString("dd-MMM-yyyy") + ".Bak";
            string filePath = Path.Combine(backupDir, fileName);

            try
            {
                var connectionString = _configuration.GetConnectionString("Default");

                if (!Directory.Exists(backupDir))
                    Directory.CreateDirectory(backupDir);

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var builder = new SqlConnectionStringBuilder(connectionString);
                    string database = builder.InitialCatalog;

                    using (var command = new SqlCommand($"BACKUP DATABASE [{database}] TO DISK = @path", connection))
                    {
                        command.Parameters.AddWithValue("@path", filePath);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Backup failed: " + ex.Message);
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);

            return new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = fileName
            };
        }
    }
}
