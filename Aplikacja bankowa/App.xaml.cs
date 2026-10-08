using Microsoft.Extensions.DependencyInjection;

using BankSQLite.Data;
using BankSQLite.Models;

namespace Aplikacja_bankowa
{
    public partial class App : Application
    {
        private readonly UserItemDatabase database;
        public App(UserItemDatabase userItemDatabase)
        {
            InitializeComponent();
            database = userItemDatabase;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new LoginPage(database)));
        }
    }
}