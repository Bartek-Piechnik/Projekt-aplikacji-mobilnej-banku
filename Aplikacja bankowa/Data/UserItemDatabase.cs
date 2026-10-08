using SQLite;
using BankSQLite.Models;

namespace BankSQLite.Data;

public class UserItemDatabase
{
    SQLiteAsyncConnection database;

    async Task Init()
    {
        if (database is not null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await database.CreateTableAsync<UserItem>();
    }

    public async Task<List<UserItem>> GetItemsAsync()
    {
        await Init();
        return await database.Table<UserItem>().ToListAsync();
    }

    public async Task<UserItem> GetItemAsync(int id)
    {
        await Init();
        return await database.Table<UserItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(UserItem item)
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

    public async Task<int> DeleteItemAsync(UserItem item)
    {
        await Init();
        return await database.DeleteAsync(item);
    }
}