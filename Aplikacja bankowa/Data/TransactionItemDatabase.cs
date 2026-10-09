using SQLite;
using BankSQLite.Models;

namespace BankSQLite.Data;

public class TransactionItemDatabase
{
    SQLiteAsyncConnection database;

    async Task Init()
    {
        if (database is not null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await database.CreateTableAsync<UserItem>();
    }

    public async Task<List<TransactionsItem>> GetItemsAsync()
    {
        await Init();
        return await database.Table<TransactionsItem>().ToListAsync();
    }

    public async Task<TransactionsItem> GetItemAsync(int id)
    {
        await Init();
        return await database.Table<TransactionsItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(TransactionsItem item)
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

    public async Task<int> DeleteItemAsync(TransactionsItem item)
    {
        await Init();
        return await database.DeleteAsync(item);
    }
}