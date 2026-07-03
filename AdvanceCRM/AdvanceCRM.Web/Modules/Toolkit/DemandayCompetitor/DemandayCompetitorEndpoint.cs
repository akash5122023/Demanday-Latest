using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using AdvanceCRM.Web.Helpers;
using AdvanceCRM.Masters;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using MyRow = AdvanceCRM.Toolkit.DemandayCompetitorRow;

namespace AdvanceCRM.Toolkit.Endpoints
{
    [Route("Services/Toolkit/DemandayCompetitor/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayCompetitorController : ServiceEndpoint
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public DemandayCompetitorController(
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            UploadHelper.Configure(configuration, env);
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayCompetitorSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayCompetitorSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayCompetitorDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayCompetitorRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayCompetitorListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayCompetitorListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandayCompetitorColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandayCompetitorList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        [HttpPost, ServiceAuthorize("DemandayCompetitor:Import")]
        public ActionResult DownloadTemplate(IDbConnection connection, RetrieveRequest request)
        {
            string[] headers = { "Company Name", "Domain", "Email", "CPC" };

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("DemandayCompetitor");
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = ws.Cells[1, i + 1];
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    ws.Column(i + 1).Width = 25;
                }

                byte[] bytes = package.GetAsByteArray();
                return File(bytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "DemandayCompetitor_Template.xlsx");
            }
        }

        [HttpPost, ServiceAuthorize("DemandayCompetitor:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, DemandayCompetitorExcelImportRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentNullException(nameof(request.FileName));
            if (request.CampaignId == null)
                throw new ValidationError("Please select a Campaign before importing");

            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentOutOfRangeException("filename");

            // The Team Leader selects the Campaign in the dialog; every imported row is tagged with it
            // (and its parent Master Account).
            var campaign = uow.Connection.TryById<DemandayCampaignIdRow>(request.CampaignId.Value);
            if (campaign == null)
                throw new ValidationError("Selected campaign was not found");

            string physicalPath = UploadHelper.DbFilePath(request.FileName);

            ExcelPackage ep = new ExcelPackage();
            using (var fs = new FileStream(physicalPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ep.Load(fs);
            }

            var response = new ExcelImportResponse();
            response.Inserted = 0;
            response.Updated = 0;
            response.ErrorList = new List<string>();

            var worksheet = ep.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new ValidationError("Uploaded excel file does not contain any worksheet");

            int ownerId = Convert.ToInt32(Context.User.GetIdentifier());

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var companyName = Convert.ToString(worksheet.Cells[row, 1].Value ?? "").Trim();
                    var domain = Convert.ToString(worksheet.Cells[row, 2].Value ?? "").Trim();
                    var email = Convert.ToString(worksheet.Cells[row, 3].Value ?? "").Trim();
                    var cpcStr = Convert.ToString(worksheet.Cells[row, 4].Value ?? "").Trim();

                    if (string.IsNullOrEmpty(companyName) && string.IsNullOrEmpty(domain) && string.IsNullOrEmpty(email))
                        continue;

                    long? cpc = null;
                    if (!string.IsNullOrEmpty(cpcStr) && long.TryParse(cpcStr, out long parsedCpc))
                        cpc = parsedCpc;

                    var newRow = new MyRow
                    {
                        CompanyName = companyName,
                        Domain = domain,
                        Email = email,
                        Cpc = cpc,
                        CampaignId = campaign.Id,
                        MasterAccountId = campaign.DemandayMasterAccountId,
                        OwnerId = ownerId
                    };

                    uow.Connection.Insert(newRow);
                    response.Inserted++;
                }
                catch (Exception ex)
                {
                    response.ErrorList.Add($"Row {row}: {ex.Message}");
                }
            }

            return response;
        }
    }
}