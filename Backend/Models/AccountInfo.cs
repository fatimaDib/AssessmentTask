namespace Project.Models
{
    public class AccountInfo
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
