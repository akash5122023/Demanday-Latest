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
    public class CMSCloseController : ApiController
    {
        List<CMSCloseModel> booker;
        public CMSCloseController()
        {
            booker = new List<CMSCloseModel>();
        }
        public IEnumerable<CMSCloseModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Count(Id) as no from CMS where Status=2";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new CMSCloseModel
                {
                    ClosedCMS = (int)dr["no"],


                });
            }
            return booker;
        }

        public IEnumerable<CMSCloseModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Count(Id) as no from CMS where Status=2 and AssignedTo=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new CMSCloseModel
                {
                    ClosedCMS = (int)dr["no"],


                });
            }
            return booker;
        }



    }
}