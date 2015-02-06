using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BankA.Data.Models.Mapping
{
    public class TransactionMap : EntityTypeConfiguration<TransactionTable>
    {
        public TransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.Tag)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Transaction");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.TransationDate).HasColumnName("TransationDate");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DebitAmount).HasColumnName("DebitAmount");
            this.Property(t => t.CreditAmount).HasColumnName("CreditAmount");
            this.Property(t => t.Tag).HasColumnName("Tag");
        }
    }
}
