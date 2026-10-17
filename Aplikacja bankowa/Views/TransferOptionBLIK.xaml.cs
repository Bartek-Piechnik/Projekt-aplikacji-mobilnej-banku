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

            await Navigation.PopAsync();
        }
    }
}