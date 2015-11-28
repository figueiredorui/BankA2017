using BankA.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Data.Contexts
{
    class BankASeed
    {
        public static void Seed(BankAContext context)
        {
            context.Set<BankVersion>().Add(new BankVersion() { Version = "1.0.0" });
            context.Set<BankAccount>().Add(new BankAccount() { BankName = "HSBC", Description = "Demo Account" });

            context.Set<BankFile>().Add(new BankFile() { AccountID = 1, FileContent = new byte[10], FileName = "statement.csv", ContentType = "csv" });

            context.Set<BankTransactionRule>().Add(new BankTransactionRule() { Description = "ABC", Tag = "Groceries", TagGroup = "Food"});

            for (int i = 12; i >= 0; i--)
            {
                var debitList = new List<decimal>() { };
                var date = DateTime.Now.AddMonths(-i);
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(4), Tag = "School", Description = "School Fee", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(6), Tag = "Utilities", Description = "City Power & Light", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(7), Tag = "Groceries", Description = "Supermarket ABC", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(7), Tag = "Groceries", Description = "Grocery Store", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(8), Tag = "Entertainment", Description = "Southridge Video", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(10), Tag = "Phone", Description = "The Phone Company", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(12), Tag = "Income", Description = "John's Paycheck", DebitAmount = 0, CreditAmount = 1000.10m, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(14), Tag = "Utilities", Description = "Water Company", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(16), Tag = "Insurance", Description = "Humongous Insurance", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(17), Tag = "Training", Description = "Fine Art Classes", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(21), Tag = "Groceries", Description = "T Stores", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(23), Tag = "Fuel", Description = "Petrol Station", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(24), Tag = "Income", Description = "Pat's Paycheck", DebitAmount = 0, CreditAmount = RandomNonNegativeDecimal(new Random(), 5, 2), FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(25), Tag = "Loan", Description = "House Loan", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(26), Tag = "Insurance", Description = "Car Insurance", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(27), Tag = "Groceries", Description = "Grocery Store", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(29), Tag = "Gym", Description = "Woodgrove Gym", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });
                context.Set<BankTransaction>().Add(new BankTransaction() { AccountID = 1, TransactionDate = date.AddDays(30), Tag = "Entertainment", Description = "Dinner & Movie", DebitAmount = RandomNonNegativeDecimal(new Random(), 4, 2), CreditAmount = 0, FileID = 1 });

            }

        }

        static decimal RandomNonNegativeDecimal(Random randomNumberGenerator, int precision, int scale)
        {
            if (randomNumberGenerator == null)
                throw new ArgumentNullException("randomNumberGenerator");
            if (!(precision >= 1 && precision <= 28))
                throw new ArgumentOutOfRangeException("precision", precision, "Precision must be between 1 and 28.");
            if (!(scale >= 0 && scale <= precision))
                throw new ArgumentOutOfRangeException("scale", precision, "Scale must be between 0 and precision.");

            Decimal d = 0m;
            for (int i = 0; i < precision; i++)
            {
                int r = randomNumberGenerator.Next(0, 10);
                d = d * 10m + r;
            }
            for (int s = 0; s < scale; s++)
            {
                d /= 10m;
            }
            return d;
        }
    }
}
