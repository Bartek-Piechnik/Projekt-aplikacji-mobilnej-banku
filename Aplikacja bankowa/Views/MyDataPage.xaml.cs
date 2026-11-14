using Aplikacja_bankowa.Helpers;
using Aplikacja_bankowa.Models;
using BankSQLite.Data;

namespace Aplikacja_bankowa
{
    public partial class MyDataPage : ContentPage
    {
        private readonly Database database;

        public UsersItem User { get; set; }

        public MyDataPage(Database database)
        {
            InitializeComponent();
            this.database = database;
            User = new UsersItem();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var userId = Preferences.Get("UserId", 0);
            User = await database.GetUserByIdAsync(userId);

            if (User != null)
            {
                BindingContext = this;
                var userInitials = User.Name[0].ToString().ToUpper() + User.Surname[0].ToString().ToUpper();
                Labelinitials.Text = $"{userInitials}";
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
            }
        }

        private async void OnButtonChangeMyDataClicked(Object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonChangeMyData);

            if (User == null)
            {
                await DisplayAlertAsync("Błąd", "Nie znaleziono użytkownika", "OK");
                return;
            }

            TextFormatting.FormatUserData(User);
            List<string> errors = FormValidation.ValidateUserData(User);
            
            if (errors.Count > 0)
            {
                string errorMessage = string.Join("\n\n", errors);
                await DisplayAlertAsync("Wykryto błędy w formularzu", errorMessage, "OK");
                return;
            }
            
            var users = await database.GetUsersAsync();
            
            var userEmail_ = users.FirstOrDefault(user =>
                user.Email.Equals(User.Email, StringComparison.OrdinalIgnoreCase) && user.ID != User.ID);

            if (userEmail_ != null)
            {
                await DisplayAlertAsync("Błąd", "Email niedostępny!", "OK");
                return;
            }
            
            await DisplayAlertAsync("Sukces", $"Pomyślnie zmieniono dane:\n Imię: {User.Name},\n Nazwisko: {User.Surname},\n Data urodzenia: {User.DateOfBirth},\n Adres e-mail: {User.Email},\n Numer telefonu: {User.PhoneNumber}", "OK");
            await database.SaveUserAsync(User);
            await Navigation.PushAsync(new MainViewPage(database));
        }
    }
}