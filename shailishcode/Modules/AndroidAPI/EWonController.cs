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
    public class EWonController : ApiController
    {
        List<EWonModel> booker;
        public EWonController()
        {
            booker = new List<EWonModel>();
        }
        public IEnumerable<EWonModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Count(Id) as no from Enquiry where ClosingType=1";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new EWonModel
                {
                    WonEnquiry = (int)dr["no"],


                });
            }
            return booker;
        }

        public IEnumerable<EWonModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Count(Id) as no from Enquiry where ClosingType=1 and AssignedId=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new EWonModel
                {
                    WonEnquiry = (int)dr["no"],


                });
            }
            return booker;
        }



    }
}