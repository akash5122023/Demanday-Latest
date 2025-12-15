using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using MyRow = AdvanceCRM.Products.BomRow;

namespace AdvanceCRM.Products.Endpoints
{
    [Route("Services/Products/Bom/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class BomController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IBomSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IBomSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IBomDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IBomRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IBomListHandler handler)
        {
            if (!BomTableExists(connection))
                return CreateMissingTableListResponse(request);

            try
            {
                return handler.List(connection, request);
            }
            catch (SqlException ex) when (IsMissingBomTable(ex))
            {
                return CreateMissingTableListResponse(request);
            }
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IBomListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.BomColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "BomList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        private static bool BomTableExists(IDbConnection connection)
        {
            if (connection == null)
                return false;

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @table";

                var schemaParameter = command.CreateParameter();
                schemaParameter.ParameterName = "@schema";
                schemaParameter.Value = "dbo";
                command.Parameters.Add(schemaParameter);

                var tableParameter = command.CreateParameter();
                tableParameter.ParameterName = "@table";
                tableParameter.Value = "Bom";
                command.Parameters.Add(tableParameter);

                var result = command.ExecuteScalar();
                return Convert.ToInt32(result) > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private static bool IsMissingBomTable(SqlException exception)
        {
            return exception?.Number == 208;
        }

        private static ListResponse<MyRow> CreateMissingTableListResponse(ListRequest request)
        {
            return new ListResponse<MyRow>
            {
                Entities = new List<MyRow>(),
                TotalCount = 0,
                Skip = request?.Skip ?? 0,
                Error = new ServiceError
                {
                    Code = "TableMissing",
                    Message = "BOM data is unavailable because the dbo.Bom table has not been created."
                }
            };
        }
    }
}