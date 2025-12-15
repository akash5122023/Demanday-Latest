using AdvanceCRM.Settings;
using AdvanceCRM.ThirdParty;
using AdvanceCRM.Attendance;
using AdvanceCRM.Contacts;
using AdvanceCRM.Quotation;
using AdvanceCRM.Enquiry;
using AdvanceCRM.Products;
using AdvanceCRM.Tasks;
using Serenity.Data;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;

namespace AdvanceCRM.Modules.ThirdParty
{
    public class LeadsController : ApiController
    {

        private readonly ISqlConnections _connections;

        double dist = 0;
        private string result1;
        public string sulkharesult;

        public LeadsController(ISqlConnections connections)
        {
            _connections = connections;
        }

        /// <summary>
        ///Enquiry From Website
        /// </summary> Id,ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId
        ///

        [HttpPost]
        public string QuotationFollowUp(string Note,string Date, string Details, int status, int QuotationId, int Owner)
        {
           // try
            {
               // var datetime = DateTime.Now.ToString("yyyy-MM-dd");
                // DateTime datetime = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                string str = "Insert into QuotationFollowups([FollowUpNote],[FollowupDate],[Status],[Details],[RepresentativeId],[QuotationId]) values " +
                    "('" + Note + "','" + Date + "'," + status + ",'" + Details + "'," + Owner + "," + QuotationId + ")";
                using (var innerConnection = _connections.NewFor<QuotationFollowupsRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return " Quotation FollowUp added Successfully";
        }

        [HttpPost]
        public string EnquiryFollowUp(string Note,string Date,string Details, int status,int EnquiryId, int Owner)
        {
           // try
            {
               // var datetime = DateTime.Now.ToString("yyyy-MM-dd");
                // DateTime datetime = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                string str = "Insert into EnquiryFollowups([FollowUpNote],[FollowupDate],[Status],[Details],[RepresentativeId],[EnquiryId]) values " +
                    "('" + Note + "','" + Date + "'," + status + ",'" + Details + "'," + Owner + "," + EnquiryId + ")";
                using (var innerConnection = _connections.NewFor<EnquiryFollowupsRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "Enquiry FollowUp added Successfully";
        }

        [HttpPost]
        public string Tasks(int Project, string task, int Status,string Details,int CreatedBy,int AssignedTo, DateTime Exdate)
        {
            var result1 = "";
           // try
            {
                dynamic exdatef;
                string str = string.Empty;
                var datetimestr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (Exdate == DateTime.MinValue)
                {
                    str = "Insert into Tasks([ProjectId],[Task],[CreationDate],[StatusId],[TypeId],[Details],[AssignedBy],[AssignedTo]) values " +
                    "(" + Project + ",'" + task + "','" + datetimestr + "','1','1','" + Details + "'," + CreatedBy + "," + AssignedTo + ")";
                    
                }
                else
                {
                    exdatef = Exdate.ToString("yyyy-MM-dd HH:mm:ss");
                    str = "Insert into Tasks([ProjectId],[Task],[CreationDate],[StatusId],[TypeId],[Details],[AssignedBy],[AssignedTo],[ExpectedCompletion]) values " +
                 "(" + Project + ",'" + task + "','" + datetimestr + "','1','1','" + Details + "'," + CreatedBy + "," + AssignedTo + ",'" + exdatef + "')";

                }

                // DateTime datetime = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                //str = "Insert into Tasks([ProjectId],[Task],[CreationDate],[StatusId],[TypeId],[Details],[AssignedBy],[AssignedTo],[ExpectedCompletion]) values " +
                //  "(" + Project + ",'" + task + "','" + datetimestr + "','1','1','" + Details + "'," + CreatedBy + "," + AssignedTo + ",'"+exdatef+"')";


                using (var innerConnection = _connections.NewFor<TasksRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "Success";


        }
        [HttpPost]
        public string TasksUpdate(int TaskId,int Project,DateTime datetime, string task, int Status, string Details, int CreatedBy, int AssignedTo, DateTime Complete)
        {
           // try
            {
                string str = string.Empty;
                // var datetime2 = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                var datetimestr = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                dynamic closingdate;//= Complete.ToString("yyyy-MM-dd HH:mm:ss");
                if (Complete == DateTime.MinValue)
                {
                    str = "Update Tasks Set [ProjectId]=" + Project + ",[Task]='" + task + "',[StatusId]='" + Status + "',[ExpectedCompletion]='" + datetimestr + "',[Details]='" + Details + "',[AssignedBy]=" + CreatedBy + ",[AssignedTo]=" + AssignedTo + " where Id=" + TaskId;

                }
                else
                {
                    closingdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                   str = "Update Tasks Set [ProjectId]=" + Project + ",[Task]='" + task + "',[StatusId]='" + Status + "',[ExpectedCompletion]='" + datetimestr + "',[CompletionDate]='" + closingdate + "',[Details]='" + Details + "',[AssignedBy]=" + CreatedBy + ",[AssignedTo]=" + AssignedTo + " where Id=" + TaskId;

                }

                //DateTime datetime1 = DateTime.ParseExact(datetime2, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                //var dd = Convert.ToString(datetime1);

                // String str = "Update Tasks Set [ProjectId]=" + Project + ",[Task]='" + task + "',[StatusId]='" + Status + "',[ExpectedCompletion]='" + datetimestr + "',[CompletionDate]='" + closingdate + "',[Details]='" + Details + "',[AssignedBy]=" + CreatedBy + ",[AssignedTo]=" + AssignedTo + " where Id=" + TaskId;
                using (var innerConnection = _connections.NewFor<TasksRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "Task Updated Successfully";


        }

        [HttpPost]
        public string EnquiryUpdate(int EnquiryId,int ContactId,int Source,int status,int Stage,int Owner,int Assign,string Description, int EnquiryNo,string EnquiryN)
        {
           
                var datetime = DateTime.Now.ToString("yyyy-MM-dd");

              string str = "Update Enquiry Set [ContactsId]=" + ContactId + ",[Date]='" + datetime + "',[Status]='" + status + "',[SourceId]='"+ Source + "',[StageId]='"+ Stage + "',[OwnerId]='" + Owner + "',[AssignedId]='" + Assign + "',[AdditionalInfo]='" + Description + "',[EnquiryNo]=" + EnquiryNo + ",[EnquiryN]='" + EnquiryN + "',[CompanyId]=1 where Id=" + EnquiryId;

                using (var innerConnection = _connections.NewFor<EnquiryRow>())
                {
                    innerConnection.Execute(str);
                }
              
           return "Enquiry Updated Successfully";
        }

        [HttpPost]
        public string IVRUpdate(string ivrno)
        {
            string str = "Update IVRConfiguration Set [IVRNumber]='+" + ivrno + "' where Id=1";

            using (var innerConnection = _connections.NewFor<IVRConfigurationRow>())
            {
                innerConnection.Execute(str);
            }

            return "IVR Number Updated Successfully";
        }


        [HttpPost]
        public string Enquiry(int ContactId, int Source, int status, int Stage, int Owner, int Assign, string Description, int EnquiryNo, String EnquiryN)
        {
           
            var datetime = DateTime.Now.ToString("yyyy-MM-dd");
            string str = "Insert into Enquiry([ContactsId],[Date],[Status],[SourceId],[StageId],[OwnerId],[AssignedId],[AdditionalInfo],[EnquiryNo],[EnquiryN],[CompanyId]) values " +
                "('" + ContactId + "','" + datetime + "','" + status + "','" + Source + "','" + Stage + "','" + Owner + "','" + Assign + "','" + Description + "','" + EnquiryNo + "','" + EnquiryN + "','1')";


            using (var innerConnection = _connections.NewFor<EnquiryRow>())
            {
                innerConnection.Execute(str);
            }
            return "Enquiry added Successfully";
        }

      


        [HttpPost]
        public string Product(string Name, string SellingPrice, string MRP, string Description)
        {
           // try
            {
                string str = "Insert into Products([Name],[SellingPrice],[Mrp],[DivisionId],[Description]) values " + "('" + Name + "','"+SellingPrice+"','" + MRP + "',1,'"+Description+"')";
                using (var innerConnection = _connections.NewFor<ProductsRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "product added Successfully";
        }

        [HttpPost]
        public string contact(int Contacttype,string Name,string Phone, string MailId, string Address, int OwnerId,int assignid)
        {
            //var tec=0;
            //try
            //{
           

            SqlConnection con = new SqlConnection(Startup.connectionString);
                string str = "Insert into Contacts ([Name],[Contacttype],[Phone],[Email],[Address],[OwnerId],[AssignedId]) values " + "('"+Name+"',"+ Contacttype + ",'"+Phone+"','"+MailId+"','"+Address+"',"+OwnerId+","+assignid+")";
                using (var innerConnection = _connections.NewFor<ContactsRow>())
                {
                    innerConnection.Execute(str);
                }
                string str1 = "Select Max(Id) from Contacts";
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
                SqlCommand sda = new SqlCommand(str1, con);
               var tec = Convert.ToInt32(sda.ExecuteScalar());
                
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "Contact added Successfully Max Id " +tec;
        }

        [HttpPost]
        public string contactUpdate(int ContactId,int Contacttype, string Name, string Phone, string MailId, string Address, int OwnerId, int assignid)
        {          

            string str = "Update Contacts Set [Name]=" + Name + ",[Contacttype]='" + Contacttype + "',[Phone]='" + Phone + "',[Email]='" + MailId + "',[Address]='" + Address + "',[OwnerId]='" + OwnerId + "',[AssignedId]='" + assignid + "' where Id=" + ContactId;
            using (var innerConnection = _connections.NewFor<ContactsRow>())
            {
                innerConnection.Execute(str);
            }

            return "Contact Updated Successfully";
        }


        [HttpPost]
        public string SubContact(int ContactsId, string Name, string Phone, string MailId, string Address)
        {
            //var tec=0;
            //try
            //{

            SqlConnection con = new SqlConnection(Startup.connectionString);
            string str = "Insert into SubContacts ([ContactsId],[Name],[Phone],[Email],[Address]) values " + "("+ContactsId+",'" + Name + "','" + Phone + "','" + MailId + "','" + Address + "')";
            using (var innerConnection = _connections.NewFor<SubContactsRow>())
            {
                innerConnection.Execute(str);
            }
        

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "Sub-Contact added Successfully";
        }

        [HttpPost]
        public string SubContactUpdate(int SubConId,int ContactsId,string Name, string Phone, string MailId, string Address)
        {
            string str = "Update SubContacts Set [ContactsId]="+ ContactsId + ",[Name]=" + Name + ",[Phone]='" + Phone + "',[Email]='" + MailId + "',[Address]='" + Address + "' where Id=" + SubConId;
            using (var innerConnection = _connections.NewFor<SubContactsRow>())
            {
                innerConnection.Execute(str);
            }
            return "Sub-Contact Updated Successfully";
        }


        //[HttpPost]
        //public String PunchOut(int UserId)
        //{
        //    //try
        //    {

        //        var punch = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //        //var datetimestr = date + " " + time;
        //        var datetime = DateTime.Now.ToString("yyyy-MM-dd");

        //       // var datetime = DateTime.Now.ToString("yyyy-MM-dd");
        //        SqlConnection con = new SqlConnection(Startup.connectionString);
        //        con.Open();
        //        ////Visit Count///
        //        int vc = 0;
        //        string vstr = "Select Count(Id) from Visit where DateNTime='" + datetime + "' and CreatedBy=" + UserId;
        //        SqlCommand cmd = new SqlCommand(vstr, con);
        //        vc = Convert.ToInt32(cmd.ExecuteScalar());
        //        if (vc > 0)
        //        {
        //            string strlist = "select Id,Location from Visit where DateNTime='" + datetime + "' and CreatedBy=" + UserId + " ORDER BY Id DESC";
        //            SqlDataAdapter sda = new SqlDataAdapter(strlist, con);
        //            DataSet ds = new DataSet();
        //            sda.Fill(ds);
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                for (int i = 0; i < ds.Tables[0].Rows.Count-1; i++)
        //                {
        //                    double lat1 = 0, lat2 = 0, lon1 = 0, lon2 = 0;

        //                    string loc1 = ds.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
        //                    string loc2 = ds.Tables[0].Rows[i + 1].ItemArray.GetValue(1).ToString();
        //                    int j = 0, k = 0;
        //                    string[] loc1list = loc1.Split(',');
        //                    foreach (string subloc1 in loc1list)
        //                        if (j == 0)
        //                        {
        //                            lat1 = Convert.ToDouble(subloc1);
        //                            j = 1;
        //                        }
        //                        else
        //                        {
        //                            lon1 = Convert.ToDouble(subloc1);
        //                        }

        //                    /////////Location2
        //                    string[] loc2list = loc2.Split(',');
        //                    foreach (string subloc2 in loc2list)
        //                        if (k == 0)
        //                        {
        //                            lat2 = Convert.ToDouble(subloc2);
        //                            k = 1;
        //                        }
        //                        else
        //                        {
        //                            lon2 = Convert.ToDouble(subloc2);
        //                        }



        //                    double dlon = lon2 - lon1;
        //                    double dlat = lat2 - lat1;
        //                    double a = Math.Pow(Math.Sin(dlat / 2), 2) +
        //                               Math.Cos(lat1) * Math.Cos(lat2) *
        //                               Math.Pow(Math.Sin(dlon / 2), 2);

        //                    double c = 2 * Math.Asin(Math.Sqrt(a));

        //                    // Radius of earth in
        //                    // kilometers. Use 3956
        //                    // for miles
        //                    double r = 6371;

        //                    // calculate the result
        //                    dist = (c * r)/10;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            dist = 0;
        //        }

        //        string str = "Update Attendance set [PunchOut]='"
        //         + punch + "',[Distance]="+dist+" where [DateNTime]='" + datetime + "' and [Name]="+UserId;


        //        using (var innerConnection = _connections.NewFor<AttendanceRow>())
        //        {
        //            innerConnection.Execute(str);
        //        }
        //    }
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine(ex);
        //    //}

        //    return "Punchout added Successfully";
        //}


        [HttpPost]
        public string PunchOut(int UserId)
        {
            try
            {
                var punch = DateTime.Now;
                var datetime = punch.ToString("yyyy-MM-dd");
                double dist = 0;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                {
                    con.Open();

                    // Retrieve PunchIn Time
                    string punchInQuery = "SELECT PunchIn FROM Attendance WHERE DateNTime = @Date AND Name = @UserId";
                    using (var cmd = new SqlCommand(punchInQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Date", datetime);
                        cmd.Parameters.AddWithValue("@UserId", UserId);

                        var punchInObj = cmd.ExecuteScalar();
                        if (punchInObj == null || !DateTime.TryParse(punchInObj.ToString(), out DateTime punchInTime))
                        {
                            return "PunchIn record not found for this user on the given date.";
                        }

                        // Calculate time difference in hours
                        double hoursDiff = (punch - punchInTime).TotalHours;
                        int type = (hoursDiff > 4) ? 1 : 3;

                        // Get Visit Count
                        string visitCountQuery = "SELECT COUNT(Id) FROM Visit WHERE DateNTime = @Date AND CreatedBy = @UserId";
                        using (var vcCmd = new SqlCommand(visitCountQuery, con))
                        {
                            vcCmd.Parameters.AddWithValue("@Date", datetime);
                            vcCmd.Parameters.AddWithValue("@UserId", UserId);

                            int visitCount = Convert.ToInt32(vcCmd.ExecuteScalar());
                            if (visitCount > 0)
                            {
                                // Fetch visit locations
                                string locationQuery = "SELECT Location FROM Visit WHERE DateNTime = @Date AND CreatedBy = @UserId ORDER BY Id DESC";
                                using (var locCmd = new SqlCommand(locationQuery, con))
                                {
                                    locCmd.Parameters.AddWithValue("@Date", datetime);
                                    locCmd.Parameters.AddWithValue("@UserId", UserId);

                                    using (var reader = locCmd.ExecuteReader())
                                    {
                                        List<string> locations = new List<string>();
                                        while (reader.Read())
                                        {
                                            locations.Add(reader.GetString(0));
                                        }

                                        // Calculate distance using Haversine formula
                                        dist = CalculateTotalDistance(locations);
                                    }
                                }
                            }
                        }

                        // Update PunchOut time, Type, and Distance
                        string updateQuery = "UPDATE Attendance SET PunchOut = @PunchOut, Type = @Type, Distance = @Distance WHERE DateNTime = @Date AND Name = @UserId";
                        using (var updateCmd = new SqlCommand(updateQuery, con))
                        {
                            updateCmd.Parameters.AddWithValue("@PunchOut", punch);
                            updateCmd.Parameters.AddWithValue("@Type", type);
                            updateCmd.Parameters.AddWithValue("@Distance", dist);
                            updateCmd.Parameters.AddWithValue("@Date", datetime);
                            updateCmd.Parameters.AddWithValue("@UserId", UserId);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }

                return "PunchOut added successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Haversine formula to calculate total distance
        private double CalculateTotalDistance(List<string> locations)
        {
            double totalDistance = 0;
            for (int i = 0; i < locations.Count - 1; i++)
            {
                string[] loc1 = locations[i].Split(',');
                string[] loc2 = locations[i + 1].Split(',');

                if (double.TryParse(loc1[0], out double lat1) && double.TryParse(loc1[1], out double lon1) &&
                    double.TryParse(loc2[0], out double lat2) && double.TryParse(loc2[1], out double lon2))
                {
                    double dlat = ToRadians(lat2 - lat1);
                    double dlon = ToRadians(lon2 - lon1);
                    double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                               Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                               Math.Pow(Math.Sin(dlon / 2), 2);
                    double c = 2 * Math.Asin(Math.Sqrt(a));
                    double r = 6371; // Radius of Earth in km
                    totalDistance += c * r;
                }
            }
            return totalDistance;
        }

        // Convert degrees to radians
        private double ToRadians(double angle)
        {
            return angle * (Math.PI / 180);
        }
        [HttpGet]
        [Route("api/Leads/CheckPunchStatus")]
        public string CheckPunchStatus(int UserId)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");

            using (var connection = _connections.NewFor<AttendanceRow>())
            {
                string query = @"SELECT TOP 1 PunchIn, PunchOut FROM Attendance 
                         WHERE Name = @UserId AND CAST(DateNTime AS DATE) = @Date 
                         ORDER BY DateNTime DESC";

                connection.Open(); // ✅ Ensure the connection is open before executing commands

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@UserId", UserId));
                    command.Parameters.Add(new SqlParameter("@Date", date));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var punchIn = reader["PunchIn"] as DateTime?;
                            var punchOut = reader["PunchOut"] as DateTime?;

                            if (punchIn != null)
                                return "PunchedIn";  // User punched in but not out

                            //if (punchIn != null && punchOut != null && punchOut != punchIn)
                            //    return "PunchedOut";  // User punched in and out                            //if (punchIn != null && punchOut != null && punchOut != punchIn)
                            //    return "PunchedOut";  // User punched in and out
                        }
                    }
                }
            }
            return "NoPunch";  // No record found
        }

        [HttpPost]
        public string PunchIn(int UserId, String Location, String Coordinates)
        {
          //  try
            {
                 var date = DateTime.Now.ToString("yyyy-MM-dd");
                var punch = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string str = "INSERT INTO Attendance([Name], [DateNTime], [Location], [Coordinates], [PunchIn],[PunchOut],[Type]) Values" +
                "(" + UserId + ",'"
                 + date + "','"
                 + Location + "','"
                 + Coordinates + "','"
                 + punch + "','"
                 + punch + "',3)";

                using (var innerConnection = _connections.NewFor<AttendanceRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "Punch-in added successfully";
        }

        [HttpPost]
        public string TicketWeb(String user, String pass, String name, String phone, String email, String address, String requirement,string ProductName,string PurchaseDate,string Complaint)
        {
            using (var connection = _connections.NewFor<TicketWebRow>())
            {
                var s = TicketWebRow.Fields;
                var Config = connection.TryFirst<TicketWebRow>(q => q
                   .SelectTableFields()
                   .Select(s.Username)
                   .Select(s.Password)
                   );

                if (Config.Username != user || Config.Password != pass)
                    return "Authentication Failed";
            }

            try
            {
                DateTime datetime = DateTime.Now;

                string str = "INSERT INTO [TicketWebDetails] ([Name], [Phone], [Email], [Address], [Requirement], [DateTime],[ProductName],[PurchaseDate],[ComplaintDetails]) VALUES " +
                "('" + name + "','"
                 + phone + "','"
                 + email + "','"
                 + address + "','"
                 + requirement + "',"
                 + datetime.ToSql() + ",'"
                 + ProductName + "','"
                 + PurchaseDate + "','"
                 + Complaint + "')";

                using (var innerConnection = _connections.NewFor<TicketWebDetailsRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            catch (Exception ex)
            {

                return "Error: " + ex.Message.ToString();
            }


            return "Success";
        }



        [HttpPost]
        public string Website(String user, String pass, String name, String phone, String email, String address, String requirement)
        {
            using (var connection = _connections.NewFor<WebsiteEnquiryConfigurationRow>())
            {
                var s = WebsiteEnquiryConfigurationRow.Fields;
                var Config = connection.TryFirst<WebsiteEnquiryConfigurationRow>(q => q
                   .SelectTableFields()
                   .Select(s.Username)
                   .Select(s.Password)
                   );

                if (Config.Username != user || Config.Password != pass)
                    return "Authentication Failed";
            }

            try
            {
                DateTime datetime = DateTime.Now;

                string str = "INSERT INTO [WebsiteEnquiryDetails] ([Name], [Phone], [Email], [Address], [Requirement], [DateTime]) VALUES " +
                "('" + name + "','"
                 + phone + "','"
                 + email + "','"
                 + address + "','"
                 + requirement + "',"
                 + datetime.ToSql() + ")";

                using (var innerConnection = _connections.NewFor<WebsiteEnquiryRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            catch (Exception ex)
            {

                return "Error: " + ex.Message.ToString();
            }


            return "Success";
        }

        [HttpPost]
        public string botWebsite(String name, String phone, String email, String address, String requirement)
        {
            try
            {
                DateTime datetime = DateTime.Now;

                string str = "INSERT INTO [WebsiteEnquiryDetails] ([Name], [Phone], [Email], [Address], [Requirement], [DateTime]) VALUES " +
                "('" + name + "','"
                 + phone + "','"
                 + email + "','"
                 + address + "','"
                 + requirement + "',"
                 + datetime.ToSql() + ")";

                using (var innerConnection = _connections.NewFor<WebsiteEnquiryRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            catch (Exception ex)
            {

                return "Error: " + ex.Message.ToString();
            }


            return "Success";
        }

        //[HttpPost]
        //public string JustDialJson(String leadid, String leadtype, String prefix, String name, String mobile, String phone, String email, String date, String category, String city, String area, String brancharea, String dncmobile, String dncphone, String company, String pincode, String time, String branchpin, String parentid)
        //{
        //    try
        //    {
        //        var datetimestr = date + " " + time;
        //        DateTime datetime = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        //        string str = "INSERT INTO JustDialDetails ([LeadId], [LeadType], [Prefix], [Name], [Mobile], [Landline], [Email], [DateTime], [Category], [City], [Area], [BranchArea], [DCNMobile], [DCNPhone], [Company], [Pin], [BranhPin], [ParentId]) VALUES " +
        //        "('" + leadid + "','"
        //         + leadtype + "','"
        //         + prefix + "','"
        //         + name + "','"
        //         + mobile + "','"
        //         + phone + "','"
        //         + email + "',"
        //         + datetime.ToSql() + ",'"
        //         + category + "','"
        //         + city + "','"
        //         + area + "','"
        //         + brancharea + "','"
        //         + dncmobile + "','"
        //         + dncphone + "','"
        //         + company + "','"
        //         + pincode + "','"
        //         + branchpin + "','"
        //         + parentid + "')";

        //        using (var innerConnection = _connections.NewFor<JustDialDetailsRow>())
        //        {
        //            innerConnection.Execute(str);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //    return "Received";
        //}


        [HttpPost]
        public IHttpActionResult JustdialJSON([FromBody] MyData data)
        {

            try
            {
                var datetimestr = data.date + " " + data.time;
                DateTime datetime = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                string str = "INSERT INTO JustDialDetails ([LeadId], [LeadType], [Prefix], [Name], [Mobile], [Landline], [Email], [DateTime], [Category], [City], [Area], [BranchArea], [DCNMobile], [DCNPhone], [Company], [Pin], [BranhPin], [ParentId]) VALUES " +
                "('" + data.leadid + "','" + data.leadtype + "','" + data.prefix + "','" + data.name + "','" + data.mobile + "','"
                 + data.phone + "','" + data.email + "'," + datetime.ToSql() + ",'" + data.category + "','" + data.city + "','" + data.area + "','" + data.brancharea + "','"
                 + data.dncmobile + "','" + data.dncphone + "','" + data.company + "','" + data.pincode + "','" + data.branchpin + "','" + data.parentid + "')";

                using (var innerConnection = _connections.NewFor<JustDialDetailsRow>())
                {
                    innerConnection.Execute(str);
                }
                // do something with the data
                result1 = "Success";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var result = new { message = result1 };
            return Ok(result);
        }

        /// <summary> Sulkha Integration


        public class MyData
        {
            public string leadid { get; set; }
            public string leadtype { get; set; }
            public string prefix { get; set; }
            public string name { get; set; }
            public string mobile { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string date { get; set; }
            public string category { get; set; }
            public string city { get; set; }
            public string area { get; set; }
            public string brancharea { get; set; }
            public string dncmobile { get; set; }
            public string dncphone { get; set; }
            public string company { get; set; }
            public string pincode { get; set; }
            public string time { get; set; }
            public string branchpin { get; set; }
            public string parentid { get; set; }
        }

        //[HttpPost]
        //public IHttpActionResult IVRJSON([FromBody] YOCC data)
        //{

        //    try
        //    {
        //        var datetimestr = data.CallDate + " " + data.StartTime;
        //        var enddate = data.CallDate + " " + data.EndTime;
        //      //  int sec = int.Parse(DateTime.Parse(enddate.ToSql()) -DateTime.Parse(datetimestr.ToSql()));
        //        DateTime datetime = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        //        DateTime datetime1 = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        //    //    DateTime sec =datetime1 - datetime;

        //        string str = "INSERT INTO KnowlarityDetails ([Name],[CustomerNumber],[EmployeeNumber],[Type],[Recording],[DateTime]) VALUES " +
        //        "('Unknown','"
        //         + data.CallerNo + "','"
        //         + data.AgentNo + "','"
        //         + data.CallStatus + "','"
        //         + data.recordingurl + "','"                              
        //         + datetime.ToSql() + "')";



        //        using (var innerConnection = _connections.NewFor<KnowlarityDetailsRow>())
        //        {
        //            innerConnection.Execute(str);
        //        }
        //        // do something with the data
        //        result1 = "Success";

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //    var result = new { message = result1 };
        //    return Ok(result);
        //}
        //public class YOCC
        //{
        //    public string CallerNo { get; set; }
        //    public string CallDate { get; set; }
        //    public string StartTime { get; set; }
        //    public string EndTime { get; set; }
        //    public string AgentNo { get; set; }
        //    public string CallStatus { get; set; }
        //    public string recordingurl { get; set; }
        //    public string CallType { get; set; }
            
        //}



        /// <summary>
        ///Enquiry From JustDial
        /// </summary>
        [HttpPost]
        public string JustDial(String leadid, String leadtype, String prefix, String name, String mobile, String phone, String email, String date, String category, String city, String area, String brancharea, String dncmobile, String dncphone, String company, String pincode, String time, String branchpin, String parentid)
        {
            try
            {
                var datetimestr = date + " " + time;
                DateTime datetime = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                string str = "INSERT INTO JustDialDetails ([LeadId], [LeadType], [Prefix], [Name], [Mobile], [Landline], [Email], [DateTime], [Category], [City], [Area], [BranchArea], [DCNMobile], [DCNPhone], [Company], [Pin], [BranhPin], [ParentId]) VALUES " +
                "('" + leadid + "','"
                 + leadtype + "','"
                 + prefix + "','"
                 + name + "','"
                 + mobile + "','"
                 + phone + "','"
                 + email + "',"
                 + datetime.ToSql() + ",'"
                 + category + "','"
                 + city + "','"
                 + area + "','"
                 + brancharea + "','"
                 + dncmobile + "','"
                 + dncphone + "','"
                 + company + "','"
                 + pincode + "','"
                 + branchpin + "','"
                 + parentid + "')";

                using (var innerConnection = _connections.NewFor<JustDialDetailsRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return "Received";
        }

        [HttpPost]
        //public string Facebook(String Name, String Phone, String Email, String CompaignName, String AdSetName, String CreatedTime)
        //{
        //    try
        //    {
        //        var datetimestr = CreatedTime;
        //        DateTime datetime = DateTime.ParseExact(datetimestr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        //        //string str = "INSERT INTO FacebookDetails ([Name],[Phone],[Email],[CompaignName],[AdSetName],[CreatedTime]) VALUES " +
        //        //"('" + Name + "','"
        //        // + Phone + "','"
        //        // + Email + "','"
        //        // + CompaignName + "','"
        //        // + AdSetName + "',"
        //        // + datetime.ToSql() +  ")";
        //        string query = "INSERT INTO FacebookDetails ([Name],[Phone],[Email],[CompaignName],[AdSetName],[CreatedTime]) " +
        //       "VALUES (@Name, @Phone, @Email, @CompaignName, @AdSetName, @CreatedTime)";

        //        using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
        //        {
        //            var command = new SqlCommand(query, connection);
        //            command.Parameters.AddWithValue("@Name", Name);
        //            command.Parameters.AddWithValue("@Phone", Phone);
        //            command.Parameters.AddWithValue("@Email", Email);
        //            command.Parameters.AddWithValue("@CompaignName", CompaignName);
        //            command.Parameters.AddWithValue("@AdSetName", AdSetName);
        //            command.Parameters.AddWithValue("@CreatedTime", datetime.ToSql());

        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }


        //        /*using (var innerConnection = _connections.NewFor<FacebookDetailsRow>())
        //        {
        //            innerConnection.Execute(str);
        //        }*/
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //    return "Received";
        //}
        public string Facebook(string Name, string Phone, string Email, string CompaignName, string AdSetName, string CreatedTime)
        {
            try
            {
                // Parse the CreatedTime into a DateTime object
                DateTime datetime = DateTime.ParseExact(CreatedTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                // SQL query with parameters
                string query = "INSERT INTO FacebookDetails ([Name],[Phone],[Email],[CompaignName],[AdSetName],[CreatedTime]) " +
                               "VALUES (@Name, @Phone, @Email, @CompaignName, @AdSetName, @CreatedTime)";

                // Connect to the database
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@CompaignName", CompaignName);
                        command.Parameters.AddWithValue("@AdSetName", AdSetName);
                        command.Parameters.AddWithValue("@CreatedTime", datetime); // Directly pass the DateTime object

                        // Open connection and execute the query
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                return "Received";
            }
            catch (Exception ex)
            {
                // Log the exception (for debugging)
                Console.WriteLine($"Error: {ex.Message}");

                // Return a meaningful error message to the client
                return $"Error: {ex.Message}";
            }
        }

    }
}
