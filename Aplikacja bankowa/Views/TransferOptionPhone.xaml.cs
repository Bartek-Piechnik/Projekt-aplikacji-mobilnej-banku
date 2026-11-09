using Aplikacja_bankowa.Helpers;
using Aplikacja_bankowa.Models;
using BankSQLite.Data;

namespace Aplikacja_bankowa
{
    public partial class TransferOptionPhone : ContentPage
    {
        private readonly Database database;
        public TransactionsItem Transaction { get; set; }

        public TransferOptionPhone(Database database)
        {
            InitializeComponent();
            this.database = database;
            Transaction = new TransactionsItem();
            BindingContext = this;
        }

        private async void OnButtonTransferClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonTransfer);

            var recipientsPhoneNumber = EntryRecipientsPhoneNumber.Text;

            Transaction.DateOfTransfer = DateTime.UtcNow;

            if (string.IsNullOrEmpty(recipientsPhoneNumber) || string.IsNullOrEmpty(Transaction.TransferTitle))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }
            else if (Transaction.TransferAmount <= 0)
            {
                await DisplayAlertAsync("Błąd", "Kwota musi być większa od zera!", "OK");
                return;
            }

            var recipient = await database.GetItemByPhoneNumberAsync(recipientsPhoneNumber);
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