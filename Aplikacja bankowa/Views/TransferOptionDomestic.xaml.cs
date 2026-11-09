using Aplikacja_bankowa.Helpers;
using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class TransferOptionDomestic : ContentPage
    {
        private readonly Database database;
        public TransactionsItem Transaction { get; set; }

        public TransferOptionDomestic(Database database_)
        {
            InitializeComponent();
            database = database_;
            Transaction = new TransactionsItem();
            BindingContext = this;
        }

        private void OnTransferTypeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                Transaction.TransferType = radioButton.Content.ToString()!;
            }
        }

        private async void OnButtonTransferClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonTransfer);

            var recipientsAccount = EntryRecipientsAccount.Text;

            Transaction.DateOfTransfer = DateTime.UtcNow;

            if (string.IsNullOrEmpty(recipientsAccount) || string.IsNullOrEmpty(Transaction.TransferTitle) || string.IsNullOrEmpty(Transaction.TransferType))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }
            else if (Transaction.TransferAmount <= 0)
            {
                await DisplayAlertAsync("Błąd", "Kwota musi być większa od zera!", "OK");
                return;
            }

            var recipient = await database.GetItemByAccountNumberAsync(recipientsAccount);
            if (recipient != null)
            {
                Transaction.RecipientId = recipient.ID;
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono konta odbiorcy!", "OK");
                return;
            }

            Transaction.SenderId = Preferences.Get("UserId", 0);
            if (Transaction.SenderId == Transaction.RecipientId)
            {
                await DisplayAlertAsync("Błąd", "Nie można robić transakcji z samym sobą!", "OK");
                return;
            }
            if (Transaction.SenderId != 0)
            {
                try
                {
                    await database.ExecuteTransferAsync(Transaction);
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