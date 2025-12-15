using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class CMSController : ControllerBase
    {
        List<CMSModel> Contact;
        private readonly string _connectionString;

        public CMSController(IConfiguration config)
        {
            Contact = new List<CMSModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string str = "Select Id,ContactsId,Date,ProductsId,SerialNo,ComplaintId,Category,Amount,ExpectedCompletion,AssignedBy,AssignedTo,Instructions,BranchId,Status,CompletionDate,Feedback,AdditionalInfo,StageId,Priority,InvestigationBy,ActionBy,SupervisedBy,Observation,Action,Comments,CMSNo,DealerId,PurchaseDate,InvoiceNo,EmployeeId,ProjectId,CMSN,TicketNo from CMS ORDER BY Id DESC";
                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic comid, comvalue, stage, priority, priorityValue, Assbyname, assbydisname, Assbyid, asstoname, asstodisname, asstoid, branch, branchid, investname, Supername, stat, cat, Statusname, CatName, conid, proname, productname, productsid, conname, conphone, conmail, conaddress, dealname, dealphone, dealmail, dealaddress, empname, empphone, empmail, empaddress, serial, amt, exdate, instn, Comdate, feedback, Addinfo, stageid, comment, investby, invest, Actionby, Action, Superby, CmsNo, Purdate, dealid, InvNo, EmpId, ProId, CMSN, TicketNo;

                    if (dr["ContactsId"] == DBNull.Value) conid = 0; else conid = Convert.ToInt32(dr["ContactsId"]);
                    if (dr["SerialNo"] == DBNull.Value) serial = ""; else serial = dr["SerialNo"].ToString();
                    if (dr["Amount"] == DBNull.Value) amt = 0; else amt = Convert.ToDouble(dr["Amount"]);
                    if (dr["ExpectedCompletion"] == DBNull.Value) exdate = DateTime.MinValue; else exdate = Convert.ToDateTime(dr["ExpectedCompletion"]);
                    if (dr["Instructions"] == DBNull.Value) instn = ""; else instn = dr["Instructions"].ToString();
                    if (dr["CompletionDate"] == DBNull.Value) Comdate = DateTime.MinValue; else Comdate = Convert.ToDateTime(dr["CompletionDate"]);
                    if (dr["Feedback"] == DBNull.Value) feedback = ""; else feedback = dr["Feedback"].ToString();
                    if (dr["AdditionalInfo"] == DBNull.Value) Addinfo = ""; else Addinfo = dr["AdditionalInfo"].ToString();
                    if (dr["StageId"] == DBNull.Value) stageid = 0; else stageid = Convert.ToInt32(dr["StageId"]);
                    if (dr["Comments"] == DBNull.Value) comment = ""; else comment = dr["Comments"].ToString();
                    if (dr["Priority"] == DBNull.Value) priority = 0; else priority = Convert.ToInt32(dr["Priority"]);
                    if (dr["InvestigationBy"] == DBNull.Value) investby = 0; else investby = Convert.ToInt32(dr["InvestigationBy"]);
                    if (dr["ActionBy"] == DBNull.Value) Actionby = 0; else Actionby = Convert.ToInt32(dr["ActionBy"]);
                    if (dr["SupervisedBy"] == DBNull.Value) Superby = 0; else Superby = Convert.ToInt32(dr["SupervisedBy"]);
                    if (dr["Observation"] == DBNull.Value) invest = ""; else invest = dr["Observation"].ToString();
                    if (dr["Action"] == DBNull.Value) Action = ""; else Action = dr["Action"].ToString();
                    if (dr["CMSNo"] == DBNull.Value) CmsNo = 0; else CmsNo = Convert.ToInt32(dr["CMSNo"]);
                    if (dr["DealerId"] == DBNull.Value) dealid = 0; else dealid = Convert.ToInt32(dr["DealerId"]);
                    if (dr["PurchaseDate"] == DBNull.Value) Purdate = DateTime.MinValue; else Purdate = Convert.ToDateTime(dr["PurchaseDate"]);
                    if (dr["InvoiceNo"] == DBNull.Value) InvNo = ""; else InvNo = dr["InvoiceNo"].ToString();
                    if (dr["EmployeeId"] == DBNull.Value) EmpId = 0; else EmpId = Convert.ToInt32(dr["EmployeeId"]);
                    if (dr["ProjectId"] == DBNull.Value) ProId = 0; else ProId = Convert.ToInt32(dr["ProjectId"]);
                    if (dr["CMSN"] == DBNull.Value) CMSN = ""; else CMSN = dr["CMSN"].ToString();
                    if (dr["TicketNo"] == DBNull.Value) TicketNo = 0; else TicketNo = Convert.ToInt32(dr["TicketNo"]);
                    if (dr["ProductsId"] == DBNull.Value) productsid = 0; else productsid = Convert.ToInt32(dr["ProductsId"]);
                    if (dr["Category"] == DBNull.Value) cat = 0; else cat = Convert.ToInt32(dr["Category"]);
                    if (dr["Status"] == DBNull.Value) stat = 0; else stat = Convert.ToInt32(dr["Status"]);
                    if (dr["ComplaintId"] == DBNull.Value) comid = 0; else comid = Convert.ToInt32(dr["ComplaintId"]);
                    if (dr["AssignedBy"] == DBNull.Value) Assbyid = 0; else Assbyid = Convert.ToInt32(dr["AssignedBy"]);
                    if (dr["AssignedTo"] == DBNull.Value) asstoid = 0; else asstoid = Convert.ToInt32(dr["AssignedTo"]);

                    if (conid != 0)
                    {
                        string str1 = "Select Name,Phone,Email,Address from Contacts where Id=" + conid;
                        SqlDataAdapter sda1 = new SqlDataAdapter(str1, con);
                        DataSet ds1 = new DataSet();
                        sda1.Fill(ds1);
                        conname = Convert.ToString(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        conphone = Convert.ToString(ds1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                        conaddress = Convert.ToString(ds1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                        conmail = Convert.ToString(ds1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                    }
                    else { conname = ""; conphone = ""; conaddress = ""; conmail = ""; }

                    if (dealid != 0)
                    {
                        string str2 = "Select DealerName,Phone,Email,Address from Dealer where Id=" + dealid;
                        SqlDataAdapter sda2 = new SqlDataAdapter(str2, con);
                        DataSet ds2 = new DataSet();
                        sda2.Fill(ds2);
                        dealname = Convert.ToString(ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        dealphone = Convert.ToString(ds2.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                        dealaddress = Convert.ToString(ds2.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                        dealmail = Convert.ToString(ds2.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                    }
                    else { dealname = ""; dealphone = ""; dealaddress = ""; dealmail = ""; }

                    if (EmpId != 0)
                    {
                        string str3 = "Select Name,Phone,Email,Address from Employee where Id=" + EmpId;
                        SqlDataAdapter sda3 = new SqlDataAdapter(str3, con);
                        DataSet ds3 = new DataSet();
                        sda3.Fill(ds3);
                        empname = Convert.ToString(ds3.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        empphone = Convert.ToString(ds3.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                        empaddress = Convert.ToString(ds3.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                        empmail = Convert.ToString(ds3.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                    }
                    else { empname = ""; empphone = ""; empaddress = ""; empmail = ""; }

                    if (ProId != 0)
                    {
                        string str4 = "Select Project from Project where Id=" + ProId;
                        SqlDataAdapter sda4 = new SqlDataAdapter(str4, con);
                        DataSet ds4 = new DataSet();
                        sda4.Fill(ds4);
                        proname = Convert.ToString(ds4.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    else { proname = ""; }

                    if (productsid != 0)
                    {
                        string str5 = "Select Name from Products where Id=" + productsid;
                        SqlDataAdapter sda5 = new SqlDataAdapter(str5, con);
                        DataSet ds5 = new DataSet();
                        sda5.Fill(ds5);
                        productname = Convert.ToString(ds5.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    else { productname = ""; }

                    if (stat == 1) { Statusname = "Open"; } else if (stat == 2) { Statusname = "Closed"; } else if (stat == 3) { Statusname = "Analysis"; } else if (stat == 4) { Statusname = "Action"; } else if (stat == 5) { Statusname = "Verify"; } else { Statusname = ""; }
                    if (cat == 1) { CatName = "Chargeable"; } else if (cat == 2) { CatName = "Non Chargeable"; } else { CatName = ""; }
                    if (priority == 1) { priorityValue = "Low"; } else if (priority == 2) { priorityValue = "Medium"; } else if (priority == 3) { priorityValue = "High"; } else if (priority == 4) { priorityValue = "Urgent"; } else { priorityValue = ""; }

                    if (comid != 0)
                    {
                        string str6 = "Select ComplaintType from ComplaintType where Id=" + comid;
                        SqlDataAdapter sda6 = new SqlDataAdapter(str6, con);
                        DataSet ds6 = new DataSet();
                        sda6.Fill(ds6);
                        comvalue = Convert.ToString(ds6.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    else { comvalue = ""; }

                    if (stageid != 0)
                    {
                        string str7 = "Select Stage from Stage where Id=" + stageid;
                        SqlDataAdapter sda7 = new SqlDataAdapter(str7, con);
                        DataSet ds7 = new DataSet();
                        sda7.Fill(ds7);
                        stage = Convert.ToString(ds7.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    else { stage = ""; }

                    if (asstoid != 0)
                    {
                        string str8 = "Select Username,DisplayName from Users where UserId=" + asstoid;
                        SqlDataAdapter sda8 = new SqlDataAdapter(str8, con);
                        DataSet ds8 = new DataSet();
                        sda8.Fill(ds8);
                        asstoname = Convert.ToString(ds8.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        asstodisname = Convert.ToString(ds8.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    }
                    else { asstoname = ""; asstodisname = ""; }

                    if (Assbyid != 0)
                    {
                        string str9 = "Select Username,DisplayName from Users where UserId=" + Assbyid;
                        SqlDataAdapter sda9 = new SqlDataAdapter(str9, con);
                        DataSet ds9 = new DataSet();
                        sda9.Fill(ds9);
                        Assbyname = Convert.ToString(ds9.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        assbydisname = Convert.ToString(ds9.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    }
                    else { Assbyname = ""; assbydisname = ""; }

                    Contact.Add(new CMSModel
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        ContactsId = conid,
                        ContactsAddress = conaddress,
                        ContactsName = conname,
                        StatusValue = Statusname,
                        CategoryValue = CatName,
                        ContactsPhone = conphone,
                        ContactsEmail = conmail,
                        DealerEmail = dealmail,
                        DealerPhone = dealphone,
                        DealerAddress = dealaddress,
                        DealerName = dealname,
                        EmployeeAddress = empaddress,
                        EmployeeEmail = empmail,
                        EmployeeName = empname,
                        EmployeePhone = empphone,
                        Date = Convert.ToDateTime(dr["Date"]),
                        ProductsId = productsid,
                        Product = productname,
                        SerialNo = serial,
                        ComplaintId = Convert.ToInt32(dr["ComplaintId"]),
                        Category = Convert.ToInt32(dr["Category"]),
                        Project = proname,
                        Amount = amt,
                        AssignedbyDisplayname = assbydisname,
                        AssignedbyUsername = Assbyname,
                        AssignedToDisplayname = asstodisname,
                        AssignedtoUsername = asstoname,
                        StageValue = stage,
                        PriorityValue = priorityValue,
                        ComplaintValue = comvalue,
                        ExpectedCompletion = exdate,
                        AssignedBy = Convert.ToInt32(dr["AssignedBy"]),
                        AssignedTo = Convert.ToInt32(dr["AssignedTo"]),
                        Instructions = instn,
                        Status = Convert.ToInt32(dr["Status"]),
                        CompletionDate = Comdate,
                        Feedback = feedback,
                        AdditionalInfo = Addinfo,
                        StageId = stageid,
                        Comments = comment,
                        Priority = priority,
                        InvestigationBy = investby,
                        ActionBy = Actionby,
                        SupervisedBy = Superby,
                        Observation = invest,
                        Action = Action,
                        CMSNo = CmsNo,
                        DealerId = dealid,
                        PurchaseDate = Purdate,
                        InvoiceNo = InvNo,
                        EmployeeId = EmpId,
                        ProjectId = ProId,
                        CMSN = CMSN,
                        TicketNo = TicketNo
                    });
                }

                int CurrentPage = pagingparametermodel.pageNumber;
                int PageSize = pagingparametermodel.pageSize;
                int TotalCount = count;
                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
                var items = Contact.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                var previousPage = CurrentPage > 1 ? "Yes" : "No";
                var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

                var paginationMetadata = new
                {
                    totalCount = TotalCount,
                    pageSize = PageSize,
                    currentPage = CurrentPage,
                    totalPages = TotalPages,
                    previousPage,
                    nextPage
                };

                Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                return Ok(items);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string str = "Select Id,ContactsId,Date,ProductsId,SerialNo,ComplaintId,Category,Amount,ExpectedCompletion,AssignedBy,AssignedTo,Instructions,BranchId,Status,CompletionDate,Feedback,AdditionalInfo,StageId,Priority,InvestigationBy,ActionBy,SupervisedBy,Observation,Action,Comments,CMSNo,DealerId,PurchaseDate,InvoiceNo,EmployeeId,ProjectId,CMSN,TicketNo from CMS Where AssignedTo=" + id + " ORDER BY Id DESC";
                SqlDataAdapter sda = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                Contact.Clear();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic comid, comvalue, stage, priority, priorityValue, Assbyname, assbydisname, Assbyid, asstoname, asstodisname, asstoid, branch, branchid, investname, Supername, stat, cat, Statusname, CatName, conid, proname, productname, productsid, conname, conphone, conmail, conaddress, dealname, dealphone, dealmail, dealaddress, empname, empphone, empmail, empaddress, serial, amt, exdate, instn, Comdate, feedback, Addinfo, stageid, comment, investby, invest, Actionby, Action, Superby, CmsNo, Purdate, dealid, InvNo, EmpId, ProId, CMSN, TicketNo;

                    if (dr["ContactsId"] == DBNull.Value) conid = 0; else conid = Convert.ToInt32(dr["ContactsId"]);
                    if (dr["SerialNo"] == DBNull.Value) serial = ""; else serial = dr["SerialNo"].ToString();
                    if (dr["Amount"] == DBNull.Value) amt = 0; else amt = Convert.ToDouble(dr["Amount"]);
                    if (dr["ExpectedCompletion"] == DBNull.Value) exdate = DateTime.MinValue; else exdate = Convert.ToDateTime(dr["ExpectedCompletion"]);
                    if (dr["Instructions"] == DBNull.Value) instn = ""; else instn = dr["Instructions"].ToString();
                    if (dr["CompletionDate"] == DBNull.Value) Comdate = DateTime.MinValue; else Comdate = Convert.ToDateTime(dr["CompletionDate"]);
                    if (dr["Feedback"] == DBNull.Value) feedback = ""; else feedback = dr["Feedback"].ToString();
                    if (dr["AdditionalInfo"] == DBNull.Value) Addinfo = ""; else Addinfo = dr["AdditionalInfo"].ToString();
                    if (dr["StageId"] == DBNull.Value) stageid = 0; else stageid = Convert.ToInt32(dr["StageId"]);
                    if (dr["Comments"] == DBNull.Value) comment = ""; else comment = dr["Comments"].ToString();
                    if (dr["Priority"] == DBNull.Value) priority = 0; else priority = Convert.ToInt32(dr["Priority"]);
                    if (dr["InvestigationBy"] == DBNull.Value) investby = 0; else investby = Convert.ToInt32(dr["InvestigationBy"]);
                    if (dr["ActionBy"] == DBNull.Value) Actionby = 0; else Actionby = Convert.ToInt32(dr["ActionBy"]);
                    if (dr["SupervisedBy"] == DBNull.Value) Superby = 0; else Superby = Convert.ToInt32(dr["SupervisedBy"]);
                    if (dr["Observation"] == DBNull.Value) invest = ""; else invest = dr["Observation"].ToString();
                    if (dr["Action"] == DBNull.Value) Action = ""; else Action = dr["Action"].ToString();
                    if (dr["CMSNo"] == DBNull.Value) CmsNo = 0; else CmsNo = Convert.ToInt32(dr["CMSNo"]);
                    if (dr["DealerId"] == DBNull.Value) dealid = 0; else dealid = Convert.ToInt32(dr["DealerId"]);
                    if (dr["PurchaseDate"] == DBNull.Value) Purdate = DateTime.MinValue; else Purdate = Convert.ToDateTime(dr["PurchaseDate"]);
                    if (dr["InvoiceNo"] == DBNull.Value) InvNo = ""; else InvNo = dr["InvoiceNo"].ToString();
                    if (dr["EmployeeId"] == DBNull.Value) EmpId = 0; else EmpId = Convert.ToInt32(dr["EmployeeId"]);
                    if (dr["ProjectId"] == DBNull.Value) ProId = 0; else ProId = Convert.ToInt32(dr["ProjectId"]);
                    if (dr["CMSN"] == DBNull.Value) CMSN = ""; else CMSN = dr["CMSN"].ToString();
                    if (dr["TicketNo"] == DBNull.Value) TicketNo = 0; else TicketNo = Convert.ToInt32(dr["TicketNo"]);
                    if (dr["ProductsId"] == DBNull.Value) productsid = 0; else productsid = Convert.ToInt32(dr["ProductsId"]);
                    if (dr["Category"] == DBNull.Value) cat = 0; else cat = Convert.ToInt32(dr["Category"]);
                    if (dr["Status"] == DBNull.Value) stat = 0; else stat = Convert.ToInt32(dr["Status"]);
                    if (dr["ComplaintId"] == DBNull.Value) comid = 0; else comid = Convert.ToInt32(dr["ComplaintId"]);
                    if (dr["AssignedBy"] == DBNull.Value) Assbyid = 0; else Assbyid = Convert.ToInt32(dr["AssignedBy"]);
                    if (dr["AssignedTo"] == DBNull.Value) asstoid = 0; else asstoid = Convert.ToInt32(dr["AssignedTo"]);

                    if (conid != 0)
                    {
                        string str1 = "Select Name,Phone,Email,Address from Contacts where Id=" + conid;
                        SqlDataAdapter sda1 = new SqlDataAdapter(str1, con);
                        DataSet ds1 = new DataSet();
                        sda1.Fill(ds1);
                        conname = Convert.ToString(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        conphone = Convert.ToString(ds1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                        conaddress = Convert.ToString(ds1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                        conmail = Convert.ToString(ds1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                    }
                    else { conname = ""; conphone = ""; conaddress = ""; conmail = ""; }

                    if (dealid != 0)
                    {
                        string str2 = "Select DealerName,Phone,Email,Address from Dealer where Id=" + dealid;
                        SqlDataAdapter sda2 = new SqlDataAdapter(str2, con);
                        DataSet ds2 = new DataSet();
                        sda2.Fill(ds2);
                        dealname = Convert.ToString(ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        dealphone = Convert.ToString(ds2.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                        dealaddress = Convert.ToString(ds2.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                        dealmail = Convert.ToString(ds2.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                    }
                    else { dealname = ""; dealphone = ""; dealaddress = ""; dealmail = ""; }

                    if (EmpId != 0)
                    {
                        string str3 = "Select Name,Phone,Email,Address from Employee where Id=" + EmpId;
                        SqlDataAdapter sda3 = new SqlDataAdapter(str3, con);
                        DataSet ds3 = new DataSet();
                        sda3.Fill(ds3);
                        empname = Convert.ToString(ds3.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        empphone = Convert.ToString(ds3.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                        empaddress = Convert.ToString(ds3.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                        empmail = Convert.ToString(ds3.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                    }
                    else { empname = ""; empphone = ""; empaddress = ""; empmail = ""; }

                    if (ProId != 0)
                    {
                        string str4 = "Select Project from Project where Id=" + ProId;
                        SqlDataAdapter sda4 = new SqlDataAdapter(str4, con);
                        DataSet ds4 = new DataSet();
                        sda4.Fill(ds4);
                        proname = Convert.ToString(ds4.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    else { proname = ""; }

                    if (productsid != 0)
                    {
                        string str5 = "Select Name from Products where Id=" + productsid;
                        SqlDataAdapter sda5 = new SqlDataAdapter(str5, con);
                        DataSet ds5 = new DataSet();
                        sda5.Fill(ds5);
                        productname = Convert.ToString(ds5.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    else { productname = ""; }

                    if (stat == 1) { Statusname = "Open"; } else if (stat == 2) { Statusname = "Closed"; } else if (stat == 3) { Statusname = "Analysis"; } else if (stat == 4) { Statusname = "Action"; } else if (stat == 5) { Statusname = "Verify"; } else { Statusname = ""; }
                    if (cat == 1) { CatName = "Chargeable"; } else if (cat == 2) { CatName = "Non Chargeable"; } else { CatName = ""; }
                    if (priority == 1) { priorityValue = "Low"; } else if (priority == 2) { priorityValue = "Medium"; } else if (priority == 3) { priorityValue = "High"; } else if (priority == 4) { priorityValue = "Urgent"; } else { priorityValue = ""; }

                    if (comid != 0)
                    {
                        string str6 = "Select ComplaintType from ComplaintType where Id=" + comid;
                        SqlDataAdapter sda6 = new SqlDataAdapter(str6, con);
                        DataSet ds6 = new DataSet();
                        sda6.Fill(ds6);
                        comvalue = Convert.ToString(ds6.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    else { comvalue = ""; }

                    if (stageid != 0)
                    {
                        string str7 = "Select Stage from Stage where Id=" + stageid;
                        SqlDataAdapter sda7 = new SqlDataAdapter(str7, con);
                        DataSet ds7 = new DataSet();
                        sda7.Fill(ds7);
                        stage = Convert.ToString(ds7.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    else { stage = ""; }

                    if (asstoid != 0)
                    {
                        string str8 = "Select Username,DisplayName from Users where UserId=" + asstoid;
                        SqlDataAdapter sda8 = new SqlDataAdapter(str8, con);
                        DataSet ds8 = new DataSet();
                        sda8.Fill(ds8);
                        asstoname = Convert.ToString(ds8.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        asstodisname = Convert.ToString(ds8.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    }
                    else { asstoname = ""; asstodisname = ""; }

                    if (Assbyid != 0)
                    {
                        string str9 = "Select Username,DisplayName from Users where UserId=" + Assbyid;
                        SqlDataAdapter sda9 = new SqlDataAdapter(str9, con);
                        DataSet ds9 = new DataSet();
                        sda9.Fill(ds9);
                        Assbyname = Convert.ToString(ds9.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        assbydisname = Convert.ToString(ds9.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    }
                    else { Assbyname = ""; assbydisname = ""; }

                    Contact.Add(new CMSModel
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        ContactsId = conid,
                        ContactsAddress = conaddress,
                        ContactsName = conname,
                        StatusValue = Statusname,
                        CategoryValue = CatName,
                        ContactsPhone = conphone,
                        ContactsEmail = conmail,
                        DealerEmail = dealmail,
                        DealerPhone = dealphone,
                        DealerAddress = dealaddress,
                        DealerName = dealname,
                        EmployeeAddress = empaddress,
                        EmployeeEmail = empmail,
                        EmployeeName = empname,
                        EmployeePhone = empphone,
                        Date = Convert.ToDateTime(dr["Date"]),
                        ProductsId = productsid,
                        Product = productname,
                        SerialNo = serial,
                        ComplaintId = Convert.ToInt32(dr["ComplaintId"]),
                        Category = Convert.ToInt32(dr["Category"]),
                        Project = proname,
                        Amount = amt,
                        AssignedbyDisplayname = assbydisname,
                        AssignedbyUsername = Assbyname,
                        AssignedToDisplayname = asstodisname,
                        AssignedtoUsername = asstoname,
                        StageValue = stage,
                        PriorityValue = priorityValue,
                        ComplaintValue = comvalue,
                        ExpectedCompletion = exdate,
                        AssignedBy = Convert.ToInt32(dr["AssignedBy"]),
                        AssignedTo = Convert.ToInt32(dr["AssignedTo"]),
                        Instructions = instn,
                        Status = Convert.ToInt32(dr["Status"]),
                        CompletionDate = Comdate,
                        Feedback = feedback,
                        AdditionalInfo = Addinfo,
                        StageId = stageid,
                        Comments = comment,
                        Priority = priority,
                        InvestigationBy = investby,
                        ActionBy = Actionby,
                        SupervisedBy = Superby,
                        Observation = invest,
                        Action = Action,
                        CMSNo = CmsNo,
                        DealerId = dealid,
                        PurchaseDate = Purdate,
                        InvoiceNo = InvNo,
                        EmployeeId = EmpId,
                        ProjectId = ProId,
                        CMSN = CMSN,
                        TicketNo = TicketNo
                    });
                }

                int CurrentPage = pagingparametermodel.pageNumber;
                int PageSize = pagingparametermodel.pageSize;
                int TotalCount = count;
                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
                var items = Contact.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                var previousPage = CurrentPage > 1 ? "Yes" : "No";
                var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

                var paginationMetadata = new
                {
                    totalCount = TotalCount,
                    pageSize = PageSize,
                    currentPage = CurrentPage,
                    totalPages = TotalPages,
                    previousPage,
                    nextPage
                };

                Response.Headers.Append("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                return Ok(items);
            }
        }
    }
}