namespace LetterStomach.Services
{
    public class SingletonService
    {
        private static SingletonService _instance;
        private static readonly object _lock = new object();

        public static SingletonService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonService();
                    }
                    return _instance;
                }
            }
        }

        public bool PauseEnglish { get; set; } = false;
        public bool PauseDeutsch { get; set; } = false;
        public bool PauseItaliano { get; set; } = false;
        public bool PauseFrancais { get; set; } = false;
        public bool PauseEspanol { get; set; } = false;

        public bool SpeakEnglish { get; set; } = false;
        public bool SpeakDeutsch { get; set; } = false;
        public bool SpeakItaliano { get; set; } = false;
        public bool SpeakFrancais { get; set; } = false;
        public bool SpeakEspanol { get; set; } = false;

    }
}
