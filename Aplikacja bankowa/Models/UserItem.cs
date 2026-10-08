using SQLite;

namespace BankSQLite.Models;

public class UserItem
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public decimal AccountBalance { get; set; }
}
