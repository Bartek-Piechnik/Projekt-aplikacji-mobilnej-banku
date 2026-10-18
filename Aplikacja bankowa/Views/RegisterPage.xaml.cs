namespace Aplikacja_bankowa
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void OnButtonChangeVisibilityPasswordClicked(object sender, EventArgs e)
        {
            if (EntryPassword.IsPassword)
            {
                EntryPassword.IsPassword = false;
                ButtonChangeVisibilityPassword.ImageSource = "password_visibility_on.png";
            }
            else
            {
                EntryPassword.IsPassword = true;
                ButtonChangeVisibilityPassword.ImageSource = "password_visibility_off.png";
            }
        }
        
        private void OnButtonChangeVisibilityRepeatedPasswordClicked(object sender, EventArgs e)
        {
            if (EntryRepeatedPassword.IsPassword)
            {
                EntryRepeatedPassword.IsPassword = false;
                ButtonChangeVisibilityRepeatedPassword.ImageSource = "password_visibility_on.png";
            }
            else
            {
                EntryRepeatedPassword.IsPassword = true;
                ButtonChangeVisibilityRepeatedPassword.ImageSource = "password_visibility_off.png";
            }
        }
        
        private async void OnButtonGoToLoginPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToLoginPage.ScaleToAsync(1.1, 150);
            await ButtonGoToLoginPage.ScaleToAsync(1.0, 150);
            
            var Name = EntryName.Text;
            var Surname = EntrySurname.Text;
            var DateOfBirth = DatePickerDateOfBirth.Date;
            var Email = EntryEmail.Text;
            var PhoneNumber = EntryPhoneNumber.Text;
            var AccountBalance = EntryAccountBalance.Text;
            var Password = EntryPassword.Text;
            var RepeatedPassword = EntryRepeatedPassword.Text;

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(AccountBalance) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(RepeatedPassword))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }

            if (Password != RepeatedPassword)
            {
                await DisplayAlertAsync("Błąd", "Hasło i powtórzone hasło muszą być identyczne!", "OK");
                return;
            }

            await Navigation.PopAsync();
        }
    }
}