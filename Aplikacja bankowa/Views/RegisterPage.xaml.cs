using Aplikacja_bankowa.Helpers;
using BankSQLite;
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
            PasswordVisibility.Toggle(EntryPassword, ButtonChangeVisibilityPassword);
        }

        private void OnButtonChangeVisibilityRepeatedPasswordClicked(object sender, EventArgs e)
        {
            PasswordVisibility.Toggle(EntryRepeatedPassword, ButtonChangeVisibilityRepeatedPassword);
        }

        private async void OnButtonGoToLoginPageClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToLoginPage);

            var password = EntryPassword.Text ?? String.Empty;
            var repeatedPassword = EntryRepeatedPassword.Text ?? String.Empty;
            List<string> errors = FormValidation.ValidateRegistration(Item, password, repeatedPassword);

            if (errors.Count > 0)
            {
                string errorMessage = string.Join("\n\n", errors);
                await DisplayAlertAsync("Wykryto błędy w formularzu", errorMessage, "OK");
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