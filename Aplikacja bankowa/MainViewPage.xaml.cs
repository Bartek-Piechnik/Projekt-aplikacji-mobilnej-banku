namespace Aplikacja_bankowa
{
    public partial class MainViewPage : ContentPage
    {
        public MainViewPage()
        {
            InitializeComponent();
        }

        private async void OnButtonLogOutClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void OnButtonGoToMenuPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage());
        }

        private async void OnButtonGoToTransferOptionBLIKClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TransferOptionBLIK());
        }

        private async void OnButtonGoToTransferOptionDomesticClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TransferOptionDomestic());
        }

        private async void OnButtonGoToTransferOptionPhoneClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TransferOptionPhone());
        }
    }
}