namespace Aplikacja_bankowa
{
    public partial class LoginPage : ContentPage
    {
        //int count = 0;

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnButtonGoToRegisterPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void OnButtonGoToMainViewPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainViewPage());
        }
        
        // private void OnCounterClicked(object? sender, EventArgs e)
        // {
        //     count++;
        //
        //     if (count == 1)
        //         CounterBtn.Text = $"Clicked {count} time";
        //     else
        //         CounterBtn.Text = $"Clicked {count} times";
        //
        //     SemanticScreenReader.Announce(CounterBtn.Text);
        // }
    }
}
