namespace Aplikacja_bankowa
{
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private async void OnButtonGoToMenuPageClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage());
        }
    }
}
