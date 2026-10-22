using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class MyDataPage : ContentPage
    {
        private readonly Database database;

        public UsersItem Item { get; set; }

        public MyDataPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            Item = new UsersItem();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var UserId = Preferences.Get("UserId", 0);
            Item = await database.GetItemAsync(UserId);

            if (Item != null)
            {
                BindingContext = this;
                var userInitials = Item.Name[0].ToString().ToUpper() + Item.Surname[0].ToString().ToUpper();
                Labelinitials.Text = $"{userInitials}";
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
            }
        }

        private async void OnButtonGoToMenuPageClicked(Object sender, EventArgs e)
        {
            await ButtonGoToMenuPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMenuPage.ScaleToAsync(1.0, 150);
            
            if (Item == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
                return;
            }
            
            Item.Name = Item.Name.Trim();
            Item.Surname = Item.Surname.Trim();
            Item.Email = Item.Email.Trim().ToLowerInvariant();
            Item.PhoneNumber = Item.PhoneNumber.Trim();
            
            if (string.IsNullOrWhiteSpace(Item.Name) || string.IsNullOrWhiteSpace(Item.Surname) || Item.DateOfBirth == null || 
                string.IsNullOrEmpty(Item.Email) || string.IsNullOrEmpty(Item.PhoneNumber))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }
            
            var users = await database.GetItemsAsync();
            
            var userEmail_ = users.FirstOrDefault(x =>
                x.Email.Equals(Item.Email, StringComparison.OrdinalIgnoreCase) && x.ID != Item.ID);

            if (userEmail_ != null)
            {
                await DisplayAlertAsync("Błąd", "Email niedostępny!", "OK");
                return;
            }

            await database.SaveItemAsync(Item);
            await Navigation.PopAsync();
        }
    }
}