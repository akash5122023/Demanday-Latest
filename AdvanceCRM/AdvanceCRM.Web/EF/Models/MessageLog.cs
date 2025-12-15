using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AdvanceCRM.Web.EF.Models
{

    public class MessageLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string ToNumber { get; set; }
        public short? FromMe { get; set; }
        public string FromNumber { get; set; }
        public string Type { get; set; }
        public string MediaId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Json { get; set; }
    }
}
