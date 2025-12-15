
namespace AdvanceCRM.Employee
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Employee"), TableName("[dbo].[EmployeeAssests]")]
    [DisplayName("Employee Assests"), InstanceName("Employee Assests")]
    [ReadPermission("?")]
    [ModifyPermission("Employee:Assests")]
    public sealed class EmployeeAssestsRow : Row<EmployeeAssestsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Items"), Size(200), NotNull, QuickSearch, NameProperty]
        public String Items
        {
            get { return Fields.Items[this]; }
            set { Fields.Items[this] = value; }
        }

        [DisplayName("Quantity"), NotNull]
        public Int32? Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("Description"), Size(2000), NotNull]
        public String Description
        {
            get { return Fields.Description[this]; }
            set { Fields.Description[this] = value; }
        }

        [DisplayName("Employee"), NotNull, ForeignKey("[dbo].[Employee]", "Id"), LeftJoin("jEmployee"), TextualField("EmployeeName")]
        public Int32? EmployeeId
        {
            get { return Fields.EmployeeId[this]; }
            set { Fields.EmployeeId[this] = value; }
        }

        //[DisplayName("Owner"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername")]
        //public Int32? OwnerId
        //{
        //    get { return Fields.OwnerId[this]; }
        //    set { Fields.OwnerId[this] = value; }
        //}

        [DisplayName("Employee Department Id"), Expression("jEmployee.[DepartmentId]")]
        public Int32? EmployeeDepartmentId
        {
            get { return Fields.EmployeeDepartmentId[this]; }
            set { Fields.EmployeeDepartmentId[this] = value; }
        }

        [DisplayName("Employee Name"), Expression("jEmployee.[Name]")]
        public String EmployeeName
        {
            get { return Fields.EmployeeName[this]; }
            set { Fields.EmployeeName[this] = value; }
        }

        [DisplayName("Employee Phone"), Expression("jEmployee.[Phone]")]
        public String EmployeePhone
        {
            get { return Fields.EmployeePhone[this]; }
            set { Fields.EmployeePhone[this] = value; }
        }

        [DisplayName("Employee Email"), Expression("jEmployee.[Email]")]
        public String EmployeeEmail
        {
            get { return Fields.EmployeeEmail[this]; }
            set { Fields.EmployeeEmail[this] = value; }
        }

        [DisplayName("Employee Address"), Expression("jEmployee.[Address]")]
        public String EmployeeAddress
        {
            get { return Fields.EmployeeAddress[this]; }
            set { Fields.EmployeeAddress[this] = value; }
        }

        [DisplayName("Employee Professional Email"), Expression("jEmployee.[ProfessionalEmail]")]
        public String EmployeeProfessionalEmail
        {
            get { return Fields.EmployeeProfessionalEmail[this]; }
            set { Fields.EmployeeProfessionalEmail[this] = value; }
        }

        [DisplayName("Employee City Id"), Expression("jEmployee.[CityId]")]
        public Int32? EmployeeCityId
        {
            get { return Fields.EmployeeCityId[this]; }
            set { Fields.EmployeeCityId[this] = value; }
        }

        [DisplayName("Employee State Id"), Expression("jEmployee.[StateId]")]
        public Int32? EmployeeStateId
        {
            get { return Fields.EmployeeStateId[this]; }
            set { Fields.EmployeeStateId[this] = value; }
        }

        [DisplayName("Employee Pin"), Expression("jEmployee.[Pin]")]
        public String EmployeePin
        {
            get { return Fields.EmployeePin[this]; }
            set { Fields.EmployeePin[this] = value; }
        }

        [DisplayName("Employee Country"), Expression("jEmployee.[Country]")]
        public Int32? EmployeeCountry
        {
            get { return Fields.EmployeeCountry[this]; }
            set { Fields.EmployeeCountry[this] = value; }
        }

        [DisplayName("Employee Additional Info"), Expression("jEmployee.[AdditionalInfo]")]
        public String EmployeeAdditionalInfo
        {
            get { return Fields.EmployeeAdditionalInfo[this]; }
            set { Fields.EmployeeAdditionalInfo[this] = value; }
        }

        [DisplayName("Employee Gender"), Expression("jEmployee.[Gender]")]
        public Int32? EmployeeGender
        {
            get { return Fields.EmployeeGender[this]; }
            set { Fields.EmployeeGender[this] = value; }
        }

        [DisplayName("Employee Religion"), Expression("jEmployee.[Religion]")]
        public Int32? EmployeeReligion
        {
            get { return Fields.EmployeeReligion[this]; }
            set { Fields.EmployeeReligion[this] = value; }
        }

        [DisplayName("Employee Area Id"), Expression("jEmployee.[AreaId]")]
        public Int32? EmployeeAreaId
        {
            get { return Fields.EmployeeAreaId[this]; }
            set { Fields.EmployeeAreaId[this] = value; }
        }

        [DisplayName("Employee Marital Status"), Expression("jEmployee.[MaritalStatus]")]
        public Int32? EmployeeMaritalStatus
        {
            get { return Fields.EmployeeMaritalStatus[this]; }
            set { Fields.EmployeeMaritalStatus[this] = value; }
        }

        [DisplayName("Employee Marriage Anniversary"), Expression("jEmployee.[MarriageAnniversary]")]
        public DateTime? EmployeeMarriageAnniversary
        {
            get { return Fields.EmployeeMarriageAnniversary[this]; }
            set { Fields.EmployeeMarriageAnniversary[this] = value; }
        }

        [DisplayName("Employee Birthdate"), Expression("jEmployee.[Birthdate]")]
        public DateTime? EmployeeBirthdate
        {
            get { return Fields.EmployeeBirthdate[this]; }
            set { Fields.EmployeeBirthdate[this] = value; }
        }

        [DisplayName("Employee Date Of Joining"), Expression("jEmployee.[DateOfJoining]")]
        public DateTime? EmployeeDateOfJoining
        {
            get { return Fields.EmployeeDateOfJoining[this]; }
            set { Fields.EmployeeDateOfJoining[this] = value; }
        }

        [DisplayName("Employee Company Id"), Expression("jEmployee.[CompanyId]")]
        public Int32? EmployeeCompanyId
        {
            get { return Fields.EmployeeCompanyId[this]; }
            set { Fields.EmployeeCompanyId[this] = value; }
        }

        [DisplayName("Employee Roles Id"), Expression("jEmployee.[RolesId]")]
        public Int32? EmployeeRolesId
        {
            get { return Fields.EmployeeRolesId[this]; }
            set { Fields.EmployeeRolesId[this] = value; }
        }

        [DisplayName("Employee Owner Id"), Expression("jEmployee.[OwnerId]")]
        public Int32? EmployeeOwnerId
        {
            get { return Fields.EmployeeOwnerId[this]; }
            set { Fields.EmployeeOwnerId[this] = value; }
        }

        [DisplayName("Employee Adhaar No"), Expression("jEmployee.[AdhaarNo]")]
        public String EmployeeAdhaarNo
        {
            get { return Fields.EmployeeAdhaarNo[this]; }
            set { Fields.EmployeeAdhaarNo[this] = value; }
        }

        [DisplayName("Employee Pan No"), Expression("jEmployee.[PANNo]")]
        public String EmployeePanNo
        {
            get { return Fields.EmployeePanNo[this]; }
            set { Fields.EmployeePanNo[this] = value; }
        }

        [DisplayName("Employee Attachment"), Expression("jEmployee.[Attachment]")]
        public String EmployeeAttachment
        {
            get { return Fields.EmployeeAttachment[this]; }
            set { Fields.EmployeeAttachment[this] = value; }
        }

        [DisplayName("Employee Bank Name"), Expression("jEmployee.[BankName]")]
        public String EmployeeBankName
        {
            get { return Fields.EmployeeBankName[this]; }
            set { Fields.EmployeeBankName[this] = value; }
        }

        [DisplayName("Employee Account Number"), Expression("jEmployee.[AccountNumber]")]
        public String EmployeeAccountNumber
        {
            get { return Fields.EmployeeAccountNumber[this]; }
            set { Fields.EmployeeAccountNumber[this] = value; }
        }

        [DisplayName("Employee Ifsc"), Expression("jEmployee.[IFSC]")]
        public String EmployeeIfsc
        {
            get { return Fields.EmployeeIfsc[this]; }
            set { Fields.EmployeeIfsc[this] = value; }
        }

        [DisplayName("Employee Bank Type"), Expression("jEmployee.[BankType]")]
        public String EmployeeBankType
        {
            get { return Fields.EmployeeBankType[this]; }
            set { Fields.EmployeeBankType[this] = value; }
        }

        [DisplayName("Employee Branch"), Expression("jEmployee.[Branch]")]
        public String EmployeeBranch
        {
            get { return Fields.EmployeeBranch[this]; }
            set { Fields.EmployeeBranch[this] = value; }
        }

        [DisplayName("Employee Tehsil Id"), Expression("jEmployee.[TehsilId]")]
        public Int32? EmployeeTehsilId
        {
            get { return Fields.EmployeeTehsilId[this]; }
            set { Fields.EmployeeTehsilId[this] = value; }
        }

        [DisplayName("Employee Village Id"), Expression("jEmployee.[VillageId]")]
        public Int32? EmployeeVillageId
        {
            get { return Fields.EmployeeVillageId[this]; }
            set { Fields.EmployeeVillageId[this] = value; }
        }

        [DisplayName("Employee Emp Code"), Expression("jEmployee.[EmpCode]")]
        public String EmployeeEmpCode
        {
            get { return Fields.EmployeeEmpCode[this]; }
            set { Fields.EmployeeEmpCode[this] = value; }
        }

        [DisplayName("Owner Username"), Expression("jOwner.[Username]")]
        public String OwnerUsername
        {
            get { return Fields.OwnerUsername[this]; }
            set { Fields.OwnerUsername[this] = value; }
        }

        [DisplayName("Owner Display Name"), Expression("jOwner.[DisplayName]")]
        public String OwnerDisplayName
        {
            get { return Fields.OwnerDisplayName[this]; }
            set { Fields.OwnerDisplayName[this] = value; }
        }

        [DisplayName("Owner Email"), Expression("jOwner.[Email]")]
        public String OwnerEmail
        {
            get { return Fields.OwnerEmail[this]; }
            set { Fields.OwnerEmail[this] = value; }
        }

        [DisplayName("Owner Source"), Expression("jOwner.[Source]")]
        public String OwnerSource
        {
            get { return Fields.OwnerSource[this]; }
            set { Fields.OwnerSource[this] = value; }
        }

        [DisplayName("Owner Password Hash"), Expression("jOwner.[PasswordHash]")]
        public String OwnerPasswordHash
        {
            get { return Fields.OwnerPasswordHash[this]; }
            set { Fields.OwnerPasswordHash[this] = value; }
        }

        [DisplayName("Owner Password Salt"), Expression("jOwner.[PasswordSalt]")]
        public String OwnerPasswordSalt
        {
            get { return Fields.OwnerPasswordSalt[this]; }
            set { Fields.OwnerPasswordSalt[this] = value; }
        }

        [DisplayName("Owner Last Directory Update"), Expression("jOwner.[LastDirectoryUpdate]")]
        public DateTime? OwnerLastDirectoryUpdate
        {
            get { return Fields.OwnerLastDirectoryUpdate[this]; }
            set { Fields.OwnerLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Owner User Image"), Expression("jOwner.[UserImage]")]
        public String OwnerUserImage
        {
            get { return Fields.OwnerUserImage[this]; }
            set { Fields.OwnerUserImage[this] = value; }
        }

        [DisplayName("Owner Insert Date"), Expression("jOwner.[InsertDate]")]
        public DateTime? OwnerInsertDate
        {
            get { return Fields.OwnerInsertDate[this]; }
            set { Fields.OwnerInsertDate[this] = value; }
        }

        [DisplayName("Owner Insert User Id"), Expression("jOwner.[InsertUserId]")]
        public Int32? OwnerInsertUserId
        {
            get { return Fields.OwnerInsertUserId[this]; }
            set { Fields.OwnerInsertUserId[this] = value; }
        }

        [DisplayName("Owner Update Date"), Expression("jOwner.[UpdateDate]")]
        public DateTime? OwnerUpdateDate
        {
            get { return Fields.OwnerUpdateDate[this]; }
            set { Fields.OwnerUpdateDate[this] = value; }
        }

        [DisplayName("Owner Update User Id"), Expression("jOwner.[UpdateUserId]")]
        public Int32? OwnerUpdateUserId
        {
            get { return Fields.OwnerUpdateUserId[this]; }
            set { Fields.OwnerUpdateUserId[this] = value; }
        }

        [DisplayName("Owner Is Active"), Expression("jOwner.[IsActive]")]
        public Int16? OwnerIsActive
        {
            get { return Fields.OwnerIsActive[this]; }
            set { Fields.OwnerIsActive[this] = value; }
        }

        [DisplayName("Owner Upper Level"), Expression("jOwner.[UpperLevel]")]
        public Int32? OwnerUpperLevel
        {
            get { return Fields.OwnerUpperLevel[this]; }
            set { Fields.OwnerUpperLevel[this] = value; }
        }

        [DisplayName("Owner Upper Level2"), Expression("jOwner.[UpperLevel2]")]
        public Int32? OwnerUpperLevel2
        {
            get { return Fields.OwnerUpperLevel2[this]; }
            set { Fields.OwnerUpperLevel2[this] = value; }
        }

        [DisplayName("Owner Upper Level3"), Expression("jOwner.[UpperLevel3]")]
        public Int32? OwnerUpperLevel3
        {
            get { return Fields.OwnerUpperLevel3[this]; }
            set { Fields.OwnerUpperLevel3[this] = value; }
        }

        [DisplayName("Owner Upper Level4"), Expression("jOwner.[UpperLevel4]")]
        public Int32? OwnerUpperLevel4
        {
            get { return Fields.OwnerUpperLevel4[this]; }
            set { Fields.OwnerUpperLevel4[this] = value; }
        }

        [DisplayName("Owner Upper Level5"), Expression("jOwner.[UpperLevel5]")]
        public Int32? OwnerUpperLevel5
        {
            get { return Fields.OwnerUpperLevel5[this]; }
            set { Fields.OwnerUpperLevel5[this] = value; }
        }

        [DisplayName("Owner Host"), Expression("jOwner.[Host]")]
        public String OwnerHost
        {
            get { return Fields.OwnerHost[this]; }
            set { Fields.OwnerHost[this] = value; }
        }

        [DisplayName("Owner Port"), Expression("jOwner.[Port]")]
        public Int32? OwnerPort
        {
            get { return Fields.OwnerPort[this]; }
            set { Fields.OwnerPort[this] = value; }
        }

        [DisplayName("Owner Ssl"), Expression("jOwner.[SSL]")]
        public Boolean? OwnerSsl
        {
            get { return Fields.OwnerSsl[this]; }
            set { Fields.OwnerSsl[this] = value; }
        }

        [DisplayName("Owner Email Id"), Expression("jOwner.[EmailId]")]
        public String OwnerEmailId
        {
            get { return Fields.OwnerEmailId[this]; }
            set { Fields.OwnerEmailId[this] = value; }
        }

        [DisplayName("Owner Email Password"), Expression("jOwner.[EmailPassword]")]
        public String OwnerEmailPassword
        {
            get { return Fields.OwnerEmailPassword[this]; }
            set { Fields.OwnerEmailPassword[this] = value; }
        }

        [DisplayName("Owner Phone"), Expression("jOwner.[Phone]")]
        public String OwnerPhone
        {
            get { return Fields.OwnerPhone[this]; }
            set { Fields.OwnerPhone[this] = value; }
        }

        [DisplayName("Owner Mcsmtp Server"), Expression("jOwner.[MCSMTPServer]")]
        public String OwnerMcsmtpServer
        {
            get { return Fields.OwnerMcsmtpServer[this]; }
            set { Fields.OwnerMcsmtpServer[this] = value; }
        }

        [DisplayName("Owner Mcsmtp Port"), Expression("jOwner.[MCSMTPPort]")]
        public Int32? OwnerMcsmtpPort
        {
            get { return Fields.OwnerMcsmtpPort[this]; }
            set { Fields.OwnerMcsmtpPort[this] = value; }
        }

        [DisplayName("Owner Mcimap Server"), Expression("jOwner.[MCIMAPServer]")]
        public String OwnerMcimapServer
        {
            get { return Fields.OwnerMcimapServer[this]; }
            set { Fields.OwnerMcimapServer[this] = value; }
        }

        [DisplayName("Owner Mcimap Port"), Expression("jOwner.[MCIMAPPort]")]
        public Int32? OwnerMcimapPort
        {
            get { return Fields.OwnerMcimapPort[this]; }
            set { Fields.OwnerMcimapPort[this] = value; }
        }

        [DisplayName("Owner Mc Username"), Expression("jOwner.[MCUsername]")]
        public String OwnerMcUsername
        {
            get { return Fields.OwnerMcUsername[this]; }
            set { Fields.OwnerMcUsername[this] = value; }
        }

        [DisplayName("Owner Mc Password"), Expression("jOwner.[MCPassword]")]
        public String OwnerMcPassword
        {
            get { return Fields.OwnerMcPassword[this]; }
            set { Fields.OwnerMcPassword[this] = value; }
        }

        [DisplayName("Owner Start Time"), Expression("jOwner.[StartTime]")]
        public String OwnerStartTime
        {
            get { return Fields.OwnerStartTime[this]; }
            set { Fields.OwnerStartTime[this] = value; }
        }

        [DisplayName("Owner End Time"), Expression("jOwner.[EndTime]")]
        public String OwnerEndTime
        {
            get { return Fields.OwnerEndTime[this]; }
            set { Fields.OwnerEndTime[this] = value; }
        }

        [DisplayName("Owner Uid"), Expression("jOwner.[UID]")]
        public String OwnerUid
        {
            get { return Fields.OwnerUid[this]; }
            set { Fields.OwnerUid[this] = value; }
        }

        [DisplayName("Owner Non Operational"), Expression("jOwner.[NonOperational]")]
        public Boolean? OwnerNonOperational
        {
            get { return Fields.OwnerNonOperational[this]; }
            set { Fields.OwnerNonOperational[this] = value; }
        }

        [DisplayName("Owner Branch Id"), Expression("jOwner.[BranchId]")]
        public Int32? OwnerBranchId
        {
            get { return Fields.OwnerBranchId[this]; }
            set { Fields.OwnerBranchId[this] = value; }
        }

        [DisplayName("Owner Company Id"), Expression("jOwner.[CompanyId]")]
        public Int32? OwnerCompanyId
        {
            get { return Fields.OwnerCompanyId[this]; }
            set { Fields.OwnerCompanyId[this] = value; }
        }

        [DisplayName("Owner Enquiry"), Expression("jOwner.[Enquiry]")]
        public Boolean? OwnerEnquiry
        {
            get { return Fields.OwnerEnquiry[this]; }
            set { Fields.OwnerEnquiry[this] = value; }
        }

        [DisplayName("Owner Quotation"), Expression("jOwner.[Quotation]")]
        public Boolean? OwnerQuotation
        {
            get { return Fields.OwnerQuotation[this]; }
            set { Fields.OwnerQuotation[this] = value; }
        }

        [DisplayName("Owner Tasks"), Expression("jOwner.[Tasks]")]
        public Boolean? OwnerTasks
        {
            get { return Fields.OwnerTasks[this]; }
            set { Fields.OwnerTasks[this] = value; }
        }

        [DisplayName("Owner Contacts"), Expression("jOwner.[Contacts]")]
        public Boolean? OwnerContacts
        {
            get { return Fields.OwnerContacts[this]; }
            set { Fields.OwnerContacts[this] = value; }
        }

        [DisplayName("Owner Purchase"), Expression("jOwner.[Purchase]")]
        public Boolean? OwnerPurchase
        {
            get { return Fields.OwnerPurchase[this]; }
            set { Fields.OwnerPurchase[this] = value; }
        }

        [DisplayName("Owner Sales"), Expression("jOwner.[Sales]")]
        public Boolean? OwnerSales
        {
            get { return Fields.OwnerSales[this]; }
            set { Fields.OwnerSales[this] = value; }
        }

        [DisplayName("Owner Cms"), Expression("jOwner.[CMS]")]
        public Boolean? OwnerCms
        {
            get { return Fields.OwnerCms[this]; }
            set { Fields.OwnerCms[this] = value; }
        }

        [DisplayName("Owner Location"), Expression("jOwner.[Location]")]
        public String OwnerLocation
        {
            get { return Fields.OwnerLocation[this]; }
            set { Fields.OwnerLocation[this] = value; }
        }

        [DisplayName("Owner Coordinates"), Expression("jOwner.[Coordinates]")]
        public String OwnerCoordinates
        {
            get { return Fields.OwnerCoordinates[this]; }
            set { Fields.OwnerCoordinates[this] = value; }
        }

        [DisplayName("Owner Teams Id"), Expression("jOwner.[TeamsId]")]
        public Int32? OwnerTeamsId
        {
            get { return Fields.OwnerTeamsId[this]; }
            set { Fields.OwnerTeamsId[this] = value; }
        }

       
        public EmployeeAssestsRow()
            : base(Fields)
        {
        }
        
        public EmployeeAssestsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Items;
            public Int32Field Quantity;
            public StringField Description;
            public Int32Field EmployeeId;
            //public Int32Field OwnerId;

            public Int32Field EmployeeDepartmentId;
            public StringField EmployeeName;
            public StringField EmployeePhone;
            public StringField EmployeeEmail;
            public StringField EmployeeAddress;
            public StringField EmployeeProfessionalEmail;
            public Int32Field EmployeeCityId;
            public Int32Field EmployeeStateId;
            public StringField EmployeePin;
            public Int32Field EmployeeCountry;
            public StringField EmployeeAdditionalInfo;
            public Int32Field EmployeeGender;
            public Int32Field EmployeeReligion;
            public Int32Field EmployeeAreaId;
            public Int32Field EmployeeMaritalStatus;
            public DateTimeField EmployeeMarriageAnniversary;
            public DateTimeField EmployeeBirthdate;
            public DateTimeField EmployeeDateOfJoining;
            public Int32Field EmployeeCompanyId;
            public Int32Field EmployeeRolesId;
            public Int32Field EmployeeOwnerId;
            public StringField EmployeeAdhaarNo;
            public StringField EmployeePanNo;
            public StringField EmployeeAttachment;
            public StringField EmployeeBankName;
            public StringField EmployeeAccountNumber;
            public StringField EmployeeIfsc;
            public StringField EmployeeBankType;
            public StringField EmployeeBranch;
            public Int32Field EmployeeTehsilId;
            public Int32Field EmployeeVillageId;
            public StringField EmployeeEmpCode;

            public StringField OwnerUsername;
            public StringField OwnerDisplayName;
            public StringField OwnerEmail;
            public StringField OwnerSource;
            public StringField OwnerPasswordHash;
            public StringField OwnerPasswordSalt;
            public DateTimeField OwnerLastDirectoryUpdate;
            public StringField OwnerUserImage;
            public DateTimeField OwnerInsertDate;
            public Int32Field OwnerInsertUserId;
            public DateTimeField OwnerUpdateDate;
            public Int32Field OwnerUpdateUserId;
            public Int16Field OwnerIsActive;
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
        }
    }
}
