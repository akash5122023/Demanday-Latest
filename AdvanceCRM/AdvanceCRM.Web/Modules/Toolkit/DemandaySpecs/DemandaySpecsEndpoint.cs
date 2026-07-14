using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using AdvanceCRM.Web.Helpers;
using AdvanceCRM.Toolkit;
using AdvanceCRM.Masters;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using MyRow = AdvanceCRM.Toolkit.DemandaySpecsRow;

namespace AdvanceCRM.Toolkit.Endpoints
{
    [Route("Services/Toolkit/DemandaySpecs/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandaySpecsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public DemandaySpecsController(
            ISqlConnections connections,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _connections = connections;
            _configuration = configuration;
            _env = env;
            UploadHelper.Configure(configuration, env);

            // Ensure template file exists and is valid
            string templatePath = Path.Combine(env.ContentRootPath, "Templates", "DemandaySpecs_Template.xlsx");
            DemandaySpecsTemplateInitializer.EnsureTemplateExists(templatePath);
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandaySpecsSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandaySpecsSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandaySpecsDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandaySpecsRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandaySpecsListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandaySpecsListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandaySpecsColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandaySpecsList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        [HttpPost, ServiceAuthorize("DemandaySpecs:Import")]
        public ActionResult DownloadTemplate(IDbConnection connection, RetrieveRequest request)
        {
            string templateFile = Path.Combine(_env.ContentRootPath, "Templates", "DemandaySpecs_Template.xlsx");
            byte[] bytes = System.IO.File.ReadAllBytes(templateFile);

            var Output = File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "DemandaySpecs_Template.xlsx");
            return Output;
        }

        [HttpPost, ServiceAuthorize("DemandaySpecs:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, DemandaySpecsExcelImportRequest request)
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

            // SrNo (column 1) is the upsert key: a row whose SrNo already exists is updated, not
            // duplicated. Preload the existing SrNo -> Id map once so the loop stays a single query each.
            var idBySrNo = uow.Connection.List<MyRow>(q => q
                    .Select(MyRow.Fields.Id).Select(MyRow.Fields.SrNo)
                    .Where(new Criteria(MyRow.Fields.SrNo).IsNotNull()))
                .Where(r => r.SrNo.HasValue)
                .GroupBy(r => r.SrNo.Value)
                .ToDictionary(g => g.Key, g => g.First().Id.Value);
            int maxSrNo = idBySrNo.Count == 0 ? 0 : idBySrNo.Keys.Max();

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var srNoStr = Convert.ToString(worksheet.Cells[row, 1].Value ?? "").Trim();
                    var orderId = Convert.ToString(worksheet.Cells[row, 2].Value ?? "").Trim();
                    var jobTitle = Convert.ToString(worksheet.Cells[row, 3].Value ?? "").Trim();
                    var jobLevel = Convert.ToString(worksheet.Cells[row, 4].Value ?? "").Trim();
                    var jobFunction = Convert.ToString(worksheet.Cells[row, 5].Value ?? "").Trim();
                    var industry = Convert.ToString(worksheet.Cells[row, 6].Value ?? "").Trim();
                    var companyEmployeeSize = Convert.ToString(worksheet.Cells[row, 7].Value ?? "").Trim();
                    var annualRevenue = Convert.ToString(worksheet.Cells[row, 8].Value ?? "").Trim();
                    var excludeCompany = Convert.ToString(worksheet.Cells[row, 9].Value ?? "").Trim();
                    var address = Convert.ToString(worksheet.Cells[row, 10].Value ?? "").Trim();
                    var city = Convert.ToString(worksheet.Cells[row, 11].Value ?? "").Trim();
                    var state = Convert.ToString(worksheet.Cells[row, 12].Value ?? "").Trim();
                    var zipCode = Convert.ToString(worksheet.Cells[row, 13].Value ?? "").Trim();
                    var country = Convert.ToString(worksheet.Cells[row, 14].Value ?? "").Trim();
                    var comments = Convert.ToString(worksheet.Cells[row, 15].Value ?? "").Trim();
                    var additionalNotes = Convert.ToString(worksheet.Cells[row, 16].Value ?? "").Trim();

                    // Skip empty rows (at least JobTitle or OrderId should have a value)
                    if (string.IsNullOrEmpty(jobTitle) && string.IsNullOrEmpty(orderId))
                        continue;

                    int? srNo = null;
                    if (!string.IsNullOrEmpty(srNoStr))
                    {
                        if (!int.TryParse(srNoStr, out var parsedSrNo))
                        {
                            response.ErrorList.Add($"Row {row}: Sr No '{srNoStr}' is not a valid number");
                            continue;
                        }
                        srNo = parsedSrNo;
                    }

                    // Blank SrNo => a brand-new row gets the next free number.
                    if (!srNo.HasValue)
                        srNo = ++maxSrNo;
                    else if (srNo.Value > maxSrNo)
                        maxSrNo = srNo.Value;

                    var data = new MyRow
                    {
                        SrNo = srNo,
                        OrderId = string.IsNullOrEmpty(orderId) ? (long?)null : Convert.ToInt64(orderId),
                        JobTitle = jobTitle,
                        JobLevel = jobLevel,
                        JobFunction = jobFunction,
                        Industry = industry,
                        CompanyEmployeeSize = companyEmployeeSize,
                        AnnualRevenue = annualRevenue,
                        ExcludeCompany = excludeCompany,
                        Address = address,
                        City = city,
                        State = state,
                        ZipCode = zipCode,
                        Country = country,
                        Comments = comments,
                        AdditionalNotes = additionalNotes,
                        CampaignId = campaign.Id,
                        MasterAccountId = campaign.DemandayMasterAccountId
                    };

                    if (idBySrNo.TryGetValue(srNo.Value, out var existingId))
                    {
                        // UpdateById only writes assigned fields, so OwnerId (creator) is preserved.
                        data.Id = existingId;
                        uow.Connection.UpdateById(data);
                        response.Updated++;
                    }
                    else
                    {
                        data.OwnerId = ownerId;
                        var newId = (int)uow.Connection.InsertAndGetID(data);
                        idBySrNo[srNo.Value] = newId;
                        response.Inserted++;
                    }
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