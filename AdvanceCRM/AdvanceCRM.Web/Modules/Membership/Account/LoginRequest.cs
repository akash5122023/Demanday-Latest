
namespace AdvanceCRM.Membership
{
    using Serenity.ComponentModel;
    using Serenity.Services;

    [FormScript("Membership.Login")]
    [BasedOnRow(typeof(Administration.UserRow))]
    public class LoginRequest : ServiceRequest
    {
        [Placeholder("Enter username here")]
        public string Username { get; set; }
        [PasswordEditor, Placeholder("Enter password here"), Required(true)]
        public string Password { get; set; }
        [Hidden]
        public string Type { get; set; }
    }
}