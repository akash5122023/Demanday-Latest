namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[ProductModules]")]
    [DisplayName("Product Module"), InstanceName("Product Module")]
    [ReadPermission("Settings:ProductPlans")]
    [ModifyPermission("Settings:ProductPlans")]
    [LookupScript]
    public sealed class ProductModuleRow : Row<ProductModuleRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => Fields.Id[this];
            set => Fields.Id[this] = value;
        }

        [DisplayName("Name"), Size(100), NotNull]
        public String Name
        {
            get => Fields.Name[this];
            set => Fields.Name[this] = value;
        }

        [DisplayName("Display Name"), Size(150), NotNull, QuickSearch, NameProperty]
        public String DisplayName
        {
            get => Fields.DisplayName[this];
            set => Fields.DisplayName[this] = value;
        }

        [DisplayName("Description"), Size(500)]
        public String Description
        {
            get => Fields.Description[this];
            set => Fields.Description[this] = value;
        }

        [DisplayName("Is Active"), NotNull]
        public Boolean? IsActive
        {
            get => Fields.IsActive[this];
            set => Fields.IsActive[this] = value;
        }

        [DisplayName("Sort Order")]
        public Int32? SortOrder
        {
            get => Fields.SortOrder[this];
            set => Fields.SortOrder[this] = value;
        }

        [DisplayName("Price"), Scale(2)]
        public Decimal? Price
        {
            get => Fields.Price[this];
            set => Fields.Price[this] = value;
        }

        [DisplayName("Currency"), Size(16)]
        public String Currency
        {
            get => Fields.Currency[this];
            set => Fields.Currency[this] = value;
        }

        [DisplayName("Created On"), ReadOnly(true), SortOrder(1, descending: true)]
        public DateTime? CreatedOn
        {
            get => Fields.CreatedOn[this];
            set => Fields.CreatedOn[this] = value;
        }

        [DisplayName("Created By"), Size(50), ReadOnly(true)]
        public String CreatedBy
        {
            get => Fields.CreatedBy[this];
            set => Fields.CreatedBy[this] = value;
        }

        [DisplayName("Modified On"), ReadOnly(true)]
        public DateTime? ModifiedOn
        {
            get => Fields.ModifiedOn[this];
            set => Fields.ModifiedOn[this] = value;
        }

        [DisplayName("Modified By"), Size(50), ReadOnly(true)]
        public String ModifiedBy
        {
            get => Fields.ModifiedBy[this];
            set => Fields.ModifiedBy[this] = value;
        }

        public ProductModuleRow()
            : base(Fields)
        {
        }

        public ProductModuleRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField DisplayName;
            public StringField Description;
            public BooleanField IsActive;
            public Int32Field SortOrder;
            public DecimalField Price;
            public StringField Currency;
            public DateTimeField CreatedOn;
            public StringField CreatedBy;
            public DateTimeField ModifiedOn;
            public StringField ModifiedBy;
        }
    }
}
