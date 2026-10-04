namespace Aplikacja_bankowa
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }
        private async void OnButtonGoToMyDataPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyDataPage());
        }
        
        private async void OnButtonGoToChangePasswordPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePasswordPage());
        }

        private async void OnButtonGoToPaymentLimitPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaymentLimitPage());
        }
    }
}