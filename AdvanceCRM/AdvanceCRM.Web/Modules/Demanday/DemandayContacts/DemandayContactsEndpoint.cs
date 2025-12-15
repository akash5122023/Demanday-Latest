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
using MyRow = AdvanceCRM.Demanday.DemandayContactsRow;

namespace AdvanceCRM.Demanday.Endpoints
{
    [Route("Services/Demanday/DemandayContacts/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayContactsController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayContactsSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayContactsSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayContactsDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayContactsRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayContactsListHandler handler)
        {
            return handler.List(connection, request);
        }
        [HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(DemandayContactsRow))]
        public FileContentResult ListExcel(
        IDbConnection connection,
        [FromForm] ListRequest request, // Bind from form POSTs
        [FromForm] string Ids,
        [FromServices] IDemandayContactsListHandler handler)
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
            var bytes = AdvanceCRM.Web.Modules.Common.AppServices.DemandayContactsExcelExporter.ExportToExcel(data);
            var fileName = "DemandayContactsList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".xlsx";
            return Serenity.Web.ExcelContentResult.Create(bytes, fileName);
        }
        [HttpPost, IgnoreAntiforgeryToken]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file, [FromServices] IDemandayContactsSaveHandler saveHandler)
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
                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            var demandaycontacts = new DemandayContactsRow
                            {
                                Id = GetInt(ws.Cells[row, 1].Value),
                                Slot = ws.Cells[row, 2].Text,
                                CompanyName = ws.Cells[row, 3].Text,
                                FirstName = ws.Cells[row, 4].Text,
                                LastName = ws.Cells[row, 5].Text,
                                Domain = ws.Cells[row, 6].Text,
                                Title = ws.Cells[row, 7].Text,
                                JobLevel = ws.Cells[row, 8].Text,
                                JobFunctionRole = ws.Cells[row, 9].Text,
                                Email = ws.Cells[row, 10].Text,
                                WorkPhone = ws.Cells[row, 11].Text,
                                AlternativeNumber = ws.Cells[row, 12].Text,
                                Street = ws.Cells[row, 13].Text,
                                City = ws.Cells[row, 14].Text,
                                State = ws.Cells[row, 15].Text,
                                ZipCode = ws.Cells[row, 16].Text,
                                Country = ws.Cells[row, 17].Text,
                                Continents = ws.Cells[row, 18].Text,
                                Industry = ws.Cells[row, 19].Text,
                                Revenue = GetDecimal(ws.Cells[row, 20].Value),
                                CompanyEmployeeSize = GetInt(ws.Cells[row, 21].Value),
                                ProfileLink = ws.Cells[row, 22].Text,
                                CompanyLink = ws.Cells[row, 23].Text,
                                RevenueLink = ws.Cells[row, 24].Text,
                                EmailFormat = ws.Cells[row, 25].Text,
                                AdressLink = ws.Cells[row, 26].Text,
                                ProspectUrl = ws.Cells[row, 27].Text,
                                PrimaryReason = ws.Cells[row, 28].Text,
                                Category = ws.Cells[row, 29].Text,
                                Comments = ws.Cells[row, 30].Text,
                                QaStatus = ws.Cells[row, 31].Text,
                                DeliveryStatus = ws.Cells[row, 32].Text,
                                AgentName = ws.Cells[row, 33].Text,
                                QaName = ws.Cells[row, 34].Text,
                                CallDate = GetDate(ws.Cells[row, 35].Value),
                                DateAudited = GetDate(ws.Cells[row, 36].Value),
                                DeliveryDate = GetDate(ws.Cells[row, 37].Value),
                                Source = ws.Cells[row, 38].Text,
                                VerificationMode = ws.Cells[row, 39].Text,
                                Asset1 = ws.Cells[row, 40].Text,
                                Asset2 = ws.Cells[row, 41].Text,
                                TlName = ws.Cells[row, 42].Text,
                                Tenurity = ws.Cells[row, 43].Text,
                                Code = ws.Cells[row, 44].Text,
                                Link = ws.Cells[row, 45].Text,
                                Md5 = ws.Cells[row, 46].Text,
                                OwnerUsername = ws.Cells[row, 47].Text
                            };
                            if (demandaycontacts.Id.HasValue && demandaycontacts.Id.Value > 0)
                            {
                                skipped++; continue;
                            }
                            var creReq = new Serenity.Services.SaveRequest<DemandayContactsRow> { Entity = demandaycontacts };
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