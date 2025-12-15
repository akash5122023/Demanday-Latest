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
    public class EFupTotalController : ApiController
    {
        List<EFupTotalModel> booker;
        public EFupTotalController()
        {
            booker = new List<EFupTotalModel>();
        }
        public IEnumerable<EFupTotalModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string str = "Select  Count(Id) as no from EnquiryFollowUps whereFollowupDate >= '" + dt + "'";/// and RepresentativeId=" + id;

            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new EFupTotalModel
                {
                   TotalEnquiryFollowups = (int)dr["no"],


                });
            }
            return booker;
        }

        public IEnumerable<EFupTotalModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string str = "Select  Count(Id) as no from EnquiryFollowUps where FollowupDate >= '" + dt + "' and RepresentativeId=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new EFupTotalModel
                {
                    TotalEnquiryFollowups = (int)dr["no"],


                });
            }
            return booker;
        }



    }
}