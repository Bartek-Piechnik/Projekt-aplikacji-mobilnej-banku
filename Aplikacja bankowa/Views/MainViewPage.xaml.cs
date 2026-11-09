using Aplikacja_bankowa.Helpers;
using BankSQLite.Data;
using System.Collections.ObjectModel;
using Aplikacja_bankowa.Models;

namespace Aplikacja_bankowa
{
    public partial class MainViewPage : ContentPage
    {
        private readonly Database database;
        public UsersItem User { get; set; }
        public ObservableCollection<TransactionDisplayItem> Transactions { get; set; } = new();

        public MainViewPage(Database database)
        {
            InitializeComponent();
            this.database = database;
            User = new UsersItem();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var userId = Preferences.Get("UserId", 0);
            User = await database.GetItemAsync(userId);

            if (User != null)
            {
                LabelRecipientsAccount.Text = User.AccountNumber;
                LabelAvailableFunds.Text = $"{User.AccountBalance:F2} PLN";

                UpdateCardImage();

                var transactions = await database.GetUserTransactionsPreviewAsync(userId);

                Transactions.Clear();

                foreach (var item in transactions)
                {
                    Transactions.Add(new TransactionDisplayItem(item, userId));
                }
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
            }
        }

        private async Task NavigateToTransferAsync(Button button, Page destination)
        {
            await ButtonAnimations.AnimateButtonAsync(button);

            if (User.IsCardBlocked)
            {
                await DisplayAlertAsync("Błąd", "Karta jest zablokowana - nie można wykonać transakcji!", "OK");
                return;
            }
            else if (User.AccountBalance <= 0)
            {
                await DisplayAlertAsync("Błąd", "Brak środków - nie można wykonać transakcji!", "OK");
                return;
            }

            await Navigation.PushAsync(destination);
        }

        private void UpdateCardImage()
        {
            if (User.IsCardBlocked)
            {
                BlockUnblockCardButton.ImageSource = "credit_card_off.png";
            }
            else
            {
                BlockUnblockCardButton.ImageSource = "credit_card_on.png";
            }
        }

        private async void OnButtonLogOutClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonLogOut);

            Preferences.Remove("UserId");
            await Navigation.PopToRootAsync();
        }

        private async void OnButtonGoToMenuPageClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToMenuPage);

            await Navigation.PushAsync(new MenuPage(database));
        }

        private async void OnButtonBlockUnblockCardClicked(object sender, EventArgs e)
        {
            if (!User.IsCardBlocked)
            {
                await DisplayAlertAsync("Sukces", "Karta została pomyślnie zablokowana", "OK");
            }
            else if(User.IsCardBlocked)
            {
                await DisplayAlertAsync("Sukces", "Karta została pomyślnie odblokowana", "OK");
            }

            User.IsCardBlocked = !User.IsCardBlocked;

            UpdateCardImage();

            await database.SaveItemAsync(User);
        }
        
        private async void OnButtonGoToTransferOptionBLIKClicked(object sender, EventArgs e)
        {
            await NavigateToTransferAsync(ButtonGoToTransferOptionBLIK, new TransferOptionBLIK());
        }

        private async void OnButtonGoToTransferOptionDomesticClicked(object sender, EventArgs e)
        {
            await NavigateToTransferAsync(ButtonGoToTransferOptionDomestic, new TransferOptionDomestic(database));
        }

        private async void OnButtonGoToTransferOptionPhoneClicked(object sender, EventArgs e)
        {
            await NavigateToTransferAsync(ButtonGoToTransferOptionPhone, new TransferOptionPhone(database));
        }

        private async void OnButtonGoToHistoryPageClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToHistoryPage);
            await Navigation.PushAsync(new HistoryPage(database));
        }
    }
}