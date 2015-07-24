namespace BankA.Models.Transactions
{
    public class TransactionRule
    {
        public TransactionRule()
        {
        }

        public int RuleID { get; set; }

        public string Description { get; set; }

        public string Tag { get; set; }
        public string TagGroup { get; set; }

        public bool IsTransfer { get; set; }
    }
}
