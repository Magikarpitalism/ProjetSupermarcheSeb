using Microsoft.Extensions.Logging;
using ProjetSupermarcheSeb.Data;
using ProjetSupermarcheSeb.Views;
using OxyPlot.Maui.Skia;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace ProjetSupermarcheSeb
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseOxyPlotSkia()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Enregistre DatabaseService comme Singleton pour accéder à la BDD
            builder.Services.AddSingleton<DatabaseService>();

            // 2) Enregistre chaque page comme Transient pour que chaque nouvelle page est son cycle de vie
            builder.Services.AddTransient<GestionPage>();
            builder.Services.AddTransient<SaisirTempsPage>();
            builder.Services.AddTransient<ConsultationPage>();
            builder.Services.AddTransient<StatistiquesPage>();
            builder.Services.AddTransient<GraphiquesPage>();

            return builder.Build();
        }
    }
}
