namespace AdvanceCRM.Administration.Forms
{
    using Serenity.ComponentModel;
    using System;
    using System.ComponentModel;

    [FormScript("Administration.User")]
    [BasedOnRow(typeof(UserRow))]
    public class UserForm
    {
        [Tab("User Management")]
        public Int32 CompanyId { get; set; }
    // Tenant / URL are SaaS-only concepts; hide them in single-tenant mode
    [ReadOnly(true)]
    public Int32 TenantId { get; set; }
    [ReadOnly(true)]
    public String Url { get; set; }
        public String Username { get; set; }
        public String DisplayName { get; set; }
        [EmailEditor]
        public String Email { get; set; }
        [MaskedEditor(Mask = "9999999999")]
        public String Phone { get; set; }
        public Boolean NonOperational { get; set; }

        public Int32 TeamsId { get; set; }
        public String UserImage { get; set; }
        [PasswordEditor, Required(true)]
        public String Password { get; set; }
        [PasswordEditor, OneWay, Required(true)]
        public string PasswordConfirm { get; set; }
        public Int32 BranchId { get; set; }

        [DisplayName("Active"), BooleanSwitchEditor]
        public Boolean IsActive { get; set; }


        //Email configuration
        public String Host { get; set; }
        [DefaultValue("587")]
        public Int32 Port { get; set; }
        [DefaultValue("1")]
        public Boolean SSL { get; set; }
        public String EmailId { get; set; }
        [PasswordEditor]
        public String EmailPassword { get; set; }

        //Hierarchy
        [DefaultValue("1")]
        public Int32 UpperLevel { get; set; }
        [DefaultValue("1")]
        public Int32 UpperLevel2 { get; set; }
        [DefaultValue("1")]
        public Int32 UpperLevel3 { get; set; }
        [DefaultValue("1")]
        public Int32 UpperLevel4 { get; set; }
        [DefaultValue("1")]
        public Int32 UpperLevel5 { get; set; }

        //Mailbox Configuration
        public String MCSMTPServer { get; set; }
        public Int32 MCSMTPPort { get; set; }
        public String MCIMAPServer { get; set; }
        public Int32 MCIMAPPort { get; set; }
        public String MCUsername { get; set; }
        [PasswordEditor]
        public String MCPassword { get; set; }
        [MaskedEditor(Mask = "99:99")]
        public String StartTime { get; set; }
        [MaskedEditor(Mask = "99:99")]
        public String EndTime { get; set; }
        [Category("Previous Location")]
        public String Location { get; set; }
        public String Coordinates { get; set; }

        [Tab("Additional Permission")]
        [Category("View All Records")]
        [BooleanSwitchEditor]
        public Boolean Enquiry { get; set; }
        [BooleanSwitchEditor]
        public Boolean Quotation { get; set; }
        [BooleanSwitchEditor]
        public Boolean Tasks { get; set; }
        [BooleanSwitchEditor]
        public Boolean Contacts { get; set; }
        [BooleanSwitchEditor]
        public Boolean Purchase { get; set; }
        [BooleanSwitchEditor]
        public Boolean Sales { get; set; }
        [BooleanSwitchEditor]
        public Boolean Cms { get; set; }


        //[OneWay]
        //public string Source { get; set; }
    }
}
