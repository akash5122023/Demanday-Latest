
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Project]")]
    [DisplayName("Project"), InstanceName("Project")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Project", Permission = "?")]
    public sealed class ProjectRow : Row<ProjectRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Project"), Size(200), NotNull, QuickSearch,NameProperty]
        public String Project
        {
            get { return Fields.Project[this]; }
            set { Fields.Project[this] = value; }
        }

        

        public ProjectRow()
            : base(Fields)
        {
        }public ProjectRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Project;
        }
    }
}
