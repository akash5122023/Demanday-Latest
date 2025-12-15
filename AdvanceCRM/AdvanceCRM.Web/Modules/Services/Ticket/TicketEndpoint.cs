
namespace AdvanceCRM.Services.Endpoints
{
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Services.Endpoints;
    using AdvanceCRM.Services;
    using System;
    using System.Data;
    
    using MyRepository = Repositories.TicketRepository;
    using MyRow = TicketRow;

    [Route("Services/Services/Ticket/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class TicketController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public TicketController(ISqlConnections connections)
        {
            _connections = connections;
        }
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

        [ServiceAuthorize("Ticket:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.TicketColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Ticket_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost]
        [ServiceAuthorize("Ticket:Move to CMS")]
        public StandardResponse MoveToCMS(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();

            var exist = new CMSRow();
            var i = CMSRow.Fields;
            exist = uow.Connection.TryFirst<CMSRow>(q => q
            .SelectTableFields()
            .Select(i.Id)
            .Where(i.TicketNo == request.Id));

            if (exist != null)
            {
                response.Id = exist.Id.Value;
                response.Status = "Already Moved!";
                return response;
            }

            var data = new QuotData();

            using (var connection = _connections.NewFor<TicketRow>())
            {
                var cms = TicketRow.Fields;
                var con = ContactsRow.Fields;
                data.CMS = connection.TryById<TicketRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(cms.Id)
                   .Select(cms.Name)
                   .Select(cms.Phone)
                   .Select(cms.ComplaintDetails)
                   .Select(cms.AdditionalDetails)
                   .Select(cms.Priority)
                   .Select(cms.ProductsId)
                   .Select(cms.AssignedId)
                   );

                if(data.CMS.Phone != null)
                {
                    data.Contact = connection.TryFirst<ContactsRow>(q => q
                      .SelectTableFields()
                      .Select(con.Id)
                      .Where(con.Phone == data.CMS.Phone));
                }
              
            }

            try
            {
                using (var connection = _connections.NewFor<CMSRow>())
                {
                    int conid = 0;
                    if(data.Contact != null)
                    {
                        conid = Convert.ToInt32(data.Contact.Id);   
                    }
                    else
                    {
                        string str1 = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,OwnerId,AssignedId) VALUES('1','81','1','" + data.CMS.Name + "','" + data.CMS.Phone+ "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "')";
                        connection.Execute(str1);
                        var c = ContactsRow.Fields;
                        var LastContact = connection.First<ContactsRow>(l => l
                            .Select(c.Id)
                            .Select(c.Name)
                            .OrderBy(c.Id, desc: true)
                            );

                        conid = Convert.ToInt32(LastContact.Id);
                    }

                    var details = "Name :" + data.CMS.Name + "-Phone :" + data.CMS.Phone + "-Details: " + data.CMS.ComplaintDetails+","+data.CMS.AdditionalDetails;
                    GetNextNumberResponse nextNumber = new CMSController(_connections, Context).GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    String str = "INSERT INTO CMS(ContactsId,ComplaintId,Category,ProductsId,CMSNo,CMSN,Date,Status,AdditionalInfo,Priority,StageId,AssignedBy,AssignedTo,TicketNo) VALUES(" + conid+ ",'1','2'," + data.CMS.ProductsId+"," + nextNumber.Serial + ",'Ticket-#" + data.CMS.Id.Value + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1','"+details+"','1','1', '" + Context.User.GetIdentifier() + "' ,'" + Convert.ToString(data.CMS.AssignedId.Value) + "','"+data.CMS.Id.Value+"')";

                    connection.Execute(str);

                    var quo = CMSRow.Fields;
                    data.LastQuot = connection.TryFirst<CMSRow>(l => l
                    .Select(quo.Id)
                    .OrderBy(quo.Id, desc: true)
                    );
                }

                //if (data.CMS.ContactsId == data.LastQuot.ContactsId)
                //{
                //    //throw new Exception("Something went wrong while generating Quotation from Enquiry\nOnly Quotation got generated, products are not copied");
                //    using (var connection = _connections.NewFor<QuotationProductsRow>())
                //    {
                //        foreach (var item in data.CMSProducts)
                //        {
                //            String str = "INSERT INTO QuotationProducts(ProductsId,Quantity,Price,Percentage1,Percentage2,QuotationId,DiscountAmount,MRP,SellingPrice) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Price.Value) + "','0','0','" + Convert.ToString(data.LastQuot.Id.Value) + "','0','0','0')";

                //            connection.Execute(str);
                //        }
                //    }
                //}

                response.Id = data.LastQuot.Id.Value;
                response.Status = "CMS generated successfully";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }
        public class QuotData
        {
            public ContactsRow Contact { get; set; }
            public TicketRow CMS { get; set; }
           
            public UserRow User { get; set; }
            public UserRow Engineer { get; set; }
            public CMSRow LastQuot { get; set; }

        }
    }
}
