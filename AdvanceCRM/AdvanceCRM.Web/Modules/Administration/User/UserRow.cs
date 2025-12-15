
namespace AdvanceCRM.Administration
{
    using AdvanceCRM.Scripts;
    using Serenity.ComponentModel;
    using AdvanceCRM.Masters;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using _Ext;

    [ConnectionKey("Default"), DisplayName("Users"), InstanceName("User"), TwoLevelCached]
    [ReadPermission(PermissionKeys.Security)]
    [ModifyPermission(PermissionKeys.Security)]
    [LookupScript("Administration.User", Permission = "?", LookupType = typeof(MultiCompanyRowLookupScript<>))]
    public sealed class UserRow : LoggingRow<UserRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog, IMultiCompanyRow
    {
        [DisplayName("User Id"), Identity,IdProperty]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        #region NonOperational
        [DisplayName("Non Operational")]
        public Boolean? NonOperational { get { return Fields.NonOperational[this]; } set { Fields.NonOperational[this] = value; } }
        #endregion NonOperational

        [DisplayName("UID"), Size(500), QuickSearch]
        public String UID
        {
            get { return Fields.UID[this]; }
            set { Fields.UID[this] = value; }
        }

        [DisplayName("Teams"), ForeignKey("[dbo].[Teams]", "Id"), LeftJoin("jTeams"), TextualField("TeamsTeam")]
        [LookupEditor(typeof(TeamsRow), InplaceAdd = true)]
        public Int32? TeamsId
        {
            get { return Fields.TeamsId[this]; }
            set { Fields.TeamsId[this] = value; }
        }

        [DisplayName("Tenant"), ForeignKey("[dbo].[Tenants]", "TenantId"), LeftJoin("jTenant"), TextualField("TenantName"), LookupInclude]
        [LookupEditor(typeof(TenantRow))]
        public Int32? TenantId
        {
            get { return Fields.TenantId[this]; }
            set { Fields.TenantId[this] = value; }
        }

        [DisplayName("Url"), Size(300)]
        public String Url
        {
            get { return Fields.Url[this]; }
            set { Fields.Url[this] = value; }
        }

        [DisplayName("Plan"), Size(100)]
        public String Plan
        {
            get { return Fields.Plan[this]; }
            set { Fields.Plan[this] = value; }
        }

        [DisplayName("Username"), Size(100), NotNull, QuickSearch, LookupInclude]
        public String Username
        {
            get { return Fields.Username[this]; }
            set { Fields.Username[this] = value; }
        }

        [DisplayName("Source"), Size(4), NotNull, Insertable(false), Updatable(false), DefaultValue("site")]
        public String Source
        {
            get { return Fields.Source[this]; }
            set { Fields.Source[this] = value; }
        }

        [DisplayName("Password Hash"), Size(86), NotNull, Insertable(false), Updatable(false), MinSelectLevel(SelectLevel.Never)]
        public String PasswordHash
        {
            get { return Fields.PasswordHash[this]; }
            set { Fields.PasswordHash[this] = value; }
        }

        [DisplayName("Password Salt"), Size(10), NotNull, Insertable(false), Updatable(false), MinSelectLevel(SelectLevel.Never)]
        public String PasswordSalt
        {
            get { return Fields.PasswordSalt[this]; }
            set { Fields.PasswordSalt[this] = value; }
        }

        [DisplayName("Display Name"), Size(100), NotNull, LookupInclude,NameProperty]
        public String DisplayName
        {
            get { return Fields.DisplayName[this]; }
            set { Fields.DisplayName[this] = value; }
        }

        [DisplayName("Email"), Size(100)]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Phone"), Size(50), LookupInclude]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        #region Branch
        [DisplayName("Branch"), ForeignKey("[dbo].[Branch]", "Id"), LeftJoin("jBranch"), TextualField("Branch"), LookupInclude]
        [LookupEditor(typeof(BranchRow), CascadeFrom = "CompanyId", CascadeValue = "CompanyId")]
        public Int32? BranchId { get { return Fields.BranchId[this]; } set { Fields.BranchId[this] = value; } }
        #endregion BranchId

