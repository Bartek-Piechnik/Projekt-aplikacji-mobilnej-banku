namespace Aplikacja_bankowa
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
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
                await DisplayAlert("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }

            if (Password != RepeatedPassword)
            {
                await DisplayAlert("Błąd", "Hasło i powtórzone hasło muszą być identyczne!", "OK");
                return;
            }

            await Navigation.PopAsync();
        }
    }
}