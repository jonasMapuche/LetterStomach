using LetterStomach.Services;
using LetterStomach.Services.Interfaces;

namespace LetterStomach
{
    public partial class App : Application
    {
        public static ISQLiteService DataService { get; set; }
        public static IMongoDBService MongoDBService { get; set; }
        public App()
        {
            try
            {
                InitializeComponent();
                Init();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Init()
        {
            try
            {
                DataService = new SQLiteService();
                DataService.Connect();
                MongoDBService = new MongoDBService();
                MongoDBService.Connect();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            /*
            NavigationPage navigation = new NavigationPage(new AppShell());

            Page paginaAtual = navigation.RootPage;
            */
            return new Window(new AppShell());
        }
    }
}