        [DisplayName("User Image"), Size(100)]
        [ImageUploadEditor(FilenameFormat = "UserImage/~", CopyToHistory = true)]
        public String UserImage
        {
            get { return Fields.UserImage[this]; }
            set { Fields.UserImage[this] = value; }
        }

        [DisplayName("Password"), Size(50), NotMapped]
        public String Password
        {
            get { return Fields.Password[this]; }
            set { Fields.Password[this] = value; }
        }

        [BooleanSwitchEditor]
        [DisplayName("Active"), NotNull, LookupInclude]
        public Boolean? IsActive
        {
            get { return Fields.IsActive[this]; }
            set { Fields.IsActive[this] = value; }
        }

        [DisplayName("Confirm Password"), Size(50), NotMapped]
        public String PasswordConfirm
        {
            get { return Fields.PasswordConfirm[this]; }
            set { Fields.PasswordConfirm[this] = value; }
        }

        [DisplayName("Last Directory Update"), Insertable(false), Updatable(false)]
        public DateTime? LastDirectoryUpdate
        {
            get { return Fields.LastDirectoryUpdate[this]; }
            set { Fields.LastDirectoryUpdate[this] = value; }
        }

        [Category("Mail Configuration")]
        [DisplayName("Host"), Size(200), Placeholder("example: smtp.gmail.com")]
        public String Host
        {
            get { return Fields.Host[this]; }
            set { Fields.Host[this] = value; }
        }

        [DisplayName("Port")]
        public Int32? Port
        {
            get { return Fields.Port[this]; }
            set { Fields.Port[this] = value; }
        }

        [DisplayName("SSL")]
        public Boolean? SSL
        {
            get { return Fields.SSL[this]; }
            set { Fields.SSL[this] = value; }
        }

        [DisplayName("EmailId/Username"), Size(200), Placeholder("example: mail@yourdomin.com")]
        public String EmailId
        {
            get { return Fields.EmailId[this]; }
            set { Fields.EmailId[this] = value; }
        }

        [DisplayName("Email Password"), Size(200), PasswordEditor]
        public String EmailPassword
        {
            get { return Fields.EmailPassword[this]; }
            set { Fields.EmailPassword[this] = value; }
        }


