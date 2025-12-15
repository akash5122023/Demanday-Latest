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
using System.Web;
using Newtonsoft.Json;
using System.IO;
//using System.Web.Script.Serialization;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class EnquiryDetailsController : ApiController
    {
        List<EnquiryModel> Contact;
        public EnquiryDetailsController()
        {
            Contact = new List<EnquiryModel>();
        }
      
        public IEnumerable<EnquiryModel> Get(string id)
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            
            //con.Open();
            string str = "Select en.Id,en.ContactsId,en.AdditionalInfo,cn.Name,en.StageId,en.SourceId,en.OwnerId,en.AssignedId,cn.Phone,cn.Address,en.Date,Status,sr.Source,st.Stage,en.EnquiryN,en.EnquiryNo,us.DisplayName from Enquiry en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.Id =" + id + " ORDER BY en.Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int i = ds.Tables[0].Rows.Count;
            if (i > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic assign, asignid,Adress,additional;
                    if (dr["AssignedId"] == DBNull.Value)
                        asignid = 0;
                    else
                        asignid = (int)dr["AssignedId"];
                    if (dr["Address"] == DBNull.Value)
                        Adress = "";
                    else
                        Adress = (string)dr["Address"];

                    if (dr["AdditionalInfo"] == DBNull.Value)
                        additional = "";
                    else
                        additional = (string)dr["AdditionalInfo"];
                    if (asignid > 0)
                    {
                        string strp = "Select DisplayName from Users where UserId=" + asignid;
                        SqlDataAdapter sda1 = new SqlDataAdapter(strp, con);
                        DataSet ds1 = new DataSet();
                        sda1.Fill(ds1);
                        assign = ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        assign = "";
                    }
                    Contact.Add(new EnquiryModel
                    {
                        id = (int)dr["Id"],
                        Address=Adress,
                        ContactsId = (int)dr["ContactsId"],
                        AdditionalInfo=additional,
                        Name = (string)dr["Name"],
                        Date = (DateTime)dr["Date"],
                        Status = (int)dr["Status"],
                        phone = (string)dr["Phone"],
                        source = (string)dr["Source"],
                        stage = (string)dr["Stage"],
                        Owner = (string)dr["DisplayName"],
                        Assignedid = (int)dr["AssignedId"],
                        EnquiryN = (string)dr["EnquiryN"],
                        EnquiryNo = (int)dr["EnquiryNo"],
                        Assign=assign,
                        Stageid= (int)dr["StageId"],
                        sourceid = (int)dr["SourceId"],
                        Ownerid= (int)dr["OwnerId"],
                    });
                }
            }
           
            return Contact;
        }
    }
}