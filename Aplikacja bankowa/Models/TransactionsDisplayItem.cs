using SQLite;

namespace BankSQLite.Models;

public class TransactionDisplayItem
{
    public TransactionsItem Transaction { get; set; }

    private int UserId { get; set; }

    public TransactionDisplayItem(TransactionsItem transaction, int userId)
    {
        Transaction = transaction;
        UserId = userId;
    }

    public string DisplayAmount => (Transaction.SenderId == UserId ? $"-{Transaction.TransferAmount:N2} zł": $"+{Transaction.TransferAmount:N2} zł");
    public Color AmountColor => (Transaction.SenderId == UserId ? Colors.Red : Colors.Green);
    public string TransferTitle => Transaction.TransferTitle;
    public string TransferType => Transaction.TransferType;
    public DateTime DateOfTransfer => Transaction.DateOfTransfer;
}