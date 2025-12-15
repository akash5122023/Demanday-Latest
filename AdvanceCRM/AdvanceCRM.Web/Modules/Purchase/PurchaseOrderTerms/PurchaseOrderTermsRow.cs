
namespace AdvanceCRM.PurchaseOrder
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("PurchaseOrder"), TableName("[dbo].[PurchaseOrderTerms]")]
    [DisplayName("Purchase Order Terms"), InstanceName("Purchase Order Terms")]
    [ReadPermission("Administration:Read")]
    [ModifyPermission("Administration:Read")]
    public sealed class PurchaseOrderTermsRow : Row<PurchaseOrderTermsRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Terms"), NotNull, ForeignKey("[dbo].[QuotationTermsMaster]", "Id"), LeftJoin("jTerms"), TextualField("Terms")]
        public Int32? TermsId
        {
            get { return Fields.TermsId[this]; }
            set { Fields.TermsId[this] = value; }
        }

        [DisplayName("Purchase Order"), NotNull, ForeignKey("[dbo].[PurchaseOrder]", "Id"), LeftJoin("jPurchaseOrder"), TextualField("PurchaseOrderDescription")]
        public Int32? PurchaseOrderId
        {
            get { return Fields.PurchaseOrderId[this]; }
            set { Fields.PurchaseOrderId[this] = value; }
        }

        [DisplayName("Terms"), Expression("jTerms.[Terms]")]
        public String Terms
        {
            get { return Fields.Terms[this]; }
            set { Fields.Terms[this] = value; }
        }

        [DisplayName("Purchase Order Contacts Id"), Expression("jPurchaseOrder.[ContactsId]")]
        public Int32? PurchaseOrderContactsId
        {
            get { return Fields.PurchaseOrderContactsId[this]; }
            set { Fields.PurchaseOrderContactsId[this] = value; }
        }

        [DisplayName("Purchase Order Date"), Expression("jPurchaseOrder.[Date]")]
        public DateTime? PurchaseOrderDate
        {
            get { return Fields.PurchaseOrderDate[this]; }
            set { Fields.PurchaseOrderDate[this] = value; }
        }

        [DisplayName("Purchase Order Status"), Expression("jPurchaseOrder.[Status]")]
        public Int32? PurchaseOrderStatus
        {
            get { return Fields.PurchaseOrderStatus[this]; }
            set { Fields.PurchaseOrderStatus[this] = value; }
        }

        [DisplayName("Purchase Order Total"), Expression("jPurchaseOrder.[Total]")]
        public Double? PurchaseOrderTotal
        {
            get { return Fields.PurchaseOrderTotal[this]; }
            set { Fields.PurchaseOrderTotal[this] = value; }
        }

        [DisplayName("Purchase Order Description"), Expression("jPurchaseOrder.[Description]")]
        public String PurchaseOrderDescription
        {
            get { return Fields.PurchaseOrderDescription[this]; }
            set { Fields.PurchaseOrderDescription[this] = value; }
        }

        [DisplayName("Purchase Order Additional Info"), Expression("jPurchaseOrder.[AdditionalInfo]")]
        public String PurchaseOrderAdditionalInfo
        {
            get { return Fields.PurchaseOrderAdditionalInfo[this]; }
            set { Fields.PurchaseOrderAdditionalInfo[this] = value; }
        }

        [DisplayName("Purchase Order Source Id"), Expression("jPurchaseOrder.[SourceId]")]
        public Int32? PurchaseOrderSourceId
        {
            get { return Fields.PurchaseOrderSourceId[this]; }
            set { Fields.PurchaseOrderSourceId[this] = value; }
        }

        [DisplayName("Purchase Order Branch Id"), Expression("jPurchaseOrder.[BranchId]")]
        public Int32? PurchaseOrderBranchId
        {
            get { return Fields.PurchaseOrderBranchId[this]; }
            set { Fields.PurchaseOrderBranchId[this] = value; }
        }

        [DisplayName("Purchase Order Terms"), Expression("jPurchaseOrder.[Terms]")]
        public String PurchaseOrderTerms
        {
            get { return Fields.PurchaseOrderTerms[this]; }
            set { Fields.PurchaseOrderTerms[this] = value; }
        }

        [DisplayName("Purchase Order Owner Id"), Expression("jPurchaseOrder.[OwnerId]")]
        public Int32? PurchaseOrderOwnerId
        {
            get { return Fields.PurchaseOrderOwnerId[this]; }
            set { Fields.PurchaseOrderOwnerId[this] = value; }
        }

        [DisplayName("Purchase Order Assigned Id"), Expression("jPurchaseOrder.[AssignedId]")]
        public Int32? PurchaseOrderAssignedId
        {
            get { return Fields.PurchaseOrderAssignedId[this]; }
            set { Fields.PurchaseOrderAssignedId[this] = value; }
        }

        [DisplayName("Purchase Order Sms Template"), Expression("jPurchaseOrder.[SMSTemplate]")]
        public String PurchaseOrderSmsTemplate
        {
            get { return Fields.PurchaseOrderSmsTemplate[this]; }
            set { Fields.PurchaseOrderSmsTemplate[this] = value; }
        }

        [DisplayName("Purchase Order Attachments"), Expression("jPurchaseOrder.[Attachments]")]
        public String PurchaseOrderAttachments
        {
            get { return Fields.PurchaseOrderAttachments[this]; }
            set { Fields.PurchaseOrderAttachments[this] = value; }
        }

        [DisplayName("Purchase Order Roundup"), Expression("jPurchaseOrder.[Roundup]")]
        public Double? PurchaseOrderRoundup
        {
            get { return Fields.PurchaseOrderRoundup[this]; }
            set { Fields.PurchaseOrderRoundup[this] = value; }
        }

        [DisplayName("Purchase Order Purchase Order No"), Expression("jPurchaseOrder.[PurchaseOrderNo]")]
        public Int32? PurchaseOrderPurchaseOrderNo
        {
            get { return Fields.PurchaseOrderPurchaseOrderNo[this]; }
            set { Fields.PurchaseOrderPurchaseOrderNo[this] = value; }
        }

        [DisplayName("Purchase Order Lines"), Expression("jPurchaseOrder.[Lines]")]
        public Int32? PurchaseOrderLines
        {
            get { return Fields.PurchaseOrderLines[this]; }
            set { Fields.PurchaseOrderLines[this] = value; }
        }

        [DisplayName("Purchase Order Conversion"), Expression("jPurchaseOrder.[Conversion]")]
        public Double? PurchaseOrderConversion
        {
            get { return Fields.PurchaseOrderConversion[this]; }
            set { Fields.PurchaseOrderConversion[this] = value; }
        }

        [DisplayName("Purchase Order Currency Conversion"), Expression("jPurchaseOrder.[CurrencyConversion]")]
        public Boolean? PurchaseOrderCurrencyConversion
        {
            get { return Fields.PurchaseOrderCurrencyConversion[this]; }
            set { Fields.PurchaseOrderCurrencyConversion[this] = value; }
        }

        [DisplayName("Purchase Order From Currency"), Expression("jPurchaseOrder.[FromCurrency]")]
        public Int32? PurchaseOrderFromCurrency
        {
            get { return Fields.PurchaseOrderFromCurrency[this]; }
            set { Fields.PurchaseOrderFromCurrency[this] = value; }
        }

        [DisplayName("Purchase Order To Currency"), Expression("jPurchaseOrder.[ToCurrency]")]
        public Int32? PurchaseOrderToCurrency
        {
            get { return Fields.PurchaseOrderToCurrency[this]; }
            set { Fields.PurchaseOrderToCurrency[this] = value; }
        }

        [DisplayName("Purchase Order Due Date"), Expression("jPurchaseOrder.[DueDate]")]
        public DateTime? PurchaseOrderDueDate
        {
            get { return Fields.PurchaseOrderDueDate[this]; }
            set { Fields.PurchaseOrderDueDate[this] = value; }
        }

        [DisplayName("Purchase Order Shipping Address"), Expression("jPurchaseOrder.[ShippingAddress]")]
        public String PurchaseOrderShippingAddress
        {
            get { return Fields.PurchaseOrderShippingAddress[this]; }
            set { Fields.PurchaseOrderShippingAddress[this] = value; }
        }

        

        public PurchaseOrderTermsRow()
            : base(Fields)
        {
        }
        
        public PurchaseOrderTermsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field TermsId;
            public Int32Field PurchaseOrderId;

            public StringField Terms;

            public Int32Field PurchaseOrderContactsId;
            public DateTimeField PurchaseOrderDate;
            public Int32Field PurchaseOrderStatus;
            public DoubleField PurchaseOrderTotal;
            public StringField PurchaseOrderDescription;
            public StringField PurchaseOrderAdditionalInfo;
            public Int32Field PurchaseOrderSourceId;
            public Int32Field PurchaseOrderBranchId;
            public StringField PurchaseOrderTerms;
            public Int32Field PurchaseOrderOwnerId;
            public Int32Field PurchaseOrderAssignedId;
            public StringField PurchaseOrderSmsTemplate;
            public StringField PurchaseOrderAttachments;
            public DoubleField PurchaseOrderRoundup;
            public Int32Field PurchaseOrderPurchaseOrderNo;
            public Int32Field PurchaseOrderLines;
            public DoubleField PurchaseOrderConversion;
            public BooleanField PurchaseOrderCurrencyConversion;
            public Int32Field PurchaseOrderFromCurrency;
            public Int32Field PurchaseOrderToCurrency;
            public DateTimeField PurchaseOrderDueDate;
            public StringField PurchaseOrderShippingAddress;
        }
    }
}
