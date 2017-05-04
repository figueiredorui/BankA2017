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
    public class BankACreateDatabaseIfNotExists : CreateDatabaseIfNotExists<BankAContext>
    {
        

        protected override void Seed(BankAContext context)
        {
            BankASeed.Seed(context);

        }

    }
}
