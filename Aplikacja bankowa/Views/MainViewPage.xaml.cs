using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class MainViewPage : ContentPage
    {
        private readonly Database database;
        public UsersItem Item { get; set; }

        public MainViewPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            Item = new UsersItem();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var UserId = Preferences.Get("UserId", 0);
            Item = await database.GetItemAsync(UserId);

            if (Item != null)
            {
                LabelRecipientsAccount.Text = Item.AccountNumber;
                LabelBalance.Text = $"{Item.AccountBalance:F2} PLN";
                LabelAvailableFunds.Text = $"{Item.AccountBalance:F2} PLN";

                if (Item.IsCardBlocked)
                {
                    BlockUnblockCardButton.ImageSource =  "credit_card_off.png";
                }
                else
                {
                    BlockUnblockCardButton.ImageSource = "credit_card_on.png";
                }
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
            if (!Item.IsCardBlocked)
            {
                Item.IsCardBlocked = true;
                BlockUnblockCardButton.ImageSource =  "credit_card_off.png";
                await DisplayAlertAsync("Sukces", "Karta została pomyślnie zablokowana", "OK");
            }
            else
            {
                Item.IsCardBlocked = false;
                BlockUnblockCardButton.ImageSource = "credit_card_on.png";
                await DisplayAlertAsync("Sukces", "Karta została pomyślnie odblokowana", "OK");
            }
            await database.SaveItemAsync(Item);
        }
        
        private async void OnButtonGoToTransferOptionBLIKClicked(object sender, EventArgs e)
        {
            await ButtonGoToTransferOptionBLIK.ScaleToAsync(1.1, 150);
            await ButtonGoToTransferOptionBLIK.ScaleToAsync(1.0, 150);

            if (!Item.IsCardBlocked)
            {
                await Navigation.PushAsync(new TransferOptionBLIK());
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Karta jest zablokowana - nie można wykonać transakcji!", "OK"); 
            }
        }

        private async void OnButtonGoToTransferOptionDomesticClicked(object sender, EventArgs e)
        {
            await ButtonGoToTransferOptionDomestic.ScaleToAsync(1.1, 150);
            await ButtonGoToTransferOptionDomestic.ScaleToAsync(1.0, 150);

            if (!Item.IsCardBlocked)
            {
                await Navigation.PushAsync(new TransferOptionDomestic(database));
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Karta jest zablokowana - nie można wykonać transakcji!", "OK"); 
            }
        }

        private async void OnButtonGoToTransferOptionPhoneClicked(object sender, EventArgs e)
        {
            await ButtonGoToTransferOptionPhone.ScaleToAsync(1.1, 150);
            await ButtonGoToTransferOptionPhone.ScaleToAsync(1.0, 150);

            if (!Item.IsCardBlocked)
            {
                await Navigation.PushAsync(new TransferOptionPhone(database));
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Karta jest zablokowana - nie można wykonać transakcji!", "OK"); 
            }
        }
    }
}