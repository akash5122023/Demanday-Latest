
namespace AdvanceCRM.Masters.Endpoints
{
    using AdvanceCRM.Masters;
    using OfficeOpenXml;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using AdvanceCRM.Web.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;

    using MyRepository = Repositories.TehsilRepository;
    using MyRow = TehsilRow;

    [Route("Services/Masters/Tehsil/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class TehsilController : ServiceEndpoint
    {
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

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.TehsilColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Tehsil_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        //Excel import contacts
        [HttpPost, ServiceAuthorize("Masters:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, ExcelImportRequest request)
        {
            Check.NotNull(request, "request");
            Check.NotNullOrWhiteSpace(request.FileName, "filename");

            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/"))
                throw new ArgumentOutOfRangeException("filename");

            ExcelPackage ep = new ExcelPackage();
            using (var fs = new FileStream(UploadHelper.DbFilePath(request.FileName), FileMode.Open, FileAccess.Read))
                ep.Load(fs);

            var sr = StateRow.Fields;
            var cr = CityRow.Fields;
            var tr = TehsilRow.Fields;

            var response = new ExcelImportResponse();
            response.ErrorList = new List<string>();

            var worksheet = ep.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new ValidationError("Uploaded excel file does not contain any worksheet");
            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var stateName = Convert.ToString(worksheet.Cells[row, 1].Value ?? "");
                    if (stateName.IsTrimmedEmpty())
                        continue;

                    var state = uow.Connection.TryFirst<StateRow>(q => q
                            .Select(sr.Id)
                            .Where(sr.State == stateName));

                    if (state == null)
                    {
                        state = new StateRow();
                        state.State = stateName;
                        var sub = new StateController();

                        SaveResponse changes = sub.Create(uow, new SaveWithLocalizationRequest<StateRow>
                        {
                            Entity = state
                        });
                        state.Id = (int)changes.EntityId;
                    }

                    var cityName = Convert.ToString(worksheet.Cells[row, 2].Value ?? "");
                    if (cityName.IsTrimmedEmpty())
                        continue;

                    var city = uow.Connection.TryFirst<CityRow>(q => q
                            .Select(cr.Id)
                            .Where(cr.City == cityName && cr.StateId == state.Id.Value));

                    if (city == null)
                    {
                        city = new CityRow();
                        city.City = cityName;
                        city.StateId = state.Id;
                        var sub = new CityController();

                        SaveResponse changes = sub.Create(uow, new SaveWithLocalizationRequest<CityRow>
                        {
                            Entity = city
                        });
                        city.Id = (int)changes.EntityId;
                    }
                    else
                    {
                        city.StateId = state.Id;
                        var sub = new CityController();
                        sub.Update(uow, new SaveWithLocalizationRequest<CityRow>
                        {
                            Entity = city,
                            EntityId = city.Id.Value
                        });
                    }

                    var tehsilName = Convert.ToString(worksheet.Cells[row, 3].Value ?? "");
                    if (tehsilName.IsTrimmedEmpty())
                        continue;

                    var tehsil = uow.Connection.TryFirst<TehsilRow>(q => q
                            .Select(tr.Id)
                            .Where(tr.Tehsil == tehsilName && tr.StateId == state.Id.Value && tr.CityId == city.Id.Value));

                    if (tehsil == null)
                    {
                        tehsil = new TehsilRow();
                        tehsil.Tehsil = tehsilName;
                        tehsil.StateId = state.Id;
                        tehsil.CityId = city.Id;
                        var sub = new TehsilController();
                        SaveResponse changes = sub.Create(uow, new SaveWithLocalizationRequest<TehsilRow>
                        {
                            Entity = tehsil
                        });
                        tehsil.Id = (int)changes.EntityId;

                        response.Inserted = response.Inserted + 1;
                    }
                    else
                    {
                        tehsil.StateId = state.Id;
                        tehsil.CityId = city.Id;
                        var sub = new TehsilController();
                        sub.Update(uow, new SaveWithLocalizationRequest<TehsilRow>
                        {
                            Entity = tehsil,
                            EntityId = tehsil.Id.Value
                        });

                        response.Updated = response.Updated + 1;
                    }

                }
                catch (Exception ex)
                {
                    response.ErrorList.Add("Exception on Row " + row + ": " + ex.Message);
                }
            }

            return response;
        }

    }
}
