using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Views;
using System.Windows.Input;

namespace LetterStomach
{
    public partial class AppShell : Shell
    {
        private Language PORTUGUES = SettingService.Instance.Portugues;
        public ICommand BotCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(BotView), typeof(BotView));
            Routing.RegisterRoute(nameof(SettingView), typeof(SettingView));
            Routing.RegisterRoute(nameof(ModalView), typeof(ModalView));

            this.BotCommand = new Command(async () => await OnBotCommand());
            this.ExitCommand = new Command(async () => await OnExitCommand());

            BindingContext = this;
        }

        private async Task OnExitCommand()
        {
            System.Environment.Exit(0);
        }

        private async Task OnBotCommand()
        {
            User user = new User();
            user = MessageService.Instance.GetUser(PORTUGUES.Lowercase);
            Dictionary<string, object> navigationParameter = new Dictionary<string, object>
                {
                    { "username", user }
                };
            await Shell.Current.GoToAsync($"{nameof(BotView)}", true, navigationParameter);
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}
