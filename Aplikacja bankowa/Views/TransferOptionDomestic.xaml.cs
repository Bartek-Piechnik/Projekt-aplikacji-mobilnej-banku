using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class TransferOptionDomestic : ContentPage
    {
        private readonly Database database;
        public TransactionsItem Item { get; set; }

        public TransferOptionDomestic(Database database_)
        {
            InitializeComponent();

            database = database_;
            Item = new TransactionsItem();
            BindingContext = this;
        }

        private void OnTransferTypeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                Item.TransferType = radioButton.Content.ToString()!;
            }
        }

        private async void OnButtonTransferClicked(object sender, EventArgs e)
        {
            await ButtonTransfer.ScaleToAsync(1.1, 150);
            await ButtonTransfer.ScaleToAsync(1.0, 150);

            var recipientsAccount = EntryRecipientsAccount.Text;

            Item.DateOfTransfer = DateTime.UtcNow;

            if (string.IsNullOrEmpty(recipientsAccount) || string.IsNullOrEmpty(Item.TransferTitle) || string.IsNullOrEmpty(Item.TransferType))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }
            else if (Item.TransferAmount <= 0)
            {
                await DisplayAlertAsync("Błąd", "Kwota musi być większa od zera!", "OK");
                return;
            }

            var recipient = await database.GetItemByAccountNumberAsync(recipientsAccount);
            if (recipient != null)
            {
                Item.RecipientId = recipient.ID;
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono konta odbiorcy!", "OK");
                return;
            }

            Item.SenderId = Preferences.Get("UserId", 0);
            if (Item.SenderId == Item.RecipientId)
            {
                await DisplayAlertAsync("Błąd", "Nie można robić transakcji z samym sobą!", "OK");
                return;
            }
            if (Item.SenderId != 0)
            {
                try
                {
                    await database.ExecuteTransferAsync(Item);
                    await DisplayAlertAsync("Sukces", "Przelew został wykonany!", "OK");
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlertAsync("Błąd", ex.Message, "OK");
                }
            }
        }
    }
}