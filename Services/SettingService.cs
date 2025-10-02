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

        public readonly Language Portugues = new Language
        {
            Name = "portugues",
            Uppercase = "Português",
            Lowercase = "português",
            Code = "pt",
            Region = "PT",
        };

        public readonly string Suject = "sujeito";
        public readonly string Predicate = "predicado";

        public readonly string Pronoun = "pronome";
        public readonly string Noun = "substantivo";
        public readonly string Verb = "verbo";
        public readonly string Adjective = "adjetivo";
        public readonly string Article = "article";
        public readonly string Numeral = "numeral";
        public readonly string Adverb = "adverbio";
        public readonly string Conjunction = "conjuncao";

        public readonly string Adverb_Adverb = "adverbio adverbio";
        public readonly string Adjective_Noun = "adjetivo substantivo";
        public readonly string Adjective_Adverb = "adjetivo adverbio";
        public readonly string Conjunction_Noun = "conjuncao substantivo";
        public readonly string Numeral_Noun = "numeral substantivo";

        public readonly string Personal = "pessoal";
        public readonly string Preposition = "preposicao";
        public readonly string Possessive = "possessivo";
        public readonly string Demostrtive = "demonstrativo";

        public readonly string Single = "singular";
        public readonly string Plural = "plural";

        public readonly string Declarative = "declarativa";

        public readonly HashSet<string> Execute = new HashSet<string>()
        {
            "load",
            "execute",
            "see",
            "view",
            "click",
            "play",
            "record",
            "download",
            "upload",
            "record",
            "rotate",
            "preview",
            "stop"
        };

        public readonly HashSet<string> Load = new HashSet<string>()
        {
            "load",
            "execute",
            "click"
        };

        public readonly HashSet<string> View = new HashSet<string>()
        {
            "view",
            "see"
        };

        public readonly HashSet<string> Play = new HashSet<string>()
        {
            "play"
        };

        public readonly HashSet<string> Record = new HashSet<string>()
        {
            "record"
        };

        public readonly HashSet<string> Stop = new HashSet<string>()
        {
            "stop"
        };

        public readonly HashSet<string> Speak = new HashSet<string>()
        {
            "speak"
        };

        public readonly HashSet<string> Rotate = new HashSet<string>()
        {
            "rotate"
        };

        public readonly HashSet<string> Download = new HashSet<string>()
        {
            "download"
        };

        public readonly HashSet<string> Upload = new HashSet<string>()
        {
            "upload"
        };

        public readonly HashSet<string> Activity = new HashSet<string>()
        {
            "gps",
            "bluetooth",
            "battery",
            "camera",
            "wav",
            "mp3",
            "file"
        };
    }
}
