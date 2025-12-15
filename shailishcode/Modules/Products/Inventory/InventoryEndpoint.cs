using AdvanceCRM.Web.Helpers;
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
using System.IO;
using MyRow = AdvanceCRM.Products.InventoryRow;

namespace AdvanceCRM.Products.Endpoints
{
    [Route("Services/Products/Inventory/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class InventoryController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IInventorySaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IInventorySaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IInventoryDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }
        [HttpPost, ServiceAuthorize("Products:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, ExcelImportRequest request)
        {

            int comid = ((UserDefinition)Serenity.Authorization.UserDefinition).CompanyId;
            request.CheckNotNull();
            Check.NotNullOrWhiteSpace(request.FileName, "filename");

            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/"))
                throw new ArgumentOutOfRangeException("filename");

            ExcelPackage ep = new ExcelPackage();
            using (var fs = new FileStream(UploadHelper.DbFilePath(request.FileName), FileMode.Open, FileAccess.Read))
                ep.Load(fs);

            var p = MyRow.Fields;
            var d = Masters.ProductsDivisionRow.Fields;
            var g = Masters.ProductsGroupRow.Fields;
            var t = Masters.TaxRow.Fields;
            var u = Masters.ProductsUnitRow.Fields;

            var response = new ExcelImportResponse();
            response.ErrorList = new List<string>();

            var worksheet = ep.Workbook.Worksheets[1];
            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                // try
                {

                    var productName = Convert.ToString(worksheet.Cells[row, 1].Value ?? "");
                    if (productName.IsTrimmedEmpty())
                        continue;

                    var product = uow.Connection.TryFirst<MyRow>(q => q
                        .Select(p.Id)
                        .Where(p.Name == productName)
                        .Where(p.CompanyId == comid)
                       );

                    if (product == null)
                        product = new MyRow
                        {
                            Name = productName
                        };
                    else
                    {
                        // avoid assignment errors    
                        product.TrackWithChecks = false;
                    }
                    product.Code = Convert.ToString(worksheet.Cells[row, 2].Value ?? "");
                    product.Hsn = Convert.ToString(worksheet.Cells[row, 3].Value ?? "");
                    // product.CompanyId = comid;

                    var Division = Convert.ToString(worksheet.Cells[row, 4].Value ?? "");
                    if (!string.IsNullOrWhiteSpace(Division))
                    {
                        var division = uow.Connection.TryFirst<Masters.ProductsDivisionRow>(q => q
                            .Select(d.Id)
                            .Where(d.ProductsDivision == Division));

                        if (division == null)
                        {
                            response.ErrorList.Add("Error On Row " + row + ": Product Division '" +
                                Division + "' is not found!");
                            continue;
                        }

                        product.DivisionId = division.Id.Value;
                    }
                    else
                        product.DivisionId = null;

                    var Group = Convert.ToString(worksheet.Cells[row, 5].Value ?? "");
                    if (!string.IsNullOrWhiteSpace(Group))
                    {
                        var group = uow.Connection.TryFirst<Masters.ProductsGroupRow>(q => q
                            .Select(g.Id)
                            .Where(g.ProductsGroup == Group));

                        if (group == null)
                        {
                            response.ErrorList.Add("Error On Row " + row + ": Group name'" +
                                Group + "' is not found!");
                            continue;
                        }

                        product.GroupId = group.Id.Value;
                    }
                    else

                        product.GroupId = null;

                    product.SellingPrice = Convert.ToDouble(worksheet.Cells[row, 6].Value ?? 0);
                    product.Mrp = Convert.ToDouble(worksheet.Cells[row, 7].Value ?? 0);
                    product.PurchasePrice = Convert.ToDouble(worksheet.Cells[row, 8].Value ?? 0);
                    //product.OpeningStock = Convert.ToDouble(worksheet.Cells[row, 9].Value ?? 0);
                    product.Quantity = Convert.ToDouble(worksheet.Cells[row, 9].Value ?? 0);
                    product.Description = Convert.ToString(worksheet.Cells[row, 10].Value ?? 0);

                    var Tax1 = Convert.ToString(worksheet.Cells[row, 11].Value ?? "");
                    if (!string.IsNullOrWhiteSpace(Tax1))
                    {
                        var tax1 = uow.Connection.TryFirst<Masters.TaxRow>(q => q
                            .Select(t.Id)
                            .Where(t.Name == Tax1));

                        if (tax1 == null)
                        {
                            response.ErrorList.Add("Error On Row " + row + ": Tax '" +
                                Tax1 + "' is not found!");
                            continue;
                        }

                        product.TaxId1 = tax1.Id.Value;
                    }
                    else
                        product.TaxId1 = null;


                    var Tax2 = Convert.ToString(worksheet.Cells[row, 12].Value ?? "");
                    if (!string.IsNullOrWhiteSpace(Tax2))
                    {
                        var tax2 = uow.Connection.TryFirst<Masters.TaxRow>(q => q
                            .Select(t.Id)
                            .Where(t.Name == Tax2));

                        if (tax2 == null)
                        {
                            response.ErrorList.Add("Error On Row " + row + ": Tax '" +
                                Tax2 + "' is not found!");
                            continue;
                        }

                        product.TaxId2 = tax2.Id.Value;
                    }
                    else
                        product.TaxId2 = null;

                    product.TechSpecs = Convert.ToString(worksheet.Cells[row, 13].Value ?? "");

                    var UNIT = Convert.ToString(worksheet.Cells[row, 14].Value ?? "");
                    if (!string.IsNullOrWhiteSpace(UNIT))
                    {
                        var unit = uow.Connection.TryFirst<Masters.ProductsUnitRow>(q => q
                            .Select(u.Id)
                            .Where(u.ProductsUnit == UNIT));

                        if (unit == null)
                        {
                            response.ErrorList.Add("Error On Row " + row + ": unit '" +
                                UNIT + "' is not found!");
                            continue;
                        }

                        product.UnitId = unit.Id.Value;
                    }
                    else
                        product.UnitId = null;

                    if (product.Id == null)
                    {
                        new InventorySaveHandler(Context).Create(uow, new SaveWithLocalizationRequest<MyRow>
                        {
                            Entity = product
                        });

                        response.Inserted = response.Inserted + 1;
                    }
                    else
                    {
                        new InventorySaveHandler(Context).Update(uow, new SaveWithLocalizationRequest<MyRow>
                        {
                            Entity = product,
                            EntityId = product.Id.Value
                        });

                        response.Updated = response.Updated + 1;
                    }
                }
            }
            return response;
        }
        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IInventoryRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IInventoryListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IInventoryListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.InventoryColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "InventoryList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
    }
}