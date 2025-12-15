using System.Collections.Generic;
using System.ComponentModel;
using MailChimp.Net.Core;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Modules.MailChimp.Models
{
    public class ModelSubscriber
    {
        public ModelSubscriber()
        {
            this.MergeFields = new Dictionary<string, object>();
            this.Interests = new Dictionary<string, bool>();
            //this.Status = StatusSubscriber.Undefined;
            //this.StatusIfNew = StatusSubscriber.Pending;
            this.Status = StatusSubscriber.Subscribed;
            this.StatusIfNew = StatusSubscriber.Subscribed;
        }
        /// <summary>
        /// Gets or sets the email address.



        [Required(ErrorMessage = "Please Enter Email Address")]
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the First Name.        
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the Last Name.
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email client.

        public string EmailClient { get; set; }

        /// <summary>
        /// Gets or sets the email type.
        public string EmailType { get; set; }

        /// <summary>
        /// Gets or sets the id.
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the interests.
        public Dictionary<string, bool> Interests { get; set; }

        /// <summary>
        /// Gets or sets the ip opt.
        public string IpOpt { get; set; }

        /// <summary>
        /// Gets or sets the ip signup.
        public string IpSignup { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the last changed.
        /// </summary>
        public string LastChanged { get; set; }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        //[JsonProperty("_links")]
        //public IEnumerable<Link> Links { get; set; }

        /// <summary>
        /// Gets or sets the list id.

        public string ListId { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Gets the location.
        /// </summary>
        //[JsonProperty("location")]
        //public Location Location { get; set; }

        /// <summary>
        /// Gets or sets the member rating.
        public int MemberRating { get; set; }

        /// <summary>
        /// Gets or sets the merge fields.
        public Dictionary<string, object> MergeFields { get; set; }

        /// <summary>
        /// Gets or sets the last Note.
        /// </summary>
        //[JsonProperty("last_note")]
        //public MemberLastNote LastNote { get; set; }

        /// <summary>
        /// Gets or sets the stats.
        /// </summary>
        //[JsonProperty("stats")]
        //public MemberStats Stats { get; set; }

        /// <summary>
        /// Sets the member's status unless they are new.  Then you need to use the <see cref="StatusIfNew"/> property.  Default value is <see cref="Status.Undefined"/>  
        /// </summary>

        [JsonConverter(typeof(StringEnumDescriptionConverter))]
        public StatusSubscriber Status { get; set; }

        [JsonConverter(typeof(StringEnumDescriptionConverter))]
        public StatusSubscriber StatusIfNew { get; set; }

        public string UnsubscribeReason { get; set; }

        /// <summary>
        /// Gets or sets the timestamp opt.
        public string TimestampOpt { get; set; }

        /// <summary>
        /// Gets or sets the timestamp signup.
        public string TimestampSignup { get; set; }

        /// <summary>
        /// Gets or sets the unique email id.
        public string UniqueEmailId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether vip.
        public bool Vip { get; set; }
        public IEnumerable<ListDetails> Details { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }



    }
    public class ListDetails
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }

    public enum StatusSubscriber
    {
        /// <summary>
        /// The subscribed.
        /// </summary>
        [Description("subscribed")]
        Subscribed,

        /// <summary>
        /// The unsubscribed.
        /// </summary>
        [Description("unsubscribed")]
        Unsubscribed,

        /// <summary>
        /// The cleaned.
        /// </summary>
        [Description("cleaned")]
        Cleaned,

        /// <summary>
        /// The pending.
        /// </summary>
        [Description("pending")]
        Pending,


        [Description("transactional")]
        Transactional,

        /// <summary>
        /// The undefined.
        /// </summary>
        [Description("")]
        Undefined

    }

}