using BankA.Data.Entities;
using BankA.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Statements
{
    public static class StatementMapExtensions
    {
        public static StatementFile ToModel(this BankStatementFile table)
        {
            return new StatementFile()
            {
                FileID = table.FileID,
                FileName = table.FileName,
                CreatedOn = table.CreatedOn,
                Account = table.BankAccount.Description
            };
        }

        public static List<StatementFile> ToModel(this List<BankStatementFile> tableLst)
        {
            var lst = new List<StatementFile>();
            tableLst.ForEach(i => lst.Add(i.ToModel()));
            return lst;
        }

        public static BankStatementFile ToTable(this StatementFile model)
        {
            return new BankStatementFile()
            {
                FileID = model.FileID,
                FileName = model.FileName,
                CreatedOn = model.CreatedOn,
                //AccountID = model.AccountID
            };
        }
    }
}
