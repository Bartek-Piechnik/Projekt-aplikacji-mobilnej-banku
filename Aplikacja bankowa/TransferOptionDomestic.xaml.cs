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
            var RecipientsAccount = EntryRecipientsAccount.Text;
            var RecipientNameAndSurname = EntryRecipientNameAndSurname.Text;
            var TransferReference = EntryTransferReference.Text;
            var TransferAmount = EntryTransferAmount.Text;
            var DateOfTransfer = DatePickerDateOfTransfer.Date;

            //   DODAĆ POBIERANIE TYPU PRZELEWU

            if (string.IsNullOrEmpty(RecipientsAccount) || string.IsNullOrEmpty(RecipientNameAndSurname) || string.IsNullOrEmpty(TransferReference) || string.IsNullOrEmpty(TransferAmount))
            {
                await DisplayAlert("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
            }
            else
            {
                await Navigation.PushAsync(new MainViewPage());
            }
        }
    }
}