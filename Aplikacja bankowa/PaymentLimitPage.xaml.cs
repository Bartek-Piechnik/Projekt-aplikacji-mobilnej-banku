namespace Aplikacja_bankowa
{
    public partial class PaymentLimitPage : ContentPage
    {
        public PaymentLimitPage()
        {
            InitializeComponent();
        }

        private async void OnButtonGoToMenuPageClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage());
        }
    }
}
