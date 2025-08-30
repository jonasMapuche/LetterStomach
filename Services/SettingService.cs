using LetterStomach.Models;

namespace LetterStomach.Services
{
    public class SettingService
    {
        private static SettingService _instance;
        private static readonly object _lock = new object();

        public static SettingService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SettingService();
                    }
                    return _instance;
                }
            }
        }

        public bool UpdateDatabase { get; set; } = false;

        public bool SQLiteDatabase { get; set; } = false;

        public int PitchSpeak { get; set; } = 50;

        public int VolumeSpeak { get; set; } = 50;

        public readonly Language English = new Language
        {
            Name = "english",
            Uppercase = "English",
            Lowercase = "english",
            Code = "en",
            Region = "US",
        };

        public readonly Language Deutsch = new Language
        {
            Name = "deutsch",
            Uppercase = "Deutsch",
            Lowercase = "deutsch",
            Code = "de",
            Region = "DE",
        };

        public readonly Language Italino = new Language
        {
            Name = "italiano",
            Uppercase = "Italiano",
            Lowercase = "italiano",
            Code = "it",
            Region = "IT",
        };

        public readonly Language Francais = new Language
        {
            Name = "francais",
            Uppercase = "Français",
            Lowercase = "français",
            Code = "fr",
            Region = "FR",
        };

        public readonly Language Espanol = new Language
        {
            Name = "espanol",
            Uppercase = "Español",
            Lowercase = "español",
            Code = "es",
            Region = "ES",
        };
    }
}
