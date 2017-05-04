using BankA.Data.Contexts;
using BankA.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Data.Repositories
{
    public class TransactionRepository : Repository<BankTransaction>
    {
        public void AddTransactions(BankFile statementFile, List<BankTransaction> transactionLst)
        {
            using (var ctx = new BankAContext())
            {
                ctx.BankFiles.Add(statementFile);

                foreach (var transaction in transactionLst.OrderBy(o => o.TransactionDate).ToList())
                {
                    var exists = ctx.BankTransactions.Any(q => q.TransactionDate == transaction.TransactionDate
                                                            && q.AccountID == statementFile.AccountID
                                                            && q.DebitAmount == transaction.DebitAmount
                                                            && q.CreditAmount == transaction.CreditAmount);
                    if (exists == false)
                    {
                        transaction.AccountID = statementFile.AccountID;
                        transaction.FileID = statementFile.FileID;

                        ctx.BankTransactions.Add(transaction);
                    }
                }
                
                ctx.SaveChanges();
            }
        }


        
    }


}