        [Category("Hierarchy")]
        #region UpperLevel
        [DisplayName("Level 1"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUL"), TextualField("UpperLevelName")]
        [HierarchyEditor(CascadeFrom = "CompanyId", CascadeValue = "CompanyId")]
        public Int32? UpperLevel { get { return Fields.UpperLevel[this]; } set { Fields.UpperLevel[this] = value; } }
        #endregion UpperLevel

        #region UpperLevel2
        [DisplayName("Level 2"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUL2"), TextualField("UpperLevelName2")]
        [HierarchyEditor(CascadeFrom = "CompanyId", CascadeValue = "CompanyId")]
        public Int32? UpperLevel2 { get { return Fields.UpperLevel2[this]; } set { Fields.UpperLevel2[this] = value; } }
        #endregion UpperLevel2

        #region UpperLevel3
        [DisplayName("Level 3"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUL3"), TextualField("UpperLevelName3")]
        [HierarchyEditor(CascadeFrom = "CompanyId", CascadeValue = "CompanyId")]
        public Int32? UpperLevel3 { get { return Fields.UpperLevel3[this]; } set { Fields.UpperLevel3[this] = value; } }
        #endregion UpperLevel3

        #region UpperLevel4
        [DisplayName("Level 4"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUL4"), TextualField("UpperLevelName4")]
        [HierarchyEditor(CascadeFrom = "CompanyId", CascadeValue = "CompanyId")]
        public Int32? UpperLevel4 { get { return Fields.UpperLevel4[this]; } set { Fields.UpperLevel4[this] = value; } }
        #endregion UpperLevel4

        #region UpperLevel5
        [DisplayName("Level 5"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUL5"), TextualField("UpperLevelName5")]
        [HierarchyEditor(CascadeFrom = "CompanyId", CascadeValue = "CompanyId")]
        public Int32? UpperLevel5 { get { return Fields.UpperLevel5[this]; } set { Fields.UpperLevel5[this] = value; } }
        #endregion UpperLevel5

        [Category("MailBox Configuration")]
        [DisplayName("SMTP Server"), Size(200), Placeholder("example: smtp.gmail.com")]
        public String MCSMTPServer
        {
            get { return Fields.MCSMTPServer[this]; }
            set { Fields.MCSMTPServer[this] = value; }
        }
        [DisplayName("Cms"), Column("CMS"), NotNull]
        public Boolean? Cms
        {
            get { return Fields.Cms[this]; }
            set { Fields.Cms[this] = value; }
        }

        [DisplayName("SMTP Port"), Placeholder("example: 587")]
        public Int32? MCSMTPPort
        {
            get { return Fields.MCSMTPPort[this]; }
            set { Fields.MCSMTPPort[this] = value; }
        }

        [DisplayName("IMAP Server"), Size(100), Placeholder("example: imap.gmail.com")]
        public String MCIMAPServer
        {
            get { return Fields.MCIMAPServer[this]; }
            set { Fields.MCIMAPServer[this] = value; }
        }

        [DisplayName("IMAP Port"), Placeholder("example: 993")]
        public Int32? MCIMAPPort
        {
            get { return Fields.MCIMAPPort[this]; }
            set { Fields.MCIMAPPort[this] = value; }
        }

        [DisplayName("Username/EmailID"), Size(100), Placeholder("example: name@gmail.com")]
        public String MCUsername
        {
            get { return Fields.MCUsername[this]; }
            set { Fields.MCUsername[this] = value; }
        }
        [DisplayName("Enquiry"), NotNull]
        public Boolean? Enquiry
        {
            get { return Fields.Enquiry[this]; }
            set { Fields.Enquiry[this] = value; }
        }

        [DisplayName("Quotation"), NotNull]
        public Boolean? Quotation
        {
            get { return Fields.Quotation[this]; }
            set { Fields.Quotation[this] = value; }
        }

        [DisplayName("Tasks"), NotNull]
        public Boolean? Tasks
        {
            get { return Fields.Tasks[this]; }
            set { Fields.Tasks[this] = value; }
        }

        [DisplayName("Contacts"), NotNull]
        public Boolean? Contacts
        {
            get { return Fields.Contacts[this]; }
            set { Fields.Contacts[this] = value; }
        }

        [DisplayName("Purchase"), NotNull]
        public Boolean? Purchase
        {
            get { return Fields.Purchase[this]; }
            set { Fields.Purchase[this] = value; }
        }

        [DisplayName("Sales"), NotNull]
        public Boolean? Sales
        {
            get { return Fields.Sales[this]; }
            set { Fields.Sales[this] = value; }
        }
        [DisplayName("Password"), Size(100)]
        public String MCPassword
        {
            get { return Fields.MCPassword[this]; }
            set { Fields.MCPassword[this] = value; }
        }

        //Forigen fields

        [DisplayName("Level 1"), Expression("jUL.[Username]")]
        public String UpperLevelName
        {
            get { return Fields.UpperLevelName[this]; }
            set { Fields.UpperLevelName[this] = value; }
        }


        [DisplayName("Location"), Size(250)]
        public String Location
        {
            get { return Fields.Location[this]; }
            set { Fields.Location[this] = value; }
        }

        [DisplayName("Coordinates"), Size(250)]
        public String Coordinates
        {
            get { return Fields.Coordinates[this]; }
            set { Fields.Coordinates[this] = value; }
        }

        [DisplayName("Level 2"), Expression("jUL2.[Username]")]
        public String UpperLevelName2
        {
            get { return Fields.UpperLevelName2[this]; }
            set { Fields.UpperLevelName2[this] = value; }
        }

        [DisplayName("Level 3"), Expression("jUL3.[Username]")]
        public String UpperLevelName3
        {
            get { return Fields.UpperLevelName3[this]; }
            set { Fields.UpperLevelName3[this] = value; }
        }

        [DisplayName("Level 4"), Expression("jUL4.[Username]")]
        public String UpperLevelName4
        {
            get { return Fields.UpperLevelName4[this]; }
            set { Fields.UpperLevelName4[this] = value; }
        }

        [DisplayName("Level 5"), Expression("jUL5.[Username]")]
        public String UpperLevelName5
        {
            get { return Fields.UpperLevelName5[this]; }
            set { Fields.UpperLevelName5[this] = value; }
        }

        [DisplayName("Teams Team"), Expression("jTeams.[Team]")]
        public String TeamsTeam
        {
            get { return Fields.TeamsTeam[this]; }
            set { Fields.TeamsTeam[this] = value; }
        }

        [DisplayName("Teams User Id"), Expression("jTeams.[UserId]")]
        public Int32? TeamsUserId
        {
            get { return Fields.TeamsUserId[this]; }
            set { Fields.TeamsUserId[this] = value; }
        }


        [Category("Time")]

        #region StartTime
        [DisplayName("Start Time(HH:mm)"), Placeholder("e.g: 10:30")]
        public String StartTime { get { return Fields.StartTime[this]; } set { Fields.StartTime[this] = value; } }
        #endregion StartTime

        #region EndTime
        [DisplayName("End Time(HH:mm)"), Placeholder("e.g: 18:30")]
        public String EndTime { get { return Fields.EndTime[this]; } set { Fields.EndTime[this] = value; } }
        #endregion EndTime

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        [LookupEditor(typeof(CompanyDetailsRow))]
        [UpdatePermission(PermissionKeys.Company)]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }
        public Int32Field CompanyIdField
        {
            get { return Fields.CompanyId; }
        }

        #region Foreign Fields
        [DisplayName("Branch"), Expression("jBranch.[Branch]"), LookupInclude]
        public String Branch { get { return Fields.Branch[this]; } set { Fields.Branch[this] = value; } }
        [DisplayName("Tenant"), Expression("jTenant.[Name]")]
        public String TenantName { get { return Fields.TenantName[this]; } set { Fields.TenantName[this] = value; } }
        #endregion Foreign Fields


        public UserRow()
            : base(Fields)
        {
        }
        
        public UserRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : LoggingRowFields
        {
            public Int32Field UserId;
            public BooleanField NonOperational;
            public StringField UID;
            public StringField Username;
            public StringField Source;
            public StringField PasswordHash;
            public StringField PasswordSalt;
            public StringField DisplayName;
            public StringField Email;
            public StringField Phone;
            public Int32Field BranchId;
            public StringField Branch;
            public StringField UserImage;
            public DateTimeField LastDirectoryUpdate;
            public BooleanField IsActive;
            public Int32Field TeamsId;
            public Int32Field TenantId;
            public StringField Url;
            public StringField Plan;

            public StringField Host;
            public Int32Field Port;
            public BooleanField SSL;
            public StringField EmailId;
            public StringField EmailPassword;

            public StringField Password;
            public StringField PasswordConfirm;
            public Int32Field UpperLevel;
            public StringField UpperLevelName;
            public Int32Field UpperLevel2;
            public StringField UpperLevelName2;
            public Int32Field UpperLevel3;
            public StringField UpperLevelName3;
            public Int32Field UpperLevel4;
            public StringField UpperLevelName4;
            public Int32Field UpperLevel5;
            public StringField UpperLevelName5;

            public StringField MCSMTPServer;
            public Int32Field MCSMTPPort;
            public StringField MCIMAPServer;
            public Int32Field MCIMAPPort;
            public StringField MCUsername;
            public StringField MCPassword;
            public StringField StartTime;
            public StringField EndTime;
            public Int32Field CompanyId;

            public StringField Location;
            public StringField Coordinates;

            public BooleanField Enquiry;
            public BooleanField Quotation;
            public BooleanField Tasks;
            public BooleanField Contacts;
            public BooleanField Purchase;
            public BooleanField Sales;
            public BooleanField Cms;

            public StringField TeamsTeam;
            public Int32Field TeamsUserId;
            public StringField TenantName;

            public RowFields()
                : base("Users")
            {
                LocalTextPrefix = "Administration.User";
            }
        }
    }
}