using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Mopups.Hosting;
using Syncfusion.Maui.Core.Hosting;

namespace MME.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureMopups()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("JosefinSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("JosefinSans-SemiBold.ttf", "OpenSansSemiBold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}