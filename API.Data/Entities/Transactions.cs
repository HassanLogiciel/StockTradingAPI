using API.Services.Services.Model;

namespace API.Data.Entities
{
    public class Transaction : EntityBase
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public StatusDb Status { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
}
