using BankA.Data.Contexts;
using BankA.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Data.Repositories
{
    public class AccountRepository : Repository<BankAccount>
    {
     
        public void DeleteAccountAndTransactions(int accountID)
        {
            using (var ctx = new BankAContext())
            {
                var transactions = ctx.BankTransactions.Where(q => q.AccountID == accountID);
                foreach (var item in transactions)
                    ctx.BankTransactions.Remove(item);

                var account = ctx.BankAccounts.Find(accountID);
                ctx.BankAccounts.Remove(account);

                ctx.SaveChanges();
            }


        }
   
    }
}
