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
    public class QLostController : ApiController
    {
        List<QLostModel> booker;
        public QLostController()
        {
            booker = new List<QLostModel>();
        }
        public IEnumerable<QLostModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Count(Id) as no from Quotation where ClosingType=2";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new QLostModel
                {
                    LostQuotation = (int)dr["no"],


                });
            }
            return booker;
        }

        public IEnumerable<QLostModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Count(Id) as no from Quotation where ClosingType=2 and AssignedId=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new QLostModel
                {
                    LostQuotation = (int)dr["no"],


                });
            }
            return booker;
        }



    }
}