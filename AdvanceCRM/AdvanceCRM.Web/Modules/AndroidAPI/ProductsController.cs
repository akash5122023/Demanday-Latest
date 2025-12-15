using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        List<ProductModel> product;
        private readonly string _connectionString;

        public ProductsController(IConfiguration configuration)
        {
            _connectionString = Startup.connectionString;
            product = new List<ProductModel>();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Id,Name,SellingPrice,Mrp,Description,TaxId1,TaxId2 from Products";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic tax1, tax2, taxper1, taxper2, taxtype1, taxtype2;

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

                            using (var sda1 = new SqlDataAdapter(str1, con))
                            {
                                DataSet ds1 = new DataSet();
                                sda1.Fill(ds1);
                                taxper1 = Convert.ToDouble(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                taxtype1 = Convert.ToString(ds1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                            }
                        }
                        else { taxtype1 = ""; taxper1 = 0; }

                        if (tax2 != 0)
                        {
                            string str2 = "Select Percentage,Type from Tax where Id=" + tax2;
                            using (var sda2 = new SqlDataAdapter(str2, con))
                            {
                                DataSet ds2 = new DataSet();
                                sda2.Fill(ds2);
                                taxper2 = Convert.ToDouble(ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                taxtype2 = Convert.ToString(ds2.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                            }
                        }
                        else { taxtype2 = ""; taxper2 = 0; }

                        product.Add(new ProductModel
                        {
                            id = (int)dr["Id"],
                            Name = (String)dr["Name"],
                            Description = (String)dr["Description"],
                            SellingPrice = (double)dr["SellingPrice"],
                            Mrp = (double)dr["MRP"],
                            TaxId1 = tax1,
                            TaxPer1 = taxper1,
                            TaxType1 = taxtype1,
                            TaxId2 = tax2,
                            TaxPer2 = taxper2,
                            TaxType2 = taxtype2
                        });
                    }

                    int CurrentPage = pagingparametermodel.pageNumber;
                    int PageSize = pagingparametermodel.pageSize;
                    int TotalCount = count;
                    int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                    var items = product.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                    var previousPage = CurrentPage > 1 ? "Yes" : "No";
                    var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

                    var paginationMetadata = new
                    {
                        totalCount = TotalCount,
                        pageSize = PageSize,
                        currentPage = CurrentPage,
                        totalPages = TotalPages,
                        previousPage,
                        nextPage
                    };

                    Response.Headers.Append("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    var abc = JsonConvert.SerializeObject(paginationMetadata);

                    return Ok(items);
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromQuery] EnquiryNModel pagingparametermodel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string str = "Select Id,Name,SellingPrice,Mrp,Description,TaxId1,TaxId2 from Products where Id=" + id + " ORDER BY Id DESC";
                using (var sda = new SqlDataAdapter(str, con))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dynamic tax1, tax2, taxper1, taxper2, taxtype1, taxtype2;

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

                            using (var sda1 = new SqlDataAdapter(str1, con))
                            {
                                DataSet ds1 = new DataSet();
                                sda.Fill(ds1);
                                taxper1 = Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                taxtype1 = Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                            }

                            using (var sda2 = new SqlDataAdapter(str2, con))
                            {
                                DataSet ds2 = new DataSet();
                                sda.Fill(ds2);
                                taxper2 = Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                taxtype2 = Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                            }
                        }
                        else { taxtype1 = ""; taxtype2 = ""; taxper1 = 0; taxper2 = 0; }

                        product.Add(new ProductModel
                        {
                            id = (int)dr["Id"],
                            Name = (String)dr["Name"],
                            Description = (String)dr["Description"],
                            SellingPrice = (double)dr["SellingPrice"],
                            Mrp = (double)dr["MRP"],
                            TaxId1 = tax1,
                            TaxPer1 = taxper1,
                            TaxType1 = taxtype1,
                            TaxId2 = tax2,
                            TaxPer2 = taxper2,
                            TaxType2 = taxtype2
                        });
                    }

                    int CurrentPage = pagingparametermodel.pageNumber;
                    int PageSize = pagingparametermodel.pageSize;
                    int TotalCount = count;
                    int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                    var items = product.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                    var previousPage = CurrentPage > 1 ? "Yes" : "No";
                    var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

                    var paginationMetadata = new
                    {
                        totalCount = TotalCount,
                        pageSize = PageSize,
                        currentPage = CurrentPage,
                        totalPages = TotalPages,
                        previousPage,
                        nextPage
                    };

                    Response.Headers.Append("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    var abc = JsonConvert.SerializeObject(paginationMetadata);

                    return Ok(items);
                }
            }
        }
    }
}