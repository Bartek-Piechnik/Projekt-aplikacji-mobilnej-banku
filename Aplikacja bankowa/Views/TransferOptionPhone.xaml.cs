using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class TransferOptionPhone : ContentPage
    {
        private readonly Database database;
        public TransactionsItem Item { get; set; }

        public TransferOptionPhone(Database database_)
        {
            InitializeComponent();

            database = database_;
            Item = new TransactionsItem();
            BindingContext = this;
        }

        private async void OnButtonTransferClicked(object sender, EventArgs e)
        {
            await ButtonTransfer.ScaleToAsync(1.1, 150);
            await ButtonTransfer.ScaleToAsync(1.0, 150);

            var recipientsPhoneNumber = EntryRecipientsPhoneNumber.Text;

            if (string.IsNullOrEmpty(recipientsPhoneNumber) || string.IsNullOrEmpty(Item.TransferTitle))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }
            else if (Item.TransferAmount <= 0)
            {
                await DisplayAlertAsync("Błąd", "Kwota musi być większa od zera!", "OK");
                return;
            }

            var recipient = await database.GetItemByAccountNumberAsync(recipientsPhoneNumber);
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
                await database.SaveTransactionsAsync(Item);
                await Navigation.PopAsync();
            }
        }
    }
}