using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class TransferOptionDomestic : ContentPage
    {
        private readonly TransactionItemDatabase _transactionItemDatabase;
        public TransactionsItem Item { get; set; }
        
        public TransferOptionDomestic(TransactionItemDatabase transactionItemDatabase)
        {
            InitializeComponent();
            
            _transactionItemDatabase = transactionItemDatabase;
            Item = new TransactionsItem();
            BindingContext = this;
        }

        private async void OnButtonTransferClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Item.RecipientsAccount) || string.IsNullOrEmpty(Item.RecipientNameAndSurname) || string.IsNullOrEmpty(Item.TransferReference) || string.IsNullOrEmpty(Item.TransferType))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }

            await _transactionItemDatabase.SaveItemAsync(Item);
            await Navigation.PopAsync();
        }
    }
}