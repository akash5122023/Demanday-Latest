using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Data;
using System.Globalization;
using MyRow = AdvanceCRM.Masters.DemandayCountryMasterRow;

namespace AdvanceCRM.Masters.Endpoints
{
    [Route("Services/Masters/DemandayCountryMaster/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayCountryMasterController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayCountryMasterSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayCountryMasterSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayCountryMasterDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayCountryMasterRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayCountryMasterListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayCountryMasterListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandayCountryMasterColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandayCountryMasterList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        [HttpPost, IgnoreAntiforgeryToken]
        [RequestSizeLimit(52428800)]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        public IActionResult ImportExcel([FromServices] IUnitOfWork uow, IFormFile file)
        {
            if (file == null || !file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                return Content("Please upload a valid .xlsx file.", "text/plain");

            var fld = MyRow.Fields;
            var existing = new System.Collections.Generic.HashSet<string>(
                uow.Connection.List<MyRow>(q => q.Select(fld.Name))
                    .Where(r => r.Name != null).Select(r => r.Name.Trim()),
                StringComparer.OrdinalIgnoreCase);

            int imported = 0, skipped = 0;
            foreach (var val in MasterExcelImportHelper.ReadColumnValues(file))
            {
                var v = val.Trim();
                if (v.Length == 0 || existing.Contains(v)) { skipped++; continue; }
                uow.Connection.Insert(new MyRow { Name = v });
                existing.Add(v);
                imported++;
            }
            return Content("Imported " + imported + ", skipped " + skipped + " duplicate/blank value(s).", "text/plain");
        }
    }
}