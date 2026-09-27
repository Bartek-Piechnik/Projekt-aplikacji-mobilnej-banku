using Microsoft.Extensions.DependencyInjection;

namespace Aplikacja_bankowa
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainViewPage());
        }
    }
}