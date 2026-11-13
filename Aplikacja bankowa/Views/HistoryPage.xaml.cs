using BankSQLite.Data;
using System.Collections.ObjectModel;
using Aplikacja_bankowa.Models;

namespace Aplikacja_bankowa
{
    public partial class HistoryPage : ContentPage
    {
        private readonly Database database;
        public UsersItem User { get; set; }
        public ObservableCollection<TransactionDisplayItem> Transactions { get; set; } = new();
        public ObservableCollection<TransactionDisplayItem> FilteredTransactions { get; set; } = new();

        public HistoryPage(Database database)
        {
            InitializeComponent();
            this.database = database;
            User = new UsersItem();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var userId = Preferences.Get("UserId", 0);
            User = await database.GetUserByIdAsync(userId);

            if (User == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
            }

            var transactions = await database.GetUserTransactionsAsync(userId);

            Transactions.Clear();

            foreach (var item in transactions)
            {
                Transactions.Add(new TransactionDisplayItem(item, userId));
            }

            FilterTransactions();
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            FilterTransactions(e.NewTextValue);
        }

        private void FilterTransactions(string searchText = "")
        {
            var filteredTransactions = Transactions.Where(transaction =>
                string.IsNullOrWhiteSpace(searchText) ||
                transaction.TransferType.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                transaction.TransferTitle.Contains(searchText, StringComparison.OrdinalIgnoreCase));

            FilteredTransactions.Clear();

            foreach (var item in filteredTransactions)
            {
                FilteredTransactions.Add(item);
            }
        }
    }
}