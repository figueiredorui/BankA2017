using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BankA.Data.Models.Mapping
{
    public class BankTransactionMap : EntityTypeConfiguration<BankTransactionTable>
    {
        public BankTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.Tag)
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(50);

            this.Property(t => t.ChangedBy)
                .HasMaxLength(50);

            this.Ignore(t => t.RowVersion);
            //this.Property(t => t.RowVersion)
            //    .IsRequired()
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BankTransaction");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DebitAmount).HasColumnName("DebitAmount");
            this.Property(t => t.CreditAmount).HasColumnName("CreditAmount");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.IsTransfer).HasColumnName("IsTransfer");
            this.Property(t => t.FileID).HasColumnName("FileID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.ChangedOn).HasColumnName("ChangedOn");
            this.Property(t => t.ChangedBy).HasColumnName("ChangedBy");
            //this.Property(t => t.RowVersion).HasColumnName("RowVersion");


            this.HasRequired(t => t.Account)
                .WithMany(o => o.Transactions)
                .HasForeignKey(t => t.AccountID);
        }
    }
}
