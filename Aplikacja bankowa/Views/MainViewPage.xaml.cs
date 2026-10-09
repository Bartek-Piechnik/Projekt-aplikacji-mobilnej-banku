using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class MainViewPage : ContentPage
    {
        private readonly UserItemDatabase _userItemDatabase;
        private readonly TransactionItemDatabase _transactionItemDatabase;
        public TransactionsItem Item { get; set; }
        
        public MainViewPage(UserItemDatabase userItemDatabase, TransactionItemDatabase transactionItemDatabase)
        {
            InitializeComponent();
            _userItemDatabase = userItemDatabase;
            _transactionItemDatabase = transactionItemDatabase;
            BindingContext = this;
        }

        private async void OnButtonLogOutClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void OnButtonGoToMenuPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuPage());
        }

        private async void OnButtonGoToTransferOptionBLIKClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TransferOptionBLIK());
        }

        private async void OnButtonGoToTransferOptionDomesticClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TransferOptionDomestic(_transactionItemDatabase));
        }

        private async void OnButtonGoToTransferOptionPhoneClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TransferOptionPhone(_transactionItemDatabase));
        }
    }
}