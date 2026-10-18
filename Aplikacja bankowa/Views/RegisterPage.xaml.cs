using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class RegisterPage : ContentPage
    {
        private readonly Database database;
        public UsersItem Item { get; set; }

        public RegisterPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            Item = new UsersItem();
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

        private void OnButtonChangeVisibilityRepeatedPasswordClicked(object sender, EventArgs e)
        {
            if (EntryRepeatedPassword.IsPassword)
            {
                EntryRepeatedPassword.IsPassword = false;
                ButtonChangeVisibilityRepeatedPassword.ImageSource = "password_visibility_on.png";
            }
            else
            {
                EntryRepeatedPassword.IsPassword = true;
                ButtonChangeVisibilityRepeatedPassword.ImageSource = "password_visibility_off.png";
            }
        }

        private async void OnButtonGoToLoginPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToLoginPage.ScaleToAsync(1.1, 150);
            await ButtonGoToLoginPage.ScaleToAsync(1.0, 150);

            var users = await database.GetItemsAsync();

            var RepeatedPassword = EntryRepeatedPassword.Text;


            if (string.IsNullOrWhiteSpace(Item.Name) || string.IsNullOrWhiteSpace(Item.Surname) ||
                string.IsNullOrEmpty(Item.Email) || string.IsNullOrEmpty(Item.PhoneNumber) ||
                string.IsNullOrEmpty(Item.Password) || string.IsNullOrEmpty(RepeatedPassword))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }

            Item.Email = Item.Email.Trim().ToLowerInvariant();
            var userEmail = users.FirstOrDefault(x =>
                x.Email == Item.Email);

            if (userEmail != null)
            {
                await DisplayAlertAsync("Błąd", "Email niedostępny!", "OK");
                return;
            }

            if (Item.Password != RepeatedPassword)
            {
                await DisplayAlertAsync("Błąd", "Hasło i powtórzone hasło muszą być identyczne!", "OK");
                return;
            }

            await database.SaveItemAsync(Item);
            await Navigation.PopAsync();
        }
    }
}