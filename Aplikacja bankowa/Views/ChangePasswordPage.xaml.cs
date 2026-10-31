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
            await ButtonGoToMenuPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMenuPage.ScaleToAsync(1.0, 150);

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
            
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(repeatedNewPassword))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }
            
            if (newPassword != repeatedNewPassword)
            {
                await DisplayAlertAsync("Błąd", "Hasło i powtórzone hasło muszą być identyczne!", "OK");
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