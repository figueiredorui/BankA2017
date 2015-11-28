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
    public class BankASqliteCreateDatabaseIfNotExists : SqliteCreateDatabaseIfNotExists<BankAContext>
    {
        public BankASqliteCreateDatabaseIfNotExists(DbModelBuilder modelBuilder)
       : base(modelBuilder)
        {

        }
        protected override void Seed(BankAContext context)
        {
            BankASeed.Seed(context);
        }
    }
}
