using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AdvanceCRM.Web.EF.Models
{
    public class UserPreference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserPreferenceId { get; set; }
        public long UserId { get; set; }
        public string PreferenceType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public User User { get; set; } // Navigation property
    }
}