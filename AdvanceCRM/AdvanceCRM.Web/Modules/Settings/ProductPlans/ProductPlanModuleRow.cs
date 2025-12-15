namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[ProductPlanModules]")]
    [DisplayName("Product Plan Module"), InstanceName("Product Plan Module")]
    [ReadPermission("Settings:ProductPlans")]
    [ModifyPermission("Settings:ProductPlans")]
    public sealed class ProductPlanModuleRow : Row<ProductPlanModuleRow.RowFields>, IIdRow
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

        [DisplayName("Module"), NotNull, ForeignKey("[dbo].[ProductModules]", "Id"), LeftJoin("jModule"), TextualField("ModuleDisplayName")]
        public Int32? ModuleId
        {
            get => Fields.ModuleId[this];
            set => Fields.ModuleId[this] = value;
        }

        [DisplayName("Plan"), Expression("jPlan.[Name]")]
        public String PlanName
        {
            get => Fields.PlanName[this];
            set => Fields.PlanName[this] = value;
        }

        [DisplayName("Module"), Expression("jModule.[DisplayName]")]
        public String ModuleDisplayName
        {
            get => Fields.ModuleDisplayName[this];
            set => Fields.ModuleDisplayName[this] = value;
        }

        [DisplayName("Module Name"), Expression("jModule.[Name]")]
        public String ModuleName
        {
            get => Fields.ModuleName[this];
            set => Fields.ModuleName[this] = value;
        }

        [DisplayName("Module Active"), Expression("jModule.[IsActive]")]
        public Boolean? ModuleIsActive
        {
            get => Fields.ModuleIsActive[this];
            set => Fields.ModuleIsActive[this] = value;
        }

        [DisplayName("Module Sort"), Expression("jModule.[SortOrder]")]
        public Int32? ModuleSortOrder
        {
            get => Fields.ModuleSortOrder[this];
            set => Fields.ModuleSortOrder[this] = value;
        }

        public ProductPlanModuleRow()
            : base(Fields)
        {
        }

        public ProductPlanModuleRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field PlanId;
            public Int32Field ModuleId;
            public StringField PlanName;
            public StringField ModuleDisplayName;
            public StringField ModuleName;
            public BooleanField ModuleIsActive;
            public Int32Field ModuleSortOrder;
        }
    }
}
