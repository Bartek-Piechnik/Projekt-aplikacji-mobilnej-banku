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
            var LOGIN = "login1234";
            var PASSWORD = "password1234";
            
            var Login = EntryLogin.Text;
            var Password = EntryPassword.Text;

            if (Login == LOGIN && Password == PASSWORD)
            {
                await Navigation.PushAsync(new MainViewPage());
            }
            else if(Login != LOGIN && Password == PASSWORD) 
            {
                await DisplayAlert("Błąd", "Login jest niepoprawny", "OK");
            }
            else if(Login == LOGIN && Password != PASSWORD) 
            {
                await DisplayAlert("Błąd", "Hasło jest niepoprawne", "OK");
            }
            else
            {
                await DisplayAlert("Błąd", "Login i hasło są niepoprawne", "OK");
            }
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
