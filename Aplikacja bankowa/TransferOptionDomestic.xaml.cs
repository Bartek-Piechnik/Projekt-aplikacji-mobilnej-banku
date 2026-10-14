namespace Aplikacja_bankowa
{
    public partial class TransferOptionDomestic : ContentPage
    {
        public TransferOptionDomestic()
        {
            InitializeComponent();
        }

        private async void OnButtonTransferClicked(object sender, EventArgs e)
        {
            await ButtonTransfer.ScaleToAsync(1.1, 150);
            await ButtonTransfer.ScaleToAsync(1.0, 150);
            
            var RecipientsAccount = EntryRecipientsAccount.Text;
            var RecipientNameAndSurname = EntryRecipientNameAndSurname.Text;
            var TransferReference = EntryTransferReference.Text;
            var TransferAmount = EntryTransferAmount.Text;
            var DateOfTransfer = DatePickerDateOfTransfer.Date;
            var TransferType = "";
            
            if (RadioButtonStandard.IsChecked)
            {
                TransferType = RadioButtonStandard.Content.ToString();
            }
            else if (RadioButtonBlueCash.IsChecked)
            {
                TransferType = RadioButtonBlueCash.Content.ToString();
            }
            else if (RadioButtonExpressElixir.IsChecked)
            {
                TransferType = RadioButtonExpressElixir.Content.ToString();
            }

            if (string.IsNullOrEmpty(RecipientsAccount) || string.IsNullOrEmpty(RecipientNameAndSurname) || string.IsNullOrEmpty(TransferReference) || string.IsNullOrEmpty(TransferAmount) || string.IsNullOrEmpty(TransferType))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
            }
            else
            {
                await Navigation.PushAsync(new MainViewPage());
            }
        }
    }
}