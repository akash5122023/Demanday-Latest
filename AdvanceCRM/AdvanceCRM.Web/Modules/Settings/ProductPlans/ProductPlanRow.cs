namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[ProductPlans]")]
    [DisplayName("Product Plan"), InstanceName("Product Plan")]
    [ReadPermission("Settings:ProductPlans")]
    [ModifyPermission("Settings:ProductPlans")]
    [LookupScript]
    public sealed class ProductPlanRow : Row<ProductPlanRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => Fields.Id[this];
            set => Fields.Id[this] = value;
        }

        [DisplayName("Name"), Size(100), NotNull, QuickSearch, NameProperty]
        public String Name
        {
            get => Fields.Name[this];
            set => Fields.Name[this] = value;
        }

        [DisplayName("Price Per User"), Scale(2), NotNull]
        public Decimal? PricePerUser
        {
            get => Fields.PricePerUser[this];
            set => Fields.PricePerUser[this] = value;
        }

        [DisplayName("Trial Days"), NotNull]
        public Int32? TrialDays
        {
            get => Fields.TrialDays[this];
            set => Fields.TrialDays[this] = value;
        }

        [DisplayName("User Limit"), NotNull]
        public Int32? UserLimit
        {
            get => Fields.UserLimit[this];
            set => Fields.UserLimit[this] = value;
        }

        [DisplayName("Non Operational Users"), NotNull, DefaultValue(0)]
        public Int32? NonOperationalUsers
        {
            get => Fields.NonOperationalUsers[this];
            set => Fields.NonOperationalUsers[this] = value;
        }

        [DisplayName("Currency"), Size(10), NotNull]
        public String Currency
        {
            get => Fields.Currency[this];
            set => Fields.Currency[this] = value;
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

        [DisplayName("Badge Label")]
        [DefaultValue(PlanBadgeLabel.None)]
        public PlanBadgeLabel? BadgeLabel
        {
            get => Fields.BadgeLabel[this];
            set => Fields.BadgeLabel[this] = value;
        }

        [DisplayName("Highlight Badge")]
        [DefaultValue(false)]
        public Boolean? BadgeHighlight
        {
            get => Fields.BadgeHighlight[this];
            set => Fields.BadgeHighlight[this] = value;
        }

        [DisplayName("Modules"), NotMapped]
        [LookupEditor(typeof(ProductModuleRow), Multiple = true, FilterField = "IsActive", FilterValue = "1")]
        [LinkingSetRelation(typeof(ProductPlanModuleRow), "PlanId", "ModuleId")]
        public List<Int32> ModuleList
        {
            get => Fields.ModuleList[this];
            set => Fields.ModuleList[this] = value;
        }

        [DisplayName("Module Names"), NotMapped]
        public List<String> ModuleNames
        {
            get => Fields.ModuleNames[this];
            set => Fields.ModuleNames[this] = value;
        }

        [DisplayName("Default Features"), NotMapped]
        [LookupEditor(typeof(DefaultFeatureRow), Multiple = true)]
        [LinkingSetRelation(typeof(ProductPlanDefaultFeatureRow), "PlanId", "FeatureId")]
        public List<Int32> FeatureList
        {
            get => Fields.FeatureList[this];
            set => Fields.FeatureList[this] = value;
        }

        [DisplayName("Feature Names"), NotMapped]
        public List<String> FeatureNames
        {
            get => Fields.FeatureNames[this];
            set => Fields.FeatureNames[this] = value;
        }

        [DisplayName("Created On"), ReadOnly(true)]
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

        public ProductPlanRow()
            : base(Fields)
        {
        }

        public ProductPlanRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public DecimalField PricePerUser;
            public Int32Field TrialDays;
            public Int32Field UserLimit;
            public Int32Field NonOperationalUsers;
            public StringField Currency;
            public BooleanField IsActive;
            public Int32Field SortOrder;
            public EnumField<PlanBadgeLabel> BadgeLabel;
            public BooleanField BadgeHighlight;
            public ListField<Int32> ModuleList;
            public ListField<String> ModuleNames;
            public ListField<Int32> FeatureList;
            public ListField<String> FeatureNames;
            public DateTimeField CreatedOn;
            public StringField CreatedBy;
            public DateTimeField ModifiedOn;
            public StringField ModifiedBy;
        }
    }
}
