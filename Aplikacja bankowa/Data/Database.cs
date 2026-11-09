using Aplikacja_bankowa.Helpers;
using Aplikacja_bankowa.Models;
using SQLite;

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
        return await database.Table<UsersItem>().Where(user => user.ID == id).FirstOrDefaultAsync();
    }

    public async Task<UsersItem> GetItemByEmailAsync(string email)
    {
        await Init();
        return await database.Table<UsersItem>().Where(user => user.Email == email).FirstOrDefaultAsync();
    }

    public async Task<UsersItem> GetItemByAccountNumberAsync(string accountNumber)
    {
        await Init();
        return await database.Table<UsersItem>().Where(user => user.AccountNumber == accountNumber).FirstOrDefaultAsync();
    }

    public async Task<UsersItem> GetItemByPhoneNumberAsync(string phoneNumber)
    {
        await Init();
        return await database.Table<UsersItem>().Where(user => user.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
    }
    
    public async Task<TransactionsItem> GetTransactionsAsync(int id)
    {
        await Init();
        return await database.Table<TransactionsItem>().Where(transaction => transaction.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(UsersItem user)
    {
        await Init();
        if (user.ID != 0)
        {
            return await database.UpdateAsync(user);
        }
        else
        {
            return await database.InsertAsync(user);
        }
    }
    
    public async Task<int> SaveTransactionsAsync(TransactionsItem transaction)
    {
        await Init();
        if (transaction.ID != 0)
        {
            return await database.UpdateAsync(transaction);
        }
        else
        {
            return await database.InsertAsync(transaction);
        }
    }

    public async Task<int> DeleteItemAsync(UsersItem user)
    {
        await Init();
        return await database.DeleteAsync(user);
    }

    public async Task<int> DeleteTransactionsAsync(TransactionsItem transaction)
    {
        await Init();
        return await database.DeleteAsync(transaction);
    }
    
    public async Task ExecuteTransferAsync(TransactionsItem transaction)
    {
        await Init();

        var sender = await GetItemAsync(transaction.SenderId);
        var recipient = await GetItemAsync(transaction.RecipientId);

        if (sender == null || recipient == null)
        { 
            throw new Exception("Nie znaleziono użytkownika.");
        }

        if (transaction.SenderId == transaction.RecipientId)
        {
            throw new Exception("Nie można wykonywać transakcji z samym sobą!");
        }

        if (transaction.TransferAmount <= 0)
        {
            throw new Exception("Kwota musi być większa od zera!");
        }
        
        if (transaction.TransferAmount > sender.PaymentLimit)
        {
            throw new Exception($"Przekroczono limit płatności! Maksymalna kwota jednej transakcji: {sender.PaymentLimit} PLN.");
        }

        if (sender.AccountBalance < transaction.TransferAmount)
        {
            throw new Exception("Brak środków.");
        }

        await database.RunInTransactionAsync(conn =>
        {
            sender.AccountBalance -= transaction.TransferAmount;
            recipient.AccountBalance += transaction.TransferAmount;

            conn.Update(sender);
            conn.Update(recipient);
            conn.Insert(transaction);
        });
    }

    public async Task<List<TransactionsItem>> GetUserTransactionsAsync(int userId)
    {
        await Init();

        return await database.Table<TransactionsItem>()
            .Where(transaction => transaction.SenderId == userId || transaction.RecipientId == userId)
            .OrderByDescending(transaction => transaction.DateOfTransfer)
            .ToListAsync();
    }
    public async Task<List<TransactionsItem>> GetUserTransactionsPreviewAsync(int userId)
    {
        await Init();

        return await database.Table<TransactionsItem>()
            .Where(transaction => transaction.SenderId == userId || transaction.RecipientId == userId)
            .OrderByDescending(transaction => transaction.DateOfTransfer)
            .Take(3)
            .ToListAsync();
    }
    public async Task DepositAsync(TransactionsItem transaction)
    {
        await ExecuteCashMachineOperationAsync(transaction, true);
    }

    public async Task WithdrawAsync(TransactionsItem transaction)
    {
        await ExecuteCashMachineOperationAsync(transaction, false);
    }

    private async Task ExecuteCashMachineOperationAsync(TransactionsItem transaction, bool isDeposit)
    {
        await Init();
        
        if (transaction.TransferAmount <= 0)
        {
            throw new Exception("Nieprawidłowa kwota.");
        }
        int userId = isDeposit ? transaction.RecipientId : transaction.SenderId;

        if (userId == 0)
        {
            throw new Exception("Nie znaleziono użytkownika.");
        }

        var user = await GetItemAsync(userId);

        await database.RunInTransactionAsync(conn =>
        {
            if (user == null)
            {
                throw new Exception("Nie znaleziono użytkownika.");
            }

            if (!isDeposit && user.AccountBalance < transaction.TransferAmount)
            {
                throw new Exception("Brak wystarczających środków na koncie.");
            }

            if (isDeposit)
            {
                user.AccountBalance += transaction.TransferAmount;

                transaction.TransferType = "Wpłata";
                transaction.TransferTitle = "Wpłata gotówki";
            }
            else
            {
                user.AccountBalance -= transaction.TransferAmount;

                transaction.TransferType = "Wypłata";
                transaction.TransferTitle = "Wypłata gotówki";
            }

            if (transaction.DateOfTransfer == default)
            {
                transaction.DateOfTransfer = DateTime.UtcNow;
            }

            conn.Update(user);
            conn.Insert(transaction);
        });
    }
}