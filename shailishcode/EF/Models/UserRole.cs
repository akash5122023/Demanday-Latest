using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AdvanceCRM.Web.EF.Models
{
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public User User { get; set; } // Navigation property
        public Role Role { get; set; } // Navigation property
    }
}