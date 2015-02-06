using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BankA.Data.Models.Mapping
{
    public class AccountMap : EntityTypeConfiguration<AccountTable>
    {
        public AccountMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountID);

            // Properties
            this.Property(t => t.AccountID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BankName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Account");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BankName).HasColumnName("BankName");
        }
    }
}
