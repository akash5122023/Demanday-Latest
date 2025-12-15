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

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IEnquiryContactsListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.EnquiryContactsColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "EnquiryContactsList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
        [HttpPost, IgnoreAntiforgeryToken]
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
                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            var enquirycontacts = new EnquiryContactsRow
                            {
                                Id = GetInt(ws.Cells[row, 1].Value),
                                CompanyName = ws.Cells[row, 2].Text,
                                FirstName = ws.Cells[row, 3].Text,
                                LastName = ws.Cells[row, 4].Text,
                                Title = ws.Cells[row, 5].Text,
                                Email = ws.Cells[row, 6].Text,
                                WorkPhone = ws.Cells[row, 7].Text,
                                AlternativeNumber = ws.Cells[row, 8].Text,
                                Street = ws.Cells[row, 9].Text,
                                City = ws.Cells[row, 10].Text,
                                State = ws.Cells[row, 11].Text,
                                ZipCode = ws.Cells[row, 12].Text,
                                Country = ws.Cells[row, 13].Text,
                                CompanyEmployeeSize = GetInt(ws.Cells[row, 14].Value),
                                Industry = ws.Cells[row, 15].Text,
                                Revenue = GetDecimal(ws.Cells[row, 16].Value),
                                ProfileLink = ws.Cells[row, 17].Text,
                                CompanyLink = ws.Cells[row, 18].Text,
                                RevenueLink = ws.Cells[row, 19].Text,
                                EmailFormat = ws.Cells[row, 20].Text,
                                AdressLink = ws.Cells[row, 21].Text,
                                Tenurity = ws.Cells[row, 22].Text,
                                Code = ws.Cells[row, 23].Text,
                                Link = ws.Cells[row, 24].Text,
                                Md5 = ws.Cells[row, 24].Text,
                                OwnerUsername = ws.Cells[row, 26].Text
                            };
                            if (enquirycontacts.Id.HasValue && enquirycontacts.Id.Value > 0)
                            {
                                skipped++; continue;
                            }
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