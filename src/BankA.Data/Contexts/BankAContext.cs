using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using BankA.Data.Models;
using BankA.Data.Models.Mapping;

namespace BankA.Data.Contexts
{
    public partial class BankAContext : DbContext
    {
        static BankAContext()
        {
            Database.SetInitializer<BankAContext>(null);
        }

        public BankAContext()
            : base("Name=DefaultConnection")
        {
            
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

        public DbSet<BankAccountTable> Accounts { get; set; }
        public DbSet<BankTransactionTable> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BankAccountMap());
            modelBuilder.Configurations.Add(new BankTransactionMap());
        }

        
    }
}
