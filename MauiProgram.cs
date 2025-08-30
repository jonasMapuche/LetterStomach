using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using LetterStomach.Services;
using LetterStomach.ViewModels;
using LetterStomach.Views;
using LetterStomach.Interfaces;

#if ANDROID
using LetterStomach.Platforms.Android.Services;
#endif

namespace LetterStomach
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .UseMauiCommunityToolkitCamera()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if ANDROID
            builder.Services.AddTransient<IAudioService, AudioService>();
            builder.Services.AddTransient<IRecordService, RecordService>();
            builder.Services.AddTransient<ITextSpeakService, TextSpeakService>();
#endif

            builder.Services.AddTransient<BotViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<SettingViewModel>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<BotView>();
            builder.Services.AddTransient<ExitView>();
            builder.Services.AddTransient<SettingView>();
            builder.Services.AddSingleton<SettingService>();
            builder.Services.AddSingleton<MessageService>();
            builder.Services.AddSingleton<SingletonService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
