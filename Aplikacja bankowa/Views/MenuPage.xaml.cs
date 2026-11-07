using Aplikacja_bankowa.Helpers;
using BankSQLite.Data;

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
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToMyDataPage);

            await Navigation.PushAsync(new MyDataPage(database));
        }

        private async void OnButtonGoToChangePasswordPageClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToChangePasswordPage);

            await Navigation.PushAsync(new ChangePasswordPage(database));
        }

        private async void OnButtonGoToPaymentLimitPageClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToPaymentLimitPage);

            await Navigation.PushAsync(new PaymentLimitPage(database));
        }
        
        private async void OnButtonGoToCashMachinePageClicked(object sender, EventArgs e)
        {
            await ButtonAnimations.AnimateButtonAsync(ButtonGoToCashMachinePage);

            await Navigation.PushAsync(new CashMachinePage(database));
        }
    }
}