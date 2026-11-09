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
            Transaction.TransferType = "Telefoniczny";

            List<string> errors = FormValidation.ValidateTransaction(Transaction);
            
            if (string.IsNullOrWhiteSpace(recipientsPhoneNumber))
            {
                errors.Add("Numer telefonu nie może być pusty!");
            }
            else if (!RegexValidate.IsValidPhoneNumber(recipientsPhoneNumber))
            {
                errors.Add("Numer telefonu musi zawierać 9 cyfr. Możesz również podać numer z prefiksem kraju, np. +48123456789!");
            }
            
            if (errors.Count > 0)
            {
                string errorMessage = string.Join("\n\n", errors);
                await DisplayAlertAsync("Wykryto błędy w formularzu", errorMessage, "OK");
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