namespace AdvanceCRM.Administration
{
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;

    [ConnectionKey("Default"), Module("Administration"), TableName("[dbo].[Tenants]")]
    [DisplayName("Tenant"), InstanceName("Tenant")]
    [ReadPermission(PermissionKeys.Security)]
    [ModifyPermission(PermissionKeys.Security)]
    [LookupScript("Administration.Tenant", Permission = "?")]
    public sealed class TenantRow : Row<TenantRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Tenant Id"), Identity, IdProperty]
        public Int32? TenantId
        {
            get { return Fields.TenantId[this]; }
            set { Fields.TenantId[this] = value; }
        }

        [DisplayName("Name"), Size(200), NotNull, QuickSearch, NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Subdomain"), Size(200)]
        public String Subdomain
        {
            get { return Fields.Subdomain[this]; }
            set { Fields.Subdomain[this] = value; }
        }

        [DisplayName("Db Name"), Size(200)]
        public String DbName
        {
            get { return Fields.DbName[this]; }
            set { Fields.DbName[this] = value; }
        }

        [DisplayName("Port")]
        public Int32? Port
        {
            get { return Fields.Port[this]; }
            set { Fields.Port[this] = value; }
        }

        [DisplayName("Plan"), Size(100)]
        public String Plan
        {
            get { return Fields.Plan[this]; }
            set { Fields.Plan[this] = value; }
        }

        [DisplayName("Modules"), Size(1000)]
        public String Modules
        {
            get { return Fields.Modules[this]; }
            set { Fields.Modules[this] = value; }
        }

        [DisplayName("License Start Date"), NotNull]
        public DateTime? LicenseStartDate
        {
            get { return Fields.LicenseStartDate[this]; }
            set { Fields.LicenseStartDate[this] = value; }
        }

        [DisplayName("License End Date"), NotNull]
        public DateTime? LicenseEndDate
        {
            get { return Fields.LicenseEndDate[this]; }
            set { Fields.LicenseEndDate[this] = value; }
        }

        [DisplayName("DNS Status"), Size(2000)]
        public String DnsStatus
        {
            get { return Fields.DnsStatus[this]; }
            set { Fields.DnsStatus[this] = value; }
        }

        public TenantRow()
            : base(Fields)
        {
        }

        public TenantRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field TenantId;
            public StringField Name;
            public StringField Subdomain;
            public StringField DbName;
            public Int32Field Port;
            public StringField Plan;
            public StringField Modules;
            public DateTimeField LicenseStartDate;
            public DateTimeField LicenseEndDate;
            public StringField DnsStatus;

            public RowFields()
            {
                LocalTextPrefix = "Administration.Tenant";
            }
        }
    }
}
