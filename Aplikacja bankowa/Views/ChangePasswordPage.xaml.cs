namespace Aplikacja_bankowa
{
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private void OnButtonChangeVisibilityOldPasswordClicked(object sender, EventArgs e)
        {
            if (EntryOldPassword.IsPassword)
            {
                EntryOldPassword.IsPassword = false;
                ButtonChangeVisibilityOldPassword.ImageSource = "password_visibility_on.png";
            }
            else
            {
                EntryOldPassword.IsPassword = true;
                ButtonChangeVisibilityOldPassword.ImageSource = "password_visibility_off.png";
            }
        }
        
        private void OnButtonChangeVisibilityNewPasswordClicked(object sender, EventArgs e)
        {
            if (EntryNewPassword.IsPassword)
            {
                EntryNewPassword.IsPassword = false;
                ButtonChangeVisibilityNewPassword.ImageSource = "password_visibility_on.png";
            }
            else
            {
                EntryNewPassword.IsPassword = true;
                ButtonChangeVisibilityNewPassword.ImageSource = "password_visibility_off.png";
            }
        }
        
        private async void OnButtonGoToMenuPageClicked(Object sender, EventArgs e)
        {
            await ButtonGoToMenuPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMenuPage.ScaleToAsync(1.0, 150);
            
            await Navigation.PushAsync(new MenuPage());
        }
    }
}
