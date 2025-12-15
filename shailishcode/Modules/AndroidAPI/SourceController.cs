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
    public class SourceController : ApiController
    {
        List<SourceModel> Source;
        public SourceController()
        {
            Source = new List<SourceModel>();
        }
        public IEnumerable<SourceModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select * from Source";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Source.Add(new SourceModel
                {
                    id = (int)dr["Id"],
                    Source = (String)dr["Source"],

                });
            }
            return Source;
        }

        public IEnumerable<SourceModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);

            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select * from Source where Id=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Source.Add(new SourceModel
                {
                    id = (int)dr["Id"],
                    Source = (String)dr["Source"],

                });
            }
            return Source;
        }
    }
}