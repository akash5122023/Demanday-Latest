using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class ProductsController : ApiController
    {
        List<ProductModel> product;
        public ProductsController()
        {
            product = new List<ProductModel>();
        }
        //public IEnumerable<ProductModel> Get([FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get( [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Id,Name,SellingPrice,Mrp,Description,TaxId1,TaxId2 from Products";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic tax1, tax2, taxper1, taxper2, taxtype1, taxtype2;//obj == DBNull.Value
                if (dr["TaxId1"] == DBNull.Value)
                    tax1 = 0;
                else
                    tax1 = (int)dr["TaxId1"];
                if (dr["TaxId2"] == DBNull.Value)
                    tax2 = 0;
                else
                    tax2 = (int)dr["TaxId2"];

                if (tax1 != 0)
                {
                    string str1 = "Select Percentage,Type from Tax where Id=" + tax1;
                   

                    SqlDataAdapter sda1 = new SqlDataAdapter(str1, con);
                    DataSet ds1 = new DataSet();
                    sda1.Fill(ds1);
                    taxper1 = Convert.ToDouble(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    taxtype1 = Convert.ToString(ds1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                }
                else { taxtype1 = "";  taxper1 = 0;  }
                if (tax2 != 0)
                {
                    string str2 = "Select Percentage,Type from Tax where Id=" + tax2;
                    SqlDataAdapter sda2 = new SqlDataAdapter(str2, con);
                    DataSet ds2 = new DataSet();
                    sda2.Fill(ds2);
                    taxper2 = Convert.ToDouble(ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    taxtype2 = Convert.ToString(ds2.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                }
                else {  taxtype2 = "";  taxper2 = 0; }


                product.Add(new ProductModel
                {
                    id = (int)dr["Id"],
                    Name = (String)dr["Name"],
                    //Code= (String)dr["Code"],
                    //HSN= (String)dr["HSN"],
                    Description = (String)dr["Description"],
                    SellingPrice = (double)dr["SellingPrice"],
                    Mrp = (double)dr["MRP"],
                    //PurchasePrice= (double)dr["PurchasePrice"],
                    TaxId1 = tax1,
                    TaxPer1 = taxper1,
                    TaxType1 = taxtype1,
                    TaxId2 = tax2,
                    TaxPer2 = taxper2,
                    TaxType2 = taxtype2
                });
            }
            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = product.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            // Setting Header  
            var response = Request.CreateResponse(HttpStatusCode.OK, items);
            response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            var abc = JsonConvert.SerializeObject(paginationMetadata);
            // Returing List of Customers Collections  
            return response;
        }

      //  public IEnumerable<ProductModel> Get(string id, [FromUri] EnquiryNModel pagingparametermodel)
            public HttpResponseMessage Get(string id, [FromUri] EnquiryNModel pagingparametermodel)

        {
            SqlConnection con = new SqlConnection(Startup.connectionString);
            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
            //con.Open();
            string str = "Select Id,Name,SellingPrice,Mrp,Description,TaxId1,TaxId2 from Products where Id=" + id + " ORDER BY Id DESC";
            SqlDataAdapter sda = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        sda.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dynamic tax1, tax2, taxper1, taxper2, taxtype1, taxtype2;//obj == DBNull.Value
                if (dr["TaxId1"] == DBNull.Value)
                    tax1 = 0;
                else
                    tax1 = (int)dr["TaxId1"];
                if (dr["TaxId2"] == DBNull.Value)
                    tax2 = 0;
                else
                    tax2 = (int)dr["TaxId2"];

                if (tax1 != 0)
                {
                    string str1 = "Select Percentage,Type from Tax where Id=" + tax1;
                    string str2 = "Select Percentage,Type from Tax where Id=" + tax2;

                    SqlDataAdapter sda1 = new SqlDataAdapter(str1, con);
                    DataSet ds1 = new DataSet();
                    sda.Fill(ds1);
                    taxper1 = Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    taxtype1 = Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    SqlDataAdapter sda2 = new SqlDataAdapter(str2, con);
                    DataSet ds2 = new DataSet();
                    sda.Fill(ds2);
                    taxper2 = Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    taxtype2 = Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());


                }
                else { taxtype1 = ""; taxtype2 = ""; taxper1 = 0; taxper2 = 0;  }


                product.Add(new ProductModel
                {
                    id = (int) dr["Id"],
                    Name = (String)dr["Name"],
                    //Code= (String)dr["Code"],
                    //HSN= (String)dr["HSN"],
                    Description = (String)dr["Description"],
                    SellingPrice = (double)dr["SellingPrice"],
                    Mrp = (double)dr["MRP"],
                    //PurchasePrice= (double)dr["PurchasePrice"],
                    TaxId1 =tax1,
                    TaxPer1=taxper1,
                    TaxType1=taxtype1,
                    TaxId2 = tax2,
                    TaxPer2=taxper2,
                    TaxType2=taxtype2
                });
            }
            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = product.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            // Setting Header  
            var response = Request.CreateResponse(HttpStatusCode.OK, items);
            response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            var abc = JsonConvert.SerializeObject(paginationMetadata);
            // Returing List of Customers Collections  
            return response;
        }
    }
}