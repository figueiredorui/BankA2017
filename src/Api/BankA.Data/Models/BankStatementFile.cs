namespace BankA.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankStatementFile")]
    public partial class BankStatementFile
    {
        [Key]
        public long FileID { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(2147483647)]
        public byte[] FileContent { get; set; }

        [Required]
        [StringLength(50)]
        public string ContentType { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ChangedOn { get; set; }

        [StringLength(50)]
        public string ChangedBy { get; set; }

        [Required]
        [MaxLength(2147483647)]
        public byte[] RowVersion { get; set; }
    }
}
