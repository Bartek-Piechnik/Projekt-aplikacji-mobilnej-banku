using BankSQLite.Data;
using BankSQLite.Models;
using System.Collections.ObjectModel;

namespace Aplikacja_bankowa
{
    public partial class LoginPage : ContentPage
    {
        private readonly Database database;
        public LoginPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            BindingContext = this;
        }

        private async void OnButtonGoToRegisterPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage(database));
        }

        private async void OnButtonGoToMainViewPageClicked(object sender, EventArgs e)
        {
            var users = await database.GetItemsAsync();
            
            var email = EntryEmail.Text;
            var password = EntryPassword.Text;

            var user = users.FirstOrDefault(x =>
                        x.Email == email &&
                        x.Password == password);
            if (user != null)
            {
                Preferences.Set("UserId", user.ID);
                await Navigation.PushAsync(new MainViewPage(database));
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Niepoprawne hasło lub email", "OK");
            }
        }
    }
}