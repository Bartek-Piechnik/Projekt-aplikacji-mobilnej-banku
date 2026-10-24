using SQLite;

namespace BankSQLite.Models;

public class TransactionsItem
{
    [PrimaryKey, AutoIncrement] public int ID { get; set; }
    public int SenderId { get; set; }
    public string SenderAccount { get; set; } = string.Empty;
    public string SenderNameAndSurname { get; set; } = string.Empty;
    public int RecipientId { get; set; }
    public string RecipientsAccount { get; set; } = string.Empty;
    public string RecipientNameAndSurname { get; set; } = string.Empty;
    public string RecipientsPhoneNumber { get; set; } = string.Empty;
    public string TransferReference { get; set; } = string.Empty;
    public decimal TransferAmount { get; set; }
    public DateTime DateOfTransfer { get; set; }
    public string TransferType { get; set; } = string.Empty;
}