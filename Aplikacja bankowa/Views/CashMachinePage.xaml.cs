using Aplikacja_bankowa.Helpers;
using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class CashMachinePage : ContentPage
    {
        private readonly Database database;
        public TransactionsItem Transaction { get; set; }
        
        public CashMachinePage(Database database_)
        {
            InitializeComponent();
            database = database_;
            Transaction = new TransactionsItem();
            BindingContext = this;
        }
        
        private void OnOperationTypeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                Transaction.TransferType = radioButton.Content.ToString()!;

                if (Transaction.TransferType == "Wpłata")
                {
                    ButtonMakeTransaction.Text = "Wpłać";
                }
                else
                {
                    ButtonMakeTransaction.Text = "Wypłać";
                }
            }
        }

        private async void OnButtonMakeTransactionClicked(Object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonMakeTransaction);

            Transaction.DateOfTransfer = DateTime.UtcNow;
            
            if (string.IsNullOrEmpty(Transaction.TransferType))
            {
                await DisplayAlertAsync("Błąd", "Wybierz typ operacji!", "OK");
                return;
            }
            else if (Transaction.TransferAmount <= 0)
            {
                await DisplayAlertAsync("Błąd", "Kwota musi być większa od zera!", "OK");
                return;
            }
            
            var userId = Preferences.Get("UserId", 0);
            if (userId != 0)
            {
                try
                {
                    if (Transaction.TransferType == "Wpłata")
                    {
                        Transaction.RecipientId = userId;
                        await database.DepositAsync(Transaction);
                    }
                    else if (Transaction.TransferType == "Wypłata")
                    {
                        Transaction.SenderId = userId;
                        await database.WithdrawAsync(Transaction);
                    }
                    
                    await DisplayAlertAsync("Sukces", "Operacja została wykonana pomyślnie!", "OK");
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