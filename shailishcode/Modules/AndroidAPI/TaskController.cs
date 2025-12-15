using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http;
using System.Net;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class TaskController : ApiController
    {
        List<TaskModel> Contact;
        public TaskController()
        {
            Contact = new List<TaskModel>();
        }
       // public IEnumerable<TaskModel> Get([FromUri] EnquiryNModel pagingparametermodel)
                        public HttpResponseMessage Get( [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select en.Id,en.Task,en.CreationDate,en.ExpectedCompletion,ss.Status,us.DisplayName,tt.Type from Tasks en,TaskStatus ss,Users us,TaskType tt where tt.Id=en.TypeId and us.UserId=en.AssignedTo";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Contact.Add(new TaskModel
                {
                    id = (int)dr["Id"],
                    Task = (string)dr["Task"],
                    CreationDate = (DateTime)dr["CreationDate"],
                    status = (string)dr["Status"],
                    type = (string)dr["Type"],                   
                    assign = (string)dr["DisplayName"]
                }) ;
            }
            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = Contact.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            // Setting Header  
            var response = Request.CreateResponse(HttpStatusCode.OK, items);
            response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            var abc = JsonConvert.SerializeObject(paginationMetadata);
            // Returing List of Customers Collections  
            return response;
        }
       // public IEnumerable<TaskModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
                        public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select en.Id,en.Task,en.ProjectId,en.CreationDate,en.Details,en.Priority,en.ProductId,en.CompletionDate,en.ExpectedCompletion,en.StatusId,ss.Status,us.DisplayName,tt.Type from Tasks en,TaskStatus ss,Users us,TaskType tt where tt.Id=en.TypeId and ss.Id=en.StatusId and us.UserId=en.AssignedTo and en.AssignedTo=" + id + " ORDER BY en.Id DESC";
            
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
           DataSet ds = new DataSet();
           sda.Fill(ds);int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic Details, proid,proname,priority,comdate,edate,projid,projname;//obj == DBNull.Value
                if (dr["Priority"] == DBNull.Value)
                    priority = 0;
                else
                    priority = (int)dr["Priority"];

                if (dr["CompletionDate"] == DBNull.Value)
                    comdate = DateTime.MinValue;
                else
                    comdate = (DateTime)dr["CompletionDate"];

                if (dr["ExpectedCompletion"] == DBNull.Value)
                    edate = DateTime.MinValue;
                else
                    edate = (DateTime)dr["ExpectedCompletion"];

                if (dr["ProductId"] == DBNull.Value)
                    proid = 0;
                else
                    proid = (int)dr["ProductId"];

                if (dr["ProjectId"] == DBNull.Value)
                    projid = 0;
                else
                    projid = (int)dr["ProjectId"];

                if (dr["Details"] == DBNull.Value)
                    Details = "";
                else
                    Details = (string)dr["Details"];

                if (proid != 0)
                {
                    string strp = "Select Name from Products where Id=" + proid;

                    SqlDataAdapter sda1 = new SqlDataAdapter(strp, con);
                    DataSet ds1 = new DataSet();
                    sda1.Fill(ds1);
                    proname = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }
                else { proname = ""; }
                if (projid != 0)
                {
                    string strpj = "Select Project from Project where Id=" + projid;
                    SqlDataAdapter sda2 = new SqlDataAdapter(strpj, con);
                    DataSet ds2 = new DataSet();
                    sda2.Fill(ds2);
                    projname = ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else { projname = ""; }

                Contact.Add(new TaskModel
                {
                    id = (int) dr["Id"],
                    Task = (string)dr["Task"],
                    CreationDate = (DateTime)dr["CreationDate"],                   
                    status = (string)dr["Status"],
                    type = (string)dr["Type"],  
                    Details=Details,
                    ProductId=proid,
                    Product= proname,
                   Priority=priority,
                   ExpectedCompletion=edate,
                   Completion=comdate,
                   Project=projname,
                   ProjectId=projid,
                    assign = (string)dr["DisplayName"]
                }) ;
            }
            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = Contact.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            // Setting Header  
            var response = Request.CreateResponse(HttpStatusCode.OK, items);
            response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata)); // Returing List of Customers Collections  
            return response;
        }


        public IEnumerable<TaskModel> Get(int taskid)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
           
            string str = "Select en.Id,en.Task,en.ProjectId,en.CreationDate,en.Details,en.Priority,en.ProductId,en.CompletionDate,en.ExpectedCompletion,en.StatusId,ss.Status,us.DisplayName,tt.Type from Tasks en,TaskStatus ss,Users us,TaskType tt where tt.Id=en.TypeId and ss.Id=en.StatusId and us.UserId=en.AssignedTo and en.Id=" + taskid + " ORDER BY en.Id DESC";

            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds); int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic Details, proid, proname, priority, comdate, edate, projid, projname;//obj == DBNull.Value
                if (dr["Priority"] == DBNull.Value)
                    priority = 0;
                else
                    priority = (int)dr["Priority"];

                if (dr["CompletionDate"] == DBNull.Value)
                    comdate = DateTime.MinValue;
                else
                    comdate = (DateTime)dr["CompletionDate"];

                if (dr["ExpectedCompletion"] == DBNull.Value)
                    edate = DateTime.MinValue;
                else
                    edate = (DateTime)dr["ExpectedCompletion"];

                if (dr["ProductId"] == DBNull.Value)
                    proid = 0;
                else
                    proid = (int)dr["ProductId"];


                if (dr["ProjectId"] == DBNull.Value)
                    projid = 0;
                else
                    projid = (int)dr["ProjectId"];

                if (dr["Details"] == DBNull.Value)
                    Details = "";
                else
                    Details = (string)dr["Details"];

                if (proid != 0 )
                {                   
                    string strp = "Select Name from Products where Id=" + proid;
                   
                    SqlDataAdapter sda1 = new SqlDataAdapter(strp, con);
                    DataSet ds1 = new DataSet();
                    sda1.Fill(ds1);
                    proname = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();                  

                }
                else { proname = "";  }
                if(projid!=0)
                {
                    string strpj = "Select Project from Project where Id=" + projid;
                    SqlDataAdapter sda2 = new SqlDataAdapter(strpj, con);
                    DataSet ds2 = new DataSet();
                    sda2.Fill(ds2);
                    projname = ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else {  projname = ""; }

                Contact.Add(new TaskModel
                {
                    id = (int)dr["Id"],
                    Task = (string)dr["Task"],
                    CreationDate = (DateTime)dr["CreationDate"],
                    status = (string)dr["Status"],
                    type = (string)dr["Type"],
                    Details = Details,
                    ProductId = proid,
                    Product = proname,
                    Priority = priority,
                    ExpectedCompletion = edate,
                    Completion = comdate,
                    Project=projname,
                    ProjectId=projid,
                    assign = (string)dr["DisplayName"]
                });
            }
            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
           
            return Contact;
        }
    }
}