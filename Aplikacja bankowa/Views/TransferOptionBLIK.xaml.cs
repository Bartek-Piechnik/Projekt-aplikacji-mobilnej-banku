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
            await Navigation.PopAsync();
        }
    }
}