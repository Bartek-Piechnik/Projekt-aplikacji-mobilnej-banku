using Aplikacja_bankowa.Helpers;
using Aplikacja_bankowa.Models;
using BankSQLite.Data;
using System.Security.Cryptography;

namespace Aplikacja_bankowa
{
    public partial class RegisterPage : ContentPage
    {
        private readonly Database database;
        public UsersItem User { get; set; }

        public RegisterPage(Database database)
        {
            InitializeComponent();
            this.database = database;
            User = new UsersItem();
            BindingContext = this;
        }

        private void OnButtonChangeVisibilityPasswordClicked(object sender, EventArgs e)
        {
            PasswordVisibility.Toggle(EntryPassword, ButtonChangeVisibilityPassword);
        }

        private void OnButtonChangeVisibilityRepeatedPasswordClicked(object sender, EventArgs e)
        {
            PasswordVisibility.Toggle(EntryRepeatedPassword, ButtonChangeVisibilityRepeatedPassword);
        }

        private async void OnButtonRegisterClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonRegister);

            var password = EntryPassword.Text ?? String.Empty;
            var repeatedPassword = EntryRepeatedPassword.Text ?? String.Empty;
            TextFormatting.FormatUserData(User);
            List<string> errors = FormValidation.ValidateRegistration(User, password, repeatedPassword);

            if (errors.Count > 0)
            {
                string errorMessage = string.Join("\n\n", errors);
                await DisplayAlertAsync("Wykryto błędy w formularzu", errorMessage, "OK");
                return;
            }

            string recipientsAccount;
            do
            {
                recipientsAccount = RandomNumberGenerator.GetInt32(0, 10_000_000).ToString("D7")
                                    + RandomNumberGenerator.GetInt32(0, 10_000_000).ToString("D7")
                                    + RandomNumberGenerator.GetInt32(0, 10_000_000).ToString("D7")
                                    + RandomNumberGenerator.GetInt32(0, 100_000).ToString("D5");
            }
            while (await database.GetUserByAccountNumberAsync(recipientsAccount) != null);
            User.AccountNumber = recipientsAccount;

            var userEmail = await database.GetUserByEmailAsync(User.Email);

            if (userEmail != null)
            {
                await DisplayAlertAsync("Błąd", "Email niedostępny!", "OK");
                return;
            }

            var (hash, salt) = PasswordHash.Hash(password);
            User.PasswordHash =  hash;
            User.PasswordSalt = salt;

            await database.SaveUserAsync(User);
            await Navigation.PopAsync();
        }
    }
}