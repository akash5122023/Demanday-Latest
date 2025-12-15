
namespace AdvanceCRM.ThirdParty
{
    using AdvanceCRM.Modules.ThirdParty.KnowlarityDetails;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[KnowlarityDetails]")]
    [DisplayName("BizplusIVR"), InstanceName("BizplusIVR")]
    [ReadPermission("KnowlarityDetails:inbox")]
    [ModifyPermission("KnowlarityDetails:inbox")]
    public sealed class KnowlarityDetailsRow : Row<KnowlarityDetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        //[DisplayName("Name"), Size(255), NotNull, QuickSearch,Expression("(SELECT Name FROM Contacts WHERE Phone = t0.[CustomerNumber])")]
        [DisplayName("Name"), Size(255), NotNull, QuickSearch,NameProperty ]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Customer Number"), Size(20)]
        public String CustomerNumber
        {
            get { return Fields.CustomerNumber[this]; }
            set { Fields.CustomerNumber[this] = value; }
        }

        [DisplayName("Company Type")]
        public Int32? CompanyType
        {
            get { return Fields.CompanyType[this]; }
            set { Fields.CompanyType[this] = value; }
        }

        [DisplayName("Email"), Size(255)]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Employee Number"), Size(100)]
        public String EmployeeNumber
        {
            get { return Fields.EmployeeNumber[this]; }
            set { Fields.EmployeeNumber[this] = value; }
        }

        [DisplayName("Duration"), Size(50)]
        public String Duration
        {
            get { return Fields.Duration[this]; }
            set { Fields.Duration[this] = value; }
        }

        [DisplayName("Call Status")]
        //[Expression("CASE " +
        // "WHEN T0.[Duration] = 0 THEN 0 " +
        // "WHEN T0.[EmployeeNumber] LIKE 'Customer Missed%' THEN 2 " +
        // "WHEN T0.[EmployeeNumber] LIKE 'Agent Missed%' THEN 3 " +
        // "WHEN CAST(T0.[DateTime] AS DATE) = CAST(GETDATE() AS DATE) THEN 4 " +
        // "WHEN T0.[DateTime] >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1) AND T0.[DateTime] < DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)) THEN 5 " +
        // "ELSE 1 END")]
        [Expression("CASE " +
       "WHEN T0.[Duration] = 0 THEN 0 " +
       "WHEN T0.[EmployeeNumber] LIKE '91%' THEN 1 " +
       "WHEN T0.[EmployeeNumber] LIKE 'Customer Missed%' THEN 2 " +
       "WHEN T0.[EmployeeNumber] LIKE 'Agent Missed%' THEN 3 " +
       "WHEN CAST(T0.[DateTime] AS DATE) = CAST(GETDATE() AS DATE) THEN 4 " +
       "WHEN CAST(T0.[DateTime] AS DATE) >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1) AND CAST(T0.[DateTime] AS DATE) < DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)) THEN 5 " +
       "ELSE 1 END")]
        public IvrCallDuration? CallDurationState
        {
            get { return (IvrCallDuration?)Fields.CallDurationState[this]; }
            set { Fields.CallDurationState[this] = (Int32?)value; }
        }


        [DisplayName("Recording"), Size(1000)]
        public String Recording
        {
            get { return Fields.Recording[this]; }
            set { Fields.Recording[this] = value; }
        }

        [DisplayName("Date Time"), NotNull]
        public DateTime? DateTime
        {
            get { return Fields.DateTime[this]; }
            set { Fields.DateTime[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

        [DisplayName("Cmiuid"), Column("CMIUID"), Size(100)]
        public String Cmiuid
        {
            get { return Fields.Cmiuid[this]; }
            set { Fields.Cmiuid[this] = value; }
        }


        [DisplayName("Billed Sec"), Size(20)]
        public String BilledSec
        {
            get { return Fields.BilledSec[this]; }
            set { Fields.BilledSec[this] = value; }
        }

        [DisplayName("Rate"), Size(20)]
        public String Rate
        {
            get { return Fields.Rate[this]; }
            set { Fields.Rate[this] = value; }
        }

        [DisplayName("Record"), Size(20)]
        public String Record
        {
            get { return Fields.Record[this]; }
            set { Fields.Record[this] = value; }
        }

        [DisplayName("From"), Size(20)]
        public String From
        {
            get { return Fields.From[this]; }
            set { Fields.From[this] = value; }
        }

        [DisplayName("EmployeeName"),Expression("(SELECT Name FROM KnowlarityAgents WHERE Number = t0.[EmployeeNumber])")]
        public String EmployeeName
        {
            get { return Fields.EmployeeName[this]; }
            set { Fields.EmployeeName[this] = value; }
        }

        [DisplayName("To"), Size(20)]
        public String To
        {
            get { return Fields.To[this]; }
            set { Fields.To[this] = value; }
        }

        [DisplayName("Type"), Size(20)]
        public String Type
        {
            get { return Fields.Type[this]; }
            set { Fields.Type[this] = value; }
        }

       

        public KnowlarityDetailsRow()
            : base(Fields)
        {
        }
        public KnowlarityDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField CustomerNumber;
            public Int32Field CompanyType;
            public StringField Email;
            public StringField EmployeeNumber;
            public StringField Duration;
            public StringField Recording;
            public DateTimeField DateTime;
            public BooleanField IsMoved;
            public StringField Cmiuid;
            public StringField BilledSec;
            public StringField Rate;
            public StringField Record;
            public StringField From;
            public StringField To;
            public StringField Type;
            public StringField EmployeeName;
            public Int32Field CallDurationState;
        }
    }
}
