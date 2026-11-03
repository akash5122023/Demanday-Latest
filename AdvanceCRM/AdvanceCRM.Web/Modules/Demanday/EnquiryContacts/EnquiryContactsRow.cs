using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Demanday
{
    [ConnectionKey("Default"), Module("Demanday"), TableName("[dbo].[EnquiryContacts]")]
    [DisplayName("Enquiry Contacts"), InstanceName("Enquiry Contacts")]
    [ReadPermission("EnquiryContacts:Read")]
    [InsertPermission("EnquiryContacts:Insert")]
    [UpdatePermission("EnquiryContacts:Update")]
    [DeletePermission("EnquiryContacts:Delete")]
    [LookupScript("Demanday.EnquiryContacts", Permission = "?")]
    public sealed class EnquiryContactsRow : Row<EnquiryContactsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Company Name"), Size(200), QuickSearch, NameProperty]
        public String CompanyName
        {
            get => fields.CompanyName[this];
            set => fields.CompanyName[this] = value;
        }

        [DisplayName("First Name"), Size(100)]
        public String FirstName
        {
            get => fields.FirstName[this];
            set => fields.FirstName[this] = value;
        }

        [DisplayName("Last Name"), Size(100)]
        public String LastName
        {
            get => fields.LastName[this];
            set => fields.LastName[this] = value;
        }

        [DisplayName("Title"), Size(150)]
        public String Title
        {
            get => fields.Title[this];
            set => fields.Title[this] = value;
        }

        [DisplayName("Email"), Size(200)]
        public String Email
        {
            get => fields.Email[this];
            set => fields.Email[this] = value;
        }

        [DisplayName("Direct Phone Number"), Size(50)]
        public String WorkPhone
        {
            get => fields.WorkPhone[this];
            set => fields.WorkPhone[this] = value;
        }

        [DisplayName("Business Phone Number"), Size(50)]
        public String AlternativeNumber
        {
            get => fields.AlternativeNumber[this];
            set => fields.AlternativeNumber[this] = value;
        }

        [DisplayName("Street"), Size(200)]
        public String Street
        {
            get => fields.Street[this];
            set => fields.Street[this] = value;
        }

        [DisplayName("City"), Size(100)]
        public String City
        {
            get => fields.City[this];
            set => fields.City[this] = value;
        }

        [DisplayName("State"), Size(100)]
        public String State
        {
            get => fields.State[this];
            set => fields.State[this] = value;
        }

        [DisplayName("Zip Code"), Size(20)]
        public String ZipCode
        {
            get => fields.ZipCode[this];
            set => fields.ZipCode[this] = value;
        }

        [DisplayName("Country"), Size(100)]
        public String Country
        {
            get => fields.Country[this];
            set => fields.Country[this] = value;
        }

        [DisplayName("Industry"), Size(100)]
        public String Industry
        {
            get => fields.Industry[this];
            set => fields.Industry[this] = value;
        }

        [DisplayName("Sub Industry"), Size(100)]
        public String SubIndustry
        {
            get => fields.SubIndustry[this];
            set => fields.SubIndustry[this] = value;
        }

        [DisplayName("Revenue"), Size(50)]
        public String Revenue
        {
            get => fields.Revenue[this];
            set => fields.Revenue[this] = value;
        }

        [DisplayName("Employee Size"), Size(100)]
        public String CompanyEmployeeSize
        {
            get => fields.CompanyEmployeeSize[this];
            set => fields.CompanyEmployeeSize[this] = value;
        }

        [DisplayName("Profile Link"), Size(2000)]
        public String ProfileLink
        {
            get => fields.ProfileLink[this];
            set => fields.ProfileLink[this] = value;
        }

        [DisplayName("Company Link"), Size(2000)]
        public String CompanyLink
        {
            get => fields.CompanyLink[this];
            set => fields.CompanyLink[this] = value;
        }

        [DisplayName("Revenue Link"), Size(2000)]
        public String RevenueLink
        {
            get => fields.RevenueLink[this];
            set => fields.RevenueLink[this] = value;
        }

        [DisplayName("Email Format"), Size(100)]
        public String EmailFormat
        {
            get => fields.EmailFormat[this];
            set => fields.EmailFormat[this] = value;
        }

        [DisplayName("Adress Link"), Size(2000)]
        public String AdressLink
        {
            get => fields.AdressLink[this];
            set => fields.AdressLink[this] = value;
        }

        [DisplayName("Tenurity"), Size(100)]
        public String Tenurity
        {
            get => fields.Tenurity[this];
            set => fields.Tenurity[this] = value;
        }

        [DisplayName("Code"), Size(100)]
        public String Code
        {
            get => fields.Code[this];
            set => fields.Code[this] = value;
        }

        [DisplayName("Link"), Size(2000)]
        public String Link
        {
            get => fields.Link[this];
            set => fields.Link[this] = value;
        }

        [DisplayName("Md5"), Size(64)]
        public String Md5
        {
            get => fields.Md5[this];
            set => fields.Md5[this] = value;
        }

        [DisplayName("Account Type"), ForeignKey("[dbo].[ContactAccountTypeMaster]", "Id"), LeftJoin("jAccountType"), TextualField("AccountTypeName"), LookupInclude]
        [LookupEditor(typeof(Masters.ContactAccountTypeMasterRow))]
        public Int32? AccountTypeId
        {
            get => fields.AccountTypeId[this];
            set => fields.AccountTypeId[this] = value;
        }

        [DisplayName("Account Type"), Expression("jAccountType.[Name]")]
        public String AccountTypeName
        {
            get => fields.AccountTypeName[this];
            set => fields.AccountTypeName[this] = value;
        }

        [DisplayName("Status"), ForeignKey("[dbo].[ContactAccountStatusMaster]", "Id"), LeftJoin("jAccountStatus"), TextualField("AccountStatusName"), LookupInclude]
        [LookupEditor(typeof(Masters.ContactAccountStatusMasterRow))]
        public Int32? AccountStatusId
        {
            get => fields.AccountStatusId[this];
            set => fields.AccountStatusId[this] = value;
        }

        [DisplayName("Status"), Expression("jAccountStatus.[Name]")]
        public String AccountStatusName
        {
            get => fields.AccountStatusName[this];
            set => fields.AccountStatusName[this] = value;
        }

        [DisplayName("Disposition"), ForeignKey("[dbo].[ContactDispositionMaster]", "Id"), LeftJoin("jDisposition"), TextualField("DispositionName"), LookupInclude]
        [LookupEditor(typeof(Masters.ContactDispositionMasterRow))]
        public Int32? DispositionId
        {
            get => fields.DispositionId[this];
            set => fields.DispositionId[this] = value;
        }

        [DisplayName("Disposition"), Expression("jDisposition.[Name]")]
        public String DispositionName
        {
            get => fields.DispositionName[this];
            set => fields.DispositionName[this] = value;
        }

        [DisplayName("Mode of Outreach"), ForeignKey("[dbo].[ContactOutreachModeMaster]", "Id"), LeftJoin("jOutreachMode"), TextualField("OutreachModeName"), LookupInclude]
        [LookupEditor(typeof(Masters.ContactOutreachModeMasterRow))]
        public Int32? OutreachModeId
        {
            get => fields.OutreachModeId[this];
            set => fields.OutreachModeId[this] = value;
        }

        [DisplayName("Mode of Outreach"), Expression("jOutreachMode.[Name]")]
        public String OutreachModeName
        {
            get => fields.OutreachModeName[this];
            set => fields.OutreachModeName[this] = value;
        }

        [DisplayName("Meeting Mode"), ForeignKey("[dbo].[ContactMeetingModeMaster]", "Id"), LeftJoin("jMeetingMode"), TextualField("MeetingModeName"), LookupInclude]
        [LookupEditor(typeof(Masters.ContactMeetingModeMasterRow))]
        public Int32? MeetingModeId
        {
            get => fields.MeetingModeId[this];
            set => fields.MeetingModeId[this] = value;
        }

        [DisplayName("Meeting Mode"), Expression("jMeetingMode.[Name]")]
        public String MeetingModeName
        {
            get => fields.MeetingModeName[this];
            set => fields.MeetingModeName[this] = value;
        }

        [DisplayName("Status (Comments)"), Size(2000)]
        public String MeetingStatus
        {
            get => fields.MeetingStatus[this];
            set => fields.MeetingStatus[this] = value;
        }

        [DisplayName("Follow-up Date")]
        public DateTime? FollowUpDate
        {
            get => fields.FollowUpDate[this];
            set => fields.FollowUpDate[this] = value;
        }

        [DisplayName("Comments"), Size(2000)]
        public String Comments
        {
            get => fields.Comments[this];
            set => fields.Comments[this] = value;
        }

        [DisplayName("Date of Meeting")]
        public DateTime? L1Meeting
        {
            get => fields.L1Meeting[this];
            set => fields.L1Meeting[this] = value;
        }

        [DisplayName("Master Account"), ForeignKey("[dbo].[DemandayMasterAccount]", "Id"), LeftJoin("jMasterAccount"), LookupInclude]
        [LookupEditor(typeof(Masters.DemandayMasterAccountRow))]
        public Int32? MasterAccountId
        {
            get => fields.MasterAccountId[this];
            set => fields.MasterAccountId[this] = value;
        }

        // The readable Account Number behind MasterAccountId, joined in so exports and the
        // Excel import template can carry the account itself rather than its internal id.
        [DisplayName("Master Account No"), Expression("jMasterAccount.[AccountNumber]")]
        public String MasterAccountNo
        {
            get => fields.MasterAccountNo[this];
            set => fields.MasterAccountNo[this] = value;
        }

        [DisplayName("Campaign Id"), Size(15)]
        public String CampaignId
        {
            get => fields.CampaignId[this];
            set => fields.CampaignId[this] = value;
        }

        [DisplayName("Created By"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername"), ReadOnly(true)]
        [Administration.UserEditor]
        public Int32? OwnerId
        {
            get { return Fields.OwnerId[this]; }
            set { Fields.OwnerId[this] = value; }
        }
        [DisplayName("Owner Username"), Expression("jOwner.[DisplayName]")]
        public String OwnerUsername
        {
            get => fields.OwnerUsername[this];
            set => fields.OwnerUsername[this] = value;
        }

        [DisplayName("Owner Display Name"), Expression("jOwner.[DisplayName]")]
        public String OwnerDisplayName
        {
            get => fields.OwnerDisplayName[this];
            set => fields.OwnerDisplayName[this] = value;
        }

        [DisplayName("Owner Email"), Expression("jOwner.[Email]")]
        public String OwnerEmail
        {
            get => fields.OwnerEmail[this];
            set => fields.OwnerEmail[this] = value;
        }

        [DisplayName("Owner Upper Level"), Expression("jOwner.[UpperLevel]")]
        public Int32? OwnerUpperLevel
        {
            get => fields.OwnerUpperLevel[this];
            set => fields.OwnerUpperLevel[this] = value;
        }

        [DisplayName("Owner Upper Level2"), Expression("jOwner.[UpperLevel2]")]
        public Int32? OwnerUpperLevel2
        {
            get => fields.OwnerUpperLevel2[this];
            set => fields.OwnerUpperLevel2[this] = value;
        }

        [DisplayName("Owner Upper Level3"), Expression("jOwner.[UpperLevel3]")]
        public Int32? OwnerUpperLevel3
        {
            get => fields.OwnerUpperLevel3[this];
            set => fields.OwnerUpperLevel3[this] = value;
        }

        [DisplayName("Owner Upper Level4"), Expression("jOwner.[UpperLevel4]")]
        public Int32? OwnerUpperLevel4
        {
            get => fields.OwnerUpperLevel4[this];
            set => fields.OwnerUpperLevel4[this] = value;
        }

        [DisplayName("Owner Upper Level5"), Expression("jOwner.[UpperLevel5]")]
        public Int32? OwnerUpperLevel5
        {
            get => fields.OwnerUpperLevel5[this];
            set => fields.OwnerUpperLevel5[this] = value;
        }

        [DisplayName("Owner Host"), Expression("jOwner.[Host]")]
        public String OwnerHost
        {
            get => fields.OwnerHost[this];
            set => fields.OwnerHost[this] = value;
        }

        [DisplayName("Owner Port"), Expression("jOwner.[Port]")]
        public Int32? OwnerPort
        {
            get => fields.OwnerPort[this];
            set => fields.OwnerPort[this] = value;
        }

        [DisplayName("Owner Ssl"), Expression("jOwner.[SSL]")]
        public Boolean? OwnerSsl
        {
            get => fields.OwnerSsl[this];
            set => fields.OwnerSsl[this] = value;
        }

        [DisplayName("Owner Email Id"), Expression("jOwner.[EmailId]")]
        public String OwnerEmailId
        {
            get => fields.OwnerEmailId[this];
            set => fields.OwnerEmailId[this] = value;
        }

        [DisplayName("Owner Email Password"), Expression("jOwner.[EmailPassword]")]
        public String OwnerEmailPassword
        {
            get => fields.OwnerEmailPassword[this];
            set => fields.OwnerEmailPassword[this] = value;
        }

        [DisplayName("Owner Phone"), Expression("jOwner.[Phone]")]
        public String OwnerPhone
        {
            get => fields.OwnerPhone[this];
            set => fields.OwnerPhone[this] = value;
        }

        [DisplayName("Owner Mcsmtp Server"), Expression("jOwner.[MCSMTPServer]")]
        public String OwnerMcsmtpServer
        {
            get => fields.OwnerMcsmtpServer[this];
            set => fields.OwnerMcsmtpServer[this] = value;
        }

        [DisplayName("Owner Mcsmtp Port"), Expression("jOwner.[MCSMTPPort]")]
        public Int32? OwnerMcsmtpPort
        {
            get => fields.OwnerMcsmtpPort[this];
            set => fields.OwnerMcsmtpPort[this] = value;
        }

        [DisplayName("Owner Mcimap Server"), Expression("jOwner.[MCIMAPServer]")]
        public String OwnerMcimapServer
        {
            get => fields.OwnerMcimapServer[this];
            set => fields.OwnerMcimapServer[this] = value;
        }

        [DisplayName("Owner Mcimap Port"), Expression("jOwner.[MCIMAPPort]")]
        public Int32? OwnerMcimapPort
        {
            get => fields.OwnerMcimapPort[this];
            set => fields.OwnerMcimapPort[this] = value;
        }

        [DisplayName("Owner Mc Username"), Expression("jOwner.[MCUsername]")]
        public String OwnerMcUsername
        {
            get => fields.OwnerMcUsername[this];
            set => fields.OwnerMcUsername[this] = value;
        }

        [DisplayName("Owner Mc Password"), Expression("jOwner.[MCPassword]")]
        public String OwnerMcPassword
        {
            get => fields.OwnerMcPassword[this];
            set => fields.OwnerMcPassword[this] = value;
        }

        [DisplayName("Owner Start Time"), Expression("jOwner.[StartTime]")]
        public String OwnerStartTime
        {
            get => fields.OwnerStartTime[this];
            set => fields.OwnerStartTime[this] = value;
        }

        [DisplayName("Owner End Time"), Expression("jOwner.[EndTime]")]
        public String OwnerEndTime
        {
            get => fields.OwnerEndTime[this];
            set => fields.OwnerEndTime[this] = value;
        }

        [DisplayName("Owner Uid"), Expression("jOwner.[UID]")]
        public String OwnerUid
        {
            get => fields.OwnerUid[this];
            set => fields.OwnerUid[this] = value;
        }

        [DisplayName("Owner Non Operational"), Expression("jOwner.[NonOperational]")]
        public Boolean? OwnerNonOperational
        {
            get => fields.OwnerNonOperational[this];
            set => fields.OwnerNonOperational[this] = value;
        }

        [DisplayName("Owner Branch Id"), Expression("jOwner.[BranchId]")]
        public Int32? OwnerBranchId
        {
            get => fields.OwnerBranchId[this];
            set => fields.OwnerBranchId[this] = value;
        }

        [DisplayName("Owner Company Id"), Expression("jOwner.[CompanyId]")]
        public Int32? OwnerCompanyId
        {
            get => fields.OwnerCompanyId[this];
            set => fields.OwnerCompanyId[this] = value;
        }

        [DisplayName("Owner Enquiry"), Expression("jOwner.[Enquiry]")]
        public Boolean? OwnerEnquiry
        {
            get => fields.OwnerEnquiry[this];
            set => fields.OwnerEnquiry[this] = value;
        }

        [DisplayName("Owner Quotation"), Expression("jOwner.[Quotation]")]
        public Boolean? OwnerQuotation
        {
            get => fields.OwnerQuotation[this];
            set => fields.OwnerQuotation[this] = value;
        }

        [DisplayName("Owner Tasks"), Expression("jOwner.[Tasks]")]
        public Boolean? OwnerTasks
        {
            get => fields.OwnerTasks[this];
            set => fields.OwnerTasks[this] = value;
        }

        [DisplayName("Owner Contacts"), Expression("jOwner.[Contacts]")]
        public Boolean? OwnerContacts
        {
            get => fields.OwnerContacts[this];
            set => fields.OwnerContacts[this] = value;
        }

        [DisplayName("Owner Purchase"), Expression("jOwner.[Purchase]")]
        public Boolean? OwnerPurchase
        {
            get => fields.OwnerPurchase[this];
            set => fields.OwnerPurchase[this] = value;
        }

        [DisplayName("Owner Sales"), Expression("jOwner.[Sales]")]
        public Boolean? OwnerSales
        {
            get => fields.OwnerSales[this];
            set => fields.OwnerSales[this] = value;
        }

        [DisplayName("Owner Cms"), Expression("jOwner.[CMS]")]
        public Boolean? OwnerCms
        {
            get => fields.OwnerCms[this];
            set => fields.OwnerCms[this] = value;
        }

        [DisplayName("Owner Location"), Expression("jOwner.[Location]")]
        public String OwnerLocation
        {
            get => fields.OwnerLocation[this];
            set => fields.OwnerLocation[this] = value;
        }

        [DisplayName("Owner Coordinates"), Expression("jOwner.[Coordinates]")]
        public String OwnerCoordinates
        {
            get => fields.OwnerCoordinates[this];
            set => fields.OwnerCoordinates[this] = value;
        }

        [DisplayName("Owner Teams Id"), Expression("jOwner.[TeamsId]")]
        public Int32? OwnerTeamsId
        {
            get => fields.OwnerTeamsId[this];
            set => fields.OwnerTeamsId[this] = value;
        }

        [DisplayName("Owner Tenant Id"), Expression("jOwner.[TenantId]")]
        public Int32? OwnerTenantId
        {
            get => fields.OwnerTenantId[this];
            set => fields.OwnerTenantId[this] = value;
        }

        [DisplayName("Owner Url"), Expression("jOwner.[Url]")]
        public String OwnerUrl
        {
            get => fields.OwnerUrl[this];
            set => fields.OwnerUrl[this] = value;
        }

        [DisplayName("Owner Plan"), Expression("jOwner.[Plan]")]
        public String OwnerPlan
        {
            get => fields.OwnerPlan[this];
            set => fields.OwnerPlan[this] = value;
        }

        public EnquiryContactsRow()
            : base()
        {
        }

        public EnquiryContactsRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField CompanyName;
            public StringField FirstName;
            public StringField LastName;
            public StringField Title;
            public StringField Email;
            public StringField WorkPhone;
            public StringField AlternativeNumber;
            public StringField Street;
            public StringField City;
            public StringField State;
            public StringField ZipCode;
            public StringField Country;
            public StringField Industry;
            public StringField SubIndustry;
            public StringField Revenue;
            public StringField CompanyEmployeeSize;
            public StringField ProfileLink;
            public StringField CompanyLink;
            public StringField RevenueLink;
            public StringField EmailFormat;
            public StringField AdressLink;
            public StringField Tenurity;
            public StringField Code;
            public StringField Link;
            public StringField Md5;
            public Int32Field OwnerId;

            public StringField OwnerUsername;
            public StringField OwnerDisplayName;
            public StringField OwnerEmail;
            public Int32Field OwnerUpperLevel;
            public Int32Field OwnerUpperLevel2;
            public Int32Field OwnerUpperLevel3;
            public Int32Field OwnerUpperLevel4;
            public Int32Field OwnerUpperLevel5;
            public StringField OwnerHost;
            public Int32Field OwnerPort;
            public BooleanField OwnerSsl;
            public StringField OwnerEmailId;
            public StringField OwnerEmailPassword;
            public StringField OwnerPhone;
            public StringField OwnerMcsmtpServer;
            public Int32Field OwnerMcsmtpPort;
            public StringField OwnerMcimapServer;
            public Int32Field OwnerMcimapPort;
            public StringField OwnerMcUsername;
            public StringField OwnerMcPassword;
            public StringField OwnerStartTime;
            public StringField OwnerEndTime;
            public StringField OwnerUid;
            public BooleanField OwnerNonOperational;
            public Int32Field OwnerBranchId;
            public Int32Field OwnerCompanyId;
            public BooleanField OwnerEnquiry;
            public BooleanField OwnerQuotation;
            public BooleanField OwnerTasks;
            public BooleanField OwnerContacts;
            public BooleanField OwnerPurchase;
            public BooleanField OwnerSales;
            public BooleanField OwnerCms;
            public StringField OwnerLocation;
            public StringField OwnerCoordinates;
            public Int32Field OwnerTeamsId;
            public Int32Field OwnerTenantId;
            public StringField OwnerUrl;
            public StringField OwnerPlan;
            public Int32Field MasterAccountId;
            public StringField MasterAccountNo;
            public StringField CampaignId;
            public Int32Field AccountTypeId;
            public StringField AccountTypeName;
            public Int32Field AccountStatusId;
            public StringField AccountStatusName;
            public Int32Field DispositionId;
            public StringField DispositionName;
            public StringField Comments;
            public DateTimeField L1Meeting;
            public Int32Field OutreachModeId;
            public StringField OutreachModeName;
            public Int32Field MeetingModeId;
            public StringField MeetingModeName;
            public StringField MeetingStatus;
            public DateTimeField FollowUpDate;
        }
    }
}
