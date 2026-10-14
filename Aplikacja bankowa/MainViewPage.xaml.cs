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
            await ButtonLogOut.ScaleToAsync(1.1, 150);
            await ButtonLogOut.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnButtonGoToMenuPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToMenuPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMenuPage.ScaleToAsync(1.0, 150);
            
            await Navigation.PushAsync(new MenuPage());
        }

        private async void OnButtonGoToTransferOptionBLIKClicked(object sender, EventArgs e)
        {
            await ButtonGoToTransferOptionBLIK.ScaleToAsync(1.1, 150);
            await ButtonGoToTransferOptionBLIK.ScaleToAsync(1.0, 150);
            
            await Navigation.PushAsync(new TransferOptionBLIK());
        }

        private async void OnButtonGoToTransferOptionDomesticClicked(object sender, EventArgs e)
        {
            await ButtonGoToTransferOptionDomestic.ScaleToAsync(1.1, 150);
            await ButtonGoToTransferOptionDomestic.ScaleToAsync(1.0, 150);
            
            await Navigation.PushAsync(new TransferOptionDomestic());
        }

        private async void OnButtonGoToTransferOptionPhoneClicked(object sender, EventArgs e)
        {
            await ButtonGoToTransferOptionPhone.ScaleToAsync(1.1, 150);
            await ButtonGoToTransferOptionPhone.ScaleToAsync(1.0, 150);
            
            await Navigation.PushAsync(new TransferOptionPhone());
        }
    }
}