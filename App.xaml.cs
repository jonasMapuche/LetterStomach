using LetterStomach.Services;
using LetterStomach.Services.Interfaces;

namespace LetterStomach
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}