using Microsoft.Extensions.DependencyInjection;
using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class App : Application
    {
        private readonly Database database;

        public App(Database database_)
        {
            InitializeComponent();
            database = database_;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new LoginPage(database)));
        }
    }
}