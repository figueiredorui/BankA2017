namespace BankA.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankVersion")]
    public partial class BankVersion
    {
        [Key]
        [StringLength(50)]
        public string Version { get; set; }
    }
}
