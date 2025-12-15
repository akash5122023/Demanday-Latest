using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Serenity.Data;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class PunchOutController : ApiController
    {
        List<PunchInModel> PunchIn;
        public PunchOutController()
        {
            PunchIn = new List<PunchInModel>();
        }
        public IEnumerable<PunchInModel> Get(int id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select PunchOut from Attendance where DateNTime='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and Name=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int i = ds.Tables[0].Rows.Count;
            if (i == 1)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PunchIn.Add(new PunchInModel
                    {
                        PunchOut = "Yes",
                        PunchIn="No"


                    });
                }
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PunchIn.Add(new PunchInModel
                    {
                        PunchOut = "No",
                        PunchIn = "No"

                    });
                }

            }

            return PunchIn;
        }
    }
}