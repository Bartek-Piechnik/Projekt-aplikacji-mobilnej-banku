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

        private void OnButtonChangeVisibilityPasswordClicked(object sender, EventArgs e)
        {
            if (EntryPassword.IsPassword)
            {
                EntryPassword.IsPassword = false;
                ButtonChangeVisibilityPassword.ImageSource = "password_visibility_on.png";
            }
            else
            {
                EntryPassword.IsPassword = true;
                ButtonChangeVisibilityPassword.ImageSource = "password_visibility_off.png";
            }
        }
        
        private async void OnButtonGoToRegisterPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToRegisterPage.ScaleToAsync(1.1, 150);
            await ButtonGoToRegisterPage.ScaleToAsync(1.0, 150);
            
            await Navigation.PushAsync(new RegisterPage(database));
        }

        private async void OnButtonGoToMainViewPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToMainViewPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMainViewPage.ScaleToAsync(1.0, 150);
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
