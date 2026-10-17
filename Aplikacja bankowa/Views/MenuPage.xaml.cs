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
            await ButtonGoToMyDataPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMyDataPage.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new MyDataPage());
        }

        private async void OnButtonGoToChangePasswordPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToChangePasswordPage.ScaleToAsync(1.1, 150);
            await ButtonGoToChangePasswordPage.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new ChangePasswordPage());
        }

        private async void OnButtonGoToPaymentLimitPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToPaymentLimitPage.ScaleToAsync(1.1, 150);
            await ButtonGoToPaymentLimitPage.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new PaymentLimitPage());
        }
    }
}