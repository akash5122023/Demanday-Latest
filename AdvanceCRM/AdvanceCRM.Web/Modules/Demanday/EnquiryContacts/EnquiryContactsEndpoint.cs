using AdvanceCRM.Web.Modules.Common.AppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using MyRow = AdvanceCRM.Demanday.EnquiryContactsRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/EnquiryContacts/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class EnquiryContactsController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IEnquiryContactsSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IEnquiryContactsSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IEnquiryContactsDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IEnquiryContactsRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IEnquiryContactsListHandler handler)
        {
            return handler.List(connection, request);
        }

        [HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(EnquiryContactsRow))]
        public FileContentResult ListExcel(
            IDbConnection connection,
            [FromForm] ListRequest request,
            [FromForm] string Ids,
            [FromServices] IEnquiryContactsListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            request ??= new ListRequest { Take = 0 }; // Defensive: always have a request
            var data = List(connection, request, handler).Entities.ToList();
            if (!string.IsNullOrWhiteSpace(Ids))
            {
                var idList = Ids.Split(',').Select(x =>
                {
                    int v; return int.TryParse(x.Trim(), out v) ? (int?)v : null;
                }).Where(x => x.HasValue).Select(x => x!.Value).ToHashSet();
                if (idList.Count > 0)
                    data = data.Where(x => x.Id.HasValue && idList.Contains(x.Id.Value)).ToList();
            }
            var bytes = exporter.Export(data, typeof(Columns.EnquiryContactsColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "EnquiryContactsList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
        [HttpPost, IgnoreAntiforgeryToken]
        [RequestSizeLimit(52428800)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file, [FromServices] IEnquiryContactsSaveHandler saveHandler)
        {
            try
            {
                if (file == null || !file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    return Content("Please upload a valid .xlsx file.", "text/plain");
                int imported = 0, skipped = 0, failed = 0;
                var errors = new List<string>();
                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    var ws = package.Workbook.Worksheets[0];
                    int rowCount = ws.Dimension.End.Row;
                    var map = ExcelImportHelper.BuildHeaderMap(ws);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            var enquirycontacts = new EnquiryContactsRow
                            {
                                Id = ExcelImportHelper.GetInt(ws, row, map, "Id"),
                                CampaignId = ExcelImportHelper.GetText(ws, row, map, "CampaignId", "Campaign Id"),
                                CompanyName = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name"),
                                FirstName = ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"),
                                LastName = ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"),
                                Title = ExcelImportHelper.GetText(ws, row, map, "Title"),
                                Email = ExcelImportHelper.GetText(ws, row, map, "Email"),
                                WorkPhone = ExcelImportHelper.GetText(ws, row, map, "WorkPhone", "Work Phone"),
                                AlternativeNumber = ExcelImportHelper.GetText(ws, row, map, "AlternativeNumber", "Alternative Number"),
                                Street = ExcelImportHelper.GetText(ws, row, map, "Street"),
                                City = ExcelImportHelper.GetText(ws, row, map, "City"),
                                State = ExcelImportHelper.GetText(ws, row, map, "State"),
                                ZipCode = ExcelImportHelper.GetText(ws, row, map, "ZipCode", "Zip Code"),
                                Country = ExcelImportHelper.GetText(ws, row, map, "Country"),
                                CompanyEmployeeSize = ExcelImportHelper.GetText(ws, row, map, "CompanyEmployeeSize", "Company Employee Size"),
                                Industry = ExcelImportHelper.GetText(ws, row, map, "Industry"),
                                SubIndustry = ExcelImportHelper.GetText(ws, row, map, "SubIndustry", "Sub Industry"),
                                Revenue = ExcelImportHelper.GetText(ws, row, map, "Revenue"),
                                ProfileLink = ExcelImportHelper.GetText(ws, row, map, "ProfileLink", "Profile Link"),
                                CompanyLink = ExcelImportHelper.GetText(ws, row, map, "CompanyLink", "Company Link"),
                                RevenueLink = ExcelImportHelper.GetText(ws, row, map, "RevenueLink", "Revenue Link"),
                                EmailFormat = ExcelImportHelper.GetText(ws, row, map, "EmailFormat", "Email Format"),
                                AdressLink = ExcelImportHelper.GetText(ws, row, map, "AdressLink", "Adress Link", "AddressLink", "Address Link"),
                                Tenurity = ExcelImportHelper.GetText(ws, row, map, "Tenurity"),
                                Code = ExcelImportHelper.GetText(ws, row, map, "Code"),
                                Link = ExcelImportHelper.GetText(ws, row, map, "Link"),
                                Md5 = ExcelImportHelper.GetText(ws, row, map, "Md5", "MD5"),
                                OwnerId = ExcelImportHelper.GetUserId(ws, row, map, uow.Connection, "OwnerId", "Owner", "Owner Name", "OwnerName", "Created By", "CreatedBy")
                            };
                            if (enquirycontacts.Id.HasValue && enquirycontacts.Id.Value > 0)
                            {
                                skipped++; continue;
                            }
                            ExcelImportHelper.ClampStringFields(enquirycontacts);
                            var creReq = new Serenity.Services.SaveRequest<EnquiryContactsRow> { Entity = enquirycontacts };
                            saveHandler.Create(uow, creReq);
                            imported++;
                        }
                        catch (Exception ex)
                        {
                            failed++; errors.Add($"Row {row}: {ex.Message}");
                        }
                    }
                }
                if (imported == 0 && failed > 0)
                    return Content("All rows failed to import.\n" + string.Join("\n", errors), "text/plain");
                return Content($"Added: {imported}, Skipped (existing IDs): {skipped}, Failed: {failed}\n" + (errors.Count > 0 ? string.Join("\n", errors) : ""), "text/plain");
            }
            catch (Exception ex)
            {
                return Content("Import failed: " + ex.Message + "\n" + ex.StackTrace, "text/plain");
            }
        }
        private static int? GetInt(object val) { if (val == null) return null; int i; return int.TryParse(val.ToString(), out i) ? i : null; }
        private static decimal? GetDecimal(object val) { if (val == null) return null; decimal d; return decimal.TryParse(val.ToString(), out d) ? d : null; }
        private static DateTime? GetDate(object val) { if (val == null) return null; DateTime dt; return DateTime.TryParse(val.ToString(), out dt) ? dt : null; }

    }
}