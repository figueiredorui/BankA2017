using BankA.Data.Entities;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Data.Contexts
{
    public class BankASQLiteContextInitializer : SqliteCreateDatabaseIfNotExists<BankAContext>
    {
        public BankASQLiteContextInitializer(string connectionString, DbModelBuilder modelBuilder)
            : base(connectionString, modelBuilder) { }

        protected override void Seed(BankAContext context)
        {
            context.Set<BankVersion>().Add(new BankVersion() { Version = "1.0.0" });
            context.Set<BankAccount>().Add(new BankAccount() { BankName = "HSBC", Description = "Demo Account" });

            for (int i = 12; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(4), Tag = "School", Description = "School Registration", DebitAmount = 225.12m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(6), Tag = "Utilities", Description = "City Power & Light", DebitAmount = 73.22m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(7), Tag = "School", Description = "School supplies", DebitAmount = 38.40m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(7), Tag = "Groceries", Description = "Grocery Store", DebitAmount = 40.04m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(8), Tag = "Entertainment", Description = "Southridge Video", DebitAmount = 7.10m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(10), Tag = "Phone", Description = "The Phone Company", DebitAmount = 24.12m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(12), Tag = "Income", Description = "John's Paycheck", DebitAmount = 0, CreditAmount = 2000.10m });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(14), Tag = "Housing", Description = "Woodgrove Bank", DebitAmount = 100.50m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(16), Tag = "Housing", Description = "Humongous Insurance", DebitAmount = 210.10m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(17), Tag = "School", Description = "School of Fine Art", DebitAmount = 800.66m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(20), Tag = "Credit Cards", Description = "Woodgrove Bank", DebitAmount = 75.50m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(21), Tag = "Savings", Description = "Woodgrove Bank", DebitAmount = 100.00m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(23), Tag = "Phone", Description = "Consolidated Messenger", DebitAmount = 80.850m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(24), Tag = "Income", Description = "Pat's Paycheck", DebitAmount = 0, CreditAmount = 750.50m });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(25), Tag = "Housing", Description = "Woodgrove Bank", DebitAmount = 201.10m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(26), Tag = "Insurance", Description = "Humongous Insurance", DebitAmount = 180.12m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(27), Tag = "Groceries", Description = "Grocery Store", DebitAmount = 180.45m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(29), Tag = "Credit Cards", Description = "Woodgrove Bank", DebitAmount = 240.34m, CreditAmount = 0 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(30), Tag = "Entertainment", Description = "Dinner & Movie", DebitAmount = 100.43m, CreditAmount = 0 });

            }
        
        }
    }
}
