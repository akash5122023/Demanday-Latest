using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvanceCRM.Web.EF.Models
{

    [Table("SapDatabases")]
    public class SapDatabase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("ServerType")]
        public string ServerType { get; set; }

        [Column("ServerName")]
        public string ServerName { get; set; }

        [Column("ODBCServer")]
        public string ODBCServer { get; set; }

        [Column("DBServerType")]
        public string DBServerType { get; set; }

        [Column("LicenseServer")]
        public string LicenseServer { get; set; }

        [Column("CompanyDB")]
        public string CompanyDB { get; set; }

        [Column("DBUserName")]
        public string DBUserName { get; set; }

        [Column("DBPassword")]
        public string DBPassword { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("ServiceLayerURL")]
        public string ServiceLayerURL { get; set; }

        [Column("ServiceLayerVersion")]
        public string ServiceLayerVersion { get; set; }

        [Column("DBDriver")]
        public string DBDriver { get; set; }
    }
}
