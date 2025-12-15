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
    public class PurposeController : ApiController
    {
        List<PurposeModel> attend;
        public PurposeController()
        {
            attend = new List<PurposeModel>();
        }
        public IEnumerable<PurposeModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select * from Purpose";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                attend.Add(new PurposeModel
                {
                    id = (int)dr["Id"],
                    Purpose = (String)dr["Purpose"],

                });
            }
            return attend;
        }

        public IEnumerable<PurposeModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select * from Purpose where id=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                attend.Add(new PurposeModel
                {
                    id = (int)dr["Id"],
                    Purpose = (String)dr["Purpose"],

                });
            }
            return attend;
        }
    }
}