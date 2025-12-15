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
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly List<TaskModel> contact;
        private readonly string _connectionString;

        public TaskController(IConfiguration configuration)
        {
            contact = new List<TaskModel>();
            _connectionString = Startup.connectionString;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingParameterModel, [FromQuery] int? taskid)
        {
            contact.Clear();
            if (taskid.HasValue)
            {
                return GetTaskById(taskid.Value);
            }
            PopulateTasks();
            return CreatePagedResponse(contact, pagingParameterModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetByAssigned(string id, [FromQuery] EnquiryNModel pagingParameterModel)
        {
            contact.Clear();
            using var con = new SqlConnection(_connectionString);

            string query = "Select en.Id,en.TaskTitle,en.ProjectId,en.CreationDate,en.Details,en.Priority,en.ProductId,en.CompletionDate,en.ExpectedCompletion,en.StatusId,ss.Status,us.DisplayName,tt.Type " +
                           "from Tasks en,TaskStatus ss,Users us,TaskType tt " +
                           "where tt.Id=en.TypeId and ss.Id=en.StatusId and us.UserId=en.AssignedTo and en.AssignedTo=" + id +
                           " ORDER BY en.Id DESC";

            var sda = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            sda.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic details, proid, proname, priority, comdate, edate, projid, projname;

                priority = dr["Priority"] == DBNull.Value ? 0 : (int)dr["Priority"];
                comdate = dr["CompletionDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)dr["CompletionDate"];
                edate = dr["ExpectedCompletion"] == DBNull.Value ? DateTime.MinValue : (DateTime)dr["ExpectedCompletion"];
                proid = dr["ProductId"] == DBNull.Value ? 0 : (int)dr["ProductId"];
                projid = dr["ProjectId"] == DBNull.Value ? 0 : (int)dr["ProjectId"];
                details = dr["Details"] == DBNull.Value ? string.Empty : (string)dr["Details"];

                if (proid != 0)
                {
                    string strp = "Select Name from Products where Id=" + proid;
                    var sda1 = new SqlDataAdapter(strp, con);
                    var ds1 = new DataSet();
                    sda1.Fill(ds1);
                    proname = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else { proname = ""; }

                if (projid != 0)
                {
                    string strpj = "Select Project from Project where Id=" + projid;
                    var sda2 = new SqlDataAdapter(strpj, con);
                    var ds2 = new DataSet();
                    sda2.Fill(ds2);
                    projname = ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else { projname = ""; }

                contact.Add(new TaskModel
                {
                    id = (int)dr["Id"],
                    Task = (string)dr["TaskTitle"],
                    CreationDate = (DateTime)dr["CreationDate"],
                    status = (string)dr["Status"],
                    type = (string)dr["Type"],
                    Details = details,
                    ProductId = proid,
                    Product = proname,
                    Priority = priority,
                    ExpectedCompletion = edate,
                    Completion = comdate,
                    Project = projname,
                    ProjectId = projid,
                    assign = (string)dr["DisplayName"]
                });
            }

            return CreatePagedResponse(contact, pagingParameterModel);
        }

        private void PopulateTasks()
        {
            using var con = new SqlConnection(_connectionString);

            const string query = "Select en.Id,en.TaskTitle,en.CreationDate,en.ExpectedCompletion,ss.Status,us.DisplayName,tt.Type " +
                                 "from Tasks en,TaskStatus ss,Users us,TaskType tt " +
                                 "where tt.Id=en.TypeId and us.UserId=en.AssignedTo";

            var sda = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            sda.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                contact.Add(new TaskModel
                {
                    id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0,
                    Task = dr["TaskTitle"] != DBNull.Value ? dr["TaskTitle"].ToString() : "",
                    CreationDate = dr["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreationDate"]) : DateTime.MinValue,
                    status = dr["Status"] != DBNull.Value ? dr["Status"].ToString() : "",
                    type = dr["Type"] != DBNull.Value ? dr["Type"].ToString() : "",
                    assign = dr["DisplayName"] != DBNull.Value ? dr["DisplayName"].ToString() : ""
                });

            }
        }

        private IActionResult CreatePagedResponse(List<TaskModel> source, EnquiryNModel pagingParameterModel)
        {
            int count = source.Count;
            int currentPage = pagingParameterModel?.pageNumber ?? 1;
            if (currentPage < 1)
                currentPage = 1;

            int pageSize = pagingParameterModel?.pageSize ?? 20;
            if (pageSize <= 0)
                pageSize = 20;

            int totalPages = (int)Math.Ceiling(count / (double)pageSize);
            var items = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var paginationMetadata = new
            {
                totalCount = count,
                pageSize,
                currentPage,
                totalPages,
                previousPage = currentPage > 1 ? "Yes" : "No",
                nextPage = currentPage < totalPages ? "Yes" : "No"
            };

            var serializedMetadata = JsonConvert.SerializeObject(paginationMetadata);
            Response.Headers["Paging-Headers"] = serializedMetadata;

            return Ok(items);
        }

        private IActionResult GetTaskById(int taskid)
        {
            using var con = new SqlConnection(_connectionString);

            string query = "Select en.Id,en.TaskTitle,en.ProjectId,en.CreationDate,en.Details,en.Priority,en.ProductId,en.CompletionDate,en.ExpectedCompletion,en.StatusId,ss.Status,us.DisplayName,tt.Type " +
                           "from Tasks en,TaskStatus ss,Users us,TaskType tt " +
                           "where tt.Id=en.TypeId and ss.Id=en.StatusId and us.UserId=en.AssignedTo and en.Id=" + taskid +
                           " ORDER BY en.Id DESC";

            var sda = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            sda.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic details, proid, proname, priority, comdate, edate, projid, projname;

                priority = dr["Priority"] == DBNull.Value ? 0 : (int)dr["Priority"];
                comdate = dr["CompletionDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)dr["CompletionDate"];
                edate = dr["ExpectedCompletion"] == DBNull.Value ? DateTime.MinValue : (DateTime)dr["ExpectedCompletion"];
                proid = dr["ProductId"] == DBNull.Value ? 0 : (int)dr["ProductId"];
                projid = dr["ProjectId"] == DBNull.Value ? 0 : (int)dr["ProjectId"];
                details = dr["Details"] == DBNull.Value ? string.Empty : (string)dr["Details"];

                if (proid != 0)
                {
                    string strp = "Select Name from Products where Id=" + proid;
                    var sda1 = new SqlDataAdapter(strp, con);
                    var ds1 = new DataSet();
                    sda1.Fill(ds1);
                    proname = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else { proname = ""; }

                if (projid != 0)
                {
                    string strpj = "Select Project from Project where Id=" + projid;
                    var sda2 = new SqlDataAdapter(strpj, con);
                    var ds2 = new DataSet();
                    sda2.Fill(ds2);
                    projname = ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else { projname = ""; }

                contact.Add(new TaskModel
                {
                    id = (int)dr["Id"],
                    Task = (string)dr["TaskTitle"],
                    CreationDate = (DateTime)dr["CreationDate"],
                    status = (string)dr["Status"],
                    type = (string)dr["Type"],
                    Details = details,
                    ProductId = proid,
                    Product = proname,
                    Priority = priority,
                    ExpectedCompletion = edate,
                    Completion = comdate,
                    Project = projname,
                    ProjectId = projid,
                    assign = (string)dr["DisplayName"]
                });
            }

            return Ok(contact);
        }
    }
}
