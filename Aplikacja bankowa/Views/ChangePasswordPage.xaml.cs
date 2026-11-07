using Aplikacja_bankowa.Helpers;
using BankSQLite;
using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class ChangePasswordPage : ContentPage
    {
        private readonly Database database;
        
        public UsersItem Item { get; set; }
        
        public ChangePasswordPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            Item = new UsersItem();
        }

        private void OnButtonChangeVisibilityOldPasswordClicked(object sender, EventArgs e)
        {
            if (EntryOldPassword.IsPassword)
            {
                EntryOldPassword.IsPassword = false;
                ButtonChangeVisibilityOldPassword.ImageSource = "password_visibility_on.png";
            }
            else
            {
                EntryOldPassword.IsPassword = true;
                ButtonChangeVisibilityOldPassword.ImageSource = "password_visibility_off.png";
            }
        }

        private void OnButtonChangeVisibilityNewPasswordClicked(object sender, EventArgs e)
        {
            if (EntryNewPassword.IsPassword)
            {
                EntryNewPassword.IsPassword = false;
                ButtonChangeVisibilityNewPassword.ImageSource = "password_visibility_on.png";
            }
            else
            {
                EntryNewPassword.IsPassword = true;
                ButtonChangeVisibilityNewPassword.ImageSource = "password_visibility_off.png";
            }
        }

        private void OnButtonChangeVisibilityRepeatedNewPasswordClicked(object sender, EventArgs e)
        {
            if (EntryRepeatedNewPassword.IsPassword)
            {
                EntryRepeatedNewPassword.IsPassword = false;
                ButtonChangeVisibilityRepeatedNewPassword.ImageSource = "password_visibility_on.png";
            }
            else
            {
                EntryRepeatedNewPassword.IsPassword = true;
                ButtonChangeVisibilityRepeatedNewPassword.ImageSource = "password_visibility_off.png";
            }
        }

        private async void OnButtonGoToMenuPageClicked(Object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToMenuPage);

            var userId = Preferences.Get("UserId", 0);
            Item = await database.GetItemAsync(userId);
            if (Item == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
                return;
            }
            
            var oldPassword = EntryOldPassword.Text ?? string.Empty;
            var newPassword = EntryNewPassword.Text ?? string.Empty;
            var repeatedNewPassword = EntryRepeatedNewPassword.Text ?? string.Empty;
            List<string> errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(oldPassword))
            {
                errors.Add("Pole stare hasło nie może być puste!");
            }
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                errors.Add("Pole nowe hasło nie może być puste!");
            }
            if (string.IsNullOrWhiteSpace(repeatedNewPassword))
            {
                errors.Add("Pole powtórzone nowe hasło nie może być puste!");
            }
            
            if (!string.IsNullOrWhiteSpace(newPassword) && !RegexValidate.IsValidPassword(newPassword))
            {
                errors.Add("Hasło musi zawierać co najmniej 8 znaków!");
            }
            
            if (!string.IsNullOrWhiteSpace(newPassword) && !string.IsNullOrWhiteSpace(repeatedNewPassword) && newPassword != repeatedNewPassword)
            {
                errors.Add("Hasło i powtórzone hasło muszą być identyczne!");
            }

            if (errors.Count > 0)
            {
                string errorMessage = string.Join("\n\n", errors);
                await DisplayAlertAsync("Wykryto błędy w formularzu", errorMessage, "OK");
                return;
            }
            
            bool isPasswordValid = BankSQLite.PasswordHash.Verify(oldPassword, Item.PasswordHash, Item.PasswordSalt);
            if (!isPasswordValid)
            {
                await DisplayAlertAsync("Błąd", "Niepoprawne stare hasło.", "OK");
                return;
            }
            
            var (hash, salt) = PasswordHash.Hash(newPassword);
            Item.PasswordHash =  hash;
            Item.PasswordSalt = salt;

            await database.SaveItemAsync(Item);
            await Navigation.PushAsync(new MenuPage(database));
        }
    }
}