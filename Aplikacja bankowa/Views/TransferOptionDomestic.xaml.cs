using Aplikacja_bankowa.Helpers;
using Aplikacja_bankowa.Models;
using BankSQLite.Data;

namespace Aplikacja_bankowa
{
    public partial class TransferOptionDomestic : ContentPage
    {
        private readonly Database database;
        public TransactionsItem Transaction { get; set; }

        public TransferOptionDomestic(Database database)
        {
            InitializeComponent();
            this.database = database;
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

            List<string> errors = FormValidation.ValidateTransaction(Transaction);

            if (string.IsNullOrWhiteSpace(recipientsAccount))
            {
                errors.Add("Pole rachunek odbiorcy nie może być puste!");
            }
            else if (!RegexValidate.IsValidRecipientsAccount(recipientsAccount))
            {
                errors.Add("Rachunek odbiorcy może składać się tylko z cyfr od 1 do 9! Musi mieć 26 znaków!");
            }
            
            if (errors.Count > 0)
            {
                string errorMessage = string.Join("\n\n", errors);
                await DisplayAlertAsync("Wykryto błędy w formularzu", errorMessage, "OK");
                return;
            }
            
            var recipient = await database.GetUserByAccountNumberAsync(recipientsAccount);
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