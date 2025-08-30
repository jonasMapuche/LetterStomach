using LetterStomach.Services;

namespace LetterStomach
{
    public partial class App : Application
    {
        public static SQLiteService DataService { get; set; }
        public static MongoDBService MongoDBService { get; set; }
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
            return new Window(new AppShell());
        }
    }
}