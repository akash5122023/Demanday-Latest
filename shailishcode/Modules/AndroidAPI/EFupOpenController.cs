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
    public class EFupOpenController : ApiController
    {
        List<EFupOpenModel> booker;
        public EFupOpenController()
        {
            booker = new List<EFupOpenModel>();
        }
        public IEnumerable<EFupOpenModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string str = "Select  Count(Id) as no from EnquiryFollowUps where Status=1 and FollowupDate >= '" + dt + "'";/// and RepresentativeId=" + id;

            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new EFupOpenModel
                {
                    OpenEnquiryFollowup = (int)dr["no"],


                });
            }
            return booker;
        }

        public IEnumerable<EFupOpenModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string str = "Select  Count(Id) as no from EnquiryFollowUps where Status=1 and FollowupDate >= '" + dt + "' and RepresentativeId=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new EFupOpenModel
                {
                    OpenEnquiryFollowup = (int)dr["no"],


                });
            }
            return booker;
        }



    }
}