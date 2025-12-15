using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace AdvanceCRM.Web.EF.Models
{ 

    [Table("City")]
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("CityName")]
        public string CityName { get; set; }

        [ForeignKey("Country")]
        [Column("CountryID")]
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}