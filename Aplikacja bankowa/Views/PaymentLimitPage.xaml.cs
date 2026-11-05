using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class PaymentLimitPage : ContentPage
    {
        private readonly Database database;
        public UsersItem Item { get; set; }
        
        public PaymentLimitPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            Item = new UsersItem();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            var UserId = Preferences.Get("UserId", 0);
            Item = await database.GetItemAsync(UserId);
            
            if (Item == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
                return;
            }
            OnPropertyChanged(nameof(Item));
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
            
            if (Item.PaymentLimit <= 0)
            {
                await DisplayAlertAsync("Błąd", "Kwota musi być większa od zera!", "OK");
                return;
            }
            
            await database.SaveItemAsync(Item);
            await DisplayAlertAsync("Sukces", $"Pomyślnie zmieniono limit platności:\n Limit płatności: {Item.PaymentLimit} zł", "OK"); 
            await Navigation.PopAsync();
        }
    }
}