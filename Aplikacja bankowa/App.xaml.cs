using Microsoft.Extensions.DependencyInjection;

using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class App : Application
    {
        private readonly UserItemDatabase _userItemDatabase;
        private readonly TransactionItemDatabase _transactionItemDatabase;
        public App(UserItemDatabase userItemDatabase, TransactionItemDatabase transactionItemDatabase)
        {
            InitializeComponent();
            _userItemDatabase = userItemDatabase;
            _transactionItemDatabase = transactionItemDatabase;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new LoginPage(_userItemDatabase, _transactionItemDatabase)));
        }
    }
}