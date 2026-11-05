using SQLite;
using BankSQLite.Models;

namespace BankSQLite.Data;

public class Database
{
    SQLiteAsyncConnection database;

    async Task Init()
    {
        if (database is not null)
        {
            return;
        }

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await database.CreateTableAsync<UsersItem>();
        await database.CreateTableAsync<TransactionsItem>();
    }

    public async Task<List<UsersItem>> GetItemsAsync()
    {
        await Init();
        return await database.Table<UsersItem>().ToListAsync();
    }

    public async Task<List<TransactionsItem>> GetTransactionsAsync()
    {
        await Init();
        return await database.Table<TransactionsItem>().ToListAsync();
    }

    public async Task<UsersItem> GetItemAsync(int id)
    {
        await Init();
        return await database.Table<UsersItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<UsersItem> GetItemByEmailAsync(string email)
    {
        await Init();
        return await database.Table<UsersItem>().Where(i => i.Email == email).FirstOrDefaultAsync();
    }

    public async Task<UsersItem> GetItemByAccountNumberAsync(string accountNumber)
    {
        await Init();
        return await database.Table<UsersItem>().Where(i => i.AccountNumber == accountNumber).FirstOrDefaultAsync();
    }

    public async Task<UsersItem> GetItemByPhoneNumberAsync(string phoneNumber)
    {
        await Init();
        return await database.Table<UsersItem>().Where(i => i.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
    }
    
    public async Task<TransactionsItem> GetTransactionsAsync(int id)
    {
        await Init();
        return await database.Table<TransactionsItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(UsersItem item)
    {
        await Init();
        if (item.ID != 0)
        {
            return await database.UpdateAsync(item);
        }
        else
        {
            return await database.InsertAsync(item);
        }
    }
    
    public async Task<int> SaveTransactionsAsync(TransactionsItem item)
    {
        await Init();
        if (item.ID != 0)
        {
            return await database.UpdateAsync(item);
        }
        else
        {
            return await database.InsertAsync(item);
        }
    }

    public async Task<int> DeleteItemAsync(UsersItem item)
    {
        await Init();
        return await database.DeleteAsync(item);
    }

    public async Task<int> DeleteTransactionsAsync(TransactionsItem item)
    {
        await Init();
        return await database.DeleteAsync(item);
    }
    
    public async Task ExecuteTransferAsync(TransactionsItem item)
    {
        await Init();

        var sender = await GetItemAsync(item.SenderId);
        var recipient = await GetItemAsync(item.RecipientId);

        if (sender == null || recipient == null)
        { 
            throw new Exception("Nie znaleziono użytkownika.");
        }

        if (item.TransferAmount <= 0)
        {
            throw new Exception("Nieprawidłowa kwota.");
        }
        
        if (item.TransferAmount > sender.PaymentLimit)
        {
            throw new Exception($"Przekroczono limit płatności! Maksymalna kwota jednej transakcji: {sender.PaymentLimit} PLN.");
        }

        if (sender.AccountBalance < item.TransferAmount)
        {
            throw new Exception("Brak środków.");
        }

        await database.RunInTransactionAsync(conn =>
        {
            sender.AccountBalance -= item.TransferAmount;
            recipient.AccountBalance += item.TransferAmount;

            conn.Update(sender);
            conn.Update(recipient);
            conn.Insert(item);
        });
    }
    
    public async Task DepositAsync(TransactionsItem item)
    {
        await ExecuteCashMachineOperationAsync(item, true);
    }

    public async Task WithdrawAsync(TransactionsItem item)
    {
        await ExecuteCashMachineOperationAsync(item, false);
    }

    private async Task ExecuteCashMachineOperationAsync(TransactionsItem item, bool isDeposit)
    {
        await Init();

        var user = await GetItemAsync(item.SenderId);

        if (item.TransferAmount <= 0)
        {
            throw new Exception("Nieprawidłowa kwota.");
        }

        if (item.SenderId == 0)
        {
            throw new Exception("Nie znaleziono użytkownika.");
        }
        
        await database.RunInTransactionAsync(conn =>
        {
            if (user == null)
            {
                throw new Exception("Nie znaleziono użytkownika.");
            }

            if (!isDeposit && user.AccountBalance < item.TransferAmount)
            {
                throw new Exception("Brak wystarczających środków na koncie.");
            }

            if (isDeposit)
            {
                user.AccountBalance += item.TransferAmount;

                item.TransferType = "Wpłata";
                item.TransferTitle = "Wpłata gotówki";
            }
            else
            {
                user.AccountBalance -= item.TransferAmount;

                item.TransferType = "Wypłata";
                item.TransferTitle = "Wypłata gotówki";
            }

            item.RecipientId = 0;

            if (item.DateOfTransfer == default)
            {
                item.DateOfTransfer = DateTime.UtcNow;
            }

            conn.Update(user);
            conn.Insert(item);
        });
    }
}