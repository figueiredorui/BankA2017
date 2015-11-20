using BankA.Data.Contexts;
using BankA.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Data.Repositories
{
    public class TransactionRuleRepository : Repository<BankTransactionRule>
    {
        public List<string> GetTags()
        {
            using (var ctx = new BankAContext())
            {
                return ctx.BankTransactions.Select(o => o.Tag).Distinct().ToList();
            }
        }

        public List<string> GetGroups()
        {
            using (var ctx = new BankAContext())
            {
                return ctx.BankTransactions.Select(o => o.TagGroup).Distinct().ToList();
            }
        }
    }
}
