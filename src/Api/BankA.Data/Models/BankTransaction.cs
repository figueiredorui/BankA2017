namespace BankA.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BankTransaction")]
    public partial class BankTransaction
    {
        public int ID { get; set; }

        public int AccountID { get; set; }

        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal CreditAmount { get; set; }

        [StringLength(50)]
        public string Tag { get; set; }

        public bool IsTransfer { get; set; }

        public int? FileID { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ChangedOn { get; set; }

        [StringLength(50)]
        public string ChangedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}
