using API.Services.Services.Model;

namespace API.Data.Entities
{
    public class Transaction : EntityBase
    {
        public string UserId { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Wallet Wallet { get; set; }
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
    public enum Status
    {
        Approved,
        Pending,
        Rejected
    }
}
