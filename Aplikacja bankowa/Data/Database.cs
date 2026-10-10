using SQLite;
using BankSQLite.Models;

namespace BankSQLite.Data;

public class Database
{
    SQLiteAsyncConnection database;

    async Task Init()
    {
        if (database is not null)
            return;

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
}