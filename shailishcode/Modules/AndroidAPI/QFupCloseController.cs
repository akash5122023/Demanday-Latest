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
    public class QFupCloseController : ApiController
    {
        List<QFupCloseModel> booker;
        public QFupCloseController()
        {
            booker = new List<QFupCloseModel>();
        }
        public IEnumerable<QFupCloseModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string str = "Select  Count(Id) as no from QuotationFollowUps where Status=2 and FollowupDate >= '" + dt + "'";/// and RepresentativeId=" + id;

            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new QFupCloseModel
                {
                    CloseQuotationFollowup = (int)dr["no"],


                });
            }
            return booker;
        }

        public IEnumerable<QFupCloseModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string str = "Select  Count(Id) as no from QuotationFollowUps where Status=2 and FollowupDate >= '" + dt + "' and RepresentativeId=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new QFupCloseModel
                {
                    CloseQuotationFollowup = (int)dr["no"],


                });
            }
            return booker;
        }



    }
}