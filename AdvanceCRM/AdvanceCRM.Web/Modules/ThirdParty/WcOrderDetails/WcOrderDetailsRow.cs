
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[WCOrderDetails]")]
    [DisplayName("Wc Order Details"), InstanceName("Wc Order Details")]
    [ReadPermission("Woocommerce:Inbox")]
    [ModifyPermission("Woocommerce:Inbox")]
    public sealed class WcOrderDetailsRow : Row<WcOrderDetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Wcoid"), Column("WCOID"), Size(255), QuickSearch,NameProperty]
        public String Wcoid
        {
            get { return Fields.Wcoid[this]; }
            set { Fields.Wcoid[this] = value; }
        }

        [DisplayName("Parent Id"), Column("ParentID"), Size(255)]
        public String ParentId
        {
            get { return Fields.ParentId[this]; }
            set { Fields.ParentId[this] = value; }
        }

        [DisplayName("Status"), Size(255)]
        public String Status
        {
            get { return Fields.Status[this]; }
            set { Fields.Status[this] = value; }
        }

        [DisplayName("Currency"), Size(255)]
        public String Currency
        {
            get { return Fields.Currency[this]; }
            set { Fields.Currency[this] = value; }
        }

        [DisplayName("Included Tax"), Size(255)]
        public String IncludedTax
        {
            get { return Fields.IncludedTax[this]; }
            set { Fields.IncludedTax[this] = value; }
        }

        [DisplayName("Date Created"), Size(255)]
        public String DateCreated
        {
            get { return Fields.DateCreated[this]; }
            set { Fields.DateCreated[this] = value; }
        }

        [DisplayName("Date Modified"), Size(255)]
        public String DateModified
        {
            get { return Fields.DateModified[this]; }
            set { Fields.DateModified[this] = value; }
        }

        [DisplayName("Discount Total"), Size(255)]
        public String DiscountTotal
        {
            get { return Fields.DiscountTotal[this]; }
            set { Fields.DiscountTotal[this] = value; }
        }

        [DisplayName("Discount Tax"), Size(255)]
        public String DiscountTax
        {
            get { return Fields.DiscountTax[this]; }
            set { Fields.DiscountTax[this] = value; }
        }

        [DisplayName("Shipping Total"), Size(255)]
        public String ShippingTotal
        {
            get { return Fields.ShippingTotal[this]; }
            set { Fields.ShippingTotal[this] = value; }
        }

        [DisplayName("Shippping Tax"), Size(255)]
        public String ShipppingTax
        {
            get { return Fields.ShipppingTax[this]; }
            set { Fields.ShipppingTax[this] = value; }
        }

        [DisplayName("Cart Tax"), Size(255)]
        public String CartTax
        {
            get { return Fields.CartTax[this]; }
            set { Fields.CartTax[this] = value; }
        }

        [DisplayName("Total"), Size(255)]
        public String Total
        {
            get { return Fields.Total[this]; }
            set { Fields.Total[this] = value; }
        }

        [DisplayName("Total Tax"), Size(255)]
        public String TotalTax
        {
            get { return Fields.TotalTax[this]; }
            set { Fields.TotalTax[this] = value; }
        }

        [DisplayName("Customer Id"), Column("CustomerID"), Size(255)]
        public String CustomerId
        {
            get { return Fields.CustomerId[this]; }
            set { Fields.CustomerId[this] = value; }
        }

        [DisplayName("Order Key"), Size(255)]
        public String OrderKey
        {
            get { return Fields.OrderKey[this]; }
            set { Fields.OrderKey[this] = value; }
        }

        [DisplayName("B First Name"), Size(255)]
        public String BFirstName
        {
            get { return Fields.BFirstName[this]; }
            set { Fields.BFirstName[this] = value; }
        }

        [DisplayName("B Last Name"), Size(255)]
        public String BLastName
        {
            get { return Fields.BLastName[this]; }
            set { Fields.BLastName[this] = value; }
        }

        [DisplayName("B Company"), Size(255)]
        public String BCompany
        {
            get { return Fields.BCompany[this]; }
            set { Fields.BCompany[this] = value; }
        }

        [DisplayName("B Email"), Size(50)]
        public String BEmail
        {
            get { return Fields.BEmail[this]; }
            set { Fields.BEmail[this] = value; }
        }

        [DisplayName("B Phone"), Size(20)]
        public String BPhone
        {
            get { return Fields.BPhone[this]; }
            set { Fields.BPhone[this] = value; }
        }

        [DisplayName("B Post Code"), Size(20)]
        public String BPostCode
        {
            get { return Fields.BPostCode[this]; }
            set { Fields.BPostCode[this] = value; }
        }

        [DisplayName("B Address1"), Size(500)]
        public String BAddress1
        {
            get { return Fields.BAddress1[this]; }
            set { Fields.BAddress1[this] = value; }
        }

        [DisplayName("B Address2"), Size(500)]
        public String BAddress2
        {
            get { return Fields.BAddress2[this]; }
            set { Fields.BAddress2[this] = value; }
        }

        [DisplayName("B City"), Size(255)]
        public String BCity
        {
            get { return Fields.BCity[this]; }
            set { Fields.BCity[this] = value; }
        }

        [DisplayName("B State"), Size(255)]
        public String BState
        {
            get { return Fields.BState[this]; }
            set { Fields.BState[this] = value; }
        }

        [DisplayName("B Country"), Size(255)]
        public String BCountry
        {
            get { return Fields.BCountry[this]; }
            set { Fields.BCountry[this] = value; }
        }

        [DisplayName("S First Name"), Size(255)]
        public String SFirstName
        {
            get { return Fields.SFirstName[this]; }
            set { Fields.SFirstName[this] = value; }
        }

        [DisplayName("S Last Name"), Size(255)]
        public String SLastName
        {
            get { return Fields.SLastName[this]; }
            set { Fields.SLastName[this] = value; }
        }

        [DisplayName("S Company"), Size(255)]
        public String SCompany
        {
            get { return Fields.SCompany[this]; }
            set { Fields.SCompany[this] = value; }
        }

        [DisplayName("S Phone"), Size(20)]
        public String SPhone
        {
            get { return Fields.SPhone[this]; }
            set { Fields.SPhone[this] = value; }
        }

        [DisplayName("S Post Code"), Size(20)]
        public String SPostCode
        {
            get { return Fields.SPostCode[this]; }
            set { Fields.SPostCode[this] = value; }
        }

        [DisplayName("S Address1"), Size(500)]
        public String SAddress1
        {
            get { return Fields.SAddress1[this]; }
            set { Fields.SAddress1[this] = value; }
        }

        [DisplayName("S Address2"), Size(500)]
        public String SAddress2
        {
            get { return Fields.SAddress2[this]; }
            set { Fields.SAddress2[this] = value; }
        }

        [DisplayName("S City"), Size(255)]
        public String SCity
        {
            get { return Fields.SCity[this]; }
            set { Fields.SCity[this] = value; }
        }

        [DisplayName("S State"), Size(255)]
        public String SState
        {
            get { return Fields.SState[this]; }
            set { Fields.SState[this] = value; }
        }

        [DisplayName("S Country"), Size(255)]
        public String SCountry
        {
            get { return Fields.SCountry[this]; }
            set { Fields.SCountry[this] = value; }
        }

        [DisplayName("Payment Method"), Size(255)]
        public String PaymentMethod
        {
            get { return Fields.PaymentMethod[this]; }
            set { Fields.PaymentMethod[this] = value; }
        }

        [DisplayName("Transaction Id"), Column("TransactionID"), Size(255)]
        public String TransactionId
        {
            get { return Fields.TransactionId[this]; }
            set { Fields.TransactionId[this] = value; }
        }

        [DisplayName("Customer Ip"), Column("CustomerIP"), Size(255)]
        public String CustomerIp
        {
            get { return Fields.CustomerIp[this]; }
            set { Fields.CustomerIp[this] = value; }
        }

        [DisplayName("Item Id"), Size(50)]
        public String ItemId
        {
            get { return Fields.ItemId[this]; }
            set { Fields.ItemId[this] = value; }
        }

        [DisplayName("Item Name"), Size(2000)]
        public String ItemName
        {
            get { return Fields.ItemName[this]; }
            set { Fields.ItemName[this] = value; }
        }

        [DisplayName("Product Id"), Size(20)]
        public String ProductId
        {
            get { return Fields.ProductId[this]; }
            set { Fields.ProductId[this] = value; }
        }

        [DisplayName("Variation Id"), Size(50)]
        public String VariationId
        {
            get { return Fields.VariationId[this]; }
            set { Fields.VariationId[this] = value; }
        }

        [DisplayName("Quantity"), Size(50)]
        public String Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("Tax Class"), Size(50)]
        public String TaxClass
        {
            get { return Fields.TaxClass[this]; }
            set { Fields.TaxClass[this] = value; }
        }

        [DisplayName("Sub Total"), Size(50)]
        public String SubTotal
        {
            get { return Fields.SubTotal[this]; }
            set { Fields.SubTotal[this] = value; }
        }

        [DisplayName("Sub Totaltax"), Size(50)]
        public String SubTotaltax
        {
            get { return Fields.SubTotaltax[this]; }
            set { Fields.SubTotaltax[this] = value; }
        }

        [DisplayName("Item Total"), Size(50)]
        public String ItemTotal
        {
            get { return Fields.ItemTotal[this]; }
            set { Fields.ItemTotal[this] = value; }
        }

        [DisplayName("Item Totaltax"), Size(50)]
        public String ItemTotaltax
        {
            get { return Fields.ItemTotaltax[this]; }
            set { Fields.ItemTotaltax[this] = value; }
        }

        [DisplayName("Tax Rate Code"), Size(50)]
        public String TaxRateCode
        {
            get { return Fields.TaxRateCode[this]; }
            set { Fields.TaxRateCode[this] = value; }
        }

        [DisplayName("Tax Rate Id"), Size(50)]
        public String TaxRateId
        {
            get { return Fields.TaxRateId[this]; }
            set { Fields.TaxRateId[this] = value; }
        }

        [DisplayName("Tax Label"), Size(50)]
        public String TaxLabel
        {
            get { return Fields.TaxLabel[this]; }
            set { Fields.TaxLabel[this] = value; }
        }

        [DisplayName("Tax Compound"), Size(50)]
        public String TaxCompound
        {
            get { return Fields.TaxCompound[this]; }
            set { Fields.TaxCompound[this] = value; }
        }

        [DisplayName("Tax Total"), Size(50)]
        public String TaxTotal
        {
            get { return Fields.TaxTotal[this]; }
            set { Fields.TaxTotal[this] = value; }
        }

        [DisplayName("Tax Shipping Total"), Size(50)]
        public String TaxShippingTotal
        {
            get { return Fields.TaxShippingTotal[this]; }
            set { Fields.TaxShippingTotal[this] = value; }
        }

        [DisplayName("Tax Rate Percent"), Size(50)]
        public String TaxRatePercent
        {
            get { return Fields.TaxRatePercent[this]; }
            set { Fields.TaxRatePercent[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

       

        public WcOrderDetailsRow()
            : base(Fields)
        {
        }
        

        public WcOrderDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Wcoid;
            public StringField ParentId;
            public StringField Status;
            public StringField Currency;
            public StringField IncludedTax;
            public StringField DateCreated;
            public StringField DateModified;
            public StringField DiscountTotal;
            public StringField DiscountTax;
            public StringField ShippingTotal;
            public StringField ShipppingTax;
            public StringField CartTax;
            public StringField Total;
            public StringField TotalTax;
            public StringField CustomerId;
            public StringField OrderKey;
            public StringField BFirstName;
            public StringField BLastName;
            public StringField BCompany;
            public StringField BEmail;
            public StringField BPhone;
            public StringField BPostCode;
            public StringField BAddress1;
            public StringField BAddress2;
            public StringField BCity;
            public StringField BState;
            public StringField BCountry;
            public StringField SFirstName;
            public StringField SLastName;
            public StringField SCompany;
            public StringField SPhone;
            public StringField SPostCode;
            public StringField SAddress1;
            public StringField SAddress2;
            public StringField SCity;
            public StringField SState;
            public StringField SCountry;
            public StringField PaymentMethod;
            public StringField TransactionId;
            public StringField CustomerIp;
            public StringField ItemId;
            public StringField ItemName;
            public StringField ProductId;
            public StringField VariationId;
            public StringField Quantity;
            public StringField TaxClass;
            public StringField SubTotal;
            public StringField SubTotaltax;
            public StringField ItemTotal;
            public StringField ItemTotaltax;
            public StringField TaxRateCode;
            public StringField TaxRateId;
            public StringField TaxLabel;
            public StringField TaxCompound;
            public StringField TaxTotal;
            public StringField TaxShippingTotal;
            public StringField TaxRatePercent;
            public BooleanField IsMoved;
        }
    }
}
