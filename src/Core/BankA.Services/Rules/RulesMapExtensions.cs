using BankA.Data.Entities;
using BankA.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Rules
{
    public static class RulesMapExtensions
    {
        public static TransactionRule ToModel(this BankTransactionRule table)
        {
            return new TransactionRule()
            {
                RuleID = table.RuleID,
                Description = table.Description,
                Tag = table.Tag,
                TagGroup = table.TagGroup,
                IsTransfer = table.IsTransfer
            };
        }

        public static List<TransactionRule> ToModel(this List<BankTransactionRule> tableLst)
        {
            var lst = new List<TransactionRule>();
            tableLst.ForEach(i => lst.Add(i.ToModel()));
            return lst;
        }

        public static BankTransactionRule ToTable(this TransactionRule model)
        {
            return new BankTransactionRule()
            {
                RuleID = model.RuleID,
                Description = model.Description,
                Tag = model.Tag,
                TagGroup = model.TagGroup,
                IsTransfer = model.IsTransfer
            };
        }
    }
}
