using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
namespace AdvanceCRM.Modules.AndroidAPI
{
    public class ComplaintTypeController : ApiController
    {
        List<ComplaintTypeModel> attend;
        public ComplaintTypeController()
        {
            attend = new List<ComplaintTypeModel>();
        }
        public IEnumerable<ComplaintTypeModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select * from ComplaintType";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                attend.Add(new ComplaintTypeModel
                {
                    id = (int)dr["Id"],
                    ComplaintType = (String)dr["ComplaintType"],

                });
            }
            return attend;
        }

        public IEnumerable<ComplaintTypeModel> Get(string id)
            // public HttpResponseMessage Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select * from ComplaintType where id=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                attend.Add(new ComplaintTypeModel
                {
                    id = (int)dr["Id"],
                    ComplaintType = (String)dr["ComplaintType"],

                });
            }
            return attend;
        }
    }
}