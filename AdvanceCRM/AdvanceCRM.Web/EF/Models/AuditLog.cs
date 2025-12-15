using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AdvanceCRM.Web.EF.Models
{
    [Table("AuditLog")]
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public long Id { get; set; }

        [Column("UserId")]
        [MaxLength(36)]
        public string UserId { get; set; }

        [Column("ActionType")]
        public int ActionType { get; set; }

        [Column("ActionDate")]
        public DateTime ActionDate { get; set; }

        [Column("TableName")]
        [MaxLength(100)]
        public string TableName { get; set; }

        [Column("EntityId")]
        [MaxLength(36)]
        public string EntityId { get; set; }

        [Column("Changes")]
        public string Changes { get; set; }

        [Column("IpAddress")]
        [MaxLength(100)]
        public string IpAddress { get; set; }

        [Column("SessionId")]
        [MaxLength(100)]
        public string SessionId { get; set; }

        [Column("RequestedURI")]
        [MaxLength(2000)]
        public string RequestedURI { get; set; }
    }
}
