namespace AdvanceCRM
{
    using Serenity;
    using System;

    [Serializable]
    public class UserDefinition : IUserDefinition
    {
        public string Id { get { return UserId.ToInvariant(); } }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserImage { get; set; }
        public short IsActive { get; set; }
        public int UserId { get; set; }
        public int UpperLevel { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Source { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? LastDirectoryUpdate { get; set; }
        public Int32? BranchId { get; set; }
        public Int32 CompanyId { get; set; }
        public bool Enquiry { get; set; }
        public bool Quotation { get; set; }
        public bool Tasks { get; set; }
        public bool Contacts { get; set; }
        public bool Purchase { get; set; }
        public bool Sales { get; set; }

        public bool Cms { get; set; }

    }
}