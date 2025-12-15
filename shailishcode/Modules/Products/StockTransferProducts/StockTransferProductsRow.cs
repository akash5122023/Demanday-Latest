
namespace AdvanceCRM.Products
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Products"), TableName("[dbo].[StockTransferProducts]")]
    [DisplayName("Stock Transfer Products"), InstanceName("Stock Transfer Products")]
    [ReadPermission("StockTransfer:Read")]
    [ModifyPermission("StockTransfer:Modify")]

    public sealed class StockTransferProductsRow : Row<StockTransferProductsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, SortOrder(1),IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Products"), NotNull, ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("ProductsName")]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        public Int32? ProductsId
        {
            get { return Fields.ProductsId[this]; }
            set { Fields.ProductsId[this] = value; }
        }

        [DisplayName("Quantity"), NotNull]
        public Double? Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("Transfer Price")]
        public Double? TransferPrice
        {
            get { return Fields.TransferPrice[this]; }
            set { Fields.TransferPrice[this] = value; }
        }

        [DisplayName("Tax Type1"), Size(100),NameProperty]
        public String TaxType1
        {
            get { return Fields.TaxType1[this]; }
            set { Fields.TaxType1[this] = value; }
        }

        [DisplayName("Percentage1"), DefaultValue("0")]
        public Double? Percentage1
        {
            get { return Fields.Percentage1[this]; }
            set { Fields.Percentage1[this] = value; }
        }

        [DisplayName("Tax Type2"), Size(100)]
        public String TaxType2
        {
            get { return Fields.TaxType2[this]; }
            set { Fields.TaxType2[this] = value; }
        }

        [DisplayName("Percentage2"), AlignRight, DefaultValue("0")]
        public Double? Percentage2
        {
            get { return Fields.Percentage2[this]; }
            set { Fields.Percentage2[this] = value; }
        }

        [DisplayName("Stock Transfer"), NotNull, ForeignKey("[dbo].[StockTransfer]", "Id"), LeftJoin("jStockTransfer"), TextualField("StockTransferAdditionalInfo")]
        [LookupEditor(typeof(StockTransferRow), InplaceAdd = true)]
        public Int32? StockTransferId
        {
            get { return Fields.StockTransferId[this]; }
            set { Fields.StockTransferId[this] = value; }
        }

        [DisplayName("Product"), Expression("jProducts.[Name]"), QuickSearch]
        public String ProductsName
        {
            get { return Fields.ProductsName[this]; }
            set { Fields.ProductsName[this] = value; }
        }

        [DisplayName("Products Code"), Expression("jProducts.[Code]")]
        public String ProductsCode
        {
            get { return Fields.ProductsCode[this]; }
            set { Fields.ProductsCode[this] = value; }
        }

        [DisplayName("Products Division Id"), Expression("jProducts.[DivisionId]")]
        public Int32? ProductsDivisionId
        {
            get { return Fields.ProductsDivisionId[this]; }
            set { Fields.ProductsDivisionId[this] = value; }
        }

        [DisplayName("Products Group Id"), Expression("jProducts.[GroupId]")]
        public Int32? ProductsGroupId
        {
            get { return Fields.ProductsGroupId[this]; }
            set { Fields.ProductsGroupId[this] = value; }
        }

        [DisplayName("Products Selling Price"), Expression("jProducts.[SellingPrice]")]
        public Double? ProductsSellingPrice
        {
            get { return Fields.ProductsSellingPrice[this]; }
            set { Fields.ProductsSellingPrice[this] = value; }
        }

        [DisplayName("Products MRP"), Expression("jProducts.[MRP]")]
        public Double? ProductsMrp
        {
            get { return Fields.ProductsMrp[this]; }
            set { Fields.ProductsMrp[this] = value; }
        }

        [DisplayName("Products Description"), Expression("jProducts.[Description]")]
        public String ProductsDescription
        {
            get { return Fields.ProductsDescription[this]; }
            set { Fields.ProductsDescription[this] = value; }
        }

        [DisplayName("Products Tax Id1"), Expression("jProducts.[TaxId1]")]
        public Int32? ProductsTaxId1
        {
            get { return Fields.ProductsTaxId1[this]; }
            set { Fields.ProductsTaxId1[this] = value; }
        }

        [DisplayName("Products Tax Id2"), Expression("jProducts.[TaxId2]")]
        public Int32? ProductsTaxId2
        {
            get { return Fields.ProductsTaxId2[this]; }
            set { Fields.ProductsTaxId2[this] = value; }
        }

        [DisplayName("Products Image"), Expression("jProducts.[Image]")]
        public String ProductsImage
        {
            get { return Fields.ProductsImage[this]; }
            set { Fields.ProductsImage[this] = value; }
        }

        [DisplayName("Products Tech Specs"), Expression("jProducts.[TechSpecs]")]
        public String ProductsTechSpecs
        {
            get { return Fields.ProductsTechSpecs[this]; }
            set { Fields.ProductsTechSpecs[this] = value; }
        }

        [DisplayName("Products Hsn"), Expression("jProducts.[HSN]")]
        public String ProductsHsn
        {
            get { return Fields.ProductsHsn[this]; }
            set { Fields.ProductsHsn[this] = value; }
        }

        [DisplayName("Products Channel Customer Price"), Expression("jProducts.[ChannelCustomerPrice]")]
        public Double? ProductsChannelCustomerPrice
        {
            get { return Fields.ProductsChannelCustomerPrice[this]; }
            set { Fields.ProductsChannelCustomerPrice[this] = value; }
        }

        [DisplayName("Products Reseller Price"), Expression("jProducts.[ResellerPrice]")]
        public Double? ProductsResellerPrice
        {
            get { return Fields.ProductsResellerPrice[this]; }
            set { Fields.ProductsResellerPrice[this] = value; }
        }

        [DisplayName("Products Wholesaler Price"), Expression("jProducts.[WholesalerPrice]")]
        public Double? ProductsWholesalerPrice
        {
            get { return Fields.ProductsWholesalerPrice[this]; }
            set { Fields.ProductsWholesalerPrice[this] = value; }
        }

        [DisplayName("Products Dealer Price"), Expression("jProducts.[DealerPrice]")]
        public Double? ProductsDealerPrice
        {
            get { return Fields.ProductsDealerPrice[this]; }
            set { Fields.ProductsDealerPrice[this] = value; }
        }

        [DisplayName("Products Distributor Price"), Expression("jProducts.[DistributorPrice]")]
        public Double? ProductsDistributorPrice
        {
            get { return Fields.ProductsDistributorPrice[this]; }
            set { Fields.ProductsDistributorPrice[this] = value; }
        }

        [DisplayName("Products Stockiest Price"), Expression("jProducts.[StockiestPrice]")]
        public Double? ProductsStockiestPrice
        {
            get { return Fields.ProductsStockiestPrice[this]; }
            set { Fields.ProductsStockiestPrice[this] = value; }
        }

        [DisplayName("Products National Distributor Price"), Expression("jProducts.[NationalDistributorPrice]")]
        public Double? ProductsNationalDistributorPrice
        {
            get { return Fields.ProductsNationalDistributorPrice[this]; }
            set { Fields.ProductsNationalDistributorPrice[this] = value; }
        }

        [DisplayName("Products Minimum Stock"), Expression("jProducts.[MinimumStock]")]
        public Double? ProductsMinimumStock
        {
            get { return Fields.ProductsMinimumStock[this]; }
            set { Fields.ProductsMinimumStock[this] = value; }
        }

        [DisplayName("Products Maximum Stock"), Expression("jProducts.[MaximumStock]")]
        public Double? ProductsMaximumStock
        {
            get { return Fields.ProductsMaximumStock[this]; }
            set { Fields.ProductsMaximumStock[this] = value; }
        }

        [DisplayName("Products Raw Material"), Expression("jProducts.[RawMaterial]")]
        public Boolean? ProductsRawMaterial
        {
            get { return Fields.ProductsRawMaterial[this]; }
            set { Fields.ProductsRawMaterial[this] = value; }
        }

        [DisplayName("Products Purchase Price"), Expression("jProducts.[PurchasePrice]")]
        public Double? ProductsPurchasePrice
        {
            get { return Fields.ProductsPurchasePrice[this]; }
            set { Fields.ProductsPurchasePrice[this] = value; }
        }

        [DisplayName("Products Opening Stock"), Expression("jProducts.[OpeningStock]")]
        public Double? ProductsOpeningStock
        {
            get { return Fields.ProductsOpeningStock[this]; }
            set { Fields.ProductsOpeningStock[this] = value; }
        }

        [DisplayName("Products Unit Id"), Expression("jProducts.[UnitId]")]
        public Int32? ProductsUnitId
        {
            get { return Fields.ProductsUnitId[this]; }
            set { Fields.ProductsUnitId[this] = value; }
        }

        [DisplayName("Stock Transfer Date"), Expression("jStockTransfer.[Date]")]
        public DateTime? StockTransferDate
        {
            get { return Fields.StockTransferDate[this]; }
            set { Fields.StockTransferDate[this] = value; }
        }

        [DisplayName("Stock Transfer From Branch Id"), Expression("jStockTransfer.[FromBranchId]")]
        public Int32? StockTransferFromBranchId
        {
            get { return Fields.StockTransferFromBranchId[this]; }
            set { Fields.StockTransferFromBranchId[this] = value; }
        }

        [DisplayName("Stock Transfer To Branch Id"), Expression("jStockTransfer.[ToBranchId]")]
        public Int32? StockTransferToBranchId
        {
            get { return Fields.StockTransferToBranchId[this]; }
            set { Fields.StockTransferToBranchId[this] = value; }
        }

        [DisplayName("Stock Transfer Total Qty"), Expression("jStockTransfer.[TotalQty]")]
        public Double? StockTransferTotalQty
        {
            get { return Fields.StockTransferTotalQty[this]; }
            set { Fields.StockTransferTotalQty[this] = value; }
        }

        [DisplayName("Stock Transfer Amount"), Expression("jStockTransfer.[Amount]")]
        public Double? StockTransferAmount
        {
            get { return Fields.StockTransferAmount[this]; }
            set { Fields.StockTransferAmount[this] = value; }
        }

        [DisplayName("Stock Transfer Additional Info"), Expression("jStockTransfer.[AdditionalInfo]")]
        public String StockTransferAdditionalInfo
        {
            get { return Fields.StockTransferAdditionalInfo[this]; }
            set { Fields.StockTransferAdditionalInfo[this] = value; }
        }

        [DisplayName("Stock Transfer Representative Id"), Expression("jStockTransfer.[RepresentativeId]")]
        public Int32? StockTransferRepresentativeId
        {
            get { return Fields.StockTransferRepresentativeId[this]; }
            set { Fields.StockTransferRepresentativeId[this] = value; }
        }
        
        [DisplayName("TAX1 Amount"), Expression("((t0.[TransferPrice])* (t0.[Percentage1] / 100)) * (t0.[Quantity])"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? Tax1Amount
        {
            get { return Fields.Tax1Amount[this]; }
            set { Fields.Tax1Amount[this] = value; }
        }


        [DisplayName("TAX2 Amount"), Expression("((t0.[TransferPrice])* (t0.[Percentage2] / 100)) * (t0.[Quantity])"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? Tax2Amount
        {
            get { return Fields.Tax2Amount[this]; }
            set { Fields.Tax2Amount[this] = value; }
        }


        [DisplayName("Line Total"), Expression("(t0.[TransferPrice] * (t0.[Quantity]))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? LineTotal
        {
            get { return Fields.LineTotal[this]; }
            set { Fields.LineTotal[this] = value; }
        }

        [DisplayName("Grand Total"), Expression("((t0.[TransferPrice] * t0.[Quantity]) + ((t0.[TransferPrice] * t0.[Quantity]) * (t0.[Percentage1] / 100)) + ((t0.[TransferPrice] * t0.[Quantity]) * (t0.[Percentage2] / 100)))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? GrandTotal
        {
            get { return Fields.GrandTotal[this]; }
            set { Fields.GrandTotal[this] = value; }
        }

       

        public StockTransferProductsRow()
            : base(Fields)
        {
        }
        

        public StockTransferProductsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProductsId;
            public DoubleField Quantity;
            public DoubleField TransferPrice;
            public StringField TaxType1;
            public DoubleField Percentage1;
            public StringField TaxType2;
            public DoubleField Percentage2;
            public Int32Field StockTransferId;

            public StringField ProductsName;
            public StringField ProductsCode;
            public Int32Field ProductsDivisionId;
            public Int32Field ProductsGroupId;
            public DoubleField ProductsSellingPrice;
            public DoubleField ProductsMrp;
            public StringField ProductsDescription;
            public Int32Field ProductsTaxId1;
            public Int32Field ProductsTaxId2;
            public StringField ProductsImage;
            public StringField ProductsTechSpecs;
            public StringField ProductsHsn;
            public DoubleField ProductsChannelCustomerPrice;
            public DoubleField ProductsResellerPrice;
            public DoubleField ProductsWholesalerPrice;
            public DoubleField ProductsDealerPrice;
            public DoubleField ProductsDistributorPrice;
            public DoubleField ProductsStockiestPrice;
            public DoubleField ProductsNationalDistributorPrice;
            public DoubleField ProductsMinimumStock;
            public DoubleField ProductsMaximumStock;
            public BooleanField ProductsRawMaterial;
            public DoubleField ProductsPurchasePrice;
            public DoubleField ProductsOpeningStock;
            public Int32Field ProductsUnitId;

            public DateTimeField StockTransferDate;
            public Int32Field StockTransferFromBranchId;
            public Int32Field StockTransferToBranchId;
            public DoubleField StockTransferTotalQty;
            public DoubleField StockTransferAmount;
            public StringField StockTransferAdditionalInfo;
            public Int32Field StockTransferRepresentativeId;
            
            public DecimalField Tax1Amount;
        public DecimalField Tax2Amount;
        public DecimalField LineTotal;
        public DecimalField GrandTotal;
        }
    }
}
