using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using BankA.Data.Models;
using SQLite.CodeFirst;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BankA.Data.Contexts
{
    public partial class BankAContext : DbContext
    {
        public BankAContext()
            : base("name=BankAContext")
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BankStatementFile> BankStatementFiles { get; set; }
        public virtual DbSet<BankTransaction> BankTransactions { get; set; }
        public virtual DbSet<BankVersion> BankVersions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<BankAccount>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<BankAccount>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<BankAccount>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<BankAccount>()
                .HasMany(e => e.BankTransactions)
                .WithRequired(e => e.BankAccount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BankStatementFile>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<BankTransaction>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<BankTransaction>()
                .Property(e => e.Tag)
                .IsUnicode(false);

            //modelBuilder.Entity<BankTransaction>()
            //    .Property(e => e.RowVersion)
            //    .IsFixedLength();
            modelBuilder.Entity<BankTransaction>().Ignore(e => e.RowVersion);
        }

       
        public override int SaveChanges()
        {
            try
            {
                var retVal = base.SaveChanges();
                return retVal;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    string entityName = validationResult.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in validationResult.ValidationErrors)
                    {
                        errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                    }
                }

                throw new Exception(String.Join("\n", errorMessages.ToArray()));
            }
            catch (Exception)
            {
                throw;
            }
        }

       

        
    }
}
