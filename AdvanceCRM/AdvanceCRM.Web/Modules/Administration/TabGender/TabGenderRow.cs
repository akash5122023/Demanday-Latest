using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Administration
{
    [ConnectionKey("Default"), Module("Administration"), TableName("[dbo].[tabGender]")]
    [DisplayName("Gender"), InstanceName("Gender")]
    [ReadPermission("Administration:General")]
    [ModifyPermission("Administration:General")]
    [LookupScript]
    public sealed class TabGenderRow : Row<TabGenderRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Name"), Column("name"), Size(140), QuickSearch]
        public String Name
        {
            get => fields.Name[this];
            set => fields.Name[this] = value;
        }

        [DisplayName("Creation"), Column("creation")]
        public DateTime? Creation
        {
            get => fields.Creation[this];
            set => fields.Creation[this] = value;
        }

        [DisplayName("Modified"), Column("modified")]
        public DateTime? Modified
        {
            get => fields.Modified[this];
            set => fields.Modified[this] = value;
        }

        [DisplayName("Modified By"), Column("modified_by"), Size(140)]
        public String ModifiedBy
        {
            get => fields.ModifiedBy[this];
            set => fields.ModifiedBy[this] = value;
        }

        [DisplayName("Owner"), Column("owner"), Size(140)]
        public String Owner
        {
            get => fields.Owner[this];
            set => fields.Owner[this] = value;
        }

        [DisplayName("Docstatus"), Column("docstatus")]
        public Int32? Docstatus
        {
            get => fields.Docstatus[this];
            set => fields.Docstatus[this] = value;
        }

        [DisplayName("Parent"), Column("parent"), Size(140)]
        public String Parent
        {
            get => fields.Parent[this];
            set => fields.Parent[this] = value;
        }

        [DisplayName("Parentfield"), Column("parentfield"), Size(140)]
        public String Parentfield
        {
            get => fields.Parentfield[this];
            set => fields.Parentfield[this] = value;
        }

        [DisplayName("Parenttype"), Column("parenttype"), Size(140)]
        public String Parenttype
        {
            get => fields.Parenttype[this];
            set => fields.Parenttype[this] = value;
        }

        [DisplayName("Idx"), Column("idx")]
        public Int32? Idx
        {
            get => fields.Idx[this];
            set => fields.Idx[this] = value;
        }

        [DisplayName("Gender"), Column("gender"), Size(140), NameProperty]
        public String Gender
        {
            get => fields.Gender[this];
            set => fields.Gender[this] = value;
        }

        

        

        public TabGenderRow()
            : base()
        {
        }

        public TabGenderRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public DateTimeField Creation;
            public DateTimeField Modified;
            public StringField ModifiedBy;
            public StringField Owner;
            public Int32Field Docstatus;
            public StringField Parent;
            public StringField Parentfield;
            public StringField Parenttype;
            public Int32Field Idx;
            public StringField Gender;
          
            
        }
    }
}
