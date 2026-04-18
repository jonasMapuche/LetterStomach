using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using LetterStomach.Services;
using LetterStomach.ViewModels;
using LetterStomach.Views;
using LetterStomach.Interfaces;
using LetterStomach.Data;

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
                    fonts.AddFont("OpenSansRegular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSansSemibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MetropolisMedium.ttf", "MetropolisMedium");
                });

#if ANDROID
            builder.Services.AddTransient<IAudioService, AudioService>();
            builder.Services.AddTransient<IRecordService, RecordService>();
            builder.Services.AddTransient<ITextSpeakService, TextSpeakService>();
#endif

            builder.Services.AddSingleton<AlgarismoContext>();
            builder.Services.AddSingleton<AssistenteContext>();
            builder.Services.AddSingleton<CircunstanciaContext>();
            builder.Services.AddSingleton<ElocucaoContext>();
            builder.Services.AddSingleton<EstoutroContext>();
            builder.Services.AddSingleton<JuncaoContext>();
            builder.Services.AddSingleton<LigacaoContext>();
            builder.Services.AddSingleton<MaterialContext>();
            builder.Services.AddSingleton<PreceitoContext>();
            builder.Services.AddSingleton<SentencaContext>();
            builder.Services.AddSingleton<MongoDBService>();

            builder.Services.AddSingleton<HttpService>();
            builder.Services.AddSingleton<ModelService>();

            builder.Services.AddSingleton<SQLiteContext>();
            builder.Services.AddSingleton<SQLiteService>();

            builder.Services.AddSingleton<SettingService>();
            builder.Services.AddSingleton<MessageService>();

            builder.Services.AddSingleton<BotService>();
            builder.Services.AddSingleton<TextToSpeakService>();

            builder.Services.AddTransient<SettingViewModel>();
            builder.Services.AddTransient<SettingView>();

            builder.Services.AddSingleton<PerceptionService>();

            builder.Services.AddTransient<BotViewModel>();
            builder.Services.AddTransient<BotView>();

            builder.Services.AddSingleton<WordEmbeddingService>();

            builder.Services.AddSingleton<SyntaxService>();
            builder.Services.AddSingleton<MorphologyService>();
            builder.Services.AddSingleton<GrammarService>();

            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<HomeView>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
