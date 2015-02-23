using AutoMapper;
using BankA.Data.Models;
using BankA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Transactions
{
    public static class TransactionExtensions
    {
        

        public static Transaction ToModel(this BankTransactionTable table)
        {
            return new Transaction()
            {
                ID = table.ID,
                AccountID = table.AccountID,
                TransactionDate = table.TransactionDate,
                Description = table.Description,
                DebitAmount = table.DebitAmount,
                CreditAmount = table.CreditAmount,
                Amount = table.CreditAmount - table.DebitAmount,
                Tag = table.Tag
            };
        }

        public static List<Transaction> ToModel(this List<BankTransactionTable> tableLst)
        {
            var lst = new List<Transaction>();
            tableLst.ForEach(i => lst.Add(i.ToModel()));
            return lst;
        }

        public static BankTransactionTable ToTable(this Transaction model)
        {
            return new BankTransactionTable()
            {
                ID = model.ID,
                AccountID = model.AccountID,
                TransactionDate = model.TransactionDate,
                Description = model.Description,
                DebitAmount = model.Amount < 0 ? -model.Amount : 0,
                CreditAmount = model.Amount > 0 ? model.Amount : 0,
                Tag = model.Tag
            };
        }

    }
}
