using BankSQLite.Data;
using BankSQLite.Models;
using System.Collections.ObjectModel;

namespace Aplikacja_bankowa
{
    public partial class LoginPage : ContentPage
    {
        private readonly UserItemDatabase database;
        public LoginPage(UserItemDatabase userItemDatabase)
        {
            InitializeComponent();
            database = userItemDatabase;
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
                await Navigation.PushAsync(new MainViewPage());
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Niepoprane hasło lub email", "OK");
            }
        }
    }
}
