namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[ProductPlanDefaultFeatures]")]
    [DisplayName("Product Plan Default Feature"), InstanceName("Product Plan Default Feature")]
    [ReadPermission("Settings:ProductPlans")]
    [ModifyPermission("Settings:ProductPlans")]
    public sealed class ProductPlanDefaultFeatureRow : Row<ProductPlanDefaultFeatureRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => Fields.Id[this];
            set => Fields.Id[this] = value;
        }

        [DisplayName("Plan"), NotNull, ForeignKey("[dbo].[ProductPlans]", "Id"), LeftJoin("jPlan"), TextualField("PlanName")]
        public Int32? PlanId
        {
            get => Fields.PlanId[this];
            set => Fields.PlanId[this] = value;
        }

        [DisplayName("Feature"), NotNull, ForeignKey("[dbo].[DefaultFeatures]", "Id"), LeftJoin("jFeature"), TextualField("FeatureName")]
        public Int32? FeatureId
        {
            get => Fields.FeatureId[this];
            set => Fields.FeatureId[this] = value;
        }

        [DisplayName("Plan"), Expression("jPlan.[Name]")]
        public String PlanName
        {
            get => Fields.PlanName[this];
            set => Fields.PlanName[this] = value;
        }

        [DisplayName("Feature"), Expression("jFeature.[Name]")]
        public String FeatureName
        {
            get => Fields.FeatureName[this];
            set => Fields.FeatureName[this] = value;
        }

        public ProductPlanDefaultFeatureRow()
            : base(Fields)
        {
        }

        public ProductPlanDefaultFeatureRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field PlanId;
            public Int32Field FeatureId;
            public StringField PlanName;
            public StringField FeatureName;
        }
    }
}
