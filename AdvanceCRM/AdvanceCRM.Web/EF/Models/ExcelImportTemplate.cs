using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvanceCRM.Web.EF.Models
{
    [Table("ExcelImportTemplates")]
    public class ExcelImportTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("TemplateName")]
        [MaxLength(100)]
        public string TemplateName { get; set; }

        [Column("MasterTableName")]
        [MaxLength(100)]
        public string MasterTableName { get; set; }

        [Column("TemplateExcelFile")]
        [MaxLength(100)]
        public string TemplateExcelFile { get; set; }

        [Column("ExcelMetadata")]
        [MaxLength(100)]
        public string ExcelMetadata { get; set; }

        [Column("ExcelSheet")]
        [MaxLength(100)]
        public string ExcelSheet { get; set; }

        [Column("FieldMappings")]
        public string FieldMappings { get; set; }

        [Column("Remarks")]
        [MaxLength(100)]
        public string Remarks { get; set; }

        [Column("InsertDate")]
        public DateTime InsertDate { get; set; }

        [Column("InsertUserId")]
        public int InsertUserId { get; set; }

        [Column("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [Column("UpdateUserId")]
        public int? UpdateUserId { get; set; }
    }

}
