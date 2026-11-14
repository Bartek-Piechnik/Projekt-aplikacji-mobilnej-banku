using Aplikacja_bankowa.Helpers;
using Aplikacja_bankowa.Models;
using BankSQLite.Data;

namespace Aplikacja_bankowa.Views
{
    public partial class CashMachinePage : ContentPage
    {
        private readonly Database database;
        public TransactionsItem Transaction { get; set; }
        
        public CashMachinePage(Database database)
        {
            InitializeComponent();
            this.database = database;
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
            
            var userId = Preferences.Get("UserId", 0);
            if (userId != 0)
            {
                try
                {
                    if (Transaction.TransferType == "Wpłata")
                    {
                        Transaction.RecipientId = userId;
                        Transaction.SenderId = 0;
                        await database.DepositAsync(Transaction);
                    }
                    else if (Transaction.TransferType == "Wypłata")
                    {
                        Transaction.SenderId = userId;
                        Transaction.RecipientId = 0;
                        await database.WithdrawAsync(Transaction);
                    }
                    
                    await DisplayAlertAsync("Sukces", "Operacja została wykonana pomyślnie!", "OK");
                    await Navigation.PushAsync(new MainViewPage(database));
                }
                catch (Exception ex)
                {
                    await DisplayAlertAsync("Błąd", ex.Message, "OK");
                }
            }
        }
    }
}