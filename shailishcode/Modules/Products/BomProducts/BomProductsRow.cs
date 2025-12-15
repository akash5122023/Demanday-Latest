using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Products
{
    [ConnectionKey("Default"), Module("Products"), TableName("[dbo].[BomProducts]")]
    [DisplayName("Bom Products"), InstanceName("Bom Products")]
    [ReadPermission("Bom:Read")]
    [ModifyPermission("Bom:Read")]
    public sealed class BomProductsRow : Row<BomProductsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        //[DisplayName("Products Id"), NotNull]
        [DisplayName("Products"), NotNull, ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("ProductsName")]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        public Int32? ProductsId
        {
            get => fields.ProductsId[this];
            set => fields.ProductsId[this] = value;
        }

        [DisplayName("Quantity"), NotNull]
        public Double? Quantity
        {
            get => fields.Quantity[this];
            set => fields.Quantity[this] = value;
        }

        [DisplayName("Mrp"), Column("MRP")]
        public Double? Mrp
        {
            get => fields.Mrp[this];
            set => fields.Mrp[this] = value;
        }

        [DisplayName("Selling Price")]
        public Double? SellingPrice
        {
            get => fields.SellingPrice[this];
            set => fields.SellingPrice[this] = value;
        }

        [DisplayName("Price")]
        public Double? Price
        {
            get => fields.Price[this];
            set => fields.Price[this] = value;
        }

        [DisplayName("Discount")]
        public Double? Discount
        {
            get => fields.Discount[this];
            set => fields.Discount[this] = value;
        }

        [DisplayName("Tax Type1"), Size(100), QuickSearch, NameProperty]
        public String TaxType1
        {
            get => fields.TaxType1[this];
            set => fields.TaxType1[this] = value;
        }

        [DisplayName("Percentage1")]
        public Double? Percentage1
        {
            get => fields.Percentage1[this];
            set => fields.Percentage1[this] = value;
        }

        [DisplayName("Tax Type2"), Size(100)]
        public String TaxType2
        {
            get => fields.TaxType2[this];
            set => fields.TaxType2[this] = value;
        }

        [DisplayName("Percentage2")]
        public Double? Percentage2
        {
            get => fields.Percentage2[this];
            set => fields.Percentage2[this] = value;
        }

        [DisplayName("Warranty Start")]
        public DateTime? WarrantyStart
        {
            get => fields.WarrantyStart[this];
            set => fields.WarrantyStart[this] = value;
        }

        [DisplayName("Warranty End")]
        public DateTime? WarrantyEnd
        {
            get => fields.WarrantyEnd[this];
            set => fields.WarrantyEnd[this] = value;
        }

        [DisplayName("Bom"), NotNull, ForeignKey("[dbo].[Bom]", "Id"), LeftJoin("jBom"), TextualField("BomAdditionalInfo")]
        public Int32? BomId
        {
            get => fields.BomId[this];
            set => fields.BomId[this] = value;
        }

        [DisplayName("Discount Amount")]
        public Double? DiscountAmount
        {
            get => fields.DiscountAmount[this];
            set => fields.DiscountAmount[this] = value;
        }

        [DisplayName("Description"), Size(2000), QuickSearch, TextAreaEditor(Rows = 4)]
        public String Description
        {
            get => fields.Description[this];
            set => fields.Description[this] = value;
        }

        [DisplayName("Unit"), Size(128)]
        public String Unit
        {
            get => fields.Unit[this];
            set => fields.Unit[this] = value;
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
        [ForeignKey("ProductsUnit", "Id"), LeftJoin("jProductsUnit")] //added
        public Int32? ProductsUnitId
        {
            get { return Fields.ProductsUnitId[this]; }
            set { Fields.ProductsUnitId[this] = value; }
        }



        [DisplayName("Bom Contacts Id"), Expression("jBom.[ContactsId]")]
        public Int32? BomContactsId
        {
            get => fields.BomContactsId[this];
            set => fields.BomContactsId[this] = value;
        }

        [DisplayName("Bom Date"), Expression("jBom.[Date]")]
        public DateTime? BomDate
        {
            get => fields.BomDate[this];
            set => fields.BomDate[this] = value;
        }

        [DisplayName("Bom Status"), Expression("jBom.[Status]")]
        public Int32? BomStatus
        {
            get => fields.BomStatus[this];
            set => fields.BomStatus[this] = value;
        }

        [DisplayName("Bom Type"), Expression("jBom.[Type]")]
        public Int32? BomType
        {
            get => fields.BomType[this];
            set => fields.BomType[this] = value;
        }

        [DisplayName("Bom Additional Info"), Expression("jBom.[AdditionalInfo]")]
        public String BomAdditionalInfo
        {
            get => fields.BomAdditionalInfo[this];
            set => fields.BomAdditionalInfo[this] = value;
        }

        [DisplayName("Branch"), Expression("jBom.[BranchId]")]
        [LookupEditor("Administration.BranchLookup")]

        public Int32? BomBranchId
        {
            get => fields.BomBranchId[this];
            set => fields.BomBranchId[this] = value;
        }

        [DisplayName("Bom Owner Id"), Expression("jBom.[OwnerId]")]
        public Int32? BomOwnerId
        {
            get => fields.BomOwnerId[this];
            set => fields.BomOwnerId[this] = value;
        }

        [DisplayName("Bom Assigned Id"), Expression("jBom.[AssignedId]")]
        public Int32? BomAssignedId
        {
            get => fields.BomAssignedId[this];
            set => fields.BomAssignedId[this] = value;
        }

        [DisplayName("Bom Other Address"), Expression("jBom.[OtherAddress]")]
        public Boolean? BomOtherAddress
        {
            get => fields.BomOtherAddress[this];
            set => fields.BomOtherAddress[this] = value;
        }

        [DisplayName("Bom Shipping Address"), Expression("jBom.[ShippingAddress]")]
        public String BomShippingAddress
        {
            get => fields.BomShippingAddress[this];
            set => fields.BomShippingAddress[this] = value;
        }

        [DisplayName("Bom Packaging Charges"), Expression("jBom.[PackagingCharges]")]
        public Double? BomPackagingCharges
        {
            get => fields.BomPackagingCharges[this];
            set => fields.BomPackagingCharges[this] = value;
        }

        [DisplayName("Bom Freight Charges"), Expression("jBom.[FreightCharges]")]
        public Double? BomFreightCharges
        {
            get => fields.BomFreightCharges[this];
            set => fields.BomFreightCharges[this] = value;
        }

        [DisplayName("Bom Advacne"), Expression("jBom.[Advacne]")]
        public Double? BomAdvacne
        {
            get => fields.BomAdvacne[this];
            set => fields.BomAdvacne[this] = value;
        }

        [DisplayName("Bom Due Date"), Expression("jBom.[DueDate]")]
        public DateTime? BomDueDate
        {
            get => fields.BomDueDate[this];
            set => fields.BomDueDate[this] = value;
        }

        [DisplayName("Bom Dispatch Details"), Expression("jBom.[DispatchDetails]")]
        public String BomDispatchDetails
        {
            get => fields.BomDispatchDetails[this];
            set => fields.BomDispatchDetails[this] = value;
        }

        [DisplayName("Bom Roundup"), Expression("jBom.[Roundup]")]
        public Double? BomRoundup
        {
            get => fields.BomRoundup[this];
            set => fields.BomRoundup[this] = value;
        }

        [DisplayName("Bom Subject"), Expression("jBom.[Subject]")]
        public String BomSubject
        {
            get => fields.BomSubject[this];
            set => fields.BomSubject[this] = value;
        }

        [DisplayName("Bom Reference"), Expression("jBom.[Reference]")]
        public String BomReference
        {
            get => fields.BomReference[this];
            set => fields.BomReference[this] = value;
        }

        [DisplayName("Bom Contact Person Id"), Expression("jBom.[ContactPersonId]")]
        public Int32? BomContactPersonId
        {
            get => fields.BomContactPersonId[this];
            set => fields.BomContactPersonId[this] = value;
        }

        [DisplayName("Bom Lines"), Expression("jBom.[Lines]")]
        public Int32? BomLines
        {
            get => fields.BomLines[this];
            set => fields.BomLines[this] = value;
        }

        [DisplayName("Bom Quotation No"), Expression("jBom.[QuotationNo]")]
        public Int32? BomQuotationNo
        {
            get => fields.BomQuotationNo[this];
            set => fields.BomQuotationNo[this] = value;
        }

        [DisplayName("Bom Quotation Date"), Expression("jBom.[QuotationDate]")]
        public DateTime? BomQuotationDate
        {
            get => fields.BomQuotationDate[this];
            set => fields.BomQuotationDate[this] = value;
        }

        [DisplayName("Bom Conversion"), Expression("jBom.[Conversion]")]
        public Double? BomConversion
        {
            get => fields.BomConversion[this];
            set => fields.BomConversion[this] = value;
        }

        [DisplayName("Bom Purchase Order No"), Expression("jBom.[PurchaseOrderNo]")]
        public String BomPurchaseOrderNo
        {
            get => fields.BomPurchaseOrderNo[this];
            set => fields.BomPurchaseOrderNo[this] = value;
        }

        [DisplayName("Bom Item Name"), Expression("jBom.[ItemName]")]
        public String BomItemName
        {
            get => fields.BomItemName[this];
            set => fields.BomItemName[this] = value;
        }

        [DisplayName("Bom Operation Cost"), Expression("jBom.[OperationCost]")]
        public Int32? BomOperationCost
        {
            get => fields.BomOperationCost[this];
            set => fields.BomOperationCost[this] = value;
        }

        [DisplayName("Bom Raw Material Cost"), Expression("jBom.[RawMaterialCost]")]
        public Int32? BomRawMaterialCost
        {
            get => fields.BomRawMaterialCost[this];
            set => fields.BomRawMaterialCost[this] = value;
        }

        [DisplayName("Bom Scrap Material Cost"), Expression("jBom.[ScrapMaterialCost]")]
        public Int32? BomScrapMaterialCost
        {
            get => fields.BomScrapMaterialCost[this];
            set => fields.BomScrapMaterialCost[this] = value;
        }

        [DisplayName("Bom Total Material Cost"), Expression("jBom.[TotalMaterialCost]")]
        public Int32? BomTotalMaterialCost
        {
            get => fields.BomTotalMaterialCost[this];
            set => fields.BomTotalMaterialCost[this] = value;
        }

        [DisplayName("Bom Operation Name"), Expression("jBom.[OperationName]")]
        public String BomOperationName
        {
            get => fields.BomOperationName[this];
            set => fields.BomOperationName[this] = value;
        }

        [DisplayName("Bom Work Station Name"), Expression("jBom.[WorkStationName]")]
        public String BomWorkStationName
        {
            get => fields.BomWorkStationName[this];
            set => fields.BomWorkStationName[this] = value;
        }

        [DisplayName("Bom Operatinng Time"), Expression("jBom.[OperatinngTime]")]
        public String BomOperatinngTime
        {
            get => fields.BomOperatinngTime[this];
            set => fields.BomOperatinngTime[this] = value;
        }

        [DisplayName("Bom Operating Cost"), Expression("jBom.[OperatingCost]")]
        public Int32? BomOperatingCost
        {
            get => fields.BomOperatingCost[this];
            set => fields.BomOperatingCost[this] = value;
        }

        [DisplayName("Bom Process Loss"), Expression("jBom.[ProcessLoss]")]
        public Int32? BomProcessLoss
        {
            get => fields.BomProcessLoss[this];
            set => fields.BomProcessLoss[this] = value;
        }

        [DisplayName("Bom Process Loss Qty"), Expression("jBom.[ProcessLossQty]")]
        public Int32? BomProcessLossQty
        {
            get => fields.BomProcessLossQty[this];
            set => fields.BomProcessLossQty[this] = value;
        }

        [DisplayName("Bom Attachments"), Expression("jBom.[Attachments]")]
        public String BomAttachments
        {
            get => fields.BomAttachments[this];
            set => fields.BomAttachments[this] = value;
        }

        [DisplayName("Bom Company Id"), Expression("jBom.[CompanyId]")]
        public Int32? BomCompanyId
        {
            get => fields.BomCompanyId[this];
            set => fields.BomCompanyId[this] = value;
        }

        [DisplayName("Bom Taxable"), Expression("jBom.[Taxable]")]
        public Int32? BomTaxable
        {
            get => fields.BomTaxable[this];
            set => fields.BomTaxable[this] = value;
        }

        [DisplayName("Bom Quantity"), Expression("jBom.[Quantity]")]
        public Double? BomQuantity
        {
            get => fields.BomQuantity[this];
            set => fields.BomQuantity[this] = value;
        }

        [DisplayName("Bom Mrp"), Expression("jBom.[MRP]")]
        public Double? BomMrp
        {
            get => fields.BomMrp[this];
            set => fields.BomMrp[this] = value;
        }

        [DisplayName("Bom Selling Price"), Expression("jBom.[SellingPrice]")]
        public Double? BomSellingPrice
        {
            get => fields.BomSellingPrice[this];
            set => fields.BomSellingPrice[this] = value;
        }

        [DisplayName("Bom Price"), Expression("jBom.[Price]")]
        public Double? BomPrice
        {
            get => fields.BomPrice[this];
            set => fields.BomPrice[this] = value;
        }

        [DisplayName("Bom Discount"), Expression("jBom.[Discount]")]
        public Double? BomDiscount
        {
            get => fields.BomDiscount[this];
            set => fields.BomDiscount[this] = value;
        }

        [DisplayName("Bom Tax Type1"), Expression("jBom.[TaxType1]")]
        public String BomTaxType1
        {
            get => fields.BomTaxType1[this];
            set => fields.BomTaxType1[this] = value;
        }

        [DisplayName("Bom Percentage1"), Expression("jBom.[Percentage1]")]
        public Double? BomPercentage1
        {
            get => fields.BomPercentage1[this];
            set => fields.BomPercentage1[this] = value;
        }

        [DisplayName("Bom Tax Type2"), Expression("jBom.[TaxType2]")]
        public String BomTaxType2
        {
            get => fields.BomTaxType2[this];
            set => fields.BomTaxType2[this] = value;
        }

        [DisplayName("Bom Percentage2"), Expression("jBom.[Percentage2]")]
        public Double? BomPercentage2
        {
            get => fields.BomPercentage2[this];
            set => fields.BomPercentage2[this] = value;
        }

        [DisplayName("Bom Warranty Start"), Expression("jBom.[WarrantyStart]")]
        public DateTime? BomWarrantyStart
        {
            get => fields.BomWarrantyStart[this];
            set => fields.BomWarrantyStart[this] = value;
        }

        [DisplayName("Bom Warranty End"), Expression("jBom.[WarrantyEnd]")]
        public DateTime? BomWarrantyEnd
        {
            get => fields.BomWarrantyEnd[this];
            set => fields.BomWarrantyEnd[this] = value;
        }

        [DisplayName("Bom Discount Amount"), Expression("jBom.[DiscountAmount]")]
        public Double? BomDiscountAmount
        {
            get => fields.BomDiscountAmount[this];
            set => fields.BomDiscountAmount[this] = value;
        }

        [DisplayName("Bom Description"), Expression("jBom.[Description]")]
        public String BomDescription
        {
            get => fields.BomDescription[this];
            set => fields.BomDescription[this] = value;
        }

        [DisplayName("Bom Unit"), Expression("jBom.[Unit]")]
        public String BomUnit
        {
            get => fields.BomUnit[this];
            set => fields.BomUnit[this] = value;
        }

        [DisplayName("Bom Image"), Expression("jBom.[Image]")]
        public String BomImage
        {
            get => fields.BomImage[this];
            set => fields.BomImage[this] = value;
        }

        [DisplayName("Bom Tech Specs"), Expression("jBom.[TechSpecs]")]
        public String BomTechSpecs
        {
            get => fields.BomTechSpecs[this];
            set => fields.BomTechSpecs[this] = value;
        }

        [DisplayName("Bom Hsn"), Expression("jBom.[HSN]")]
        public String BomHsn
        {
            get => fields.BomHsn[this];
            set => fields.BomHsn[this] = value;
        }

        [DisplayName("Bom Code"), Expression("jBom.[Code]")]
        public String BomCode
        {
            get => fields.BomCode[this];
            set => fields.BomCode[this] = value;
        }

        [DisplayName("Bom Products Id"), Expression("jBom.[ProductsId]")]
        public Int32? BomProductsId
        {
            get => fields.BomProductsId[this];
            set => fields.BomProductsId[this] = value;
        }

        [DisplayName("Code"), Size(100), NotMapped]
        public String Code { get { return Fields.Code[this]; } set { Fields.Code[this] = value; } }

        [DisplayName("TAX Inclusive"), NotMapped]
        [BooleanSwitchEditor]
        public Boolean? Inclusive { get { return Fields.Inclusive[this]; } set { Fields.Inclusive[this] = value; } }

        [DisplayName("Discounted Price"), Expression("(t0.[Price] - (t0.[DiscountAmount] / t0.[Quantity]) - (t0.[Price]* t0.[Discount] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? DiscPrice
        {
            get { return Fields.DiscPrice[this]; }
            set { Fields.DiscPrice[this] = value; }
        }

        [DisplayName("TAX1 Amount"), Expression("(((t0.[Price] * t0.[Quantity]) - (t0.[DiscountAmount] + (t0.[Price] * t0.[Quantity] * (t0.[Discount] / 100)))) * (t0.[Percentage1] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? Tax1Amount
        {
            get { return Fields.Tax1Amount[this]; }
            set { Fields.Tax1Amount[this] = value; }
        }

        [DisplayName("TAX2 Amount"), Expression("(((t0.[Price] * t0.[Quantity]) - (t0.[DiscountAmount] + (t0.[Price] * t0.[Quantity] * (t0.[Discount] / 100)))) * (t0.[Percentage2] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? Tax2Amount
        {
            get { return Fields.Tax2Amount[this]; }
            set { Fields.Tax2Amount[this] = value; }
        }

        [DisplayName("Line Total"), Expression("(t0.[Price] * (t0.[Quantity]) - t0.[DiscountAmount] - ((t0.[Price] * (t0.[Quantity])) * t0.[Discount] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? LineTotal
        {
            get { return Fields.LineTotal[this]; }
            set { Fields.LineTotal[this] = value; }
        }

        [DisplayName("Grand Total"), Expression("(((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) + (IIF((SELECT Taxable FROM Bom WHERE Id=t0.[BomId]) = 0, 0,  (((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) * (t0.[Percentage1] / 100)) + (((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) * (t0.[Percentage2] / 100)))))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? GrandTotal
        {
            get { return Fields.GrandTotal[this]; }
            set { Fields.GrandTotal[this] = value; }
        }

        public BomProductsRow()
            : base()
        {
        }

        public BomProductsRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProductsId;
            public DoubleField Quantity;
            public DoubleField Mrp;
            public DoubleField SellingPrice;
            public DoubleField Price;
            public DoubleField Discount;
            public StringField TaxType1;
            public DoubleField Percentage1;
            public StringField TaxType2;
            public DoubleField Percentage2;
            public DateTimeField WarrantyStart;
            public DateTimeField WarrantyEnd;
            public Int32Field BomId;
            public DoubleField DiscountAmount;
            public StringField Description;
            public StringField Unit;


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



            public Int32Field BomContactsId;
            public DateTimeField BomDate;
            public Int32Field BomStatus;
            public Int32Field BomType;
            public StringField BomAdditionalInfo;
            public Int32Field BomBranchId;
            public Int32Field BomOwnerId;
            public Int32Field BomAssignedId;
            public BooleanField BomOtherAddress;
            public StringField BomShippingAddress;
            public DoubleField BomPackagingCharges;
            public DoubleField BomFreightCharges;
            public DoubleField BomAdvacne;
            public DateTimeField BomDueDate;
            public StringField BomDispatchDetails;
            public DoubleField BomRoundup;
            public StringField BomSubject;
            public StringField BomReference;
            public Int32Field BomContactPersonId;
            public Int32Field BomLines;
            public Int32Field BomQuotationNo;
            public DateTimeField BomQuotationDate;
            public DoubleField BomConversion;
            public StringField BomPurchaseOrderNo;
            public StringField BomItemName;
            public Int32Field BomOperationCost;
            public Int32Field BomRawMaterialCost;
            public Int32Field BomScrapMaterialCost;
            public Int32Field BomTotalMaterialCost;
            public StringField BomOperationName;
            public StringField BomWorkStationName;
            public StringField BomOperatinngTime;
            public Int32Field BomOperatingCost;
            public Int32Field BomProcessLoss;
            public Int32Field BomProcessLossQty;
            public StringField BomAttachments;
            public Int32Field BomCompanyId;
            public Int32Field BomTaxable;
            public DoubleField BomQuantity;
            public DoubleField BomMrp;
            public DoubleField BomSellingPrice;
            public DoubleField BomPrice;
            public DoubleField BomDiscount;
            public StringField BomTaxType1;
            public DoubleField BomPercentage1;
            public StringField BomTaxType2;
            public DoubleField BomPercentage2;
            public DateTimeField BomWarrantyStart;
            public DateTimeField BomWarrantyEnd;
            public DoubleField BomDiscountAmount;
            public StringField BomDescription;
            public StringField BomUnit;
            public StringField BomImage;
            public StringField BomTechSpecs;
            public StringField BomHsn;
            public StringField BomCode;
            public Int32Field BomProductsId;

            public StringField Code;
            public BooleanField Inclusive;
            public DecimalField DiscPrice;
            public DecimalField Tax1Amount;
            public DecimalField Tax2Amount;
            public DecimalField LineTotal;
            public DecimalField GrandTotal;

        }
    }
}
