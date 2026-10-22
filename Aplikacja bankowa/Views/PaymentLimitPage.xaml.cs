using BankSQLite.Data;

namespace Aplikacja_bankowa
{
    public partial class PaymentLimitPage : ContentPage
    {
        private readonly Database database;
        
        public PaymentLimitPage(Database database_)
        {
            InitializeComponent();
            database = database_;
            BindingContext = this;
        }

        private async void OnButtonGoToMenuPageClicked(Object sender, EventArgs e)
        {
            await ButtonGoToMenuPage.ScaleToAsync(1.1, 150);
            await ButtonGoToMenuPage.ScaleToAsync(1.0, 150);

            await Navigation.PushAsync(new MenuPage(database));
        }
    }
}