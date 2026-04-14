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

            builder.Services.AddSingleton<SQLiteContext>();
            builder.Services.AddSingleton<SettingService>();
            builder.Services.AddSingleton<MessageService>();

            builder.Services.AddTransient<BotService>();
            builder.Services.AddTransient<HttpService>();
            builder.Services.AddTransient<ModelService>();
            builder.Services.AddTransient<PerceptionService>();
            builder.Services.AddSingleton<SQLiteService>();
            builder.Services.AddTransient<TextToSpeakService>();
            builder.Services.AddTransient<WordEmbeddingService>();

            builder.Services.AddTransient<SyntaxService>();
            builder.Services.AddTransient<MorphologyService>();
            builder.Services.AddTransient<GrammarService>();

            builder.Services.AddTransient<BotViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<SettingViewModel>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<BotView>();
            builder.Services.AddTransient<SettingView>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
