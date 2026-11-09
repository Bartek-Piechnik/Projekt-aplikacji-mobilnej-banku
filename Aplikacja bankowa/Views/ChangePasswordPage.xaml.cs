using Aplikacja_bankowa.Helpers;
using BankSQLite;
using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class ChangePasswordPage : ContentPage
    {
        private readonly Database database;
        
        public UsersItem User { get; set; }
        
        public ChangePasswordPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            User = new UsersItem();
        }

        private void OnButtonChangeVisibilityOldPasswordClicked(object sender, EventArgs e)
        {
            PasswordVisibility.Toggle(EntryOldPassword, ButtonChangeVisibilityOldPassword);
        }

        private void OnButtonChangeVisibilityNewPasswordClicked(object sender, EventArgs e)
        {
            PasswordVisibility.Toggle(EntryNewPassword, ButtonChangeVisibilityNewPassword);
        }

        private void OnButtonChangeVisibilityRepeatedNewPasswordClicked(object sender, EventArgs e)
        {
            PasswordVisibility.Toggle(EntryRepeatedNewPassword, ButtonChangeVisibilityRepeatedNewPassword);
        }

        private async void OnButtonChangePasswordClicked(Object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonChangePassword);

            var userId = Preferences.Get("UserId", 0);
            User = await database.GetItemAsync(userId);
            if (User == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
                return;
            }
            
            var oldPassword = EntryOldPassword.Text ?? string.Empty;
            var newPassword = EntryNewPassword.Text ?? string.Empty;
            var repeatedNewPassword = EntryRepeatedNewPassword.Text ?? string.Empty;

            List<string> errors = FormValidation.ValidatePasswordChange(oldPassword, newPassword, repeatedNewPassword);

            if (errors.Count > 0)
            {
                string errorMessage = string.Join("\n\n", errors);
                await DisplayAlertAsync("Wykryto błędy w formularzu", errorMessage, "OK");
                return;
            }
            
            bool isPasswordValid = PasswordHash.Verify(oldPassword, User.PasswordHash, User.PasswordSalt);
            if (!isPasswordValid)
            {
                await DisplayAlertAsync("Błąd", "Niepoprawne stare hasło.", "OK");
                return;
            }
            
            var (hash, salt) = PasswordHash.Hash(newPassword);
            User.PasswordHash =  hash;
            User.PasswordSalt = salt;

            await database.SaveItemAsync(User);
            await Navigation.PushAsync(new MenuPage(database));
        }
    }
}