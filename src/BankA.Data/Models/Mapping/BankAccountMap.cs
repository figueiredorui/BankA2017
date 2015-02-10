using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BankA.Data.Models.Mapping
{
    public class BankAccountMap : EntityTypeConfiguration<BankAccountTable>
    {
        public BankAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountID);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BankName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(50);

            this.Property(t => t.ChangedBy)
                .HasMaxLength(50);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BankAccount");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.ChangedOn).HasColumnName("ChangedOn");
            this.Property(t => t.ChangedBy).HasColumnName("ChangedBy");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");
        }
    }
}
