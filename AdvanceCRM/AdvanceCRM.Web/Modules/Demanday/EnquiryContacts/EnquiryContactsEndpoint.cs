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
        /// <summary>
        /// Blank import sheet with exactly the form's fields, in form order, under the header names
        /// ImportExcel reads. The dropdown columns carry a pick-list of the master values.
        /// Created By is left out: it is stamped from the signed-in user.
        /// </summary>
        [HttpPost, IgnoreAntiforgeryToken, AuthorizeList(typeof(EnquiryContactsRow))]
        public FileContentResult DownloadTemplate(IDbConnection connection)
        {
            string[] headers = new[]
            {
                "Company Name", "Account Type", "Country", "Status",
                "First Name", "Last Name", "Title", "Email", "Direct Phone Number", "Business Phone Number",
                "Industry", "Employee Size", "Profile Link", "Company Link", "Mode of Outreach", "Disposition", "Comments",
                "Date of Meeting", "Meeting Mode", "Status (Comments)", "Follow-up Date"
            };

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("EnquiryContacts");
            var lists = package.Workbook.Worksheets.Add("Lists");
            lists.Hidden = eWorkSheetHidden.Hidden;

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cells[1, i + 1];
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            int listCol = 1;
            void PickList(string header, IEnumerable<string> values)
            {
                var items = values.Where(v => !string.IsNullOrWhiteSpace(v)).ToList();
                if (items.Count == 0)
                    return;
                for (int i = 0; i < items.Count; i++)
                    lists.Cells[i + 1, listCol].Value = items[i];
                var source = lists.Cells[1, listCol, items.Count, listCol];
                var target = ws.Cells[2, Array.IndexOf(headers, header) + 1, 5000, Array.IndexOf(headers, header) + 1];
                var validation = ws.DataValidations.AddListValidation(target.Address);
                validation.Formula.ExcelFormula = "Lists!" + source.Address;
                validation.ShowErrorMessage = true;
                validation.Error = "Pick a value from the list.";
                listCol++;
            }

            PickList("Account Type", connection.List<Masters.ContactAccountTypeMasterRow>(q => q.Select(Masters.ContactAccountTypeMasterRow.Fields.Name).OrderBy(Masters.ContactAccountTypeMasterRow.Fields.Name)).Select(r => r.Name));
            PickList("Status", connection.List<Masters.ContactAccountStatusMasterRow>(q => q.Select(Masters.ContactAccountStatusMasterRow.Fields.Name).OrderBy(Masters.ContactAccountStatusMasterRow.Fields.Name)).Select(r => r.Name));
            PickList("Mode of Outreach", connection.List<Masters.ContactOutreachModeMasterRow>(q => q.Select(Masters.ContactOutreachModeMasterRow.Fields.Name).OrderBy(Masters.ContactOutreachModeMasterRow.Fields.Name)).Select(r => r.Name));
            PickList("Disposition", connection.List<Masters.ContactDispositionMasterRow>(q => q.Select(Masters.ContactDispositionMasterRow.Fields.Name).OrderBy(Masters.ContactDispositionMasterRow.Fields.Name)).Select(r => r.Name));
            PickList("Meeting Mode", connection.List<Masters.ContactMeetingModeMasterRow>(q => q.Select(Masters.ContactMeetingModeMasterRow.Fields.Name).OrderBy(Masters.ContactMeetingModeMasterRow.Fields.Name)).Select(r => r.Name));

            foreach (var dateHeader in new[] { "Date of Meeting", "Follow-up Date" })
                ws.Column(Array.IndexOf(headers, dateHeader) + 1).Style.Numberformat.Format = "dd-mmm-yyyy";

            ws.Cells[1, 1, 1, headers.Length].AutoFitColumns(14, 30);
            ws.View.FreezePanes(2, 1);

            return Serenity.Web.ExcelContentResult.Create(package.GetAsByteArray(),
                "EnquiryContacts_Template.xlsx");
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
                    // Account Number -> id, read once so the per-row lookup below costs nothing.
                    var accounts = ExcelImportHelper.LoadMasterAccountMap(uow.Connection);
                    // Dropdown masters are matched by name (case-insensitive), as typed in the sheet.
                    var accountTypes = LoadMasterMap<Masters.ContactAccountTypeMasterRow>(uow.Connection, Masters.ContactAccountTypeMasterRow.Fields.Id, Masters.ContactAccountTypeMasterRow.Fields.Name);
                    var accountStatuses = LoadMasterMap<Masters.ContactAccountStatusMasterRow>(uow.Connection, Masters.ContactAccountStatusMasterRow.Fields.Id, Masters.ContactAccountStatusMasterRow.Fields.Name);
                    var dispositions = LoadMasterMap<Masters.ContactDispositionMasterRow>(uow.Connection, Masters.ContactDispositionMasterRow.Fields.Id, Masters.ContactDispositionMasterRow.Fields.Name);
                    var meetingModes = LoadMasterMap<Masters.ContactMeetingModeMasterRow>(uow.Connection, Masters.ContactMeetingModeMasterRow.Fields.Id, Masters.ContactMeetingModeMasterRow.Fields.Name);
                    var outreachModes = LoadMasterMap<Masters.ContactOutreachModeMasterRow>(uow.Connection, Masters.ContactOutreachModeMasterRow.Fields.Id, Masters.ContactOutreachModeMasterRow.Fields.Name);
                    for (int row = 2; row <= rowCount; row++)
                    {
                        try
                        {
                            var enquirycontacts = new EnquiryContactsRow
                            {
                                Id = ExcelImportHelper.GetInt(ws, row, map, "Id"),
                                // Prefer the readable "Master Account No" that the template and the export now
                                // carry; fall back to a raw id column so older files still import.
                                MasterAccountId = ExcelImportHelper.GetMasterAccountId(ws, row, map, accounts,
                                        "Master Account No", "Account Number", "Account No")
                                    ?? ExcelImportHelper.GetInt(ws, row, map, "MasterAccountId", "Master Account Id"),
                                CampaignId = ExcelImportHelper.GetText(ws, row, map, "CampaignId", "Campaign Id"),
                                CompanyName = ExcelImportHelper.GetText(ws, row, map, "CompanyName", "Company Name"),
                                AccountTypeId = ResolveMaster(ws, row, map, accountTypes, "Account Type", "AccountType", "Vertical"),
                                AccountStatusId = ResolveMaster(ws, row, map, accountStatuses, "Status", "Account Status", "AccountStatus"),
                                DispositionId = ResolveMaster(ws, row, map, dispositions, "Disposition"),
                                Comments = ExcelImportHelper.GetText(ws, row, map, "Comments"),
                                L1Meeting = ExcelImportHelper.GetDate(ws, row, map, "Date of Meeting", "L1 Meeting", "L1Meeting"),
                                MeetingModeId = ResolveMaster(ws, row, map, meetingModes, "Meeting Mode", "MeetingMode"),
                                MeetingStatus = ExcelImportHelper.GetText(ws, row, map, "Status (Comments)", "Meeting Status", "MeetingStatus"),
                                FollowUpDate = ExcelImportHelper.GetDate(ws, row, map, "Follow-up Date", "Follow Up Date", "FollowUpDate"),
                                OutreachModeId = ResolveMaster(ws, row, map, outreachModes, "Mode of Outreach", "Outreach Mode"),
                                FirstName = ExcelImportHelper.GetText(ws, row, map, "FirstName", "First Name"),
                                LastName = ExcelImportHelper.GetText(ws, row, map, "LastName", "Last Name"),
                                Title = ExcelImportHelper.GetText(ws, row, map, "Title"),
                                Email = ExcelImportHelper.GetText(ws, row, map, "Email"),
                                WorkPhone = ExcelImportHelper.GetText(ws, row, map, "WorkPhone", "Work Phone", "Direct Phone Number", "Direct Phone"),
                                AlternativeNumber = ExcelImportHelper.GetText(ws, row, map, "AlternativeNumber", "Alternative Number", "Business Phone Number", "Business Phone"),
                                Street = ExcelImportHelper.GetText(ws, row, map, "Street"),
                                City = ExcelImportHelper.GetText(ws, row, map, "City"),
                                State = ExcelImportHelper.GetText(ws, row, map, "State"),
                                ZipCode = ExcelImportHelper.GetText(ws, row, map, "ZipCode", "Zip Code"),
                                Country = ExcelImportHelper.GetText(ws, row, map, "Country"),
                                CompanyEmployeeSize = ExcelImportHelper.GetText(ws, row, map, "CompanyEmployeeSize", "Company Employee Size", "Employee Size"),
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
        // Name -> Id of a dropdown master, matched case-insensitively.
        private static Dictionary<string, int> LoadMasterMap<TRow>(IDbConnection connection, Int32Field idField, StringField nameField)
            where TRow : class, IRow, new()
        {
            var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var r in connection.List<TRow>(q => q.Select(idField).Select(nameField)))
            {
                var name = nameField[r]?.Trim();
                if (!string.IsNullOrEmpty(name) && idField[r].HasValue)
                    map[name] = idField[r].Value;
            }
            return map;
        }

        // Blank cell -> no value; a name that is not in the master is a row error, not a silent skip.
        private static int? ResolveMaster(ExcelWorksheet ws, int row, Dictionary<string, int> map,
            Dictionary<string, int> master, params string[] headerNames)
        {
            var text = ExcelImportHelper.GetText(ws, row, map, headerNames)?.Trim();
            if (string.IsNullOrEmpty(text))
                return null;
            if (!master.TryGetValue(text, out var id))
                throw new Exception($"'{text}' is not in the {headerNames[0]} master.");
            return id;
        }

        private static int? GetInt(object val) { if (val == null) return null; int i; return int.TryParse(val.ToString(), out i) ? i : null; }
        private static decimal? GetDecimal(object val) { if (val == null) return null; decimal d; return decimal.TryParse(val.ToString(), out d) ? d : null; }
        private static DateTime? GetDate(object val) { if (val == null) return null; DateTime dt; return DateTime.TryParse(val.ToString(), out dt) ? dt : null; }

    }
}