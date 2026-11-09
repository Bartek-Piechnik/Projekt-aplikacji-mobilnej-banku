using Aplikacja_bankowa.Helpers;
using BankSQLite.Data;

namespace Aplikacja_bankowa
{
    public partial class LoginPage : ContentPage
    {
        private readonly Database database;

        public LoginPage(Database database)
        {
            InitializeComponent();
            this.database = database;
            BindingContext = this;
        }

        private void OnButtonChangeVisibilityPasswordClicked(object sender, EventArgs e)
        {
            PasswordVisibility.Toggle(EntryPassword, ButtonChangeVisibilityPassword);
        }

        private async void OnButtonGoToRegisterPageClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToRegisterPage);

            await Navigation.PushAsync(new RegisterPage(database));
        }

        private async void OnButtonLoginClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonLogin);

            var email = EntryEmail.Text?.Trim().ToLowerInvariant() ?? String.Empty;
            var password = EntryPassword.Text ?? string.Empty;
            List<string> errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add("Pole email nie może być puste!");
            }
            else if (!RegexValidate.IsValidEmail(email))
            {
                errors.Add("Podaj poprawny adres email w formacie np. przyklad@gmail.com!");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                errors.Add("Pole hasło nie może być puste!");
            }

            if (errors.Count > 0)
            {
                string errorMessage = string.Join("\n\n", errors);
                await DisplayAlertAsync("Wykryto błędy w formularzu", errorMessage, "OK");
                return;
            }
            
            var user = await database.GetItemByEmailAsync(email);
            if (user == null)
            {
                await DisplayAlertAsync("Błąd", "Niepoprawny email lub hasło.", "OK");
                return;
            }
            
            bool isPasswordValid = PasswordHash.Verify(password, user.PasswordHash, user.PasswordSalt);
            if (isPasswordValid)
            {
                Preferences.Set("UserId", user.ID);
                await Navigation.PushAsync(new MainViewPage(database));
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Niepoprawny email lub hasło.", "OK");
            }
        }
    }
}