
namespace AdvanceCRM.Purchase
{
    using AdvanceCRM.Masters;
    using AdvanceCRM.Products;
    using AdvanceCRM.Scripts;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Purchase"), TableName("[dbo].[Products]")]
    [DisplayName("Reorder"), InstanceName("Reorder")]
    [ReadPermission("Rorder:Read")]
    [InsertPermission("Rorder:Insert")]
    [UpdatePermission("Rorder:Update")]
    [DeletePermission("Rorder:Delete")]
    // [LookupScript("Purchase.RorderRow", Permission = "?")]
    //[LookupScript("Products.Rorder", Permission = "?", LookupType = typeof(MultiCompanyRowLookupScript<>))]
    [LookupScript("Products.Rorder", Permission = "?")]
    //public sealed class RorderRow : Row, IIdRow, INameRow, _Ext.IAuditLog, IMultiCompanyRow
    public sealed class RorderRow : Row<RorderRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog, IMultiCompanyRow

    {
        [DisplayName("Id"), Identity, SortOrder(1, true)]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Name"), Size(200), NotNull, QuickSearch, LookupInclude, NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Code/Barcode"), Size(100), QuickSearch, LookupInclude]
        public String Code
        {
            get { return Fields.Code[this]; }
            set { Fields.Code[this] = value; }
        }

        [DisplayName("Division/Brand"), NotNull, ForeignKey("[dbo].[ProductsDivision]", "Id"), LeftJoin("jDivision"), TextualField("DivisionProductsDivision")]
        [LookupEditor(typeof(ProductsDivisionRow), InplaceAdd = true)]
        public Int32? DivisionId
        {
            get { return Fields.DivisionId[this]; }
            set { Fields.DivisionId[this] = value; }
        }


        [DisplayName("Group"), ForeignKey("[dbo].[ProductsGroup]", "Id"), LeftJoin("jGroup"), TextualField("GroupProductsGroup")]
        [LookupEditor(typeof(ProductsGroupRow), InplaceAdd = true)]
        public Int32? GroupId
        {
            get { return Fields.GroupId[this]; }
            set { Fields.GroupId[this] = value; }
        }

        [DisplayName("Bottom Price"), DisplayFormat("#,##0.####"), NotNull, LookupInclude, DecimalEditor(Decimals = 4)]
        public Double? SellingPrice
        {
            get { return Fields.SellingPrice[this]; }
            set { Fields.SellingPrice[this] = value; }
        }

        [DisplayName("MRP"), DisplayFormat("#,##0.####"), Column("MRP"), NotNull, LookupInclude, DecimalEditor(Decimals = 4)]
        public Double? Mrp
        {
            get { return Fields.Mrp[this]; }
            set { Fields.Mrp[this] = value; }
        }

        [DisplayName("Description"), Size(4000), NotNull, LookupInclude, TextAreaEditor(Rows = 4)]
        public String Description
        {
            get { return Fields.Description[this]; }
            set { Fields.Description[this] = value; }
        }

        [DisplayName("Tax1"), ForeignKey("[dbo].[Tax]", "Id"), LeftJoin("jTaxId1"), TextualField("TaxId1Name"), LookupInclude]
        [LookupEditor(typeof(TaxRow), InplaceAdd = true)]
        public Int32? TaxId1
        {
            get { return Fields.TaxId1[this]; }
            set { Fields.TaxId1[this] = value; }
        }

        [DisplayName("Tax2"), ForeignKey("[dbo].[Tax]", "Id"), LeftJoin("jTaxId2"), TextualField("TaxId2Name"), LookupInclude]
        [LookupEditor(typeof(TaxRow), InplaceAdd = true)]
        public Int32? TaxId2
        {
            get { return Fields.TaxId2[this]; }
            set { Fields.TaxId2[this] = value; }
        }

        [DisplayName("Image"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Product/~", CopyToHistory = true)]
        public String Image
        {
            get { return Fields.Image[this]; }
            set { Fields.Image[this] = value; }
        }

        [DisplayName("TechSpecs"), Size(2000), TextAreaEditor(Rows = 8)]
        public String TechSpecs
        {
            get { return Fields.TechSpecs[this]; }
            set { Fields.TechSpecs[this] = value; }
        }

        [DisplayName("HSN"), Size(100)]
        public String HSN
        {
            get { return Fields.HSN[this]; }
            set { Fields.HSN[this] = value; }
        }

        [DisplayName("Customer Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? ChannelCustomerPrice
        {
            get { return Fields.ChannelCustomerPrice[this]; }
            set { Fields.ChannelCustomerPrice[this] = value; }
        }

        [DisplayName("Reseller Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? ResellerPrice
        {
            get { return Fields.ResellerPrice[this]; }
            set { Fields.ResellerPrice[this] = value; }
        }

        [DisplayName("Wholesaler Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? WholesalerPrice
        {
            get { return Fields.WholesalerPrice[this]; }
            set { Fields.WholesalerPrice[this] = value; }
        }

        [DisplayName("Dealer Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? DealerPrice
        {
            get { return Fields.DealerPrice[this]; }
            set { Fields.DealerPrice[this] = value; }
        }

        [DisplayName("Distributor Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? DistributorPrice
        {
            get { return Fields.DistributorPrice[this]; }
            set { Fields.DistributorPrice[this] = value; }
        }

        [DisplayName("Stockiest Price"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? StockiestPrice
        {
            get { return Fields.StockiestPrice[this]; }
            set { Fields.StockiestPrice[this] = value; }
        }

        [DisplayName("National Distributor"), NotNull, LookupInclude, DefaultValue("0")]
        public Double? NationalDistributorPrice
        {
            get { return Fields.NationalDistributorPrice[this]; }
            set { Fields.NationalDistributorPrice[this] = value; }
        }

        [DisplayName("Minimum Stock"), NotNull, DefaultValue("0"), LookupInclude]
        public Double? MinimumStock
        {
            get { return Fields.MinimumStock[this]; }
            set { Fields.MinimumStock[this] = value; }
        }

        [DisplayName("Maximum Stock"), NotNull, DefaultValue("0"), LookupInclude]
        public Double? MaximumStock
        {
            get { return Fields.MaximumStock[this]; }
            set { Fields.MaximumStock[this] = value; }
        }

        [BSSwitchEditor(OnText = "Yes", OffText = "No")]
        [DisplayName("Raw Material"), LookupInclude]
        public Boolean? RawMaterial
        {
            get { return Fields.RawMaterial[this]; }
            set { Fields.RawMaterial[this] = value; }
        }

        [DisplayName("Purchase Price"), Column("PurchasePrice"), NotNull, DefaultValue("0"), LookupInclude, DecimalEditor(Decimals = 4)]
        public Double? PurchasePrice
        {
            get { return Fields.PurchasePrice[this]; }
            set { Fields.PurchasePrice[this] = value; }
        }

        [DisplayName("Opening Stock"), Column("OpeningStock"), NotNull, DefaultValue("0")]
        public Double? OpeningStock
        {
            get { return Fields.OpeningStock[this]; }
            set { Fields.OpeningStock[this] = value; }
        }

        [DisplayName("Unit"), ForeignKey("[dbo].[ProductsUnit]", "Id"), LeftJoin("jUnit"), TextualField("UnitProductsUnit"), LookupInclude]
        [LookupEditor(typeof(ProductsUnitRow), InplaceAdd = true)]
        public Int32? UnitId
        {
            get { return Fields.UnitId[this]; }
            set { Fields.UnitId[this] = value; }
        }

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        [Insertable(false), Updatable(false)]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }
        public Int32Field CompanyIdField
        {
            get { return Fields.CompanyId; }
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

        //[DisplayName("WareHouse"), NotNull, ForeignKey("[dbo].[WareHouse]", "Id"), LeftJoin("jWareHouse"), TextualField("WareHouseName")]
        //[LookupEditor(typeof(WareHouseRow), InplaceAdd = true)]
        //public Int32? WareHouseId
        //{
        //    get { return Fields.WareHouseId[this]; }
        //    set { Fields.WareHouseId[this] = value; }
        //}

        //[DisplayName("WareHouse"), Expression("jWareHouse.[WareHouseName]")]
        //public String WareHouseName
        //{
        //    get { return Fields.WareHouseName[this]; }
        //    set { Fields.WareHouseName[this] = value; }
        //}

        //[DisplayName("Products"), ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("jProductsName")]
        //[DisplayName("Products"), ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts")]
        //[LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        //public Int32? ProductsId
        //{
        //    get { return Fields.ProductsId[this]; }
        //    set { Fields.ProductsId[this] = value; }
        // }
        //[DisplayName("Products"), ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts")]
        //[LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        //public Int32? ProductsId
        //{
        //    get { return Fields.ProductsId[this]; }
        //    set { Fields.ProductsId[this] = value; }
        //}


        //[DisplayName("Products"), Expression("jProducts.[ProductsName]")]
        //public String ProductsName
        //{
        //    get { return Fields.ProductsName[this]; }
        //    set { Fields.ProductsName[this] = value; }
        //}
        //[DisplayName("Quantity"), DefaultValue("0"), LookupInclude]
        //public Double? Quantity
        //{
        //    get { return Fields.Quantity[this]; }
        //    set { Fields.Quantity[this] = value; }
        //}
        //IIdField IIdRow.IdField
        //{
        //    get { return Fields.Id; }
        //}

        //StringField INameRow.NameField
        //{
        //    get { return Fields.Name; }
        //}

        //public static readonly RowFields Fields = new RowFields().Init();

        //public RorderRow()
        //    : base(Fields)
        //{
        //}

        public RorderRow()
    : base(Fields)
        {
        }

        public RorderRow(RowFields fields)
            : base(Fields)
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
            public StringField HSN;
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
            public DoubleField OpeningStock;
            public Int32Field UnitId;
            public Int32Field CompanyId;

            public StringField DivisionProductsDivision;

            public StringField GroupProductsGroup;

            public StringField TaxId1Name;
            public StringField TaxId1Type;
            public DoubleField TaxId1Percentage;

            public StringField TaxId2Name;
            public StringField TaxId2Type;
            public DoubleField TaxId2Percentage;

            public StringField UnitProductsUnit;

            public StringField CodePlusName;

            //public Int32Field WareHouseId;

            //public StringField WareHouseName;

            //public StringField ProductsName;


            //public Int32Field ProductsId;
            //public DoubleField Quantity;
        }
    }
}
