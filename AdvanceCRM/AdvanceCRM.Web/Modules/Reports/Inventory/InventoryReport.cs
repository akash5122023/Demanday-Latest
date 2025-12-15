using _Ext;
using AdvanceCRM.Administration;

using AdvanceCRM.Masters;
using AdvanceCRM.Products;
using AdvanceCRM.Purchase;
using AdvanceCRM.Sales;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Reports
{
    [Report("Reports.InventoryReport")]
    [ReportDesign(MVC.Views.Reports.Inventory.InventoryReport)]
    public class InventoryReport : ListReportBase, IReport
    {
        public new StockReportRequest Request { get; set; }

        public InventoryReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public InventoryReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<ProductsRow>())
            {
                return new InventoryReportModel(connection, Request);
            }
        }
    }

    public class InventoryReportModel : ListReportModelBase
    {
        public new StockReportRequest Request { get; set; }
        public List<ProductsRow> Products { get; set; }

        public List<PurchaseProductsRow> PurchaseProducts { get; set; }
        public List<SalesProductsRow> SalesProducts { get; set; }
        public List<PurchaseReturnProductsRow> PurchaseReturnProducts { get; set; }
        public List<SalesReturnProductsRow> SalesReturnProducts { get; set; }
        public List<ChallanProductsRow> ChallanProducts { get; set; }
        public List<StockTransferProductsRow> StockTransferFrom { get; set; }
        public List<StockTransferProductsRow> StockTransferTo { get; set; }
        public BranchRow Branch { get; set; }

        public List<BranchRow> AllBranch { get; set; }
        public ProductsDivisionRow Division { get; set; }
        public ProductsGroupRow Group { get; set; }
        public CompanyDetailsRow Company { get; set; }

        public InventoryReportModel(IDbConnection connection, StockReportRequest request)
        {
            Request = request;
            var p = ProductsRow.Fields;
            var pp = PurchaseProductsRow.Fields;
            var sp = SalesProductsRow.Fields;
            var prp = PurchaseReturnProductsRow.Fields;
            var srp = SalesReturnProductsRow.Fields;
            var cp = ChallanProductsRow.Fields;
            var stp = StockTransferProductsRow.Fields;

            if (Request.Type == Reports.StockReportType.Branchwise)
            {
                Products = connection.List<ProductsRow>(pr => pr
                 .SelectTableFields()
                 .Select(p.Name)
                 .Where(p.RawMaterial == 0)
                 );

                PurchaseProducts = (connection.List<PurchaseProductsRow>(ppr => ppr
                 .SelectTableFields()
                 .Select(pp.ProductsName)
                 .Select(pp.Quantity)
                 .Select(pp.Price)
                 .Select(pp.PurchaseBranchId)
                .Where(pp.PurchaseBranchId == Request.Branch.Value)
                 ));

                SalesProducts = connection.List<SalesProductsRow>(spr => spr
                 .SelectTableFields()
                 .Select(sp.ProductsName)
                 .Select(sp.Quantity)
                 .Select(sp.Price)
                 .Select(sp.SalesBranchId)
                .Where(sp.SalesBranchId == Request.Branch.Value)
                 );

                PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(prpr => prpr
                 .SelectTableFields()
                 .Select(prp.ProductsName)
                 .Select(prp.Quantity)
                 .Select(prp.Price)
                 .Select(prp.PurchaseReturnBranchId)
                .Where(prp.PurchaseReturnBranchId == Request.Branch.Value)
                 );

                SalesReturnProducts = connection.List<SalesReturnProductsRow>(srpr => srpr
                 .SelectTableFields()
                 .Select(srp.ProductsName)
                 .Select(srp.Quantity)
                 .Select(srp.Price)
                 .Select(srp.SalesReturnBranchId)
                .Where(srp.SalesReturnBranchId == Request.Branch.Value)
                 );

                ChallanProducts = connection.List<ChallanProductsRow>(chr => chr
                 .SelectTableFields()
                 .Select(cp.ProductsName)
                 .Select(cp.Quantity)
                 .Select(cp.Price)
                 .Select(cp.ChallanBranchId)
                 .Where(cp.ChallanInvoiceMade != 1)
                .Where(cp.ChallanBranchId == Request.Branch.Value)
                 );

                StockTransferFrom = connection.List<StockTransferProductsRow>(stpf => stpf
                 .SelectTableFields()
                 .Select(stp.ProductsName)
                 .Select(stp.Quantity)
                 .Select(stp.TransferPrice)
                 .Select(stp.StockTransferFromBranchId)
                .Where(stp.StockTransferFromBranchId == Request.Branch.Value)
                 );

                StockTransferTo = connection.List<StockTransferProductsRow>(stpt => stpt
                 .SelectTableFields()
                 .Select(stp.ProductsName)
                 .Select(stp.Quantity)
                 .Select(stp.TransferPrice)
                 .Select(stp.StockTransferToBranchId)
                 .Where(stp.StockTransferToBranchId == Request.Branch.Value)
                 );
                var brn = BranchRow.Fields;
                Branch = connection.TryFirst<BranchRow>(q => q
                    .SelectTableFields()
                    .Select(brn.Branch)
                    .Where(brn.Id == request.Branch.Value)
                    );
            }
            else if (Request.Type == Reports.StockReportType.AllBranchwise)
            {
                Products = connection.List<ProductsRow>(pr => pr
                 .SelectTableFields()
                 .Select(p.Name)
                 .Where(p.RawMaterial == 0)
                 );

                PurchaseProducts = (connection.List<PurchaseProductsRow>(ppr => ppr
                 .SelectTableFields()
                 .Select(pp.ProductsName)
                 .Select(pp.Quantity)
                 .Select(pp.Price)
                 .Select(pp.PurchaseBranchId)
                // .Where(pp.PurchaseBranchId == Request.Branch.Value)
                 ));

                SalesProducts = connection.List<SalesProductsRow>(spr => spr
                 .SelectTableFields()
                 .Select(sp.ProductsName)
                 .Select(sp.Quantity)
                 .Select(sp.Price)
                 .Select(sp.SalesBranchId)
                // .Where(sp.SalesBranchId == Request.Branch.Value)
                 );

                PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(prpr => prpr
                 .SelectTableFields()
                 .Select(prp.ProductsName)
                 .Select(prp.Quantity)
                 .Select(prp.Price)
                 .Select(prp.PurchaseReturnBranchId)
                // .Where(prp.PurchaseReturnBranchId == Request.Branch.Value)
                 );

                SalesReturnProducts = connection.List<SalesReturnProductsRow>(srpr => srpr
                 .SelectTableFields()
                 .Select(srp.ProductsName)
                 .Select(srp.Quantity)
                 .Select(srp.Price)
                 .Select(srp.SalesReturnBranchId)
                // .Where(srp.SalesReturnBranchId == Request.Branch.Value)
                 );

                ChallanProducts = connection.List<ChallanProductsRow>(chr => chr
                 .SelectTableFields()
                 .Select(cp.ProductsName)
                 .Select(cp.Quantity)
                 .Select(cp.Price)
                 .Select(cp.ChallanBranchId)
                 .Where(cp.ChallanInvoiceMade != 1)
                // .Where(cp.ChallanBranchId == Request.Branch.Value)
                 );

                StockTransferFrom = connection.List<StockTransferProductsRow>(stpf => stpf
                 .SelectTableFields()
                 .Select(stp.ProductsName)
                 .Select(stp.Quantity)
                 .Select(stp.TransferPrice)
                 .Select(stp.StockTransferFromBranchId)
                // .Where(stp.StockTransferFromBranchId == Request.Branch.Value)
                 );

                StockTransferTo = connection.List<StockTransferProductsRow>(stpt => stpt
                 .SelectTableFields()
                 .Select(stp.ProductsName)
                 .Select(stp.Quantity)
                 .Select(stp.TransferPrice)
                 .Select(stp.StockTransferToBranchId)
                // .Where(stp.StockTransferToBranchId == Request.Branch.Value)
                 );

                var brn = BranchRow.Fields;
                AllBranch = connection.List<BranchRow>(q => q
                 .SelectTableFields()
                 .Select(brn.Branch)
                 //.Where(p.RawMaterial == 0)
                 );
            }
            else if (Request.Type == Reports.StockReportType.AllBranchDivisionWise)
            {
                Products = connection.List<ProductsRow>(pr => pr
                 .SelectTableFields()
                 .Select(p.Name)
                 .Where(p.RawMaterial == 0)
                 .Where(p.DivisionId == Request.Division.Value)
                 );

                PurchaseProducts = (connection.List<PurchaseProductsRow>(ppr => ppr
                 .SelectTableFields()
                 .Select(pp.ProductsName)
                 .Select(pp.Quantity)
                 .Select(pp.Price)
                 .Select(pp.PurchaseBranchId)
                // .Where(pp.PurchaseBranchId == Request.Branch.Value)
                 ));

                SalesProducts = connection.List<SalesProductsRow>(spr => spr
                 .SelectTableFields()
                 .Select(sp.ProductsName)
                 .Select(sp.Quantity)
                 .Select(sp.Price)
                 .Select(sp.SalesBranchId)
                // .Where(sp.SalesBranchId == Request.Branch.Value)
                 );

                PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(prpr => prpr
                 .SelectTableFields()
                 .Select(prp.ProductsName)
                 .Select(prp.Quantity)
                 .Select(prp.Price)
                 .Select(prp.PurchaseReturnBranchId)
                // .Where(prp.PurchaseReturnBranchId == Request.Branch.Value)
                 );

                SalesReturnProducts = connection.List<SalesReturnProductsRow>(srpr => srpr
                 .SelectTableFields()
                 .Select(srp.ProductsName)
                 .Select(srp.Quantity)
                 .Select(srp.Price)
                 .Select(srp.SalesReturnBranchId)
                // .Where(srp.SalesReturnBranchId == Request.Branch.Value)
                 );

                ChallanProducts = connection.List<ChallanProductsRow>(chr => chr
                 .SelectTableFields()
                 .Select(cp.ProductsName)
                 .Select(cp.Quantity)
                 .Select(cp.Price)
                 .Select(cp.ChallanBranchId)
                 .Where(cp.ChallanInvoiceMade != 1)
                // .Where(cp.ChallanBranchId == Request.Branch.Value)
                 );

                StockTransferFrom = connection.List<StockTransferProductsRow>(stpf => stpf
                 .SelectTableFields()
                 .Select(stp.ProductsName)
                 .Select(stp.Quantity)
                 .Select(stp.TransferPrice)
                 .Select(stp.StockTransferFromBranchId)
                // .Where(stp.StockTransferFromBranchId == Request.Branch.Value)
                 );

                StockTransferTo = connection.List<StockTransferProductsRow>(stpt => stpt
                 .SelectTableFields()
                 .Select(stp.ProductsName)
                 .Select(stp.Quantity)
                 .Select(stp.TransferPrice)
                 .Select(stp.StockTransferToBranchId)
                // .Where(stp.StockTransferToBranchId == Request.Branch.Value)
                 );

                var brn1 = ProductsDivisionRow.Fields;
                Division = connection.TryFirst<ProductsDivisionRow>(q => q
                    .SelectTableFields()
                    .Select(brn1.ProductsDivision)
                    .Where(brn1.Id == Request.Division.Value)
                    );

                var brn = BranchRow.Fields;
                AllBranch = connection.List<BranchRow>(q => q
                 .SelectTableFields()
                 .Select(brn.Branch)
                 //.Where(p.RawMaterial == 0)
                 );
            }
            else if (Request.Type == Reports.StockReportType.AllBranchProductWise)
            {
                Products = connection.List<ProductsRow>(pr => pr
                 .SelectTableFields()
                 .Select(p.Name)
                 .Where(p.RawMaterial == 0)
                 .Where(p.Id == Request.Product.Value)
                 );

                PurchaseProducts = (connection.List<PurchaseProductsRow>(ppr => ppr
                 .SelectTableFields()
                 .Select(pp.ProductsName)
                 .Select(pp.Quantity)
                 .Select(pp.Price)
                 .Select(pp.PurchaseBranchId)
                // .Where(pp.PurchaseBranchId == Request.Branch.Value)
                 ));

                SalesProducts = connection.List<SalesProductsRow>(spr => spr
                 .SelectTableFields()
                 .Select(sp.ProductsName)
                 .Select(sp.Quantity)
                 .Select(sp.Price)
                 .Select(sp.SalesBranchId)
                // .Where(sp.SalesBranchId == Request.Branch.Value)
                 );

                PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(prpr => prpr
                 .SelectTableFields()
                 .Select(prp.ProductsName)
                 .Select(prp.Quantity)
                 .Select(prp.Price)
                 .Select(prp.PurchaseReturnBranchId)
                // .Where(prp.PurchaseReturnBranchId == Request.Branch.Value)
                 );

                SalesReturnProducts = connection.List<SalesReturnProductsRow>(srpr => srpr
                 .SelectTableFields()
                 .Select(srp.ProductsName)
                 .Select(srp.Quantity)
                 .Select(srp.Price)
                 .Select(srp.SalesReturnBranchId)
                // .Where(srp.SalesReturnBranchId == Request.Branch.Value)
                 );

                ChallanProducts = connection.List<ChallanProductsRow>(chr => chr
                 .SelectTableFields()
                 .Select(cp.ProductsName)
                 .Select(cp.Quantity)
                 .Select(cp.Price)
                 .Select(cp.ChallanBranchId)
                 .Where(cp.ChallanInvoiceMade != 1)
                // .Where(cp.ChallanBranchId == Request.Branch.Value)
                 );

                StockTransferFrom = connection.List<StockTransferProductsRow>(stpf => stpf
                 .SelectTableFields()
                 .Select(stp.ProductsName)
                 .Select(stp.Quantity)
                 .Select(stp.TransferPrice)
                 .Select(stp.StockTransferFromBranchId)
                // .Where(stp.StockTransferFromBranchId == Request.Branch.Value)
                 );

                StockTransferTo = connection.List<StockTransferProductsRow>(stpt => stpt
                 .SelectTableFields()
                 .Select(stp.ProductsName)
                 .Select(stp.Quantity)
                 .Select(stp.TransferPrice)
                 .Select(stp.StockTransferToBranchId)
                // .Where(stp.StockTransferToBranchId == Request.Branch.Value)
                 );

                var brn = BranchRow.Fields;
                AllBranch = connection.List<BranchRow>(q => q
                 .SelectTableFields()
                 .Select(brn.Branch)
                 //.Where(p.RawMaterial == 0)
                 );
            }
            else if (Request.Type == Reports.StockReportType.Divisionwise)
            {
                Products = connection.List<ProductsRow>(pr => pr
                .SelectTableFields()
                .Select(p.Name)
                .Where(p.RawMaterial == 0)
                .Where(p.DivisionId == Request.Division.Value)
                );

                PurchaseProducts = (connection.List<PurchaseProductsRow>(ppr => ppr
                 .SelectTableFields()
                 .Select(pp.ProductsName)
                 .Select(pp.Quantity)
                 .Select(pp.Price)
                 ));

                SalesProducts = connection.List<SalesProductsRow>(spr => spr
                 .SelectTableFields()
                 .Select(sp.ProductsName)
                 .Select(sp.Quantity)
                 .Select(sp.Price)
                 );

                PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(prpr => prpr
                 .SelectTableFields()
                 .Select(prp.ProductsName)
                 .Select(prp.Quantity)
                 .Select(prp.Price)
                 );

                SalesReturnProducts = connection.List<SalesReturnProductsRow>(srpr => srpr
                 .SelectTableFields()
                 .Select(srp.ProductsName)
                 .Select(srp.Quantity)
                 .Select(srp.Price)
                 );

                ChallanProducts = connection.List<ChallanProductsRow>(chr => chr
                 .SelectTableFields()
                 .Select(cp.ProductsName)
                 .Select(cp.Quantity)
                 .Select(cp.Price)
                 .Where(cp.ChallanInvoiceMade != 1)
                 );

                var brn = ProductsDivisionRow.Fields;
                Division = connection.TryFirst<ProductsDivisionRow>(q => q
                    .SelectTableFields()
                    .Select(brn.ProductsDivision)
                    .Where(brn.Id == Request.Division.Value)
                    );
            }
            else if (Request.Type == Reports.StockReportType.Groupwise)
            {
                Products = connection.List<ProductsRow>(pr => pr
                 .SelectTableFields()
                 .Select(p.Name)
                 .Where(p.GroupId == Request.Group.Value)
                 );

                PurchaseProducts = (connection.List<PurchaseProductsRow>(ppr => ppr
                 .SelectTableFields()
                 .Select(pp.ProductsName)
                 .Select(pp.Quantity)
                 .Select(pp.Price)
                 ));

                SalesProducts = connection.List<SalesProductsRow>(spr => spr
                 .SelectTableFields()
                 .Select(sp.ProductsName)
                 .Select(sp.Quantity)
                 .Select(sp.Price)
                 );

                PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(prpr => prpr
                 .SelectTableFields()
                 .Select(prp.ProductsName)
                 .Select(prp.Quantity)
                 .Select(prp.Price)
                 );

                SalesReturnProducts = connection.List<SalesReturnProductsRow>(srpr => srpr
                 .SelectTableFields()
                 .Select(srp.ProductsName)
                 .Select(srp.Quantity)
                 .Select(srp.Price)
                 );

                ChallanProducts = connection.List<ChallanProductsRow>(chr => chr
                 .SelectTableFields()
                 .Select(cp.ProductsName)
                 .Select(cp.Quantity)
                 .Select(cp.Price)
                 .Where(cp.ChallanInvoiceMade != 1)
                 );

                var brn = ProductsGroupRow.Fields;
                Group = connection.TryFirst<ProductsGroupRow>(q => q
                    .SelectTableFields()
                    .Select(brn.ProductsGroup)
                    .Where(brn.Id == Request.Group.Value)
                    );
            }
            else if (Request.Type == Reports.StockReportType.Reorder)
            {
                Products = connection.List<ProductsRow>(q => q
                 .SelectTableFields()
                 .Select(p.Name)
                 .Select(p.OpeningStock)
                 .Select(p.MinimumStock)
                 .Select(p.MaximumStock)
                 .Where(p.RawMaterial == 0)
                 );

                PurchaseProducts = (connection.List<PurchaseProductsRow>(q => q
                 .SelectTableFields()
                 .Select(pp.ProductsName)
                 .Select(pp.Quantity)
                 .Select(pp.Price)
                 ));

                SalesProducts = connection.List<SalesProductsRow>(q => q
                 .SelectTableFields()
                 .Select(sp.ProductsName)
                 .Select(sp.Quantity)
                 .Select(sp.Price)
                 );

                PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(q => q
                 .SelectTableFields()
                 .Select(prp.ProductsName)
                 .Select(prp.Quantity)
                 .Select(prp.Price)
                 );

                SalesReturnProducts = connection.List<SalesReturnProductsRow>(q => q
                 .SelectTableFields()
                 .Select(srp.ProductsName)
                 .Select(srp.Quantity)
                 .Select(srp.Price)
                 );

                ChallanProducts = connection.List<ChallanProductsRow>(q => q
                 .SelectTableFields()
                 .Select(cp.ProductsName)
                 .Select(cp.Quantity)
                 .Select(cp.Price)
                 .Where(cp.ChallanInvoiceMade != 1)
                 );
            }
            else
            {
                Products = connection.List<ProductsRow>(q => q
                .SelectTableFields()
                .Select(p.Name)
                 .Select(p.OpeningStock)
                .Where(p.RawMaterial == 0)
                );

                PurchaseProducts = (connection.List<PurchaseProductsRow>(q => q
                 .SelectTableFields()
                 .Select(pp.ProductsName)
                 .Select(pp.Quantity)
                 .Select(pp.Price)
                 ));

                SalesProducts = connection.List<SalesProductsRow>(q => q
                 .SelectTableFields()
                 .Select(sp.ProductsName)
                 .Select(sp.Quantity)
                 .Select(sp.Price)
                 );

                PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(q => q
                 .SelectTableFields()
                 .Select(prp.ProductsName)
                 .Select(prp.Quantity)
                 .Select(prp.Price)
                 );

                SalesReturnProducts = connection.List<SalesReturnProductsRow>(q => q
                 .SelectTableFields()
                 .Select(srp.ProductsName)
                 .Select(srp.Quantity)
                 .Select(srp.Price)
                 );

                ChallanProducts = connection.List<ChallanProductsRow>(q => q
                 .SelectTableFields()
                 .Select(cp.ProductsName)
                 .Select(cp.Quantity)
                 .Select(cp.Price)
                 .Where(cp.ChallanInvoiceMade != 1)
                 );
            }


            var cmp = CompanyDetailsRow.Fields;
            Company = connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.Name)
                .Select(cmp.Slogan)
                .Select(cmp.Address)
                .Select(cmp.Phone)
                .Select(cmp.Logo)
                .Select(cmp.LogoHeight)
                .Select(cmp.LogoWidth)
                );
        }
    }
}