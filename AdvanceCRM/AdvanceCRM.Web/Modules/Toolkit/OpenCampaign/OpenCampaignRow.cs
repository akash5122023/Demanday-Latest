using AdvanceCRM.Masters;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Toolkit
{
    [ConnectionKey("Default"), Module("Toolkit"), TableName("[dbo].[OpenCampaign]")]
    [DisplayName("Open Campaign"), InstanceName("Open Campaign")]
    [ReadPermission("OpenCampaign:Read")]
    [InsertPermission("OpenCampaign:Insert")]
    [UpdatePermission("OpenCampaign:Update")]
    [DeletePermission("OpenCampaign:Delete")]
    [LookupScript("OpenCampaign.OpenCampaign", Permission = "OpenCampaign:Read")]
    public sealed class OpenCampaignRow : Row<OpenCampaignRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Master Account"), ForeignKey("[dbo].[DemandayMasterAccount]", "Id"), LeftJoin("jMasterAccount"), TextualField("MasterAccountAccountNumber")]
        [LookupEditor(typeof(DemandayMasterAccountRow), InplaceAdd = true)]
        public Int32? MasterAccountId
        {
            get => fields.MasterAccountId[this];
            set => fields.MasterAccountId[this] = value;
        }

        [DisplayName("Campaign"), ForeignKey("[dbo].[DemandayCampaignId]", "Id"), LeftJoin("jCampaign"), TextualField("CampaignCampaignId")]
        [LookupEditor(typeof(DemandayCampaignIdRow), InplaceAdd = true)]
        public Int32? CampaignId
        {
            get => fields.CampaignId[this];
            set => fields.CampaignId[this] = value;
        }

        [DisplayName("Domain"), Size(100)]
        public String Domain
        {
            get => fields.Domain[this];
            set => fields.Domain[this] = value;
        }

        [DisplayName("Demanday User"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jDemandayUser"), TextualField("DemandayUserUsername")]
        public Int32? DemandayUserId
        {
            get => fields.DemandayUserId[this];
            set => fields.DemandayUserId[this] = value;
        }

        [DisplayName("Time Stamp")]
        public DateTime? TimeStamp
        {
            get => fields.TimeStamp[this];
            set => fields.TimeStamp[this] = value;
        }

        //[DisplayName("Master Account"), ForeignKey("[dbo].[DemandayMasterAccount]", "Id"), LeftJoin("jMasterAccount"), TextualField("MasterAccountAccountNumber")]
        //public Int32? MasterAccountId
        //{
        //    get => fields.MasterAccountId[this];
        //    set => fields.MasterAccountId[this] = value;
        //}

        [DisplayName("Created By"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername"), ReadOnly(true)]
        [Administration.UserEditor]
        public Int32? OwnerId
        {
            get { return Fields.OwnerId[this]; }
            set { Fields.OwnerId[this] = value; }
        }

        [DisplayName("Demanday User Username"), Expression("jDemandayUser.[Username]")]
        public String DemandayUserUsername
        {
            get => fields.DemandayUserUsername[this];
            set => fields.DemandayUserUsername[this] = value;
        }

        [DisplayName("Demanday User Display Name"), Expression("jDemandayUser.[DisplayName]")]
        public String DemandayUserDisplayName
        {
            get => fields.DemandayUserDisplayName[this];
            set => fields.DemandayUserDisplayName[this] = value;
        }

        [DisplayName("Demanday User Email"), Expression("jDemandayUser.[Email]")]
        public String DemandayUserEmail
        {
            get => fields.DemandayUserEmail[this];
            set => fields.DemandayUserEmail[this] = value;
        }

        [DisplayName("Demanday User Upper Level"), Expression("jDemandayUser.[UpperLevel]")]
        public Int32? DemandayUserUpperLevel
        {
            get => fields.DemandayUserUpperLevel[this];
            set => fields.DemandayUserUpperLevel[this] = value;
        }

        [DisplayName("Demanday User Upper Level2"), Expression("jDemandayUser.[UpperLevel2]")]
        public Int32? DemandayUserUpperLevel2
        {
            get => fields.DemandayUserUpperLevel2[this];
            set => fields.DemandayUserUpperLevel2[this] = value;
        }

        [DisplayName("Demanday User Upper Level3"), Expression("jDemandayUser.[UpperLevel3]")]
        public Int32? DemandayUserUpperLevel3
        {
            get => fields.DemandayUserUpperLevel3[this];
            set => fields.DemandayUserUpperLevel3[this] = value;
        }

        [DisplayName("Demanday User Upper Level4"), Expression("jDemandayUser.[UpperLevel4]")]
        public Int32? DemandayUserUpperLevel4
        {
            get => fields.DemandayUserUpperLevel4[this];
            set => fields.DemandayUserUpperLevel4[this] = value;
        }

        [DisplayName("Demanday User Upper Level5"), Expression("jDemandayUser.[UpperLevel5]")]
        public Int32? DemandayUserUpperLevel5
        {
            get => fields.DemandayUserUpperLevel5[this];
            set => fields.DemandayUserUpperLevel5[this] = value;
        }

        [DisplayName("Demanday User Host"), Expression("jDemandayUser.[Host]")]
        public String DemandayUserHost
        {
            get => fields.DemandayUserHost[this];
            set => fields.DemandayUserHost[this] = value;
        }

        [DisplayName("Demanday User Port"), Expression("jDemandayUser.[Port]")]
        public Int32? DemandayUserPort
        {
            get => fields.DemandayUserPort[this];
            set => fields.DemandayUserPort[this] = value;
        }

        [DisplayName("Demanday User Ssl"), Expression("jDemandayUser.[SSL]")]
        public Boolean? DemandayUserSsl
        {
            get => fields.DemandayUserSsl[this];
            set => fields.DemandayUserSsl[this] = value;
        }

        [DisplayName("Demanday User Email Id"), Expression("jDemandayUser.[EmailId]")]
        public String DemandayUserEmailId
        {
            get => fields.DemandayUserEmailId[this];
            set => fields.DemandayUserEmailId[this] = value;
        }

        [DisplayName("Demanday User Email Password"), Expression("jDemandayUser.[EmailPassword]")]
        public String DemandayUserEmailPassword
        {
            get => fields.DemandayUserEmailPassword[this];
            set => fields.DemandayUserEmailPassword[this] = value;
        }

        [DisplayName("Demanday User Phone"), Expression("jDemandayUser.[Phone]")]
        public String DemandayUserPhone
        {
            get => fields.DemandayUserPhone[this];
            set => fields.DemandayUserPhone[this] = value;
        }

        [DisplayName("Demanday User Mcsmtp Server"), Expression("jDemandayUser.[MCSMTPServer]")]
        public String DemandayUserMcsmtpServer
        {
            get => fields.DemandayUserMcsmtpServer[this];
            set => fields.DemandayUserMcsmtpServer[this] = value;
        }

        [DisplayName("Demanday User Mcsmtp Port"), Expression("jDemandayUser.[MCSMTPPort]")]
        public Int32? DemandayUserMcsmtpPort
        {
            get => fields.DemandayUserMcsmtpPort[this];
            set => fields.DemandayUserMcsmtpPort[this] = value;
        }

        [DisplayName("Demanday User Mcimap Server"), Expression("jDemandayUser.[MCIMAPServer]")]
        public String DemandayUserMcimapServer
        {
            get => fields.DemandayUserMcimapServer[this];
            set => fields.DemandayUserMcimapServer[this] = value;
        }

        [DisplayName("Demanday User Mcimap Port"), Expression("jDemandayUser.[MCIMAPPort]")]
        public Int32? DemandayUserMcimapPort
        {
            get => fields.DemandayUserMcimapPort[this];
            set => fields.DemandayUserMcimapPort[this] = value;
        }

        [DisplayName("Demanday User Mc Username"), Expression("jDemandayUser.[MCUsername]")]
        public String DemandayUserMcUsername
        {
            get => fields.DemandayUserMcUsername[this];
            set => fields.DemandayUserMcUsername[this] = value;
        }

        [DisplayName("Demanday User Mc Password"), Expression("jDemandayUser.[MCPassword]")]
        public String DemandayUserMcPassword
        {
            get => fields.DemandayUserMcPassword[this];
            set => fields.DemandayUserMcPassword[this] = value;
        }

        [DisplayName("Demanday User Start Time"), Expression("jDemandayUser.[StartTime]")]
        public String DemandayUserStartTime
        {
            get => fields.DemandayUserStartTime[this];
            set => fields.DemandayUserStartTime[this] = value;
        }

        [DisplayName("Demanday User End Time"), Expression("jDemandayUser.[EndTime]")]
        public String DemandayUserEndTime
        {
            get => fields.DemandayUserEndTime[this];
            set => fields.DemandayUserEndTime[this] = value;
        }

        [DisplayName("Demanday User Uid"), Expression("jDemandayUser.[UID]")]
        public String DemandayUserUid
        {
            get => fields.DemandayUserUid[this];
            set => fields.DemandayUserUid[this] = value;
        }

        [DisplayName("Demanday User Non Operational"), Expression("jDemandayUser.[NonOperational]")]
        public Boolean? DemandayUserNonOperational
        {
            get => fields.DemandayUserNonOperational[this];
            set => fields.DemandayUserNonOperational[this] = value;
        }

        [DisplayName("Demanday User Branch Id"), Expression("jDemandayUser.[BranchId]")]
        public Int32? DemandayUserBranchId
        {
            get => fields.DemandayUserBranchId[this];
            set => fields.DemandayUserBranchId[this] = value;
        }

        [DisplayName("Demanday User Company Id"), Expression("jDemandayUser.[CompanyId]")]
        public Int32? DemandayUserCompanyId
        {
            get => fields.DemandayUserCompanyId[this];
            set => fields.DemandayUserCompanyId[this] = value;
        }

        [DisplayName("Demanday User Enquiry"), Expression("jDemandayUser.[Enquiry]")]
        public Boolean? DemandayUserEnquiry
        {
            get => fields.DemandayUserEnquiry[this];
            set => fields.DemandayUserEnquiry[this] = value;
        }

        [DisplayName("Demanday User Quotation"), Expression("jDemandayUser.[Quotation]")]
        public Boolean? DemandayUserQuotation
        {
            get => fields.DemandayUserQuotation[this];
            set => fields.DemandayUserQuotation[this] = value;
        }

        [DisplayName("Demanday User Tasks"), Expression("jDemandayUser.[Tasks]")]
        public Boolean? DemandayUserTasks
        {
            get => fields.DemandayUserTasks[this];
            set => fields.DemandayUserTasks[this] = value;
        }

        [DisplayName("Demanday User Contacts"), Expression("jDemandayUser.[Contacts]")]
        public Boolean? DemandayUserContacts
        {
            get => fields.DemandayUserContacts[this];
            set => fields.DemandayUserContacts[this] = value;
        }

        [DisplayName("Demanday User Purchase"), Expression("jDemandayUser.[Purchase]")]
        public Boolean? DemandayUserPurchase
        {
            get => fields.DemandayUserPurchase[this];
            set => fields.DemandayUserPurchase[this] = value;
        }

        [DisplayName("Demanday User Sales"), Expression("jDemandayUser.[Sales]")]
        public Boolean? DemandayUserSales
        {
            get => fields.DemandayUserSales[this];
            set => fields.DemandayUserSales[this] = value;
        }

        [DisplayName("Demanday User Cms"), Expression("jDemandayUser.[CMS]")]
        public Boolean? DemandayUserCms
        {
            get => fields.DemandayUserCms[this];
            set => fields.DemandayUserCms[this] = value;
        }

        [DisplayName("Demanday User Location"), Expression("jDemandayUser.[Location]")]
        public String DemandayUserLocation
        {
            get => fields.DemandayUserLocation[this];
            set => fields.DemandayUserLocation[this] = value;
        }

        [DisplayName("Demanday User Coordinates"), Expression("jDemandayUser.[Coordinates]")]
        public String DemandayUserCoordinates
        {
            get => fields.DemandayUserCoordinates[this];
            set => fields.DemandayUserCoordinates[this] = value;
        }

        [DisplayName("Demanday User Teams Id"), Expression("jDemandayUser.[TeamsId]")]
        public Int32? DemandayUserTeamsId
        {
            get => fields.DemandayUserTeamsId[this];
            set => fields.DemandayUserTeamsId[this] = value;
        }

        [DisplayName("Demanday User Tenant Id"), Expression("jDemandayUser.[TenantId]")]
        public Int32? DemandayUserTenantId
        {
            get => fields.DemandayUserTenantId[this];
            set => fields.DemandayUserTenantId[this] = value;
        }

        [DisplayName("Demanday User Url"), Expression("jDemandayUser.[Url]")]
        public String DemandayUserUrl
        {
            get => fields.DemandayUserUrl[this];
            set => fields.DemandayUserUrl[this] = value;
        }

        [DisplayName("Demanday User Plan"), Expression("jDemandayUser.[Plan]")]
        public String DemandayUserPlan
        {
            get => fields.DemandayUserPlan[this];
            set => fields.DemandayUserPlan[this] = value;
        }

        [DisplayName("Master Account Number"), Expression("jMasterAccount.[AccountNumber]"), QuickSearch, NameProperty]
        public String MasterAccountAccountNumber
        {
            get => fields.MasterAccountAccountNumber[this];
            set => fields.MasterAccountAccountNumber[this] = value;
        }

        [DisplayName("Campaign Id Value"), Expression("jCampaign.[CampaignId]")]
        public String CampaignIdValue
        {
            get => fields.CampaignIdValue[this];
            set => fields.CampaignIdValue[this] = value;
        }

        [DisplayName("Owner Username"), Expression("jOwner.[Username]")]
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

        public OpenCampaignRow()
            : base()
        {
        }

        public OpenCampaignRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field CampaignId;
            public StringField Domain;
            public Int32Field DemandayUserId;
            public DateTimeField TimeStamp;
            public Int32Field MasterAccountId;
            public Int32Field OwnerId;

            public StringField DemandayUserUsername;
            public StringField DemandayUserDisplayName;
            public StringField DemandayUserEmail;
            public Int32Field DemandayUserUpperLevel;
            public Int32Field DemandayUserUpperLevel2;
            public Int32Field DemandayUserUpperLevel3;
            public Int32Field DemandayUserUpperLevel4;
            public Int32Field DemandayUserUpperLevel5;
            public StringField DemandayUserHost;
            public Int32Field DemandayUserPort;
            public BooleanField DemandayUserSsl;
            public StringField DemandayUserEmailId;
            public StringField DemandayUserEmailPassword;
            public StringField DemandayUserPhone;
            public StringField DemandayUserMcsmtpServer;
            public Int32Field DemandayUserMcsmtpPort;
            public StringField DemandayUserMcimapServer;
            public Int32Field DemandayUserMcimapPort;
            public StringField DemandayUserMcUsername;
            public StringField DemandayUserMcPassword;
            public StringField DemandayUserStartTime;
            public StringField DemandayUserEndTime;
            public StringField DemandayUserUid;
            public BooleanField DemandayUserNonOperational;
            public Int32Field DemandayUserBranchId;
            public Int32Field DemandayUserCompanyId;
            public BooleanField DemandayUserEnquiry;
            public BooleanField DemandayUserQuotation;
            public BooleanField DemandayUserTasks;
            public BooleanField DemandayUserContacts;
            public BooleanField DemandayUserPurchase;
            public BooleanField DemandayUserSales;
            public BooleanField DemandayUserCms;
            public StringField DemandayUserLocation;
            public StringField DemandayUserCoordinates;
            public Int32Field DemandayUserTeamsId;
            public Int32Field DemandayUserTenantId;
            public StringField DemandayUserUrl;
            public StringField DemandayUserPlan;

            public StringField MasterAccountAccountNumber;
            public StringField CampaignIdValue;

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
        }
    }
}
