namespace BankA.Data.Contexts
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BankA.Data.Entities;
    using SQLite.CodeFirst;

    public partial class BankAContext : DbContext
    {
        public BankAContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BankFile> BankFiles { get; set; }
        public virtual DbSet<BankTransaction> BankTransactions { get; set; }
        public virtual DbSet<BankTransactionRule> BankTransactionRules { get; set; }
        public virtual DbSet<BankVersion> BankVersions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>()
                .HasKey(e => e.AccountID)
                .HasMany(e => e.BankTransactions)
                .WithRequired(e => e.BankAccount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BankAccount>()
                .HasMany(e => e.BankStatementFiles)
                .WithRequired(e => e.BankAccount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BankTransaction>()
                .HasKey(e => e.ID)
                .Property(e => e.DebitAmount)
                .HasPrecision(30, 2);

            modelBuilder.Entity<BankTransaction>()
                .Property(e => e.CreditAmount)
                .HasPrecision(30, 2);

            modelBuilder.Entity<BankFile>()
                .HasKey(e => e.FileID)
                .HasMany(e => e.BankTransactions)
                .WithRequired(e => e.BankStatementFile)
                .WillCascadeOnDelete(false);

            if (Database.Connection is System.Data.SQLite.SQLiteConnection)
            {
                var sqliteConnectionInitializer = new BankASqliteCreateDatabaseIfNotExists(modelBuilder);
                Database.SetInitializer(sqliteConnectionInitializer);
            }
            if (Database.Connection is System.Data.SqlClient.SqlConnection)
            {
                var sqliteConnectionInitializer = new BankACreateDatabaseIfNotExists();
                Database.SetInitializer(sqliteConnectionInitializer);
            }

        }
    }
}
