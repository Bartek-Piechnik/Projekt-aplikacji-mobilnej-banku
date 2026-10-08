using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class RegisterPage : ContentPage
    {
        private readonly UserItemDatabase database;
        public UserItem Item { get; set; }
        public RegisterPage(UserItemDatabase userItemDatabase)
        {
            InitializeComponent();

            database = userItemDatabase;
            Item = new UserItem();
            BindingContext = this;
        }

        private async void OnButtonGoToLoginPageClicked(object sender, EventArgs e)
        {
            var RepeatedPassword = EntryRepeatedPassword.Text;

            if (string.IsNullOrWhiteSpace(Item.Name) || string.IsNullOrWhiteSpace(Item.Surname) || string.IsNullOrEmpty(Item.Email) || string.IsNullOrEmpty(Item.PhoneNumber) || string.IsNullOrEmpty(Item.Password) || string.IsNullOrEmpty(RepeatedPassword))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
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