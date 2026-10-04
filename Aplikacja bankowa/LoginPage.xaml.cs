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
            var EMAIL = "login1234";
            var PASSWORD = "password1234";
            
            var Email = EntryEmail.Text;
            var Password = EntryPassword.Text;

            if (Email == EMAIL && Password == PASSWORD)
            {
                await Navigation.PushAsync(new MainViewPage());
                EntryEmail.Text = "";
                EntryPassword.Text = "";
            }
            else if (Email != EMAIL && Password == PASSWORD)
            {
                await DisplayAlert("Błąd", "Login jest niepoprawny", "OK");
                EntryEmail.Text = "";
            }
            else if(Email == EMAIL && Password != PASSWORD) 
            {
                await DisplayAlert("Błąd", "Hasło jest niepoprawne", "OK");
                EntryPassword.Text = "";
            }
            else
            {
                await DisplayAlert("Błąd", "Login i hasło są niepoprawne", "OK");
                EntryEmail.Text = "";
                EntryPassword.Text = "";
            }
        }
    }
}
