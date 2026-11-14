using Aplikacja_bankowa.Helpers;
using Aplikacja_bankowa.Models;
using BankSQLite.Data;

namespace Aplikacja_bankowa
{
    public partial class PaymentLimitPage : ContentPage
    {
        private readonly Database database;
        public UsersItem User { get; set; }
        
        public PaymentLimitPage(Database database)
        {
            InitializeComponent();
            this.database = database;
            User = new UsersItem();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            var userId = Preferences.Get("UserId", 0);
            User = await database.GetUserByIdAsync(userId);
            
            if (User == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
                return;
            }
            OnPropertyChanged(nameof(User));
        }

        private async void OnButtonChangeMyPaymentLimitClicked(Object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonChangeMyPaymentLimit);

            if (User == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
                return;
            }
            
            if (User.PaymentLimit <= 0)
            {
                await DisplayAlertAsync("Błąd", "Kwota musi być większa od zera!", "OK");
                return;
            }
            
            await database.SaveUserAsync(User);
            await DisplayAlertAsync("Sukces", $"Pomyślnie zmieniono limit platności:\n Limit płatności: {User.PaymentLimit} zł", "OK"); 
            await Navigation.PushAsync(new MainViewPage(database));
        }
    }
}