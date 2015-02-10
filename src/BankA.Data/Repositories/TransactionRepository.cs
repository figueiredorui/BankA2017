using BankA.Data.Contexts;
using BankA.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Data.Repositories
{
    public class TransactionRepository : RepositoryBase<BankTransactionTable>
    {

        public void AddBatch(List<BankTransactionTable> transactionLst)
        {
            using (var ctx = new BankAContext())
            {
                foreach (var trans in transactionLst)
                {
                    var accountTrans = ctx.Transactions.SingleOrDefault(q => q.TransationDate == trans.TransationDate
                                                                                       && q.Description == trans.Description
                                                                                       && q.DebitAmount == trans.DebitAmount
                                                                                       && q.CreditAmount == trans.CreditAmount);
                    if (accountTrans == null)
                        ctx.Transactions.Add(trans);
                }
                
                ctx.SaveChanges();
            }
        }
    }
}
