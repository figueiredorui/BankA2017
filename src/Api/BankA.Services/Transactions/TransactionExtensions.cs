using AutoMapper;
using BankA.Data.Models;
using BankA.Models;
using BankA.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Transactions
{
    public static class TransactionExtensions
    {
        public static Transaction ToModel(this BankTransaction table)
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
                IsTransfer = table.IsTransfer,
                Tag = table.Tag
            };
        }

        public static List<Transaction> ToModel(this List<BankTransaction> tableLst)
        {
            var lst = new List<Transaction>();
            tableLst.ForEach(i => lst.Add(i.ToModel()));
            return lst;
        }

        public static BankTransaction ToTable(this Transaction model)
        {
            return new BankTransaction()
            {
                ID = model.ID,
                AccountID = model.AccountID,
                TransactionDate = model.TransactionDate,
                Description = model.Description,
                DebitAmount = model.Amount < 0 ? -model.Amount : 0,
                CreditAmount = model.Amount > 0 ? model.Amount : 0,
                IsTransfer = model.IsTransfer,
                Tag = model.Tag
            };
        }
    }
}
