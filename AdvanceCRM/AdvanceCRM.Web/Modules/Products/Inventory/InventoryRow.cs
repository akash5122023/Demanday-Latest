using AdvanceCRM.Masters;
using AdvanceCRM.Scripts;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Products
{
    [ConnectionKey("Default"), Module("Products"), TableName("[dbo].[Inventory]")]
    [DisplayName("Inventory"), InstanceName("Inventory")]
    [ReadPermission("Inventory:Read")]
    [InsertPermission("Inventory:Insert")]
    [UpdatePermission("Inventory:Update")]
    [DeletePermission("Inventory:Delete")]
    [LookupScript("Products.Inventory", Permission = "?", LookupType = typeof(MultiCompanyRowLookupScript<>))]
    public sealed class InventoryRow : Row<InventoryRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog, IMultiCompanyRow
    {
        [DisplayName("Id"), Identity, IdProperty, SortOrder(1, true)]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Name"), Size(200), NotNull, QuickSearch, LookupInclude, NameProperty]
        public String Name
        {
            get => fields.Name[this];
            set => fields.Name[this] = value;
        }

        [DisplayName("Code"), Size(100), QuickSearch, LookupInclude]
        public String Code
        {
            get => fields.Code[this];
            set => fields.Code[this] = value;
        }

        [DisplayName("Division/Brand"), ForeignKey("[dbo].[ProductsDivision]", "Id"), LeftJoin("jDivision"), TextualField("DivisionProductsDivision")]
        [LookupEditor(typeof(ProductsDivisionRow), InplaceAdd = true)]
        public Int32? DivisionId
        {
            get => fields.DivisionId[this];
            set => fields.DivisionId[this] = value;
        }

        [DisplayName("Group"), ForeignKey("[dbo].[ProductsGroup]", "Id"), LeftJoin("jGroup"), TextualField("GroupProductsGroup")]
        [LookupEditor(typeof(ProductsGroupRow), InplaceAdd = true)]
        public Int32? GroupId
        {
            get => fields.GroupId[this];
            set => fields.GroupId[this] = value;
        }

        [DisplayName("Bottom Price"), DisplayFormat("#,##0.####"), NotNull, LookupInclude, DecimalEditor(Decimals = 4)]
        public Double? SellingPrice
        {
            get => fields.SellingPrice[this];
            set => fields.SellingPrice[this] = value;
        }

        [DisplayName("MRP"), DisplayFormat("#,##0.####"), Column("MRP"), NotNull, LookupInclude, DecimalEditor(Decimals = 4)]
        public Double? Mrp
        {
            get => fields.Mrp[this];
            set => fields.Mrp[this] = value;
        }

        [DisplayName("Description"), Size(4000), LookupInclude, TextAreaEditor(Rows = 4)]
        public String Description
        {
            get => fields.Description[this];
            set => fields.Description[this] = value;
        }

        [DisplayName("Tax1"), ForeignKey("[dbo].[Tax]", "Id"), LeftJoin("jTaxId1"), TextualField("TaxId1Name"), LookupInclude]
        [LookupEditor(typeof(TaxRow), InplaceAdd = true)]
        public Int32? TaxId1
        {
            get => fields.TaxId1[this];
            set => fields.TaxId1[this] = value;
        }

        [DisplayName("Tax2"), ForeignKey("[dbo].[Tax]", "Id"), LeftJoin("jTaxId2"), TextualField("TaxId2Name"), LookupInclude]
        [LookupEditor(typeof(TaxRow), InplaceAdd = true)]
        public Int32? TaxId2
        {
            get => fields.TaxId2[this];
            set => fields.TaxId2[this] = value;
        }

        [DisplayName("Image"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Product/~", CopyToHistory = true)]
        public String Image
        {
            get => fields.Image[this];
            set => fields.Image[this] = value;
        }

        [DisplayName("TechSpecs"), Size(2000), TextAreaEditor(Rows = 8)]
        public String TechSpecs
        {
            get => fields.TechSpecs[this];
            set => fields.TechSpecs[this] = value;
        }

        [DisplayName("HSN"), Size(100)]
        public String Hsn
        {
            get => fields.Hsn[this];
            set => fields.Hsn[this] = value;
        }

        [DisplayName("Channel Customer Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? ChannelCustomerPrice
        {
            get => fields.ChannelCustomerPrice[this];
            set => fields.ChannelCustomerPrice[this] = value;
        }

        [DisplayName("Reseller Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? ResellerPrice
        {
            get => fields.ResellerPrice[this];
            set => fields.ResellerPrice[this] = value;
        }

        [DisplayName("Wholesaler Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? WholesalerPrice
        {
            get => fields.WholesalerPrice[this];
            set => fields.WholesalerPrice[this] = value;
        }

        [DisplayName("Dealer Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? DealerPrice
        {
            get => fields.DealerPrice[this];
            set => fields.DealerPrice[this] = value;
        }

        [DisplayName("Distributor Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? DistributorPrice
        {
            get => fields.DistributorPrice[this];
            set => fields.DistributorPrice[this] = value;
        }

        [DisplayName("Stockiest Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? StockiestPrice
        {
            get => fields.StockiestPrice[this];
            set => fields.StockiestPrice[this] = value;
        }

        [DisplayName("National Distributor"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? NationalDistributorPrice
        {
            get => fields.NationalDistributorPrice[this];
            set => fields.NationalDistributorPrice[this] = value;
        }

        [DisplayName("Minimum Stock"), NotNull, DefaultValue("0"), LookupInclude]
        public Double? MinimumStock
        {
            get => fields.MinimumStock[this];
            set => fields.MinimumStock[this] = value;
        }

        [DisplayName("Maximum Stock"), NotNull, DefaultValue("0"), LookupInclude]
        public Double? MaximumStock
        {
            get => fields.MaximumStock[this];
            set => fields.MaximumStock[this] = value;
        }

        [BSSwitchEditor(OnText = "Yes", OffText = "No")]
        [DisplayName("Raw Material"), LookupInclude]
        public Boolean? RawMaterial
        {
            get => fields.RawMaterial[this];
            set => fields.RawMaterial[this] = value;
        }

        [DisplayName("Purchase Price"), Column("PurchasePrice"), NotNull, DefaultValue("0"), LookupInclude, DecimalEditor(Decimals = 4)]

        public Double? PurchasePrice
        {
            get => fields.PurchasePrice[this];
            set => fields.PurchasePrice[this] = value;
        }

        [DisplayName("Opening Stock"), NotNull]
         public Double? OpeningStock
         {
             get => fields.OpeningStock[this];
             set => fields.OpeningStock[this] = value;
         }
        
        [DisplayName("Unit"), ForeignKey("[dbo].[ProductsUnit]", "Id"), LeftJoin("jUnit"), TextualField("UnitProductsUnit"), LookupInclude]
        [LookupEditor(typeof(ProductsUnitRow), InplaceAdd = true)]
        public Int32? UnitId
        {
            get => fields.UnitId[this];
            set => fields.UnitId[this] = value;
        }

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        [Insertable(false), Updatable(false)]
        public Int32? CompanyId
        {
            get => fields.CompanyId[this];
            set => fields.CompanyId[this] = value;
        }
        public Int32Field CompanyIdField
        {
            get { return Fields.CompanyId; }
        }
        [DisplayName("Product Type Id")]
        public Int32? ProductTypeId
        {
            get => fields.ProductTypeId[this];
            set => fields.ProductTypeId[this] = value;
        }

        [DisplayName("Model Segment Id")]
        public Int32? ModelSegmentId
        {
            get => fields.ModelSegmentId[this];
            set => fields.ModelSegmentId[this] = value;
        }

        [DisplayName("Model Name Id"), Column("ModelNameID")]
        public Int32? ModelNameId
        {
            get => fields.ModelNameId[this];
            set => fields.ModelNameId[this] = value;
        }

        [DisplayName("Model Code Id")]
        public Int32? ModelCodeId
        {
            get => fields.ModelCodeId[this];
            set => fields.ModelCodeId[this] = value;
        }

        [DisplayName("Model Varient Id")]
        public Int32? ModelVarientId
        {
            get => fields.ModelVarientId[this];
            set => fields.ModelVarientId[this] = value;
        }

        [DisplayName("Model Color Id")]
        public Int32? ModelColorId
        {
            get => fields.ModelColorId[this];
            set => fields.ModelColorId[this] = value;
        }

        [DisplayName("Serial No"), Size(200)]
        public String SerialNo
        {
            get => fields.SerialNo[this];
            set => fields.SerialNo[this] = value;
        }

        [DisplayName("Ex Showroom Price")]
        public Double? ExShowroomPrice
        {
            get => fields.ExShowroomPrice[this];
            set => fields.ExShowroomPrice[this] = value;
        }

        [DisplayName("Insurance Amount")]
        public Double? InsuranceAmount
        {
            get => fields.InsuranceAmount[this];
            set => fields.InsuranceAmount[this] = value;
        }

        [DisplayName("Registration Amount")]
        public Double? RegistrationAmount
        {
            get => fields.RegistrationAmount[this];
            set => fields.RegistrationAmount[this] = value;
        }

        [DisplayName("Road Tax")]
        public Double? RoadTax
        {
            get => fields.RoadTax[this];
            set => fields.RoadTax[this] = value;
        }

        [DisplayName("On Road Price")]
        public Double? OnRoadPrice
        {
            get => fields.OnRoadPrice[this];
            set => fields.OnRoadPrice[this] = value;
        }

        [DisplayName("Other Taxes")]
        public Double? OtherTaxes
        {
            get => fields.OtherTaxes[this];
            set => fields.OtherTaxes[this] = value;
        }

        [DisplayName("Extended Warranty")]
        public Double? ExtendedWarranty
        {
            get => fields.ExtendedWarranty[this];
            set => fields.ExtendedWarranty[this] = value;
        }

        [DisplayName("RSA"), Column("RSA")]
        public Double? Rsa
        {
            get => fields.Rsa[this];
            set => fields.Rsa[this] = value;
        }

        [DisplayName("Image Attachment"), Size(500)]
        public String ImageAttachment
        {
            get => fields.ImageAttachment[this];
            set => fields.ImageAttachment[this] = value;
        }

        [DisplayName("File Attachment"), Size(1000)]
        public String FileAttachment
        {
            get => fields.FileAttachment[this];
            set => fields.FileAttachment[this] = value;
        }

        [DisplayName("From"), Size(100)]
        public String From
        {
            get => fields.From[this];
            set => fields.From[this] = value;
        }

        [DisplayName("To"), Size(100)]
        public String To
        {
            get => fields.To[this];
            set => fields.To[this] = value;
        }

        [DisplayName("Date")]
        public DateTime? Date
        {
            get => fields.Date[this];
            set => fields.Date[this] = value;
        }

        [DisplayName("Adults"), Size(50)]
        public String Adults
        {
            get => fields.Adults[this];
            set => fields.Adults[this] = value;
        }

        [DisplayName("Childrens"), Size(50)]
        public String Childrens
        {
            get => fields.Childrens[this];
            set => fields.Childrens[this] = value;
        }

        [DisplayName("Destination"), Size(100)]
        public String Destination
        {
            get => fields.Destination[this];
            set => fields.Destination[this] = value;
        }

        [DisplayName("Nights"), Size(50)]
        public String Nights
        {
            get => fields.Nights[this];
            set => fields.Nights[this] = value;
        }

        [DisplayName("Hotel Name"), Size(100)]
        public String HotelName
        {
            get => fields.HotelName[this];
            set => fields.HotelName[this] = value;
        }

        [DisplayName("Meal Plan"), Size(100)]
        public String MealPlan
        {
            get => fields.MealPlan[this];
            set => fields.MealPlan[this] = value;
        }
        //        [DisplayName("Quantity")]
        //        [Expression(@"(
        //    ISNULL((SELECT SUM(Quantity) FROM dbo.PurchaseProducts WHERE ProductsId = T0.ProductsId), 0)
        //    + ISNULL((SELECT SUM(Quantity) FROM dbo.SalesReturnProducts WHERE ProductsId = T0.ProductsId), 0)
        //    + ISNULL((SELECT OpeningStock FROM dbo.Products WHERE Id = T0.ProductsId), 0)
        //    + ISNULL((SELECT SUM(Quantity) FROM dbo.InwardProducts WHERE ProductsId = T0.ProductsId), 0)
        //    - ISNULL((SELECT SUM(Quantity) FROM dbo.SalesProducts WHERE ProductsId = T0.ProductsId), 0)
        //    - ISNULL((SELECT SUM(Quantity) FROM dbo.PurchaseReturnProducts WHERE ProductsId = T0.ProductsId), 0)
        //    - ISNULL((SELECT SUM(Quantity) FROM dbo.ChallanProducts WHERE ProductsId = T0.ProductsId), 0)
        //    - ISNULL((SELECT SUM(Quantity) FROM dbo.BomProducts WHERE ProductsId = T0.ProductsId), 0)
        //)")]
        [DisplayName("Quantity"), Insertable(false), Updatable(false)]
        [Expression(@"
(
    ISNULL((SELECT SUM(Quantity) FROM dbo.PurchaseProducts WHERE ProductsId = T0.ProductsId), 0)
  + ISNULL((SELECT SUM(Quantity) FROM dbo.SalesReturnProducts WHERE ProductsId = T0.ProductsId), 0)
  + ISNULL((SELECT OpeningStock FROM dbo.Products WHERE Id = T0.ProductsId), 0)
  + ISNULL((SELECT SUM(Quantity) FROM dbo.InwardProducts WHERE ProductsId = T0.ProductsId), 0)
  - ISNULL((SELECT SUM(Quantity) FROM dbo.SalesProducts WHERE ProductsId = T0.ProductsId), 0)
  - ISNULL((SELECT SUM(Quantity) FROM dbo.PurchaseReturnProducts WHERE ProductsId = T0.ProductsId), 0)
  - ISNULL((SELECT SUM(Quantity) FROM dbo.ChallanProducts WHERE ProductsId = T0.ProductsId), 0)
  - ISNULL((
        SELECT SUM(bp.Quantity * sp.Quantity)
        FROM dbo.SalesProducts sp
        INNER JOIN dbo.Bom b ON b.ProductsId = sp.ProductsId
        INNER JOIN dbo.BomProducts bp ON bp.BomId = b.Id
        WHERE bp.ProductsId = T0.ProductsId
    ), 0)
)")]

        public double? Quantity
        {
            get => fields.Quantity[this];
            set => fields.Quantity[this] = value;
        }

        [DisplayName("Track Inventory")]
        public Boolean? TrackInventory
        {
            get => fields.TrackInventory[this];
            set => fields.TrackInventory[this] = value;
        }
        [DisplayName("Products Name"), Expression("jProducts.[Name]")]
        public String ProductsName
        {
            get => fields.ProductsName[this];
            set => fields.ProductsName[this] = value;
        }

        [DisplayName("Products Code"), Expression("jProducts.[Code]")]
        public String ProductsCode
        {
            get => fields.ProductsCode[this];
            set => fields.ProductsCode[this] = value;
        }

        [DisplayName("Products Division Id"), Expression("jProducts.[DivisionId]")]
        public Int32? ProductsDivisionId
        {
            get => fields.ProductsDivisionId[this];
            set => fields.ProductsDivisionId[this] = value;
        }

        [DisplayName("Products Group Id"), Expression("jProducts.[GroupId]")]
        public Int32? ProductsGroupId
        {
            get => fields.ProductsGroupId[this];
            set => fields.ProductsGroupId[this] = value;
        }

        [DisplayName("Products Selling Price"), Expression("jProducts.[SellingPrice]")]
        public Double? ProductsSellingPrice
        {
            get => fields.ProductsSellingPrice[this];
            set => fields.ProductsSellingPrice[this] = value;
        }

        [DisplayName("Products Mrp"), Expression("jProducts.[MRP]")]
        public Double? ProductsMrp
        {
            get => fields.ProductsMrp[this];
            set => fields.ProductsMrp[this] = value;
        }

        [DisplayName("Products Description"), Expression("jProducts.[Description]")]
        public String ProductsDescription
        {
            get => fields.ProductsDescription[this];
            set => fields.ProductsDescription[this] = value;
        }

        [DisplayName("Products Tax Id1"), Expression("jProducts.[TaxId1]")]
        public Int32? ProductsTaxId1
        {
            get => fields.ProductsTaxId1[this];
            set => fields.ProductsTaxId1[this] = value;
        }

        [DisplayName("Products Tax Id2"), Expression("jProducts.[TaxId2]")]
        public Int32? ProductsTaxId2
        {
            get => fields.ProductsTaxId2[this];
            set => fields.ProductsTaxId2[this] = value;
        }

        [DisplayName("Products Image"), Expression("jProducts.[Image]")]
        public String ProductsImage
        {
            get => fields.ProductsImage[this];
            set => fields.ProductsImage[this] = value;
        }

        [DisplayName("Products Tech Specs"), Expression("jProducts.[TechSpecs]")]
        public String ProductsTechSpecs
        {
            get => fields.ProductsTechSpecs[this];
            set => fields.ProductsTechSpecs[this] = value;
        }

        [DisplayName("Products Hsn"), Expression("jProducts.[HSN]")]
        public String ProductsHsn
        {
            get => fields.ProductsHsn[this];
            set => fields.ProductsHsn[this] = value;
        }

        [DisplayName("Products Channel Customer Price"), Expression("jProducts.[ChannelCustomerPrice]")]
        public Double? ProductsChannelCustomerPrice
        {
            get => fields.ProductsChannelCustomerPrice[this];
            set => fields.ProductsChannelCustomerPrice[this] = value;
        }

        [DisplayName("Products Reseller Price"), Expression("jProducts.[ResellerPrice]")]
        public Double? ProductsResellerPrice
        {
            get => fields.ProductsResellerPrice[this];
            set => fields.ProductsResellerPrice[this] = value;
        }

        [DisplayName("Products Wholesaler Price"), Expression("jProducts.[WholesalerPrice]")]
        public Double? ProductsWholesalerPrice
        {
            get => fields.ProductsWholesalerPrice[this];
            set => fields.ProductsWholesalerPrice[this] = value;
        }

        [DisplayName("Products Dealer Price"), Expression("jProducts.[DealerPrice]")]
        public Double? ProductsDealerPrice
        {
            get => fields.ProductsDealerPrice[this];
            set => fields.ProductsDealerPrice[this] = value;
        }

        [DisplayName("Products Distributor Price"), Expression("jProducts.[DistributorPrice]")]
        public Double? ProductsDistributorPrice
        {
            get => fields.ProductsDistributorPrice[this];
            set => fields.ProductsDistributorPrice[this] = value;
        }

        [DisplayName("Products Stockiest Price"), Expression("jProducts.[StockiestPrice]")]
        public Double? ProductsStockiestPrice
        {
            get => fields.ProductsStockiestPrice[this];
            set => fields.ProductsStockiestPrice[this] = value;
        }

        [DisplayName("Products National Distributor Price"), Expression("jProducts.[NationalDistributorPrice]")]
        public Double? ProductsNationalDistributorPrice
        {
            get => fields.ProductsNationalDistributorPrice[this];
            set => fields.ProductsNationalDistributorPrice[this] = value;
        }

        [DisplayName("Products Minimum Stock"), Expression("jProducts.[MinimumStock]")]
        public Double? ProductsMinimumStock
        {
            get => fields.ProductsMinimumStock[this];
            set => fields.ProductsMinimumStock[this] = value;
        }

        [DisplayName("Products Maximum Stock"), Expression("jProducts.[MaximumStock]")]
        public Double? ProductsMaximumStock
        {
            get => fields.ProductsMaximumStock[this];
            set => fields.ProductsMaximumStock[this] = value;
        }

        [DisplayName("Products Raw Material"), Expression("jProducts.[RawMaterial]")]
        public Boolean? ProductsRawMaterial
        {
            get => fields.ProductsRawMaterial[this];
            set => fields.ProductsRawMaterial[this] = value;
        }

        [DisplayName("Products Purchase Price"), Expression("jProducts.[PurchasePrice]")]
        public Double? ProductsPurchasePrice
        {
            get => fields.ProductsPurchasePrice[this];
            set => fields.ProductsPurchasePrice[this] = value;
        }

        [DisplayName("Products Opening Stock"), Expression("jProducts.[OpeningStock]")]
        public Double? ProductsOpeningStock
        {
            get => fields.ProductsOpeningStock[this];
            set => fields.ProductsOpeningStock[this] = value;
        }

        [DisplayName("Products Unit Id"), Expression("jProducts.[UnitId]")]
        public Int32? ProductsUnitId
        {
            get => fields.ProductsUnitId[this];
            set => fields.ProductsUnitId[this] = value;
        }

        [DisplayName("Products Company Id"), Expression("jProducts.[CompanyId]")]
        public Int32? ProductsCompanyId
        {
            get => fields.ProductsCompanyId[this];
            set => fields.ProductsCompanyId[this] = value;
        }

        [DisplayName("Products Product Type Id"), Expression("jProducts.[ProductTypeId]")]
        public Int32? ProductsProductTypeId
        {
            get => fields.ProductsProductTypeId[this];
            set => fields.ProductsProductTypeId[this] = value;
        }

        [DisplayName("Products Model Segment Id"), Expression("jProducts.[ModelSegmentId]")]
        public Int32? ProductsModelSegmentId
        {
            get => fields.ProductsModelSegmentId[this];
            set => fields.ProductsModelSegmentId[this] = value;
        }

        [DisplayName("Products Model Name Id"), Expression("jProducts.[ModelNameID]")]
        public Int32? ProductsModelNameId
        {
            get => fields.ProductsModelNameId[this];
            set => fields.ProductsModelNameId[this] = value;
        }

        [DisplayName("Products Model Code Id"), Expression("jProducts.[ModelCodeId]")]
        public Int32? ProductsModelCodeId
        {
            get => fields.ProductsModelCodeId[this];
            set => fields.ProductsModelCodeId[this] = value;
        }

        [DisplayName("Products Model Varient Id"), Expression("jProducts.[ModelVarientId]")]
        public Int32? ProductsModelVarientId
        {
            get => fields.ProductsModelVarientId[this];
            set => fields.ProductsModelVarientId[this] = value;
        }

        [DisplayName("Products Model Color Id"), Expression("jProducts.[ModelColorId]")]
        public Int32? ProductsModelColorId
        {
            get => fields.ProductsModelColorId[this];
            set => fields.ProductsModelColorId[this] = value;
        }

        [DisplayName("Products Serial No"), Expression("jProducts.[SerialNo]")]
        public String ProductsSerialNo
        {
            get => fields.ProductsSerialNo[this];
            set => fields.ProductsSerialNo[this] = value;
        }

        [DisplayName("Products Ex Showroom Price"), Expression("jProducts.[ExShowroomPrice]")]
        public Double? ProductsExShowroomPrice
        {
            get => fields.ProductsExShowroomPrice[this];
            set => fields.ProductsExShowroomPrice[this] = value;
        }

        [DisplayName("Products Insurance Amount"), Expression("jProducts.[InsuranceAmount]")]
        public Double? ProductsInsuranceAmount
        {
            get => fields.ProductsInsuranceAmount[this];
            set => fields.ProductsInsuranceAmount[this] = value;
        }

        [DisplayName("Products Registration Amount"), Expression("jProducts.[RegistrationAmount]")]
        public Double? ProductsRegistrationAmount
        {
            get => fields.ProductsRegistrationAmount[this];
            set => fields.ProductsRegistrationAmount[this] = value;
        }

        [DisplayName("Products Road Tax"), Expression("jProducts.[RoadTax]")]
        public Double? ProductsRoadTax
        {
            get => fields.ProductsRoadTax[this];
            set => fields.ProductsRoadTax[this] = value;
        }

        [DisplayName("Products On Road Price"), Expression("jProducts.[OnRoadPrice]")]
        public Double? ProductsOnRoadPrice
        {
            get => fields.ProductsOnRoadPrice[this];
            set => fields.ProductsOnRoadPrice[this] = value;
        }

        [DisplayName("Products Other Taxes"), Expression("jProducts.[OtherTaxes]")]
        public Double? ProductsOtherTaxes
        {
            get => fields.ProductsOtherTaxes[this];
            set => fields.ProductsOtherTaxes[this] = value;
        }

        [DisplayName("Products Extended Warranty"), Expression("jProducts.[ExtendedWarranty]")]
        public Double? ProductsExtendedWarranty
        {
            get => fields.ProductsExtendedWarranty[this];
            set => fields.ProductsExtendedWarranty[this] = value;
        }

        [DisplayName("Products Rsa"), Expression("jProducts.[RSA]")]
        public Double? ProductsRsa
        {
            get => fields.ProductsRsa[this];
            set => fields.ProductsRsa[this] = value;
        }

        [DisplayName("Products Image Attachment"), Expression("jProducts.[ImageAttachment]")]
        public String ProductsImageAttachment
        {
            get => fields.ProductsImageAttachment[this];
            set => fields.ProductsImageAttachment[this] = value;
        }

        [DisplayName("Products File Attachment"), Expression("jProducts.[FileAttachment]")]
        public String ProductsFileAttachment
        {
            get => fields.ProductsFileAttachment[this];
            set => fields.ProductsFileAttachment[this] = value;
        }

        [DisplayName("Products From"), Expression("jProducts.[From]")]
        public String ProductsFrom
        {
            get => fields.ProductsFrom[this];
            set => fields.ProductsFrom[this] = value;
        }

        [DisplayName("Products To"), Expression("jProducts.[To]")]
        public String ProductsTo
        {
            get => fields.ProductsTo[this];
            set => fields.ProductsTo[this] = value;
        }

        [DisplayName("Products Date"), Expression("jProducts.[Date]")]
        public DateTime? ProductsDate
        {
            get => fields.ProductsDate[this];
            set => fields.ProductsDate[this] = value;
        }

        [DisplayName("Products Adults"), Expression("jProducts.[Adults]")]
        public String ProductsAdults
        {
            get => fields.ProductsAdults[this];
            set => fields.ProductsAdults[this] = value;
        }

        [DisplayName("Products Childrens"), Expression("jProducts.[Childrens]")]
        public String ProductsChildrens
        {
            get => fields.ProductsChildrens[this];
            set => fields.ProductsChildrens[this] = value;
        }

        [DisplayName("Products Destination"), Expression("jProducts.[Destination]")]
        public String ProductsDestination
        {
            get => fields.ProductsDestination[this];
            set => fields.ProductsDestination[this] = value;
        }

        [DisplayName("Products Nights"), Expression("jProducts.[Nights]")]
        public String ProductsNights
        {
            get => fields.ProductsNights[this];
            set => fields.ProductsNights[this] = value;
        }

        [DisplayName("Products Hotel Name"), Expression("jProducts.[HotelName]")]
        public String ProductsHotelName
        {
            get => fields.ProductsHotelName[this];
            set => fields.ProductsHotelName[this] = value;
        }

        [DisplayName("Products Meal Plan"), Expression("jProducts.[MealPlan]")]
        public String ProductsMealPlan
        {
            get => fields.ProductsMealPlan[this];
            set => fields.ProductsMealPlan[this] = value;
        }

        [DisplayName("Products Branch Id"), Expression("jProducts.[BranchId]")]
        public Int32? ProductsBranchId
        {
            get => fields.ProductsBranchId[this];
            set => fields.ProductsBranchId[this] = value;
        }

        [DisplayName("Products Quantity"), Expression("jProducts.[Quantity]")]
        public Double? ProductsQuantity
        {
            get => fields.ProductsQuantity[this];
            set => fields.ProductsQuantity[this] = value;
        }

        [DisplayName("Products Track Inventory"), Expression("jProducts.[TrackInventory]")]
        public Boolean? ProductsTrackInventory
        {
            get => fields.ProductsTrackInventory[this];
            set => fields.ProductsTrackInventory[this] = value;
        }
        [DisplayName("Division"), Expression("jDivision.[ProductsDivision]")]
        public String DivisionProductsDivision
        {
            get { return Fields.DivisionProductsDivision[this]; }
            set { Fields.DivisionProductsDivision[this] = value; }
        }


        [DisplayName("Group"), Expression("jGroup.[ProductsGroup]")]
        public String GroupProductsGroup
        {
            get { return Fields.GroupProductsGroup[this]; }
            set { Fields.GroupProductsGroup[this] = value; }
        }

        [DisplayName("Tax 1"), Expression("jTaxId1.[Name]")]
        public String TaxId1Name
        {
            get { return Fields.TaxId1Name[this]; }
            set { Fields.TaxId1Name[this] = value; }
        }

        [DisplayName("Tax Id1 Type"), Expression("jTaxId1.[Type]")]
        public String TaxId1Type
        {
            get { return Fields.TaxId1Type[this]; }
            set { Fields.TaxId1Type[this] = value; }
        }

        [DisplayName("Tax Id1 Percentage"), Expression("jTaxId1.[Percentage]")]
        public Double? TaxId1Percentage
        {
            get { return Fields.TaxId1Percentage[this]; }
            set { Fields.TaxId1Percentage[this] = value; }
        }

        [DisplayName("Tax 2"), Expression("jTaxId2.[Name]")]
        public String TaxId2Name
        {
            get { return Fields.TaxId2Name[this]; }
            set { Fields.TaxId2Name[this] = value; }
        }

        [DisplayName("Tax Id2 Type"), Expression("jTaxId2.[Type]")]
        public String TaxId2Type
        {
            get { return Fields.TaxId2Type[this]; }
            set { Fields.TaxId2Type[this] = value; }
        }

        [DisplayName("Tax Id2 Percentage"), Expression("jTaxId2.[Percentage]")]
        public Double? TaxId2Percentage
        {
            get { return Fields.TaxId2Percentage[this]; }
            set { Fields.TaxId2Percentage[this] = value; }
        }

        [DisplayName("Unit"), Expression("jUnit.[ProductsUnit]")]
        public String UnitProductsUnit
        {
            get { return Fields.UnitProductsUnit[this]; }
            set { Fields.UnitProductsUnit[this] = value; }
        }

        [DisplayName("CodePlusName"), Expression("(t0.Code + ' # ' + t0.Name)"), NotMapped, MinSelectLevel(SelectLevel.List), LookupInclude]
        public String CodePlusName
        {
            get { return Fields.CodePlusName[this]; }
            set { Fields.CodePlusName[this] = value; }
        }
        [DisplayName("Branch"), Expression("jProducts.[BranchId]"), Insertable(false), Updatable(false)]
        [LookupEditor("Administration.BranchLookup"), TextualField("Branch"), ReadOnly(true)]
        public Int32? BranchId
        {
            get => fields.BranchId[this];
            set => fields.BranchId[this] = value;
        }
        [DisplayName("Branch"), Expression("(SELECT TOP 1 b.Branch FROM dbo.Branch b WHERE b.Id = jProducts.[BranchId])")]
        public String Branch
        {
            get => fields.Branch[this];
            set => fields.Branch[this] = value;
        }


        [DisplayName("Products"), ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts")]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        public Int32? ProductsId
        {
            get { return Fields.ProductsId[this]; }
            set { Fields.ProductsId[this] = value; }
        }
        public InventoryRow()
            : base()
        {
        }

        public InventoryRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField Code;
            public Int32Field DivisionId;
            public Int32Field GroupId;
            public DoubleField SellingPrice;
            public DoubleField Mrp;
            public StringField Description;
            public Int32Field TaxId1;
            public Int32Field TaxId2;
            public StringField Image;
            public StringField TechSpecs;
            public StringField Hsn;
            public DoubleField ChannelCustomerPrice;
            public DoubleField ResellerPrice;
            public DoubleField WholesalerPrice;
            public DoubleField DealerPrice;
            public DoubleField DistributorPrice;
            public DoubleField StockiestPrice;
            public DoubleField NationalDistributorPrice;
            public DoubleField MinimumStock;
            public DoubleField MaximumStock;
            public BooleanField RawMaterial;
            public DoubleField PurchasePrice;
            public Int32Field UnitId;
            public Int32Field CompanyId;
            public Int32Field ProductTypeId;
            public Int32Field ModelSegmentId;
            public Int32Field ModelNameId;
            public Int32Field ModelCodeId;
            public Int32Field ModelVarientId;
            public Int32Field ModelColorId;
            public StringField SerialNo;
            public DoubleField ExShowroomPrice;
            public DoubleField InsuranceAmount;
            public DoubleField RegistrationAmount;
            public DoubleField RoadTax;
            public DoubleField OnRoadPrice;
            public DoubleField OtherTaxes;
            public DoubleField ExtendedWarranty;
            public DoubleField Rsa;
            public StringField ImageAttachment;
            public StringField FileAttachment;
            public StringField From;
            public StringField To;
            public DateTimeField Date;
            public StringField Adults;
            public StringField Childrens;
            public StringField Destination;
            public StringField Nights;
            public StringField HotelName;
            public StringField MealPlan;
            public Int32Field BranchId;
            public DoubleField Quantity;
            public BooleanField TrackInventory;
            public Int32Field ProductsId;

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
            public Int32Field ProductsCompanyId;
            public Int32Field ProductsProductTypeId;
            public Int32Field ProductsModelSegmentId;
            public Int32Field ProductsModelNameId;
            public Int32Field ProductsModelCodeId;
            public Int32Field ProductsModelVarientId;
            public Int32Field ProductsModelColorId;
            public StringField ProductsSerialNo;
            public DoubleField ProductsExShowroomPrice;
            public DoubleField ProductsInsuranceAmount;
            public DoubleField ProductsRegistrationAmount;
            public DoubleField ProductsRoadTax;
            public DoubleField ProductsOnRoadPrice;
            public DoubleField ProductsOtherTaxes;
            public DoubleField ProductsExtendedWarranty;
            public DoubleField ProductsRsa;
            public StringField ProductsImageAttachment;
            public StringField ProductsFileAttachment;
            public StringField ProductsFrom;
            public StringField ProductsTo;
            public DateTimeField ProductsDate;
            public StringField ProductsAdults;
            public StringField ProductsChildrens;
            public StringField ProductsDestination;
            public StringField ProductsNights;
            public StringField ProductsHotelName;
            public StringField ProductsMealPlan;
            public Int32Field ProductsBranchId;
            public DoubleField ProductsQuantity;
            public BooleanField ProductsTrackInventory;
            public DoubleField OpeningStock;
            public StringField DivisionProductsDivision;

            public StringField GroupProductsGroup;

            public StringField TaxId1Name;
            public StringField TaxId1Type;
            public DoubleField TaxId1Percentage;

            public StringField TaxId2Name;
            public StringField TaxId2Type;
            public DoubleField TaxId2Percentage;

            public StringField UnitProductsUnit;
           // public Int32Field BranchId;
            public StringField Branch;
            public StringField CodePlusName;
        }
    }
}
