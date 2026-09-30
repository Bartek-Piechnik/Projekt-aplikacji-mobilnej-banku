namespace Aplikacja_bankowa
{
    public partial class LoginPage : ContentPage
    {
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
                EntryLogin.Text = "";
                EntryPassword.Text = "";
            }
            else if (Login != LOGIN && Password == PASSWORD)
            {
                await DisplayAlert("Błąd", "Login jest niepoprawny", "OK");
                EntryLogin.Text = "";
            }
            else if(Login == LOGIN && Password != PASSWORD) 
            {
                await DisplayAlert("Błąd", "Hasło jest niepoprawne", "OK");
                EntryPassword.Text = "";
            }
            else
            {
                await DisplayAlert("Błąd", "Login i hasło są niepoprawne", "OK");
                EntryLogin.Text = "";
                EntryPassword.Text = "";
            }
        }
    }
}
