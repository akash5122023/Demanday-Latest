using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationPrintController : ControllerBase
    {
        List<QuotationModel> Contact;
        private readonly string _connectionString;

        public QuotationPrintController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            Contact = new List<QuotationModel>();
        }

        private DataTable GetData(string query)
        {
            string conString = _connectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        [HttpGet]
        public IEnumerable<QuotationModel> Get()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                string str = "Select en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId,en.QuotationN,en.QuotationNo from Quotation en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId";

                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                }
            }

            return Contact;
        }
    }
}