using BankA.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankA.Services.Statements
{
    [AttributeUsage(AttributeTargets.Class)]
    class BankNameAttribute : Attribute
    {
        public BankEnum BankName { get; private set; }
        public BankNameAttribute(BankEnum bankName)
        {
            this.BankName = bankName;

        }
    }
}
