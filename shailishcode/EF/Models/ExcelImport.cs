using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AdvanceCRM.Web.EF.Models
{

    [Table("ExcelImports")]
    public class ExcelImport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("ExcelImportTemplate")]
        [Column("TemplateId")]
        public long TemplateId { get; set; }

        [Column("MasterTableName")]
        [MaxLength(100)]
        public string MasterTableName { get; set; }

        [Column("FieldMappings")]
        public string FieldMappings { get; set; }

        [Column("ImportedExcelFile")]
        [MaxLength(100)]
        public string ImportedExcelFile { get; set; }

        [Column("ExcelImportStatus")]
        public int ExcelImportStatus { get; set; }

        [Column("ImportedData")]
        public string ImportedData { get; set; }

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

        public virtual ExcelImportTemplate ExcelImportTemplate { get; set; }
    }
}
