using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class MainViewPage : ContentPage
    {
        private readonly Database database;
        private bool IsCardBlocked = false;

        public MainViewPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            BindingContext = this;
            
            var RecipientsAccount = Preferences.Get("recipientsAccount", string.Empty);
            LabelRecipientsAccount.Text = RecipientsAccount;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var UserId = Preferences.Get("UserId", 0);
            UsersItem user = await database.GetItemAsync(UserId);

            if (user != null)
            {
                LabelBalance.Text = $"{user.AccountBalance:F2} PLN";
                LabelAvailableFunds.Text = $"{user.AccountBalance:F2} PLN";
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
            }
        }

        private async void OnButtonLogOutClicked(object sender, EventArgs e)
        {
            await ButtonLogOut.ScaleToAsync(1.1, 150);
            await ButtonLogOut.ScaleToAsync(1.0, 150);

            Preferences.Remove("UserId");
            await Navigation.PopToRootAsync();
        }

        private async void OnButtonGoToMenuPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToMenuPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMenuPage.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new MenuPage(database));
        }

        private async void OnButtonBlockUnblockCardClicked(object sender, EventArgs e)
        {
            if (IsCardBlocked)
            {
                IsCardBlocked = false;
                Preferences.Set("IsCardBlocked", false);
                BlockUnblockCardButton.ImageSource = "credit_card_on.png";
                await DisplayAlertAsync("Sukces", "Karta została pomyślnie odblokowana", "OK");
            }
            else
            {
                IsCardBlocked = true;
                Preferences.Set("IsCardBlocked", true);
                BlockUnblockCardButton.ImageSource =  "credit_card_off.png";
                await DisplayAlertAsync("Sukces", "Karta została pomyślnie zablokowana", "OK");
            }
        }
        
        private async void OnButtonGoToTransferOptionBLIKClicked(object sender, EventArgs e)
        {
            await ButtonGoToTransferOptionBLIK.ScaleToAsync(1.1, 150);
            await ButtonGoToTransferOptionBLIK.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new TransferOptionBLIK());
        }

        private async void OnButtonGoToTransferOptionDomesticClicked(object sender, EventArgs e)
        {
            await ButtonGoToTransferOptionDomestic.ScaleToAsync(1.1, 150);
            await ButtonGoToTransferOptionDomestic.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new TransferOptionDomestic(database));
        }

        private async void OnButtonGoToTransferOptionPhoneClicked(object sender, EventArgs e)
        {
            await ButtonGoToTransferOptionPhone.ScaleToAsync(1.1, 150);
            await ButtonGoToTransferOptionPhone.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new TransferOptionPhone(database));
        }
    }
}