using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdvanceCRM.Modules.Administration.User;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class ProfileController : ApiController
    {
        List<UserPModel> User;
        public ProfileController()
        {
            User = new List<UserPModel>();
        }
        public IEnumerable<UserPModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
           
            string str = "Select UserId,Username,Phone,NonOperational,IsActive from Users";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic phone,nonop,active,activestat;//obj == DBNull.Value
                if (dr["Phone"] == DBNull.Value)
                    phone = "";
                else
                    phone = (string)dr["Phone"];

                if (dr["NonOperational"] == DBNull.Value)
                    nonop = "";
                else
                    nonop = (bool)dr["NonOperational"];

                if (dr["IsActive"] == DBNull.Value)
                    active = "";
                else
                    active = dr["IsActive"];

                if (active == 1)
                {
                    activestat = "True";
                }
                else
                {
                    activestat = "False";
                }


                User.Add(new UserPModel
                {
                    Id = (int)dr["UserId"],
                    Name = (string)dr["Username"],
                    MobileNo = phone,
                    NonOperational=nonop,
                   IsActive= activestat

                });
            }
            //// Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            //int CurrentPage = pagingparametermodel.pageNumber;

            //// Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            //int PageSize = pagingparametermodel.pageSize;

            //// Display TotalCount to Records to User  
            //int TotalCount = count;

            //// Calculating Totalpage by Dividing (No of Records / Pagesize)  
            //int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            //// Returns List of Customer after applying Paging   
            //var items = User.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            //// if CurrentPage is greater than 1 means it has previousPage  
            //var previousPage = CurrentPage > 1 ? "Yes" : "No";

            //// if TotalPages is greater than CurrentPage means it has nextPage  
            //var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            //// Object which we are going to send in header   
            //var paginationMetadata = new
            //{
            //    totalCount = TotalCount,
            //    pageSize = PageSize,
            //    currentPage = CurrentPage,
            //    totalPages = TotalPages,
            //    previousPage,
            //    nextPage
            //};

            //// Setting Header  
            //HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

            //var abc = JsonConvert.SerializeObject(paginationMetadata);
            //// Returing List of Customers Collections  
            return User;
        }

        // GET api/<controller>/5
        public IEnumerable<UserPModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
           
            string str = "Select UserId,Username,Phone,NonOperational,IsActive from Users where IsActive=1 and Phone=" + id;
            SqlDataAdapter sda1 = new SqlDataAdapter(str, con);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
          
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                dynamic phone, nonop, active, activestat;//obj == DBNull.Value
                if (dr["Phone"] == DBNull.Value)
                    phone = "";
                else
                    phone = (string)dr["Phone"];

                if (dr["NonOperational"] == DBNull.Value)
                    nonop = "";
                else
                    nonop = (bool)dr["NonOperational"];

                if (dr["IsActive"] == DBNull.Value)
                    active = "";
                else
                    active = dr["IsActive"];

                if (active == 1)
                {
                    activestat = "True";
                }
                else
                {
                    activestat = "False";
                }


                User.Add(new UserPModel
                {
                    Id = (int)dr["UserId"],
                    Name = (string)dr["Username"],
                    MobileNo = phone,
                    NonOperational = nonop,
                    IsActive = activestat

                });
            }
            return User;
        }

        
    }
}