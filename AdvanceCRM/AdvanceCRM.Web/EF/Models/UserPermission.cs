using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AdvanceCRM.Web.EF.Models
{
    public class UserPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserPermissionId { get; set; }
        public int UserId { get; set; }
        public string PermissionKey { get; set; }
        public bool Granted { get; set; }

        public User User { get; set; } // Navigation property
    }
}