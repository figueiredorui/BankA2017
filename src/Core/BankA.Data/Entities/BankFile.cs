namespace BankA.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankFile")]
    public partial class BankFile
    {
        [Key]
        public int FileID { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(2147483647)]
        public byte[] FileContent { get; set; }

        [Required]
        [StringLength(50)]
        public string ContentType { get; set; }

        public int AccountID { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ChangedOn { get; set; }

        [StringLength(50)]
        public string ChangedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }

        public virtual BankAccount BankAccount { get; set; }

    }
}
