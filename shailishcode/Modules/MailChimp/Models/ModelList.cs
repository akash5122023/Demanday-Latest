using System;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Modules.MailChimp.Models
{
    public class ModelList
    {
        public string BeamerAddress { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Language { get; set; }
        public string Subject { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string ListCountry { get; set; }
        //public Collection<dynamic> Countries { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Phone { get; set; }
        public string State { get; set; }
        [DataType(DataType.PostalCode)]
        //[RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string Zip { get; set; }
        public DateTime DateCreated { get; set; }
        public bool EmailTypeOption { get; set; }
        public string Id { get; set; }
        public int ListRating { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string NotifyOnSubscribe { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string NotifyOnUnsubscribe { get; set; }
        public string PermissionReminder { get; set; }
        public double AvgSubRate { get; set; }
        public double AvgUnsubRate { get; set; }
        public int CampaignCount { get; set; }
        public string CampaignLastSent { get; set; }
        public int CleanedCount { get; set; }
        public int CleanedCountSinceSend { get; set; }
        public double ClickRate { get; set; }
        public string LastSubDate { get; set; }
        public string LastUnsubDate { get; set; }
        public int MemberCount { get; set; }
        public int MemberCountSinceSend { get; set; }
        public int MergeFieldCount { get; set; }
        public double OpenRate { get; set; }
        public double TargetSubRate { get; set; }
        public int UnsubscribeCount { get; set; }
        public int UnsubscribeCountSinceSend { get; set; }
        public string SubscribeUrlLong { get; set; }
        public string SubscribeUrlShort { get; set; }
        public bool UseArchiveBar { get; set; }
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Schema { get; set; }
        public string TargetSchema { get; set; }
        public int WebId { get; set; }
    }


}