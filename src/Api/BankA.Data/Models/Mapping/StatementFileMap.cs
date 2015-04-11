using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BankA.Data.Models.Mapping
{
    public class StatementFileMap : EntityTypeConfiguration<StatementFileTable>
    {
        public StatementFileMap()
        {
            // Primary Key
            this.HasKey(t => t.FileID);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FileContent)
                .IsRequired();

            this.Property(t => t.ContentType)
                .IsRequired();

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
            this.ToTable("StatementFile");
            this.Property(t => t.FileID).HasColumnName("FileID");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileContent).HasColumnName("FileContent");
            this.Property(t => t.ContentType).HasColumnName("ContentType");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.ChangedOn).HasColumnName("ChangedOn");
            this.Property(t => t.ChangedBy).HasColumnName("ChangedBy");
            //this.Property(t => t.RowVersion).HasColumnName("RowVersion");
        }
    }
}
