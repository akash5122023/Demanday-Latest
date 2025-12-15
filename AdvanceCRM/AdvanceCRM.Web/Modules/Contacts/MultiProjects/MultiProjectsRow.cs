
namespace AdvanceCRM.Contacts
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Contacts"), TableName("[dbo].[MultiProjects]")]
    [DisplayName("Multi Projects"), InstanceName("Multi Projects")]
    [ReadPermission("Contacts:Read")]
    [InsertPermission("Contacts:Insert")]
    [UpdatePermission("Contacts:Update")]
    [DeletePermission("Contacts:Delete")]
    [LookupScript("Contacts.MultiProjects", Permission = "?")]
    public sealed class MultiProjectsRow : Row<MultiProjectsRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Project"), NotNull, ForeignKey("[dbo].[Project]", "Id"), LeftJoin("jProject"), TextualField("Project"), LookupInclude]
        public Int32? ProjectId
        {
            get { return Fields.ProjectId[this]; }
            set { Fields.ProjectId[this] = value; }
        }

        [DisplayName("Sub Contacts"), NotNull, ForeignKey("[dbo].[SubContacts]", "Id"), LeftJoin("jSubContacts"), TextualField("SubContactsName"), LookupInclude]
        public Int32? SubContactsId
        {
            get { return Fields.SubContactsId[this]; }
            set { Fields.SubContactsId[this] = value; }
        }

        [DisplayName("Project"), Expression("jProject.[Project]"),LookupInclude]
        public String Project
        {
            get { return Fields.Project[this]; }
            set { Fields.Project[this] = value; }
        }

        [DisplayName("Sub Contacts Name"), Expression("jSubContacts.[Name]")]
        public String SubContactsName
        {
            get { return Fields.SubContactsName[this]; }
            set { Fields.SubContactsName[this] = value; }
        }

        [DisplayName("Sub Contacts Phone"), Expression("jSubContacts.[Phone]")]
        public String SubContactsPhone
        {
            get { return Fields.SubContactsPhone[this]; }
            set { Fields.SubContactsPhone[this] = value; }
        }

        [DisplayName("Sub Contacts Residential Phone"), Expression("jSubContacts.[ResidentialPhone]")]
        public String SubContactsResidentialPhone
        {
            get { return Fields.SubContactsResidentialPhone[this]; }
            set { Fields.SubContactsResidentialPhone[this] = value; }
        }

        [DisplayName("Sub Contacts Email"), Expression("jSubContacts.[Email]")]
        public String SubContactsEmail
        {
            get { return Fields.SubContactsEmail[this]; }
            set { Fields.SubContactsEmail[this] = value; }
        }

        [DisplayName("Sub Contacts Designation"), Expression("jSubContacts.[Designation]")]
        public String SubContactsDesignation
        {
            get { return Fields.SubContactsDesignation[this]; }
            set { Fields.SubContactsDesignation[this] = value; }
        }

        [DisplayName("Sub Contacts Address"), Expression("jSubContacts.[Address]")]
        public String SubContactsAddress
        {
            get { return Fields.SubContactsAddress[this]; }
            set { Fields.SubContactsAddress[this] = value; }
        }

        [DisplayName("Sub Contacts Gender"), Expression("jSubContacts.[Gender]")]
        public Int32? SubContactsGender
        {
            get { return Fields.SubContactsGender[this]; }
            set { Fields.SubContactsGender[this] = value; }
        }

        [DisplayName("Sub Contacts Religion"), Expression("jSubContacts.[Religion]")]
        public Int32? SubContactsReligion
        {
            get { return Fields.SubContactsReligion[this]; }
            set { Fields.SubContactsReligion[this] = value; }
        }

        [DisplayName("Sub Contacts Marital Status"), Expression("jSubContacts.[MaritalStatus]")]
        public Int32? SubContactsMaritalStatus
        {
            get { return Fields.SubContactsMaritalStatus[this]; }
            set { Fields.SubContactsMaritalStatus[this] = value; }
        }

        [DisplayName("Sub Contacts Marriage Anniversary"), Expression("jSubContacts.[MarriageAnniversary]")]
        public DateTime? SubContactsMarriageAnniversary
        {
            get { return Fields.SubContactsMarriageAnniversary[this]; }
            set { Fields.SubContactsMarriageAnniversary[this] = value; }
        }

        [DisplayName("Sub Contacts Birthdate"), Expression("jSubContacts.[Birthdate]")]
        public DateTime? SubContactsBirthdate
        {
            get { return Fields.SubContactsBirthdate[this]; }
            set { Fields.SubContactsBirthdate[this] = value; }
        }

        [DisplayName("Sub Contacts Contacts Id"), Expression("jSubContacts.[ContactsId]")]
        public Int32? SubContactsContactsId
        {
            get { return Fields.SubContactsContactsId[this]; }
            set { Fields.SubContactsContactsId[this] = value; }
        }

        [DisplayName("Sub Contacts Project"), Expression("jSubContacts.[Project]")]
        public String SubContactsProject
        {
            get { return Fields.SubContactsProject[this]; }
            set { Fields.SubContactsProject[this] = value; }
        }

        [DisplayName("Sub Contacts Whatsapp"), Expression("jSubContacts.[Whatsapp]")]
        public String SubContactsWhatsapp
        {
            get { return Fields.SubContactsWhatsapp[this]; }
            set { Fields.SubContactsWhatsapp[this] = value; }
        }

       
        public MultiProjectsRow()
            : base(Fields)
        {
        }
        
        public MultiProjectsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProjectId;
            public Int32Field SubContactsId;

            public StringField Project;

            public StringField SubContactsName;
            public StringField SubContactsPhone;
            public StringField SubContactsResidentialPhone;
            public StringField SubContactsEmail;
            public StringField SubContactsDesignation;
            public StringField SubContactsAddress;
            public Int32Field SubContactsGender;
            public Int32Field SubContactsReligion;
            public Int32Field SubContactsMaritalStatus;
            public DateTimeField SubContactsMarriageAnniversary;
            public DateTimeField SubContactsBirthdate;
            public Int32Field SubContactsContactsId;
            public StringField SubContactsProject;
            public StringField SubContactsWhatsapp;
        }
    }
}
