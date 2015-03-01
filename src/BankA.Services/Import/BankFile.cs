using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Import
{
    [DelimitedRecord(",")]
    class BankFile
    {
    }

    [DelimitedRecord(",")]
    class FileHSBC : BankFile
    {
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime TransactionDate;

        public string Description;

        public decimal Amount;
    }

    [DelimitedRecord(",")]
    class FileLLOYDS : BankFile
    {
        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        public DateTime TransactionDate;

        public string Type;
        public string SortCode;
        public string AccountNo;

        public string Description;

        [FieldNullValue(typeof(decimal), "0")]
        public decimal DebitAmount;

        [FieldNullValue(typeof(decimal), "0")]
        public decimal CreditAmount;

        [FieldNullValue(typeof(decimal), "0")]
        public decimal Balance;

    }
}
