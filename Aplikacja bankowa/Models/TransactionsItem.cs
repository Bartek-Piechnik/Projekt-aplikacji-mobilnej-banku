using SQLite;

namespace BankSQLite.Models;

public class TransactionsItem
{
    [PrimaryKey, AutoIncrement] public int ID { get; set; }
    public int SenderId { get; set; }
    public int RecipientId { get; set; }
    public string TransferTitle { get; set; } = string.Empty;
    public decimal TransferAmount { get; set; }
    public DateTime DateOfTransfer { get; set; }
    public string TransferType { get; set; } = string.Empty;
}