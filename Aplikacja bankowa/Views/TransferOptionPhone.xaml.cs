using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class TransferOptionPhone : ContentPage
    {
        private readonly TransactionItemDatabase _transactionItemDatabase;
        public TransactionsItem Item { get; set; }
        
        public TransferOptionPhone(TransactionItemDatabase  transactionItemDatabase)
        {
            InitializeComponent();
            
            _transactionItemDatabase = transactionItemDatabase;
            Item = new TransactionsItem();
            BindingContext =  this;
        }

        private async void OnButtonTransferClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Item.RecipientsPhoneNumber) || string.IsNullOrEmpty(Item.RecipientNameAndSurname) || string.IsNullOrEmpty(Item.TransferReference))
            {
                await DisplayAlert("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }
            
            await _transactionItemDatabase.SaveItemAsync(Item);
            await Navigation.PopAsync();
        }
    }
}