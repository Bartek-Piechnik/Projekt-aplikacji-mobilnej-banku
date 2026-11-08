using Aplikacja_bankowa.Helpers;
using BankSQLite;
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
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToMenuPage);

            if (Item == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
                return;
            }
            
            Item.Name = Item.Name.Trim();
            Item.Surname = Item.Surname.Trim();
            Item.Email = Item.Email.Trim().ToLowerInvariant();
            Item.PhoneNumber = Item.PhoneNumber.Trim();
            List<string> errors = FormValidation.ValidateUserData(Item);
            
            if (errors.Count > 0)
            {
                string errorMessage = string.Join("\n\n", errors);
                await DisplayAlertAsync("Wykryto błędy w formularzu", errorMessage, "OK");
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
            
            await DisplayAlertAsync("Sukces", $"Pomyślnie zmieniono dane:\n Imię: {Item.Name},\n Nazwisko: {Item.Surname},\n Data urodzenia: {Item.DateOfBirth},\n Adres e-mail: {Item.Email},\n Numer telefonu: {Item.PhoneNumber}", "OK");
            await database.SaveItemAsync(Item);
            await Navigation.PopAsync();
        }
    }
}