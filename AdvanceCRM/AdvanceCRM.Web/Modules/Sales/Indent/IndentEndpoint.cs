using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
using Microsoft.AspNetCore.Mvc;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using MyRow = AdvanceCRM.Sales.IndentRow;

namespace AdvanceCRM.Sales.Endpoints
{
    [Route("Services/Sales/Indent/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class IndentController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IIndentSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IIndentSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IIndentDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IIndentRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IIndentListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IIndentListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.IndentColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "IndentList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }
        public GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request)
        {
            var response = new GetNextNumberResponse();
            response.Serial = "1";

            var sl = MyRow.Fields;
            var data = new MyRow();
            var br = UserRow.Fields;
            var UData = new UserRow();

            var currentUserId = ((UserDefinition)Serenity.Authorization.UserDefinition).UserId;
            UData = connection.First<UserRow>(q => q
             .SelectTableFields()
             .Where(br.UserId == currentUserId)
            );

            var br1 = CompanyDetailsRow.Fields;
            var Bdata = new CompanyDetailsRow();
            Bdata = connection.First<CompanyDetailsRow>(q => q
              .SelectTableFields()
              .Select(br1.InvoicePrefix)
              .Select(br1.InvoiceSuffix)
              .Select(br1.YearInPrefix)
              .Select(br1.InvStartNo)
             );

            data = connection.TryFirst<MyRow>(q => q
                .SelectTableFields()
                .Select(sl.Id)
                .Select(sl.InvoiceNo)
                .OrderBy(sl.Id, desc: true)
            );

            var Ypre = string.Empty;
            int month = Convert.ToInt32(DateTime.Now.Month);
            var year = Convert.ToString(DateTime.Now.Year);

            if (Bdata.YearInPrefix == AdvanceCRM.Masters.YearInPrefix.Year)
            {
                Ypre = year;
            }
            else if (Bdata.YearInPrefix == AdvanceCRM.Masters.YearInPrefix.FinacialYear)
            {
                var yearF = year.Substring(year.Length - 2);
                if (month >= 4)
                {
                    var year1 = Convert.ToString(Convert.ToInt32(yearF) + 1);
                    Ypre = yearF + "" + year1;
                }
                else
                {
                    var year1 = Convert.ToString(Convert.ToInt32(yearF) - 1);
                    Ypre = year1 + "" + yearF;
                }
            }

            string stPre = string.Empty;
            string stsuf = string.Empty;
            if (Bdata.InvoicePrefix != null)
            {
                stPre = Bdata.InvoicePrefix;
            }
            if (Bdata.InvoiceSuffix != null)
            {
                stsuf = Bdata.InvoiceSuffix;
            }

            if (Bdata.InvStartNo == null)
            {
                if (data != null)
                {
                    response.SerialN = stPre + " " + Ypre + " " + (data.InvoiceNo + 1).ToString() + " " + stsuf;
                    response.Serial = (data.InvoiceNo + 1).ToString();
                }
                else
                {
                    response.SerialN = stPre + " " + Ypre + " " + (1).ToString() + " " + stsuf;
                    response.Serial = (1).ToString();
                }
            }
            else
            {
                response.SerialN = stPre + " " + Ypre + " " + (Bdata.InvStartNo).ToString() + " " + stsuf;
                response.Serial = (Bdata.InvStartNo).ToString();
            }

            return response;
        }
        public class IndentData
        {
            public ContactsRow Contact { get; set; }
            public UserRow User { get; set; }
            public MyRow Sales { get; set; }
            public IndentRow LastInv { get; set; }
            public CompanyDetailsRow Company { get; set; }
            public List<IndentProductsRow> SalesProducts { get; set; }

        }
    }
}