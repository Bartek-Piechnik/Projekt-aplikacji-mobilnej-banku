using Aplikacja_bankowa.Helpers;
using BankSQLite.Data;
using BankSQLite.Models;
using System.Collections.ObjectModel;

namespace Aplikacja_bankowa
{
    public partial class HistoryPage : ContentPage
    {
        private readonly Database database;
        public UsersItem Item { get; set; }
        public ObservableCollection<TransactionsItem> Transactions { get; set; } = new();

        public HistoryPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            Item = new UsersItem();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var userId = Preferences.Get("UserId", 0);
            Item = await database.GetItemAsync(userId);

            if (Item == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
            }

            var transactions = await database.GetUserTransactionsAsync(userId);

            Transactions.Clear();

            foreach (var item in transactions)
            {
                Transactions.Add(item);
            }
        }

    }
}