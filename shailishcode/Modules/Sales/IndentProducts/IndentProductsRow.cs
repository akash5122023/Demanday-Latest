using AdvanceCRM.Products;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Sales
{
    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[IndentProducts]")]
    [DisplayName("Indent Products"), InstanceName("Indent Products")]
    [ReadPermission("IndentProducts")]
    [ModifyPermission("IndentProducts")]
    public sealed class IndentProductsRow : Row<IndentProductsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
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
        [DisplayName("Quantity"), NotNull]
        public Double? Quantity
        {
            get => fields.Quantity[this];
            set => fields.Quantity[this] = value;
        }

        [DisplayName("Remark"), Size(2000), QuickSearch]
        public String Description
        {
            get => fields.Description[this];
            set => fields.Description[this] = value;
        }

        [DisplayName("Products Name"), Expression("jProducts.[Name]"), NameProperty]
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

        [DisplayName("Indent Contacts Id"), Expression("jIndent.[ContactsId]")]
        public Int32? IndentContactsId
        {
            get => fields.IndentContactsId[this];
            set => fields.IndentContactsId[this] = value;
        }

        [DisplayName("Indent Date"), Expression("jIndent.[Date]")]
        public DateTime? IndentDate
        {
            get => fields.IndentDate[this];
            set => fields.IndentDate[this] = value;
        }

        [DisplayName("Indent Status"), Expression("jIndent.[Status]")]
        public Int32? IndentStatus
        {
            get => fields.IndentStatus[this];
            set => fields.IndentStatus[this] = value;
        }

        [DisplayName("Indent Additional Info"), Expression("jIndent.[AdditionalInfo]")]
        public String IndentAdditionalInfo
        {
            get => fields.IndentAdditionalInfo[this];
            set => fields.IndentAdditionalInfo[this] = value;
        }

        [DisplayName("Indent Invoice No"), Expression("jIndent.[InvoiceNo]")]
        public String IndentInvoiceNo
        {
            get => fields.IndentInvoiceNo[this];
            set => fields.IndentInvoiceNo[this] = value;
        }

        [DisplayName("Indent Branch Id"), Expression("jIndent.[BranchId]")]
        public Int32? IndentBranchId
        {
            get => fields.IndentBranchId[this];
            set => fields.IndentBranchId[this] = value;
        }

        [DisplayName("Indent Owner Id"), Expression("jIndent.[OwnerId]")]
        public Int32? IndentOwnerId
        {
            get => fields.IndentOwnerId[this];
            set => fields.IndentOwnerId[this] = value;
        }

        [DisplayName("Indent Assigned Id"), Expression("jIndent.[AssignedId]")]
        public Int32? IndentAssignedId
        {
            get => fields.IndentAssignedId[this];
            set => fields.IndentAssignedId[this] = value;
        }

        [DisplayName("Indent Subject"), Expression("jIndent.[Subject]")]
        public String IndentSubject
        {
            get => fields.IndentSubject[this];
            set => fields.IndentSubject[this] = value;
        }

        [DisplayName("Indent Reference"), Expression("jIndent.[Reference]")]
        public String IndentReference
        {
            get => fields.IndentReference[this];
            set => fields.IndentReference[this] = value;
        }

        [DisplayName("Indent Contact Person Id"), Expression("jIndent.[ContactPersonId]")]
        public Int32? IndentContactPersonId
        {
            get => fields.IndentContactPersonId[this];
            set => fields.IndentContactPersonId[this] = value;
        }

        [DisplayName("Indent Lines"), Expression("jIndent.[Lines]")]
        public Int32? IndentLines
        {
            get => fields.IndentLines[this];
            set => fields.IndentLines[this] = value;
        }
        [DisplayName("Indent"), NotNull, ForeignKey("[dbo].[Indent]", "Id"), LeftJoin("jIndent"), TextualField("IndentAdditionalInfo")]
        [LookupEditor(typeof(InvoiceRow), InplaceAdd = true)]
        public Int32? IndentId
        {
            get { return Fields.IndentId[this]; }
            set { Fields.IndentId[this] = value; }
        }
        [DisplayName("Products"), MasterDetailRelation(foreignKey: "IndentId", IncludeColumns = "ProductsName"), NotMapped]
        public List<IndentProductsRow> Products { get { return Fields.Products[this]; } set { Fields.Products[this] = value; } }
        /*   IIdField IIdRow.IdField
           {
               get { return Fields.Id; }
           }

           StringField INameRow.NameField
           {
               get { return Fields.Description; }
           }*/
        public IndentProductsRow()
            : base()
        {
        }

        public IndentProductsRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProductsId;
            //public Int32Field IndentId;
            public DoubleField Quantity;
            public StringField Description;

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

            public Int32Field IndentContactsId;
            public DateTimeField IndentDate;
            public Int32Field IndentStatus;
            public StringField IndentAdditionalInfo;
            public StringField IndentInvoiceNo;
            public Int32Field IndentBranchId;
            public Int32Field IndentOwnerId;
            public Int32Field IndentAssignedId;
            public StringField IndentSubject;
            public StringField IndentReference;
            public Int32Field IndentContactPersonId;
            public Int32Field IndentLines;

            public Int32Field IndentId;


            public readonly RowListField<IndentProductsRow> Products;            
        }
    }
}
