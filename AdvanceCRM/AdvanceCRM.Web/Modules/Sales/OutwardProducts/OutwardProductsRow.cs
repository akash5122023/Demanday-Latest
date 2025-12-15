using AdvanceCRM.Products;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Sales
{
    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[OutwardProducts]")]
    [DisplayName("Outward Products"), InstanceName("Outward Products")]
    [ReadPermission("Outward:Read")]
    [ModifyPermission("Outward:Read")]
    public sealed class OutwardProductsRow : Row<OutwardProductsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, IdProperty, SortOrder(1)]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Products"), NotNull, ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("ProductsName")]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]        
        public Int32? ProductsId
        {
            get => fields.ProductsId[this];
            set => fields.ProductsId[this] = value;
        }

        [DisplayName("Serial"), Size(100), QuickSearch, NameProperty]
        public String Serial
        {
            get => fields.Serial[this];
            set => fields.Serial[this] = value;
        }

        [DisplayName("Batch"), Size(100)]
        public String Batch
        {
            get => fields.Batch[this];
            set => fields.Batch[this] = value;
        }

        [DisplayName("Quantity"), NotNull]
        public Double? Quantity
        {
            get => fields.Quantity[this];
            set => fields.Quantity[this] = value;
        }

        [DisplayName("Price"), DisplayFormat("#,##0.####"), NotNull]
        public Double? Price
        {
            get => fields.Price[this];
            set => fields.Price[this] = value;
        }

        [DisplayName("Selling Price"), DisplayFormat("#,##0.####"), NotNull]
        public Double? SellingPrice
        {
            get => fields.SellingPrice[this];
            set => fields.SellingPrice[this] = value;
        }

        [DisplayName("MRP"), DisplayFormat("#,##0.####"), Column("MRP"), NotNull]
        public Double? Mrp
        {
            get => fields.Mrp[this];
            set => fields.Mrp[this] = value;
        }

        [DisplayName("Discount"), NotNull, DefaultValue("0")]
        public Double? Discount
        {
            get => fields.Discount[this];
            set => fields.Discount[this] = value;
        }

        [DisplayName("Discount Amount"), NotNull, DefaultValue("0")]
        public Double? DiscountAmount
        {
            get => fields.DiscountAmount[this];
            set => fields.DiscountAmount[this] = value;
        }

        [DisplayName("Outward"), NotNull, ForeignKey("[dbo].[Outward]", "Id"), LeftJoin("jOutward"), TextualField("OutwardShippingAddress")]
        [LookupEditor(typeof(OutwardRow), InplaceAdd = true)]
        public Int32? OutwardId
        {
            get => fields.OutwardId[this];
            set => fields.OutwardId[this] = value;
        }

        [DisplayName("Description"), Size(2000), TextAreaEditor(Rows = 4)]
        public String Description
        {
            get => fields.Description[this];
            set => fields.Description[this] = value;
        }
        [DisplayName("Unit"), Expression("jProductsUnit.[ProductsUnit]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String Unit
        {
            get => fields.Unit[this];
            set => fields.Unit[this] = value;
        }

        [DisplayName("Tax Type1"), Size(100)]
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

        [DisplayName("Branch"), NotNull, ForeignKey("[dbo].[Branch]", "Id"), LeftJoin("jBranch"), TextualField("Branch")]
        public Int32? BranchId
        {
            get => fields.BranchId[this];
            set => fields.BranchId[this] = value;
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
        [ForeignKey("ProductsUnit", "Id"), LeftJoin("jProductsUnit")]
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

        [DisplayName("Branch"), Expression("jBranch.[Branch]")]
        public String Branch
        {
            get => fields.Branch[this];
            set => fields.Branch[this] = value;
        }

        [DisplayName("Branch Phone"), Expression("jBranch.[Phone]")]
        public String BranchPhone
        {
            get => fields.BranchPhone[this];
            set => fields.BranchPhone[this] = value;
        }

        [DisplayName("Branch Email"), Expression("jBranch.[Email]")]
        public String BranchEmail
        {
            get => fields.BranchEmail[this];
            set => fields.BranchEmail[this] = value;
        }

        [DisplayName("Branch Address"), Expression("jBranch.[Address]")]
        public String BranchAddress
        {
            get => fields.BranchAddress[this];
            set => fields.BranchAddress[this] = value;
        }

        [DisplayName("Branch City Id"), Expression("jBranch.[CityId]")]
        public Int32? BranchCityId
        {
            get => fields.BranchCityId[this];
            set => fields.BranchCityId[this] = value;
        }

        [DisplayName("Branch State Id"), Expression("jBranch.[StateId]")]
        public Int32? BranchStateId
        {
            get => fields.BranchStateId[this];
            set => fields.BranchStateId[this] = value;
        }

        [DisplayName("Branch Pin"), Expression("jBranch.[Pin]")]
        public String BranchPin
        {
            get => fields.BranchPin[this];
            set => fields.BranchPin[this] = value;
        }

        [DisplayName("Branch Country"), Expression("jBranch.[Country]")]
        public Int32? BranchCountry
        {
            get => fields.BranchCountry[this];
            set => fields.BranchCountry[this] = value;
        }

        [DisplayName("Branch Company Id"), Expression("jBranch.[CompanyId]")]
        public Int32? BranchCompanyId
        {
            get => fields.BranchCompanyId[this];
            set => fields.BranchCompanyId[this] = value;
        }
        
        public OutwardProductsRow()
            : base()
        {
        }

        public OutwardProductsRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProductsId;
            public StringField Serial;
            public StringField Batch;
            public DoubleField Quantity;
            public DoubleField Price;
            public DoubleField SellingPrice;
            public DoubleField Mrp;
            public DoubleField Discount;
            public DoubleField DiscountAmount;
            public Int32Field OutwardId;
            public StringField Description;
            public StringField Unit;
            public StringField TaxType1;
            public DoubleField Percentage1;
            public StringField TaxType2;
            public DoubleField Percentage2;
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

            public StringField Branch;
            public StringField BranchPhone;
            public StringField BranchEmail;
            public StringField BranchAddress;
            public Int32Field BranchCityId;
            public Int32Field BranchStateId;
            public StringField BranchPin;
            public Int32Field BranchCountry;
            public Int32Field BranchCompanyId;
        }
    }
}
