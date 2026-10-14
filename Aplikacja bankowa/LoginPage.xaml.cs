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
            await ButtonGoToRegisterPage.ScaleToAsync(1.1, 150);
            await ButtonGoToRegisterPage.ScaleToAsync(1.0, 150);
            
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void OnButtonGoToMainViewPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToMainViewPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMainViewPage.ScaleToAsync(1.0, 150);
            
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
