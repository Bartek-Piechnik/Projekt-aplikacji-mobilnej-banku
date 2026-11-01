using BankSQLite;
using BankSQLite.Data;
using BankSQLite.Models;
using System.Security.Cryptography;

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

            var password = EntryPassword.Text ?? String.Empty;
            var repeatedPassword = EntryRepeatedPassword.Text ?? String.Empty;

            if (string.IsNullOrWhiteSpace(Item.Name) || string.IsNullOrWhiteSpace(Item.Surname) || Item.DateOfBirth == null ||
                string.IsNullOrEmpty(Item.Email) || string.IsNullOrEmpty(Item.PhoneNumber) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatedPassword))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }

            if (password != repeatedPassword)
            {
                await DisplayAlertAsync("Błąd", "Hasło i powtórzone hasło muszą być identyczne!", "OK");
                return;
            }
            
            Random random = new Random();
            var number1 = random.Next(0, 10000000);
            var number2 = random.Next(0, 10000000);
            var number3 = random.Next(0, 10000000);
            var number4 = random.Next(0, 100000);
            var recipientsAccount = number1.ToString("D7") + number2.ToString("D7") + number3.ToString("D7") +
                                    number4.ToString("D5");
            Item.AccountNumber = recipientsAccount;
            Preferences.Set("recipientsAccount", recipientsAccount);

            Item.Email = Item.Email.Trim().ToLowerInvariant();
            var userEmail = await database.GetItemByEmailAsync(Item.Email);

            if (userEmail != null)
            {
                await DisplayAlertAsync("Błąd", "Email niedostępny!", "OK");
                return;
            }

            var (hash, salt) = PasswordHash.Hash(password);
            Item.PasswordHash =  hash;
            Item.PasswordSalt = salt;

            await database.SaveItemAsync(Item);
            await Navigation.PopAsync();
        }
    }
}