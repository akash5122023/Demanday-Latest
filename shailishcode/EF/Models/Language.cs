using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AdvanceCRM.Web.EF.Models
{
    [Table("Languages")]
    public class Language
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string LanguageId { get; set; }

        [Required]
        [StringLength(50)]
        public string LanguageName { get; set; }
    }


}
