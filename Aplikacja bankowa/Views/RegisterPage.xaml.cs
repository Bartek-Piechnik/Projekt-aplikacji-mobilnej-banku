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
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Item.Name))
            {
                errors.Add("Pole imię nie może być puste!");
            }
            else if (!RegexValidate.IsValidName(Item.Name))
            {
                errors.Add("Imię musi zaczynać się wielką literą. Możliwe jest imię dwuczłonowe!");
            }

            if (string.IsNullOrWhiteSpace(Item.Surname))
            {
                errors.Add("Pole nazwisko nie może być puste!");
            }
            else if (!RegexValidate.IsValidSurname(Item.Surname))
            {
                errors.Add("Nazwisko musi zaczynać się wielką literą. Możliwe są nazwiska dwuczłonowe!");
            }

            if (Item.DateOfBirth == null)
            {
                errors.Add("Wybierz datę urodzenia!");
            }

            if (string.IsNullOrWhiteSpace(Item.Email))
            {
                errors.Add("Pole email nie może być puste!");
            }
            else if (!RegexValidate.IsValidEmail(Item.Email))
            {
                errors.Add("Podaj poprawny adres email np. przyklad@gmail.com!");
            }

            if (string.IsNullOrWhiteSpace(Item.PhoneNumber))
            {
                errors.Add("Pole numer telefonu nie może być puste!");
            }
            else if (!RegexValidate.IsValidPhoneNumber(Item.PhoneNumber))
            {
                errors.Add("Numer telefonu musi zawierać 9 cyfr. Możesz również podać numer z prefiksem kraju, np. +48123456789!");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                errors.Add("Pole hasło nie może być puste!");
            }
            else if (!RegexValidate.IsValidPassword(password))
            {
                errors.Add("Hasło musi zawierać co najmniej 8 znaków!");
            }

            if (string.IsNullOrWhiteSpace(repeatedPassword))
            {
                errors.Add("Pole powtórzone hasło nie może być puste!");
            }
            else if (password != repeatedPassword)
            {
                errors.Add("Hasło i powtórzone hasło muszą być identyczne!");
            }

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