using Aplikacja_bankowa.Helpers;
using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class CashMachinePage : ContentPage
    {
        private readonly Database database;
        public TransactionsItem Item { get; set; }
        
        public CashMachinePage(Database database_)
        {
            InitializeComponent();
            database = database_;
            Item = new TransactionsItem();
            BindingContext = this;
        }
        
        private void OnOperationTypeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                Item.TransferType = radioButton.Content.ToString()!;

                if (Item.TransferType == "Wpłata")
                {
                    ButtonGoToMenuPage.Text = "Wpłać";
                }
                else
                {
                    ButtonGoToMenuPage.Text = "Wypłać";
                }
            }
        }

        private async void OnButtonGoToMenuPageClicked(Object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToMenuPage);

            Item.DateOfTransfer = DateTime.UtcNow;
            
            if (string.IsNullOrEmpty(Item.TransferType))
            {
                await DisplayAlertAsync("Błąd", "Wybierz typ operacji!", "OK");
                return;
            }
            else if (Item.TransferAmount <= 0)
            {
                await DisplayAlertAsync("Błąd", "Kwota musi być większa od zera!", "OK");
                return;
            }
            
            Item.SenderId = Preferences.Get("UserId", 0);
            if (Item.SenderId != 0)
            {
                try
                {
                    if (Item.TransferType == "Wpłata")
                    {
                        await database.DepositAsync(Item);
                    }
                    else if (Item.TransferType == "Wypłata")
                    {
                        await database.WithdrawAsync(Item);
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