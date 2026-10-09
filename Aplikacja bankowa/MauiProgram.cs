using Microsoft.Extensions.Logging;
using BankSQLite.Data;

namespace Aplikacja_bankowa
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

    		builder.Logging.AddDebug();

            builder.Services.AddSingleton<UserItemDatabase>();
            builder.Services.AddSingleton<TransactionItemDatabase>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();


            return builder.Build();
        }
    }
}
