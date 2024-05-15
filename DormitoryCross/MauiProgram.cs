using DormitoryCross.Services;
using DormitoryCross.View;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;

namespace DormitoryCross
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);

            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

            builder.Services.AddSingleton<ServerServices>();

            builder.Services.AddSingleton<StudentsViewModel>();
            builder.Services.AddTransient<StudentsDetailsViewModel>();

            builder.Services.AddTransient<DetailsPage>();
            

            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
