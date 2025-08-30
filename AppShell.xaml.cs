using LetterStomach.Views;

namespace LetterStomach
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(BotView), typeof(BotView));
            Routing.RegisterRoute(nameof(SettingView), typeof(SettingView));
        }
    }
}
