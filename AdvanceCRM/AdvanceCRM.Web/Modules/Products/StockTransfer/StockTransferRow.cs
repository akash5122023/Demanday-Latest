
namespace AdvanceCRM.Products
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Scripts;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Products"), TableName("[dbo].[StockTransfer]")]
    [DisplayName("Stock Transfer"), InstanceName("Stock Transfer")]
    [ReadPermission("StockTransfer:Read")]
    [ModifyPermission("StockTransfer:Modify")]
    [LookupScript("Products.StockTransfer", Permission = "?", LookupType = typeof(MultiCompanyRowLookupScript<>))]

    public sealed class StockTransferRow : Row<StockTransferRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog, IMultiCompanyRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Date"), NotNull]
        public DateTime? Date
        {
            get { return Fields.Date[this]; }
            set { Fields.Date[this] = value; }
        }

        [DisplayName("From Branch"), NotNull, ForeignKey("[dbo].[Branch]", "Id"), LeftJoin("jFromBranch"), TextualField("FromBranchBranch")]
        [LookupEditor("Administration.BranchLookup")]
        public Int32? FromBranchId
        {
            get { return Fields.FromBranchId[this]; }
            set { Fields.FromBranchId[this] = value; }
        }

        [DisplayName("To Branch"), NotNull, ForeignKey("[dbo].[Branch]", "Id"), LeftJoin("jToBranch"), TextualField("ToBranchBranch")]
        [LookupEditor("Administration.BranchLookup")]
        public Int32? ToBranchId
        {
            get { return Fields.ToBranchId[this]; }
            set { Fields.ToBranchId[this] = value; }
        }

        [DisplayName("Total Qty"), Expression("(SELECT SUM(Quantity) FROM StockTransferProducts WHERE StockTransferId=t0.[Id])")]
        public Double? TotalQty
        {
            get { return Fields.TotalQty[this]; }
            set { Fields.TotalQty[this] = value; }
        }

        [DisplayName("Amount"), Expression("(SELECT SUM((TransferPrice * Quantity) + ((TransferPrice * Quantity) * (Percentage1 / 100)) + ((TransferPrice * Quantity) * (Percentage2 / 100))) FROM StockTransferProducts WHERE StockTransferId=t0.[Id])")]
        public Double? Amount
        {
            get { return Fields.Amount[this]; }
            set { Fields.Amount[this] = value; }
        }

        [DisplayName("Additional Info"), Size(2000), QuickSearch, TextAreaEditor(Rows = 4),NameProperty]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
        }

        [DisplayName("Representative"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jRepresentative"), TextualField("RepresentativeUsername")]
        [Administration.UserEditor]
        public Int32? RepresentativeId
        {
            get { return Fields.RepresentativeId[this]; }
            set { Fields.RepresentativeId[this] = value; }
        }

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        [Insertable(false), Updatable(false)]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }
        public Int32Field CompanyIdField
        {
            get { return Fields.CompanyId; }
        }

        [DisplayName("From Branch Branch"), Expression("jFromBranch.[Branch]")]
        public String FromBranchBranch
        {
            get { return Fields.FromBranchBranch[this]; }
            set { Fields.FromBranchBranch[this] = value; }
        }

        [DisplayName("From Branch Phone"), Expression("jFromBranch.[Phone]")]
        public String FromBranchPhone
        {
            get { return Fields.FromBranchPhone[this]; }
            set { Fields.FromBranchPhone[this] = value; }
        }

        [DisplayName("From Branch Email"), Expression("jFromBranch.[Email]")]
        public String FromBranchEmail
        {
            get { return Fields.FromBranchEmail[this]; }
            set { Fields.FromBranchEmail[this] = value; }
        }

        [DisplayName("From Branch Address"), Expression("jFromBranch.[Address]")]
        public String FromBranchAddress
        {
            get { return Fields.FromBranchAddress[this]; }
            set { Fields.FromBranchAddress[this] = value; }
        }

        [DisplayName("From Branch City Id"), Expression("jFromBranch.[CityId]")]
        public Int32? FromBranchCityId
        {
            get { return Fields.FromBranchCityId[this]; }
            set { Fields.FromBranchCityId[this] = value; }
        }

        [DisplayName("From Branch State Id"), Expression("jFromBranch.[StateId]")]
        public Int32? FromBranchStateId
        {
            get { return Fields.FromBranchStateId[this]; }
            set { Fields.FromBranchStateId[this] = value; }
        }

        [DisplayName("From Branch Pin"), Expression("jFromBranch.[Pin]")]
        public String FromBranchPin
        {
            get { return Fields.FromBranchPin[this]; }
            set { Fields.FromBranchPin[this] = value; }
        }

        [DisplayName("From Branch Country"), Expression("jFromBranch.[Country]")]
        public Int32? FromBranchCountry
        {
            get { return Fields.FromBranchCountry[this]; }
            set { Fields.FromBranchCountry[this] = value; }
        }

        [DisplayName("To Branch Branch"), Expression("jToBranch.[Branch]")]
        public String ToBranchBranch
        {
            get { return Fields.ToBranchBranch[this]; }
            set { Fields.ToBranchBranch[this] = value; }
        }

        [DisplayName("To Branch Phone"), Expression("jToBranch.[Phone]")]
        public String ToBranchPhone
        {
            get { return Fields.ToBranchPhone[this]; }
            set { Fields.ToBranchPhone[this] = value; }
        }

        [DisplayName("To Branch Email"), Expression("jToBranch.[Email]")]
        public String ToBranchEmail
        {
            get { return Fields.ToBranchEmail[this]; }
            set { Fields.ToBranchEmail[this] = value; }
        }

        [DisplayName("To Branch Address"), Expression("jToBranch.[Address]")]
        public String ToBranchAddress
        {
            get { return Fields.ToBranchAddress[this]; }
            set { Fields.ToBranchAddress[this] = value; }
        }

        [DisplayName("To Branch City Id"), Expression("jToBranch.[CityId]")]
        public Int32? ToBranchCityId
        {
            get { return Fields.ToBranchCityId[this]; }
            set { Fields.ToBranchCityId[this] = value; }
        }

        [DisplayName("To Branch State Id"), Expression("jToBranch.[StateId]")]
        public Int32? ToBranchStateId
        {
            get { return Fields.ToBranchStateId[this]; }
            set { Fields.ToBranchStateId[this] = value; }
        }

        [DisplayName("To Branch Pin"), Expression("jToBranch.[Pin]")]
        public String ToBranchPin
        {
            get { return Fields.ToBranchPin[this]; }
            set { Fields.ToBranchPin[this] = value; }
        }

        [DisplayName("To Branch Country"), Expression("jToBranch.[Country]")]
        public Int32? ToBranchCountry
        {
            get { return Fields.ToBranchCountry[this]; }
            set { Fields.ToBranchCountry[this] = value; }
        }

        [DisplayName("Representative"), Expression("jRepresentative.[Username]")]
        public String RepresentativeUsername
        {
            get { return Fields.RepresentativeUsername[this]; }
            set { Fields.RepresentativeUsername[this] = value; }
        }

        [DisplayName("Representative"), Expression("jRepresentative.[DisplayName]")]
        public String RepresentativeDisplayName
        {
            get { return Fields.RepresentativeDisplayName[this]; }
            set { Fields.RepresentativeDisplayName[this] = value; }
        }

        [DisplayName("Representative Email"), Expression("jRepresentative.[Email]")]
        public String RepresentativeEmail
        {
            get { return Fields.RepresentativeEmail[this]; }
            set { Fields.RepresentativeEmail[this] = value; }
        }

        [DisplayName("Representative Source"), Expression("jRepresentative.[Source]")]
        public String RepresentativeSource
        {
            get { return Fields.RepresentativeSource[this]; }
            set { Fields.RepresentativeSource[this] = value; }
        }

        [DisplayName("Representative Password Hash"), Expression("jRepresentative.[PasswordHash]")]
        public String RepresentativePasswordHash
        {
            get { return Fields.RepresentativePasswordHash[this]; }
            set { Fields.RepresentativePasswordHash[this] = value; }
        }

        [DisplayName("Representative Password Salt"), Expression("jRepresentative.[PasswordSalt]")]
        public String RepresentativePasswordSalt
        {
            get { return Fields.RepresentativePasswordSalt[this]; }
            set { Fields.RepresentativePasswordSalt[this] = value; }
        }

        [DisplayName("Representative Last Directory Update"), Expression("jRepresentative.[LastDirectoryUpdate]")]
        public DateTime? RepresentativeLastDirectoryUpdate
        {
            get { return Fields.RepresentativeLastDirectoryUpdate[this]; }
            set { Fields.RepresentativeLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Representative User Image"), Expression("jRepresentative.[UserImage]")]
        public String RepresentativeUserImage
        {
            get { return Fields.RepresentativeUserImage[this]; }
            set { Fields.RepresentativeUserImage[this] = value; }
        }

        [DisplayName("Representative Insert Date"), Expression("jRepresentative.[InsertDate]")]
        public DateTime? RepresentativeInsertDate
        {
            get { return Fields.RepresentativeInsertDate[this]; }
            set { Fields.RepresentativeInsertDate[this] = value; }
        }

        [DisplayName("Representative Insert User Id"), Expression("jRepresentative.[InsertUserId]")]
        public Int32? RepresentativeInsertUserId
        {
            get { return Fields.RepresentativeInsertUserId[this]; }
            set { Fields.RepresentativeInsertUserId[this] = value; }
        }

        [DisplayName("Representative Update Date"), Expression("jRepresentative.[UpdateDate]")]
        public DateTime? RepresentativeUpdateDate
        {
            get { return Fields.RepresentativeUpdateDate[this]; }
            set { Fields.RepresentativeUpdateDate[this] = value; }
        }

        [DisplayName("Representative Update User Id"), Expression("jRepresentative.[UpdateUserId]")]
        public Int32? RepresentativeUpdateUserId
        {
            get { return Fields.RepresentativeUpdateUserId[this]; }
            set { Fields.RepresentativeUpdateUserId[this] = value; }
        }

        [DisplayName("Representative Is Active"), Expression("jRepresentative.[IsActive]")]
        public Int16? RepresentativeIsActive
        {
            get { return Fields.RepresentativeIsActive[this]; }
            set { Fields.RepresentativeIsActive[this] = value; }
        }

        [DisplayName("Representative Upper Level"), Expression("jRepresentative.[UpperLevel]")]
        public Int32? RepresentativeUpperLevel
        {
            get { return Fields.RepresentativeUpperLevel[this]; }
            set { Fields.RepresentativeUpperLevel[this] = value; }
        }

        [DisplayName("Representative Upper Level2"), Expression("jRepresentative.[UpperLevel2]")]
        public Int32? RepresentativeUpperLevel2
        {
            get { return Fields.RepresentativeUpperLevel2[this]; }
            set { Fields.RepresentativeUpperLevel2[this] = value; }
        }

        [DisplayName("Representative Upper Level3"), Expression("jRepresentative.[UpperLevel3]")]
        public Int32? RepresentativeUpperLevel3
        {
            get { return Fields.RepresentativeUpperLevel3[this]; }
            set { Fields.RepresentativeUpperLevel3[this] = value; }
        }

        [DisplayName("Representative Upper Level4"), Expression("jRepresentative.[UpperLevel4]")]
        public Int32? RepresentativeUpperLevel4
        {
            get { return Fields.RepresentativeUpperLevel4[this]; }
            set { Fields.RepresentativeUpperLevel4[this] = value; }
        }

        [DisplayName("Representative Upper Level5"), Expression("jRepresentative.[UpperLevel5]")]
        public Int32? RepresentativeUpperLevel5
        {
            get { return Fields.RepresentativeUpperLevel5[this]; }
            set { Fields.RepresentativeUpperLevel5[this] = value; }
        }

        [DisplayName("Representative Host"), Expression("jRepresentative.[Host]")]
        public String RepresentativeHost
        {
            get { return Fields.RepresentativeHost[this]; }
            set { Fields.RepresentativeHost[this] = value; }
        }

        [DisplayName("Representative Port"), Expression("jRepresentative.[Port]")]
        public Int32? RepresentativePort
        {
            get { return Fields.RepresentativePort[this]; }
            set { Fields.RepresentativePort[this] = value; }
        }

        [DisplayName("Representative SSL"), Expression("jRepresentative.[SSL]")]
        public Boolean? RepresentativeSsl
        {
            get { return Fields.RepresentativeSsl[this]; }
            set { Fields.RepresentativeSsl[this] = value; }
        }

        [DisplayName("Representative Email Id"), Expression("jRepresentative.[EmailId]")]
        public String RepresentativeEmailId
        {
            get { return Fields.RepresentativeEmailId[this]; }
            set { Fields.RepresentativeEmailId[this] = value; }
        }

        [DisplayName("Representative Email Password"), Expression("jRepresentative.[EmailPassword]")]
        public String RepresentativeEmailPassword
        {
            get { return Fields.RepresentativeEmailPassword[this]; }
            set { Fields.RepresentativeEmailPassword[this] = value; }
        }

        [DisplayName("Representative Phone"), Expression("jRepresentative.[Phone]")]
        public String RepresentativePhone
        {
            get { return Fields.RepresentativePhone[this]; }
            set { Fields.RepresentativePhone[this] = value; }
        }

        [DisplayName("Representative Mcsmtp Server"), Expression("jRepresentative.[MCSMTPServer]")]
        public String RepresentativeMcsmtpServer
        {
            get { return Fields.RepresentativeMcsmtpServer[this]; }
            set { Fields.RepresentativeMcsmtpServer[this] = value; }
        }

        [DisplayName("Representative Mcsmtp Port"), Expression("jRepresentative.[MCSMTPPort]")]
        public Int32? RepresentativeMcsmtpPort
        {
            get { return Fields.RepresentativeMcsmtpPort[this]; }
            set { Fields.RepresentativeMcsmtpPort[this] = value; }
        }

        [DisplayName("Representative Mcimap Server"), Expression("jRepresentative.[MCIMAPServer]")]
        public String RepresentativeMcimapServer
        {
            get { return Fields.RepresentativeMcimapServer[this]; }
            set { Fields.RepresentativeMcimapServer[this] = value; }
        }

        [DisplayName("Representative Mcimap Port"), Expression("jRepresentative.[MCIMAPPort]")]
        public Int32? RepresentativeMcimapPort
        {
            get { return Fields.RepresentativeMcimapPort[this]; }
            set { Fields.RepresentativeMcimapPort[this] = value; }
        }

        [DisplayName("Representative Mc Username"), Expression("jRepresentative.[MCUsername]")]
        public String RepresentativeMcUsername
        {
            get { return Fields.RepresentativeMcUsername[this]; }
            set { Fields.RepresentativeMcUsername[this] = value; }
        }

        [DisplayName("Representative Mc Password"), Expression("jRepresentative.[MCPassword]")]
        public String RepresentativeMcPassword
        {
            get { return Fields.RepresentativeMcPassword[this]; }
            set { Fields.RepresentativeMcPassword[this] = value; }
        }

        [DisplayName("Representative Start Time"), Expression("jRepresentative.[StartTime]")]
        public String RepresentativeStartTime
        {
            get { return Fields.RepresentativeStartTime[this]; }
            set { Fields.RepresentativeStartTime[this] = value; }
        }

        [DisplayName("Representative End Time"), Expression("jRepresentative.[EndTime]")]
        public String RepresentativeEndTime
        {
            get { return Fields.RepresentativeEndTime[this]; }
            set { Fields.RepresentativeEndTime[this] = value; }
        }

        [DisplayName("Representative Branch Id"), Expression("jRepresentative.[BranchId]")]
        public Int32? RepresentativeBranchId
        {
            get { return Fields.RepresentativeBranchId[this]; }
            set { Fields.RepresentativeBranchId[this] = value; }
        }

        [DisplayName("Representative Uid"), Expression("jRepresentative.[UID]")]
        public String RepresentativeUid
        {
            get { return Fields.RepresentativeUid[this]; }
            set { Fields.RepresentativeUid[this] = value; }
        }

        [DisplayName("Representative Non Operational"), Expression("jRepresentative.[NonOperational]")]
        public Boolean? RepresentativeNonOperational
        {
            get { return Fields.RepresentativeNonOperational[this]; }
            set { Fields.RepresentativeNonOperational[this] = value; }
        }

        [DisplayName("Products"), MasterDetailRelation(foreignKey: "StockTransferId", IncludeColumns = "ProductsId,ProductsName"), NotMapped]
        public List<StockTransferProductsRow> Products { get { return Fields.Products[this]; } set { Fields.Products[this] = value; } }


       

        

        public StockTransferRow()
            : base(Fields)
        {
        }
        

        public StockTransferRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public DateTimeField Date;
            public Int32Field FromBranchId;
            public Int32Field ToBranchId;
            public DoubleField TotalQty;
            public DoubleField Amount;
            public StringField AdditionalInfo;
            public Int32Field RepresentativeId;
            public Int32Field CompanyId;

            public StringField FromBranchBranch;
            public StringField FromBranchPhone;
            public StringField FromBranchEmail;
            public StringField FromBranchAddress;
            public Int32Field FromBranchCityId;
            public Int32Field FromBranchStateId;
            public StringField FromBranchPin;
            public Int32Field FromBranchCountry;

            public StringField ToBranchBranch;
            public StringField ToBranchPhone;
            public StringField ToBranchEmail;
            public StringField ToBranchAddress;
            public Int32Field ToBranchCityId;
            public Int32Field ToBranchStateId;
            public StringField ToBranchPin;
            public Int32Field ToBranchCountry;

            public StringField RepresentativeUsername;
            public StringField RepresentativeDisplayName;
            public StringField RepresentativeEmail;
            public StringField RepresentativeSource;
            public StringField RepresentativePasswordHash;
            public StringField RepresentativePasswordSalt;
            public DateTimeField RepresentativeLastDirectoryUpdate;
            public StringField RepresentativeUserImage;
            public DateTimeField RepresentativeInsertDate;
            public Int32Field RepresentativeInsertUserId;
            public DateTimeField RepresentativeUpdateDate;
            public Int32Field RepresentativeUpdateUserId;
            public Int16Field RepresentativeIsActive;
            public Int32Field RepresentativeUpperLevel;
            public Int32Field RepresentativeUpperLevel2;
            public Int32Field RepresentativeUpperLevel3;
            public Int32Field RepresentativeUpperLevel4;
            public Int32Field RepresentativeUpperLevel5;
            public StringField RepresentativeHost;
            public Int32Field RepresentativePort;
            public BooleanField RepresentativeSsl;
            public StringField RepresentativeEmailId;
            public StringField RepresentativeEmailPassword;
            public StringField RepresentativePhone;
            public StringField RepresentativeMcsmtpServer;
            public Int32Field RepresentativeMcsmtpPort;
            public StringField RepresentativeMcimapServer;
            public Int32Field RepresentativeMcimapPort;
            public StringField RepresentativeMcUsername;
            public StringField RepresentativeMcPassword;
            public StringField RepresentativeStartTime;
            public StringField RepresentativeEndTime;
            public Int32Field RepresentativeBranchId;
            public StringField RepresentativeUid;
            public BooleanField RepresentativeNonOperational;

            public readonly RowListField<StockTransferProductsRow> Products;
        }
    }
}
