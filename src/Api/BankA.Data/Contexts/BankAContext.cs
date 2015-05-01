namespace BankA.Data.Contexts
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BankA.Data.Models;
    using SQLite.CodeFirst;

    public partial class BankAContext : DbContext
    {
        public BankAContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BankStatementFile> BankStatementFiles { get; set; }
        public virtual DbSet<BankTransaction> BankTransactions { get; set; }
        public virtual DbSet<BankVersion> BankVersions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<BankAccount>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<BankAccount>()
                .HasMany(e => e.BankTransactions)
                .WithRequired(e => e.BankAccount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BankTransaction>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<BankTransaction>()
                .Property(e => e.DebitAmount)
                .HasPrecision(53, 0);

            modelBuilder.Entity<BankTransaction>()
                .Property(e => e.CreditAmount)
                .HasPrecision(53, 0);

            modelBuilder.Entity<BankTransaction>()
                .Property(e => e.Tag)
                .IsUnicode(false);

            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<BankAContext>(Database.Connection.ConnectionString, modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
