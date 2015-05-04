using BankA.Data.Contexts;
using BankA.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Data.Repositories
{
    public class TransactionRepository : Repository<BankTransaction>
    {

        
        public void AddBatch(List<BankTransaction> transactionLst)
        {
            transactionLst = transactionLst.OrderBy(o => o.TransactionDate).ToList();    
            using (var ctx = new BankAContext())
            {
                foreach (var trans in transactionLst)
                {
                    var accountTrans = ctx.BankTransactions.SingleOrDefault(q => q.TransactionDate == trans.TransactionDate
                                                                                       //&& q.Description == trans.Description
                                                                                       && q.DebitAmount == trans.DebitAmount
                                                                                       && q.CreditAmount == trans.CreditAmount);
                    if (accountTrans == null)
                        ctx.BankTransactions.Add(trans);
                }
                
                ctx.SaveChanges();
            }
        }

        public List<string> GetTags()
        {
            using (var ctx = new BankAContext())
            {
                return ctx.BankTransactions.Select(o => o.Tag).Distinct().ToList();
            }
        }
    }
}
