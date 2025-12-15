using AdvanceCRM.Purchase.QualityCheck;
using Microsoft.AspNetCore.Mvc;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Data;
using System.Globalization;
using MyRow = AdvanceCRM.Purchase.QualityCheckRow;

namespace AdvanceCRM.Purchase.Endpoints
{
    [Route("Services/Purchase/QualityCheck/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class QualityCheckController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IQualityCheckSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IQualityCheckSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IQualityCheckDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IQualityCheckRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IQualityCheckListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IQualityCheckListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.QualityCheckColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "QualityCheckList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }


        // MoveToRejectionOutward
        [HttpPost]
        [ServiceAuthorize("QualityCheck:Move to Rejection Outward")]
        public StandardResponse MoveToRejectionOutward(
     IUnitOfWork uow,
     StandardRequest request,
     [FromServices] ISqlConnections sqlConnections)
        {
            var response = new StandardResponse();

            var qc = QualityCheckRow.Fields;
            var qualityCheckData = uow.Connection.TryById<QualityCheckRow>(request.Id, q => q
                .Select(qc.QcNumber)
                .Select(qc.ProductId)
                .Select(qc.QtyRejected)
                .Select(qc.AdditionalInfo)
                .Select(qc.Attachments)
                .Select(qc.PurchaseFromId)
            );

            if (qualityCheckData == null)
                throw new ValidationError("QualityCheckNotFound", "The specified Quality Check does not exist.");

            var qcNumber = qualityCheckData.QcNumber.Value;

            var rejectionOutwardFields = RejectionOutwardRow.Fields;
            var existingEntry = uow.Connection.TryFirst<RejectionOutwardRow>(q => q
                .SelectTableFields()
                .Where(rejectionOutwardFields.QcNumber == qcNumber)
            );

            if (existingEntry != null)
            {
                response.Id = existingEntry.Id.Value;
                response.Status = "Already Moved!";
                return response;
            }

            // ✅ Use injected ISqlConnections
            using (var connection = sqlConnections.NewFor<RejectionOutwardRow>())
            {
                string insertStr = "INSERT INTO RejectionOutward (ProductId, QtyRejected, QcNumber, Date, Status, AdditionalInfo, Attachments, PurchaseFromId) " +
                                   "VALUES ('" + Convert.ToString(qualityCheckData.ProductId.Value) + "', '" +
                                   Convert.ToString(qualityCheckData.QtyRejected.Value) + "', '" +
                                   Convert.ToString(qcNumber) + "', '" +
                                   DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                                   "1, " +
                                   "'" + qualityCheckData.AdditionalInfo?.Replace("'", "''") + "', " +
                                   "'" + qualityCheckData.Attachments?.Replace("'", "''") + "', " +
                                   "'" + qualityCheckData.PurchaseFromId.Value + "')";

                connection.Execute(insertStr);

                var rejection = RejectionOutwardRow.Fields;
                var lastRejection = connection.TryFirst<RejectionOutwardRow>(l => l
                    .Select(rejection.Id)
                    .OrderBy(rejection.Id, desc: true)
                );

                response.Id = lastRejection.Id.Value;
                response.Status = "Quality Check moved to Rejection Outward successfully";
            }

            return response;
        }



        public GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request)
        {
            var response = new GetNextNumberResponse();
            response.Serial = "1";
            try
            {
                var sl = MyRow.Fields;
                var data = new MyRow();


                data = connection.TryFirst<MyRow>(q => q
                    .SelectTableFields()
                    .Select(sl.Id)
                    .Select(sl.QcNumber)
                    .OrderBy(sl.Id, desc: true)
                    );

                if (data != null)
                    response.Serial = (data.QcNumber + 1).ToString();
            }
            catch (Exception)
            {

                return null;
            }

            return response;
        }




        public class QualityCheckData
        {
            public QualityCheckRow QualityCheck { get; set; }    // Holds details from the Quality Check row
            public RejectionOutwardRow LastRejection { get; set; } // Stores the last rejection outward entry
        }

    }
}