namespace Aplikacja_bankowa
{
    public partial class TransferOptionBLIK : ContentPage
    {
        public TransferOptionBLIK()
        {
            InitializeComponent();
        }

        private async void OnButtonCopyCodeClicked(object sender, EventArgs e)
        {
            await ButtonCopyCode.ScaleToAsync(1.1, 150);
            await ButtonCopyCode.ScaleToAsync(1.0, 150);

            await Clipboard.Default.SetTextAsync(LabelCode.Text);

            await DisplayAlertAsync("Sukces", "Kod został skopiowany!", "OK");
            await Navigation.PopAsync();
        }
    }
}