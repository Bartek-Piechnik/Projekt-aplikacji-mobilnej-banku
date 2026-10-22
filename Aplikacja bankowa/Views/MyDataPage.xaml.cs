using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class MyDataPage : ContentPage
    {
        private readonly Database database;

        public MyDataPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var UserId = Preferences.Get("UserId", 0);
            UsersItem user = await database.GetItemAsync(UserId);

            if (user != null)
            {
                var userName = user.Name;
                var userSurname = user.Surname;
                var userDateOfBirth = user.DateOfBirth;
                var userEmail = user.Email;
                var userPhoneNumber = user.PhoneNumber;

                var userNameFirstLetter = userName[0].ToString().ToUpper();
                var userSurnameFirstLetter = userSurname[0].ToString().ToUpper();
                var userInitials = userNameFirstLetter + userSurnameFirstLetter;
                Labelinitials.Text = $"{userInitials}";

                EntryName.Text = $"{userName}";
                EntrySurname.Text = $"{userSurname}";
                DatePickerDateOfBirth.Date = userDateOfBirth ?? DateTime.Now;
                EntryEmail.Text = $"{userEmail}";
                EntryPhoneNumber.Text = $"{userPhoneNumber}";
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
            }
        }

        private async void OnButtonGoToMenuPageClicked(Object sender, EventArgs e)
        {
            await ButtonGoToMenuPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMenuPage.ScaleToAsync(1.0, 150);
            
            var UserId = Preferences.Get("UserId", 0);
            UsersItem user = await database.GetItemAsync(UserId);
            
            if (user == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
                return;
            }
            
            var userName = EntryName.Text?.Trim();
            var userSurname = EntrySurname.Text?.Trim();
            var userDateOfBirth = DatePickerDateOfBirth.Date;
            var userEmail = EntryEmail.Text?.Trim();
            var userPhoneNumber = EntryPhoneNumber.Text?.Trim();
            
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userSurname) || userDateOfBirth == null || 
                string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userPhoneNumber))
            {
                await DisplayAlertAsync("Błąd", "Wszystkie pola muszą być uzupełnione!", "OK");
                return;
            }
            
            user.Name = userName;
            user.Surname = userSurname;
            user.DateOfBirth = userDateOfBirth;
            user.Email = userEmail;
            user.PhoneNumber = userPhoneNumber;
            
            var users = await database.GetItemsAsync();
            
            var userEmail_ = users.FirstOrDefault(x =>
                x.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase) && x.ID != user.ID);

            if (userEmail_ != null)
            {
                await DisplayAlertAsync("Błąd", "Email niedostępny!", "OK");
                return;
            }

            await database.SaveItemAsync(user);
            await Navigation.PopAsync();
        }
    }
}