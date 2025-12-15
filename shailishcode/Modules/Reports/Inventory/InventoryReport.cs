using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Masters;
using AdvanceCRM.Products;
using AdvanceCRM.Purchase;
using AdvanceCRM.Sales;
using SAPbouiCOM;
using Serenity.Data;
using Serenity.Extensions.DependencyInjection;
using Serenity.Reporting;
using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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

        public List<BomProductsRow> BomProducts { get; set; }
        public List<SalesReturnProductsRow> SalesReturnProducts { get; set; }
        public List<ChallanProductsRow> ChallanProducts { get; set; }
        public List<OutwardProductsRow> OutwardProducts { get; }
        public List<InwardProductsRow> InwardProducts { get; }
        public List<StockTransferProductsRow> StockTransferFrom { get; set; }
        public List<StockTransferProductsRow> StockTransferTo { get; set; }
        public BranchRow Branch { get; set; }

        public List<BranchRow> AllBranch { get; set; }
        public ProductsDivisionRow Division { get; set; }
        public ProductsGroupRow Group { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public List<GrnTwoRow> Grn { get; set; }

        public List<GrnProductsTwoRow> GrnProducts { get; set; }
        public List<OutwardRow> Outward { get; set; }
        public List<InwardRow> Inward { get; set; }

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
            var gr = GrnTwoRow.Fields;
            var grp = GrnProductsTwoRow.Fields;
            var iws = InwardRow.Fields;
            var iwps = InwardProductsRow.Fields;
            var ows = OutwardRow.Fields;
            var owps = OutwardProductsRow.Fields;
            var bomr = BomRow.Fields;
            var bomp = BomProductsRow.Fields;

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

               GrnProducts = (connection.List<GrnProductsTwoRow>(grpo => grpo
                 .SelectTableFields()
                 .Select(grp.ProductsName)
                 .Select(grp.ReceivedQuantity)
                 .Where(grp.BranchId == Request.Branch.Value)
                 ));


                StockTransferFrom = connection.List<StockTransferProductsRow>(stpf => stpf
                 .SelectTableFields()
                 .Select(stp.ProductsName)
                 .Select(stp.Quantity)
                 .Select(stp.TransferPrice)
                 .Select(stp.StockTransferFromBranchId)
                .Where(stp.StockTransferFromBranchId == Request.Branch.Value)
                 );

                //Bom = connection.List<BomRow>

                BomProducts = connection.List<BomProductsRow>(qp => qp
                .SelectTableFields()
                .Select(bomp.ProductsName)
                .Select(bomp.Quantity)                                                                                                                                           
                .Select(bomp.Price)
                .Select(bomp.BomBranchId)
               //.Where(bomp.BomBranchId == Request.Branch.Value)

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

                GrnProducts = (connection.List<GrnProductsTwoRow>(grpo => grpo
                 .SelectTableFields()
                 .Select(grp.ProductsName)
                 .Select(grp.ReceivedQuantity)
                 ));

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

                GrnProducts = (connection.List<GrnProductsTwoRow>(grpo => grpo
                 .SelectTableFields()
                 .Select(grp.ProductsName)
                 .Select(grp.ReceivedQuantity)
                 ));

                BomProducts = connection.List<BomProductsRow>(q => q
                .SelectTableFields()
                 .Select(bomp.ProductsName)
                 .Select(bomp.Quantity)
                 .Select(bomp.Price)
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

                GrnProducts = (connection.List<GrnProductsTwoRow>(grpo => grpo
                 .SelectTableFields()
                 .Select(grp.ProductsName)
                 .Select(grp.ReceivedQuantity)
                 ));

                BomProducts = connection.List<BomProductsRow>(q => q
                .SelectTableFields()
                 .Select(bomp.ProductsName)
                 .Select(bomp.Quantity)
                 .Select(bomp.Price)
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
                BomProducts = connection.List<BomProductsRow>(q => q
                .SelectTableFields()
                 .Select(bomp.ProductsName)
                 .Select(bomp.Quantity)
                 .Select(bomp.Price)
                 );


                GrnProducts = (connection.List<GrnProductsTwoRow>(grpo => grpo
                 .SelectTableFields()
                 .Select(grp.ProductsName)
                 .Select(grp.ReceivedQuantity)
                 ));

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

                BomProducts = connection.List<BomProductsRow>(q => q
                .SelectTableFields()
                 .Select(bomp.ProductsName)
                 .Select(bomp.Quantity)
                 .Select(bomp.Price)
                 );
                GrnProducts = (connection.List<GrnProductsTwoRow>(grpo => grpo
                 .SelectTableFields()
                 .Select(grp.ProductsName)
                 .Select(grp.ReceivedQuantity)
                 ));

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

                GrnProducts = (connection.List<GrnProductsTwoRow>(grpo => grpo
                 .SelectTableFields()
                 .Select(grp.ProductsName)
                 .Select(grp.ReceivedQuantity)
                 ));
            }
            else if (Request.Type == Reports.StockReportType.GRNReport)
            {
                Grn = connection.List<GrnTwoRow>(grn => grn
                  .SelectTableFields()
                    .Select(gr.ContactsId)
                    .Select(gr.ContactsName)
                    .Select(gr.Po)
                    .Select(gr.PoDate)
                    .Select(gr.InvoiceNo)
                    .Select(gr.InvoiceDate)
                    .Select(gr.GrnDate)
                    .Select(gr.Status)

                  );

                GrnProducts = (connection.List<GrnProductsTwoRow>(grpo => grpo
                 .SelectTableFields()
                 .Select(grp.ProductsName)
                 .Select(grp.OrderQuantity)
                 .Select(grp.ReceivedQuantity)
                 .Select(grp.ExtraQuantity)
                 .Select(grp.RejectedQuantity)
                 ));
            }
            else if (Request.Type == Reports.StockReportType.ProductwiseGRNReport)
            {
                // Initialize collections to prevent null reference exceptions
                Products = new List<ProductsRow>();
                Grn = new List<GrnTwoRow>();
                GrnProducts = new List<GrnProductsTwoRow>();

                if (Request.Product.HasValue)
                {
                    // Get single product
                    var product = connection.TryFirst<ProductsRow>(q => q
                        .Select(ProductsRow.Fields.Name)
                        .Select(ProductsRow.Fields.Id)
                        .Where(ProductsRow.Fields.Id == Request.Product.Value)
                    );

                    if (product != null)
                        Products = new List<ProductsRow> { product };

                    // Base query for GRN records
                    var grnQuery = connection.List<GrnTwoRow>(q => q
                        .SelectTableFields()
                        .Select(GrnTwoRow.Fields.ContactsId)
                        .Select(GrnTwoRow.Fields.ContactsName)
                        .Select(GrnTwoRow.Fields.Po)
                        .Select(GrnTwoRow.Fields.PoDate)
                        .Select(GrnTwoRow.Fields.InvoiceNo)
                        .Select(GrnTwoRow.Fields.InvoiceDate)
                        .Select(GrnTwoRow.Fields.GrnDate)
                        .Select(GrnTwoRow.Fields.Status)
                    );

                    if (Request.Status.HasValue)
                        Grn = grnQuery.Where(g => g.Status.HasValue && (int)(object)g.Status.Value == Request.Status.Value).ToList();
                    else
                        Grn = grnQuery.ToList();

                    if (Request.PurchaseOrderNo.HasValue)
                        Grn = Grn.Where(g => g.Po == Request.PurchaseOrderNo.Value.ToString()).ToList();

                    var grnIds = Grn.Select(g => g.Id).ToList();

                    GrnProducts = connection.List<GrnProductsTwoRow>(q => q
                        .SelectTableFields()
                        .Select(GrnProductsTwoRow.Fields.ProductsName)
                        .Select(GrnProductsTwoRow.Fields.OrderQuantity)
                        .Select(GrnProductsTwoRow.Fields.ReceivedQuantity)
                        .Select(GrnProductsTwoRow.Fields.ExtraQuantity)
                        .Select(GrnProductsTwoRow.Fields.RejectedQuantity)
                        .Where(GrnProductsTwoRow.Fields.ProductsId == Request.Product.Value)
                        .Where(GrnProductsTwoRow.Fields.GrnId.In(grnIds))
                    );
                }
            }
            else if (Request.Type == Reports.StockReportType.OutwardReport)
            {
                Outward = connection.List<OutwardRow>(outward => outward
                  .SelectTableFields()
               .Select(ows.ContactsId)
               .Select(ows.ContactsName)
               .Select(ows.Products)
               .Select(ows.QuotationNo)
               .Select(ows.QuotationDate)
               .Select(ows.Date)
               .Select(ows.Status)
               );

                OutwardProducts = (connection.List<OutwardProductsRow>(OutwardProducts => OutwardProducts
                 .SelectTableFields()
                .Select(owps.ProductsName)
                .Select(owps.Quantity)

                ));
            }
            else if (Request.Type == Reports.StockReportType.ProductwiseOutwardReport)
            {
                // Initialize collections to prevent null reference exceptions
                Products = new List<ProductsRow>();
                Outward = new List<OutwardRow>();
                OutwardProducts = new List<OutwardProductsRow>();

                if (Request.Product.HasValue)
                {
                    // Get single product
                    var product = connection.TryFirst<ProductsRow>(q => q
                        .Select(ProductsRow.Fields.Name)
                        .Select(ProductsRow.Fields.Id)
                        .Where(ProductsRow.Fields.Id == Request.Product.Value)
                    );

                    if (product != null)
                        Products = new List<ProductsRow> { product };

                    // Base query for GRN records
                    var OutwardQuery = connection.List<OutwardRow>(q => q
                        .SelectTableFields()
               .Select(OutwardRow.Fields.ContactsId)
               .Select(OutwardRow.Fields.ContactsName)
               .Select(OutwardRow.Fields.Products)
               .Select(OutwardRow.Fields.QuotationNo)
               .Select(OutwardRow.Fields.QuotationDate)
               .Select(OutwardRow.Fields.Date)
               .Select(OutwardRow.Fields.Status)
               );
                    if (Request.Status.HasValue)
                        Outward = OutwardQuery.Where(g => g.Status.HasValue && (int)(object)g.Status.Value == Request.Status.Value).ToList();
                    else
                        Outward = OutwardQuery.ToList();

                    if (Request.Product.HasValue)
                        Outward = Outward.Where(g => g.ChallanNo == Request.Product.Value).ToList();

                    var outwardId = Outward.Select(g => g.Id).ToList();

                    OutwardProducts = connection.List<OutwardProductsRow>(q => q
                    .SelectTableFields()
                .Select(OutwardProductsRow.Fields.ProductsName)
                .Select(OutwardProductsRow.Fields.Quantity)
                .Where(OutwardProductsRow.Fields.ProductsId == Request.Product.Value)
                        .Where(OutwardProductsRow.Fields.OutwardId.In(outwardId))
                );                    
                }
            }

            else if (Request.Type == Reports.StockReportType.InwardReport)
            {
                Inward = connection.List<InwardRow>(inward => inward
                  .SelectTableFields()
               .Select(iws.ContactsId)
               .Select(iws.ContactsName)
               .Select(iws.Products)
               .Select(iws.QuotationNo)
               .Select(iws.QuotationDate)
               .Select(iws.Date)
               .Select(iws.Status)            
               
               );

                InwardProducts = (connection.List<InwardProductsRow>(inwarpProducts => inwarpProducts
                 .SelectTableFields()
                .Select(iwps.ProductsName)
                .Select(iwps.Quantity)
                ));
            }
            else if (Request.Type == Reports.StockReportType.ProductwiseInwardReport)
            {
                // Initialize collections to prevent null reference exceptions
                Products = new List<ProductsRow>();
                Inward = new List<InwardRow>();
                InwardProducts = new List<InwardProductsRow>();

                if (Request.Product.HasValue)
                {
                    // Get single product
                    var product = connection.TryFirst<ProductsRow>(q => q
                        .Select(ProductsRow.Fields.Name)
                        .Select(ProductsRow.Fields.Id)
                        .Where(ProductsRow.Fields.Id == Request.Product.Value)
                    );

                    if (product != null)
                        Products = new List<ProductsRow> { product };

                    // Base query for GRN records
                    var InwardQuery = connection.List<InwardRow>(q => q
                        .SelectTableFields()
               .Select(InwardRow.Fields.ContactsId)
               .Select(InwardRow.Fields.ContactsName)
               .Select(InwardRow.Fields.Products)
               .Select(InwardRow.Fields.QuotationNo)
               .Select(InwardRow.Fields.QuotationDate)
               .Select(InwardRow.Fields.Date)
               .Select(InwardRow.Fields.Status)
               );
                    if (Request.Status.HasValue)
                        Inward = InwardQuery.Where(g => g.Status.HasValue && (int)(object)g.Status.Value == Request.Status.Value).ToList();
                    else
                        Inward = InwardQuery.ToList();

                    if (Request.Product.HasValue)
                        Inward = Inward.Where(g => g.ChallanNo == Request.Product.Value).ToList();

                    var inwardId = Inward.Select(g => g.Id).ToList();

                    InwardProducts = connection.List<InwardProductsRow>(q => q
                    .SelectTableFields()
                .Select(InwardProductsRow.Fields.ProductsName)
                .Select(InwardProductsRow.Fields.Quantity)
                .Where(InwardProductsRow.Fields.InwardId == Request.Product.Value)
                .Where(InwardProductsRow.Fields.InwardId.In(inwardId))
                );
                }
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
                OutwardProducts = connection.List<OutwardProductsRow>(q => q
                .SelectTableFields()
                 .Select(owps.ProductsName)
                 .Select(owps.Quantity)
                 .Select(owps.Price)
                 );
                InwardProducts = connection.List<InwardProductsRow>(q => q
                .SelectTableFields()
                 .Select(iwps.ProductsName)
                 .Select(iwps.Quantity)
                 .Select(iwps.Price)
                 );
               BomProducts = connection.List<BomProductsRow>(q => q
                .SelectTableFields()
                 .Select(bomp.ProductsName)
                 .Select(bomp.Quantity)
                 .Select(bomp.Price)
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