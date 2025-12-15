using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AdvanceCRM.Web.EF.Models
{
    public class Exceptions
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public Guid GUID { get; set; }
        public string ApplicationName { get; set; }
        public string MachineName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Type { get; set; }
        public bool? IsProtected { get; set; }
        public string Host { get; set; }
        public string Url { get; set; }
        public string HTTPMethod { get; set; }
        public string IPAddress { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public int? StatusCode { get; set; }
        public string SQL { get; set; }
        public DateTime? DeletionDate { get; set; }
        public string FullJson { get; set; }
        public int? ErrorHash { get; set; }
        public int? DuplicateCount { get; set; }
    }


}
