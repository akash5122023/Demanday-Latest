using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AdvanceCRM.Web.EF.Models
{
    public class RolePermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public string PermissionKey { get; set; }

        public Role Role { get; set; } // Navigation property
    }
}