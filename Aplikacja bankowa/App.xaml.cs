using BankSQLite.Data;

namespace Aplikacja_bankowa
{
    public partial class App : Application
    {
        private readonly Database database;

        public App(Database database)
        {
            InitializeComponent();
            this.database = database;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new LoginPage(database)));
        }
    }
}