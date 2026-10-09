using BankSQLite.Data;
using BankSQLite.Models;
using System.Collections.ObjectModel;

namespace Aplikacja_bankowa
{
    public partial class LoginPage : ContentPage
    {
        private readonly UserItemDatabase _userItemDatabase;
        private readonly TransactionItemDatabase _transactionItemDatabase;
        public LoginPage(UserItemDatabase userItemDatabase,  TransactionItemDatabase transactionItemDatabase)
        {
            InitializeComponent();
            _userItemDatabase = userItemDatabase;
            _transactionItemDatabase = transactionItemDatabase;
            BindingContext = this;
        }

        private async void OnButtonGoToRegisterPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage(_userItemDatabase));
        }

        private async void OnButtonGoToMainViewPageClicked(object sender, EventArgs e)
        {
            var users = await _userItemDatabase.GetItemsAsync();
            
            var email = EntryEmail.Text;
            var password = EntryPassword.Text;

            var user = users.FirstOrDefault(x =>
                        x.Email == email &&
                        x.Password == password);
            if (user != null)
            {
                await Navigation.PushAsync(new MainViewPage(_userItemDatabase, _transactionItemDatabase));
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Niepoprawne hasło lub email", "OK");
            }
        }
    }
}