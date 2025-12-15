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
    public class QCloseController : ApiController
    {
        List<QCloseModel> booker;
        public QCloseController()
        {
            booker = new List<QCloseModel>();
        }
        public IEnumerable<QCloseModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Count(Id) as no from Quotation where Status=2";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new QCloseModel
                {
                    CloseQuotation = (int)dr["no"],


                });
            }
            return booker;
        }

        public IEnumerable<QCloseModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Count(Id) as no from Quotation where Status=2 and AssignedId=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                booker.Add(new QCloseModel
                {
                    CloseQuotation = (int)dr["no"],


                });
            }
            return booker;
        }



    }
}