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
            else if (RadioButtonBlueCash.IsChecked)
            {
                TransferType = RadioButtonBlueCash.Content.ToString();
            }

        private async void OnButtonTransferClicked(object sender, EventArgs e)
            {
            await ButtonTransfer.ScaleToAsync(1.1, 150);
            await ButtonTransfer.ScaleToAsync(1.0, 150);

            if (string.IsNullOrEmpty(Item.RecipientsAccount) || string.IsNullOrEmpty(Item.RecipientNameAndSurname) || string.IsNullOrEmpty(Item.TransferReference) || string.IsNullOrEmpty(Item.TransferType))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }
            else if (Item.TransferAmount <= 0)
            {
                await DisplayAlertAsync("Błąd", "Kwota musi być większa od zera!", "OK");
                return;
            }

            await database.SaveTransactionsAsync(Item);
            await Navigation.PopAsync();
        }

        
    }
}