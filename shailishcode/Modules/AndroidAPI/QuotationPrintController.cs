using System.Collections.Generic;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class QuotationPrintController : ApiController
    {
        List<QuotationModel> Contact;
        public QuotationPrintController()
        {
            Contact = new List<QuotationModel>();
        }

        private DataTable GetData(string query)
        {
            string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
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

        ////
        //protected void GenerateReport(object sender, EventArgs e)
        //{
        //    DataRow dr = GetData("SELECT * FROM Employees where EmployeeId = 1" ).Rows[0]; 
        //    Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
        //  //  Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
        //    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        //    {
        //        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        //        Phrase phrase = null;
        //        PdfPCell cell = null;
        //        PdfPTable table = null;
        //       // Color color = null;

        //        document.Open();

        //        //Header Table
        //        table = new PdfPTable(2);
        //        table.TotalWidth = 500f;
        //        table.LockedWidth = true;
        //        table.SetWidths(new float[] { 0.3f, 0.7f });

        //        //Company Logo
        //        cell = ImageCell("~/images/northwindlogo.gif", 30f, PdfPCell.ALIGN_CENTER);
        //        table.AddCell(cell);

        //        //Company Name and Address
        //        phrase = new Phrase();
        //        phrase.Add(new Chunk("Microsoft Northwind Traders Company\n\n", FontFactory.GetFont("Arial", 16)));
        //        phrase.Add(new Chunk("107, Park site,\n", FontFactory.GetFont("Arial", 8)));
        //        phrase.Add(new Chunk("Salt Lake Road,\n", FontFactory.GetFont("Arial", 8)));
        //        phrase.Add(new Chunk("Seattle, USA", FontFactory.GetFont("Arial", 8)));
        //        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
        //        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        //        table.AddCell(cell);

        //        //Separater Line
        //        //color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
        //        //DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, color);
        //        //DrawLine(writer, 25f, document.Top - 80f, document.PageSize.Width - 25f, document.Top - 80f, color);
        //        //document.Add(table);

        //        table = new PdfPTable(2);
        //        table.HorizontalAlignment = Element.ALIGN_LEFT;
        //        table.SetWidths(new float[] { 0.3f, 1f });
        //        table.SpacingBefore = 20f;

        //        //Employee Details
        //        cell = PhraseCell(new Phrase("Employee Record", FontFactory.GetFont("Arial", 12)), PdfPCell.ALIGN_CENTER);
        //        cell.Colspan = 2;
        //        table.AddCell(cell);
        //        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        //        cell.Colspan = 2;
        //        cell.PaddingBottom = 30f;
        //        table.AddCell(cell);

        //        //Photo
        //        cell = ImageCell(string.Format("~/photos/{0}.jpg", dr["EmployeeId"]), 25f, PdfPCell.ALIGN_CENTER);
        //        table.AddCell(cell);

        //        //Name
        //        phrase = new Phrase();
        //        phrase.Add(new Chunk(dr["TitleOfCourtesy"] + " " + dr["FirstName"] + " " + dr["LastName"] + "\n", FontFactory.GetFont("Arial", 10)));
        //        phrase.Add(new Chunk("(" + dr["Title"].ToString() + ")", FontFactory.GetFont("Arial", 8)));
        //        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
        //        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //        table.AddCell(cell);
        //        document.Add(table);

        //        //DrawLine(writer, 160f, 80f, 160f, 690f);
        //        //DrawLine(writer, 115f, document.Top - 200f, document.PageSize.Width - 100f, document.Top - 200f);

        //        table = new PdfPTable(2);
        //        table.SetWidths(new float[] { 0.5f, 2f });
        //        table.TotalWidth = 340f;
        //        table.LockedWidth = true;
        //        table.SpacingBefore = 20f;
        //        table.HorizontalAlignment = Element.ALIGN_RIGHT;

        //        //Employee Id
        //        table.AddCell(PhraseCell(new Phrase("Employee code:", FontFactory.GetFont("Arial", 8)), PdfPCell.ALIGN_LEFT));
        //        table.AddCell(PhraseCell(new Phrase("000" + dr["EmployeeId"], FontFactory.GetFont("Arial", 8)), PdfPCell.ALIGN_LEFT));
        //        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        //        cell.Colspan = 2;
        //        cell.PaddingBottom = 10f;
        //        table.AddCell(cell);


        //        //Address
        //        table.AddCell(PhraseCell(new Phrase("Address:", FontFactory.GetFont("Arial", 8)), PdfPCell.ALIGN_LEFT));
        //        phrase = new Phrase(new Chunk(dr["Address"] + "\n", FontFactory.GetFont("Arial", 8)));
        //        phrase.Add(new Chunk(dr["City"] + "\n", FontFactory.GetFont("Arial", 8)));
        //        phrase.Add(new Chunk(dr["Region"] + " " + dr["Country"] + " " + dr["PostalCode"], FontFactory.GetFont("Arial", 8)));
        //        table.AddCell(PhraseCell(phrase, PdfPCell.ALIGN_LEFT));
        //        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        //        cell.Colspan = 2;
        //        cell.PaddingBottom = 10f;
        //        table.AddCell(cell);

        //        //Date of Birth
        //        table.AddCell(PhraseCell(new Phrase("Date of Birth:", FontFactory.GetFont("Arial", 8)), PdfPCell.ALIGN_LEFT));
        //        table.AddCell(PhraseCell(new Phrase(Convert.ToDateTime(dr["BirthDate"]).ToString("dd MMMM, yyyy"), FontFactory.GetFont("Arial", 8)), PdfPCell.ALIGN_LEFT));
        //        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        //        cell.Colspan = 2;
        //        cell.PaddingBottom = 10f;
        //        table.AddCell(cell);

        //        //Phone
        //        table.AddCell(PhraseCell(new Phrase("Phone Number:", FontFactory.GetFont("Arial", 8)), PdfPCell.ALIGN_LEFT));
        //        table.AddCell(PhraseCell(new Phrase(dr["HomePhone"] + " Ext: " + dr["Extension"], FontFactory.GetFont("Arial", 8)), PdfPCell.ALIGN_LEFT));
        //        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        //        cell.Colspan = 2;
        //        cell.PaddingBottom = 10f;
        //        table.AddCell(cell);

        //        //Addtional Information
        //        table.AddCell(PhraseCell(new Phrase("Addtional Information:", FontFactory.GetFont("Arial", 8)), PdfPCell.ALIGN_LEFT));
        //        table.AddCell(PhraseCell(new Phrase(dr["Notes"].ToString(), FontFactory.GetFont("Arial", 8)), PdfPCell.ALIGN_JUSTIFIED));
        //        document.Add(table);
        //        document.Close();
        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.Close();
        //        Response.Clear();
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf");
        //        Response.ContentType = "application/pdf";
        //        Response.Buffer = true;
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.BinaryWrite(bytes);
        //        Response.End();
        //        Response.Close();
        //    }
        //}

        //private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
        //{
        //    PdfContentByte contentByte = writer.DirectContent;
        //    contentByte.SetColorStroke(color);
        //    contentByte.MoveTo(x1, y1);
        //    contentByte.LineTo(x2, y2);
        //    contentByte.Stroke();
        //}
        //private static PdfPCell PhraseCell(Phrase phrase, int align)
        //{
        //    PdfPCell cell = new PdfPCell(phrase);
        // Class1.cs   cell.BorderColor = Color.WHITE;
        //    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        //    cell.HorizontalAlignment = align;
        //    cell.PaddingBottom = 2f;
        //    cell.PaddingTop = 0f;
        //    return cell;
        //}
        //private static PdfPCell ImageCell(string path, float scale, int align)
        //{
        //    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
        //    image.ScalePercent(scale);
        //    PdfPCell cell = new PdfPCell(image);
        //    cell.BorderColor = Color.WHITE;
        //    cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        //    cell.HorizontalAlignment = align;
        //    cell.PaddingBottom = 0f;
        //    cell.PaddingTop = 0f;
        //    return cell;
        //}
        ////

        public IEnumerable<QuotationModel> Get()
        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string str = "Select en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId,en.QuotationN,en.QuotationNo from Quotation en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            

            
            return Contact;
            
        }
        //public IEnumerable<QuotationModel> Get(string id)
        //{
        //    SqlConnection con = new SqlConnection(Startup.connectionString);
        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }
        //    con.Open();
        //    string str = "Select en.Id,cn.Name,cn.Phone,en.Date,Status,sr.Source,st.Stage,us.DisplayName,en.AssignedId,en.QuotationN,en.QuotationNo from Quotation en,Source sr,Stage st,Contacts cn,Users us where us.UserId=en.OwnerId and en.ContactsId=cn.Id and st.Id=en.stageId and sr.Id=en.SourceId and en.OwnerId=" + id;
        //    SqlDataAdapter sda = new SqlDataAdapter(str, con);
        //    DataSet ds = new DataSet();
        //    sda.Fill(ds);
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        Contact.Add(new QuotationModel
        //        {
        //            id = (int)dr["Id"],
        //            Name = (string)dr["Name"],
        //            Date = (DateTime)dr["Date"],
        //            Status = (int)dr["Status"],
        //            source = (string)dr["Source"],
        //            stage = (string)dr["Stage"],
        //            Owner = (string)dr["DisplayName"],
        //            Assignedid = (int)dr["AssignedId"],
        //            QuotationN = (string)dr["QuotationN"],
        //            QuotationNo = (int)dr["QuotationNo"]
        //        });
        //    }
        //    return Contact;
        //}

        
    }
}