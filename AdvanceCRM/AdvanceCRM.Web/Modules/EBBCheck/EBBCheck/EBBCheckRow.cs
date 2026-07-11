using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace AdvanceCRM.EBBCheck
{
    [ConnectionKey("Default"), Module("EBBCheck"), TableName("[dbo].[EBBCheck]")]
    [DisplayName("EBB Check"), InstanceName("EBB Check")]
    [ReadPermission("EBBCheck:Read")]
    [InsertPermission("EBBCheck:Insert")]
    [UpdatePermission("EBBCheck:Update")]
    [DeletePermission("EBBCheck:Delete")]
    public sealed class EBBCheckRow : Row<EBBCheckRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("First Name"), Size(200), QuickSearch, NameProperty]
        public String FirstName
        {
            get => fields.FirstName[this];
            set => fields.FirstName[this] = value;
        }

        [DisplayName("Email"), Size(200), QuickSearch]
        public String Email
        {
            get => fields.Email[this];
            set => fields.Email[this] = value;
        }

        // Only the Quality team (EBBCheck:ChangeStatus) may change this – enforced in EBBCheckSaveHandler.
        [DisplayName("Status")]
        public EbbStatus? Status
        {
            get => (EbbStatus?)fields.Status[this];
            set => fields.Status[this] = (Int32?)value;
        }

        // Auto-stamped with the creating (Enquiry) user – see EBBCheckSaveHandler.
        [DisplayName("User"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUser"), TextualField("UserName"), ReadOnly(true)]
        [Administration.UserEditor]
        public Int32? UserId
        {
            get => fields.UserId[this];
            set => fields.UserId[this] = value;
        }

        // Auto-stamped when the Enquiry user enters the data – see EBBCheckSaveHandler.
        [DisplayName("TimeStamp"), ReadOnly(true)]
        public DateTime? TimeStamp
        {
            get => fields.TimeStamp[this];
            set => fields.TimeStamp[this] = value;
        }

        [DisplayName("Date")]
        public DateTime? Date
        {
            get => fields.Date[this];
            set => fields.Date[this] = value;
        }

        // "Created By" – auto-stamped with the creating user (see EBBCheckSaveHandler).
        [DisplayName("Created By"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername"), ReadOnly(true)]
        [Administration.UserEditor]
        public Int32? OwnerId
        {
            get => fields.OwnerId[this];
            set => fields.OwnerId[this] = value;
        }

        [DisplayName("User Name"), Expression("jUser.[Username]")]
        public String UserName
        {
            get => fields.UserName[this];
            set => fields.UserName[this] = value;
        }

        [DisplayName("User Display Name"), Expression("jUser.[DisplayName]")]
        public String UserDisplayName
        {
            get => fields.UserDisplayName[this];
            set => fields.UserDisplayName[this] = value;
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

        public EBBCheckRow()
            : base()
        {
        }

        public EBBCheckRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField FirstName;
            public StringField Email;
            public Int32Field Status;
            public Int32Field UserId;
            public DateTimeField TimeStamp;
            public DateTimeField Date;
            public Int32Field OwnerId;

            public StringField UserName;
            public StringField UserDisplayName;
            public StringField OwnerUsername;
            public StringField OwnerDisplayName;
        }
    }
}
