using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class MenuPage : ContentPage
    {
        private readonly Database database;
        
        public MenuPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            BindingContext = this;
        }

        private async void OnButtonGoToMyDataPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToMyDataPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMyDataPage.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new MyDataPage(database));
        }

        private async void OnButtonGoToChangePasswordPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToChangePasswordPage.ScaleToAsync(1.1, 150);
            await ButtonGoToChangePasswordPage.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new ChangePasswordPage(database));
        }

        private async void OnButtonGoToPaymentLimitPageClicked(object sender, EventArgs e)
        {
            await ButtonGoToPaymentLimitPage.ScaleToAsync(1.1, 150);
            await ButtonGoToPaymentLimitPage.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new PaymentLimitPage(database));
        }
    }
}