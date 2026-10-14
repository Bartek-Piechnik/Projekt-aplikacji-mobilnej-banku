namespace Aplikacja_bankowa
{
    public partial class TransferOptionPhone : ContentPage
    {
        public TransferOptionPhone()
        {
            InitializeComponent();
        }

        private async void OnButtonTransferClicked(object sender, EventArgs e)
        {
            await ButtonTransfer.ScaleToAsync(1.1, 150);
            await ButtonTransfer.ScaleToAsync(1.0, 150);
            
            var RecipientsPhoneNumber = EntryRecipientsPhoneNumber.Text;
            var RecipientNameAndSurname = EntryRecipientNameAndSurname.Text;
            var TransferReference = EntryTransferReference.Text;
            var TransferAmount = EntryTransferAmount.Text;

            if (string.IsNullOrEmpty(RecipientsPhoneNumber) || string.IsNullOrEmpty(RecipientNameAndSurname) || string.IsNullOrEmpty(TransferReference) || string.IsNullOrEmpty(TransferAmount))
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