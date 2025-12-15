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
    public class CMSMaxController : ApiController
    {
        List<CMSModel> Contact;
        public CMSMaxController()
        {
            Contact = new List<CMSModel>();
        }
        public IEnumerable<CMSModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            //string str = "Select en.Id,en.EnquiryN,en.EnquiryNo,cn.Name,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId,mn.ModelName,mc.ModelCat,mv.Modelvarient) " +
            //    "from Enquiry en,Source sr,Stage st,Contacts cn,Users us,Products p,ModelName mn,ModelCat mc,Modelvarient Mv where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and mv.Id=p.ModelvarientId and sr.Id=en.SourceId and mc.Id=en.ModelCatId and mn.Id=en.ModelNameId and p.Id=en.ProductsId";

            string str = "Select MAX(CMSNo) as CMSNo from CMS";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                Contact.Add(new CMSModel
                {
                    CMSNo = (int)dr["CMSNo"]

                }

                 );
            }
            return Contact;
        }
        public IEnumerable<CMSModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            // string str = "Select en.Id,cn.Name,en.EnquiryNo,en.EnquiryN,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId ";
            string str = "Select MAX(CMSNo) as CMSNo from CMS en,Users u where u.CompanyId=en.CompanyId and u.CompanyId=" + id;
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Contact.Add(new CMSModel
                {
                   
                    CMSNo = (int)dr["CMSNo"]
                });
            }
            return Contact;
        }
    }
}