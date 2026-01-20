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

        public bool ModeBot { get; set; } = false;

        private static string english = "english";
        private static string deutsch = "deutsch";
        private static string italiano = "italiano";
        private static string français = "français";
        private static string espanol = "español";
        private static string portugues = "português";

        public readonly Language English = new Language
        {
            Name = "english",
            Uppercase = "English",
            Lowercase = english,
            Code = "en",
            Region = "US",
        };

        public readonly Language Deutsch = new Language
        {
            Name = "deutsch",
            Uppercase = "Deutsch",
            Lowercase = deutsch,
            Code = "de",
            Region = "DE",
        };

        public readonly Language Italino = new Language
        {
            Name = "italiano",
            Uppercase = "Italiano",
            Lowercase = italiano,
            Code = "it",
            Region = "IT",
        };

        public readonly Language Francais = new Language
        {
            Name = "francais",
            Uppercase = "Français",
            Lowercase = français,
            Code = "fr",
            Region = "FR",
        };

        public readonly Language Espanol = new Language
        {
            Name = "espanol",
            Uppercase = "Español",
            Lowercase = espanol,
            Code = "es",
            Region = "ES",
        };

        public readonly Language Portugues = new Language
        {
            Name = "portugues",
            Uppercase = "Português",
            Lowercase = portugues,
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

        private static readonly string load_english = "load";
        private static readonly string execute_english = "execute";
        private static readonly string see_english = "see";
        private static readonly string view_english = "view";
        private static readonly string click_english = "click";
        private static readonly string play_english = "play";
        private static readonly string record_english = "record";
        private static readonly string download_english = "download";
        private static readonly string upload_english = "upload";
        private static readonly string rotate_english = "rotate";
        private static readonly string preview_english = "preview";
        private static readonly string stop_english = "stop";
        private static readonly string capture_english = "capture";
        private static readonly string speak_english = "speak";
        private static readonly string save_english = "save";
        private static readonly string dont_capture_english = "do not capture";
        private static readonly string turn_english = "turn";
        private static readonly string start_english = "start";
        private static readonly string dont_start_english = "do not start";
        private static readonly string send_english = "send";
        private static readonly string dont_send_english = "do not send";
        private static readonly string scan_english = "scan";
        private static readonly string is_english = "is";
        private static readonly string choose_english = "choose";
        private static readonly string connect_english = "connect";
        private static readonly string share_english = "share";
        private static readonly string write_english = "write";
        private static readonly string end_english = "end";
        private static readonly string terminate_english = "terminate";
        private static readonly string turn_on_english = "turn on";

        private static readonly string gps_english = "gps";
        private static readonly string bluetooth_english = "bluetooth";
        private static readonly string battery_english = "battery";
        private static readonly string wav_english = "wav";
        private static readonly string mp3_english = "mp3";
        private static readonly string camera_english = "camera";
        private static readonly string file_english = "file";
        private static readonly string vibration_english = "vibration";
        private static readonly string phone_english = "phone";
        private static readonly string text_english = "text";
        private static readonly string flash_english = "flash";
        private static readonly string audio_english = "audio";
        private static readonly string connection_english = "connection";
        private static readonly string name_english = "name";
        private static readonly string on_english = "on";
        private static readonly string off_english = "off";
        private static readonly string auto_english = "auto";
        private static readonly string front_english = "front";
        private static readonly string rear_english = "rear";
        private static readonly string options_english = "options";
        private static readonly string bot_english = "bot";
        private static readonly string raspberry_english = "raspberry";
        private static readonly string bluetooth_3_english = "bluetooth 3";
        private static readonly string bluetooth_4_english = "bluetooth 4";

        private static readonly string what_english = "what";

        public Dictionary<string, string> Execute = new Dictionary<string, string>()
        {
            { load_english, english},
            { execute_english, english},
            { see_english, english },
            { click_english, english },
            { play_english, english },
            { record_english, english },
            { download_english, english },
            { upload_english, english },
            { rotate_english, english },
            { preview_english, english },
            { stop_english, english },
            { capture_english, english },
            { speak_english, english },
            { view_english, english },
            { terminate_english, english },
            { start_english, english },
            { save_english,english },
            { turn_english, english },
            { turn_on_english, english },
            { share_english, english }
        };

        public Dictionary<string, string> Load = new Dictionary<string, string>()
        {
            { load_english, english },
            { execute_english, english },
            { click_english, english },
        };

        public Dictionary<string, string> View = new Dictionary<string, string>()
        {
            { view_english, english },
            { see_english, english }
        };

        public Dictionary<string, string> Play = new Dictionary<string, string>()
        {
            { play_english, english }
        };

        public Dictionary<string, string> Record = new Dictionary<string, string>()
        {
            { record_english, english }
        };

        public Dictionary<string, string> Stop = new Dictionary<string, string>()
        {
            { stop_english, english }
        };

        public Dictionary<string, string> Speak = new Dictionary<string, string>()
        {
            { speak_english, english }
        };

        public Dictionary<string, string> Rotate = new Dictionary<string, string>()
        {
            { rotate_english, english }
        };

        public Dictionary<string, string> Download = new Dictionary<string, string>()
        {
            { download_english, english }
        };

        public Dictionary<string, string> Upload = new Dictionary<string, string>()
        {
            { upload_english, english }
        };

        public Dictionary<string, string> Capture = new Dictionary<string, string>()
        {
            { capture_english, english },
            { record_english, english }
        };

        public Dictionary<string, string> Save = new Dictionary<string, string>()
        {
            { save_english,english }
        };

        public Dictionary<string, string> Write = new Dictionary<string, string>()
        {
            { write_english, english }
        };

        public Dictionary<string, string> Activity = new Dictionary<string, string>()
        {
            { gps_english, english },
            { bluetooth_english, english },
            { battery_english, english },
            { camera_english, english },
            { wav_english, english },
            { mp3_english, english },
            { file_english, english },
            { vibration_english, english },
            { phone_english, english },
            { audio_english, english },
            { bot_english, english },
            { flash_english, english }
        };

        public Dictionary<string, string> Feature = new Dictionary<string, string>()
        {
            { on_english, english },
            { off_english, english },
            { auto_english, english },
            { front_english, english },
            { rear_english, english }  
        };

        public Dictionary<string, string> GPS = new Dictionary<string, string>()
        {
            { gps_english, english }
        };

        public Dictionary<string, string> Bluetooth = new Dictionary<string, string>()
        {
            { bluetooth_english, english }
        };

        public Dictionary<string, string> Bluetooth3 = new Dictionary<string, string>()
        {
            { bluetooth_3_english, english }
        };

        public Dictionary<string, string> Bluetooth4 = new Dictionary<string, string>()
        {
            { bluetooth_4_english, english }
        };

        public Dictionary<string, string> Bluetooths = new Dictionary<string, string>()
        {
            { bluetooth_4_english, english },
            { bluetooth_3_english, english },
            { bluetooth_english, english }
        };

        public Dictionary<string, string> Battery = new Dictionary<string, string>()
        {
            { battery_english, english }
        };

        public Dictionary<string, string> Camera = new Dictionary<string, string>()
        {
            { camera_english, english }
        };

        public Dictionary<string, string> WAV = new Dictionary<string, string>()
        {
            { wav_english, english }
        };

        public Dictionary<string, string> MP3 = new Dictionary<string, string>()
        {
            { mp3_english, english }
        };

        public Dictionary<string, string> File = new Dictionary<string, string>()
        {
            { file_english, english }
        };

        public Dictionary<string, string> Vibration = new Dictionary<string, string>()
        {
            { vibration_english, english }
        };

        public Dictionary<string, string> Phone = new Dictionary<string, string>()
        {
            { phone_english, english }
        };

        public Dictionary<string, string> Text = new Dictionary<string, string>()
        {
            { text_english, english }
        };

        public Dictionary<string, string> Flash = new Dictionary<string, string>()
        {
            { flash_english, english }
        };

        public Dictionary<string, string> On = new Dictionary<string, string>()
        {
            { on_english, english }
        };

        public Dictionary<string, string> Off = new Dictionary<string, string>()
        {
            { off_english, english }
        };

        public Dictionary<string, string> Auto = new Dictionary<string, string>()
        {
            { auto_english, english }
        };

        public Dictionary<string, string> Raspberry = new Dictionary<string, string>()
        {
            { raspberry_english, english }
        };

        public Dictionary<string, string> Catch = new Dictionary<string, string>()
        {
            { on_english, english },
            { off_english, english },
            { auto_english, english },
            { front_english, english },
            { rear_english, english },
            { capture_english, english },
            { dont_capture_english, english },
            { mp3_english, english },
            { wav_english, english },
            { stop_english, english },
            { terminate_english, english },
            { upload_english, english },
            { bluetooth_english, english },
            { download_english, english },
            { raspberry_english, english },
            { bluetooth_3_english, english },
            { bluetooth_4_english, english },
            { save_english, english }
        };

        public Dictionary<string, string> Catch_Camera = new Dictionary<string, string>()
        {
            { on_english, english },
            { off_english, english },
            { auto_english, english },
            { front_english, english },
            { rear_english, english },
            { capture_english, english },
            { save_english, english }
        };

        public Dictionary<string, string> Catch_Record = new Dictionary<string, string>()
        {
            { mp3_english, english },
            { wav_english, english },
            { stop_english, english }
        };

        public Dictionary<string, string> Catch_Flash = new Dictionary<string, string>()
        {
            { on_english, english },
            { off_english, english },
            { auto_english, english }
        };

        public Dictionary<string, string> Catch_Rotate = new Dictionary<string, string>()
        {
            { front_english, english },
            { rear_english, english }
        };

        public Dictionary<string, string> Catch_Capture = new Dictionary<string, string>()
        {
            { capture_english, english }
        };

        public Dictionary<string, string> Shoot = new Dictionary<string, string>()
        {
            { capture_english, english }
        };

        public Dictionary<string, string> Dont_Shoot = new Dictionary<string, string>()
        {
            { dont_capture_english, english }
        };

        public Dictionary<string, string> Front = new Dictionary<string, string>()
        {
            { front_english, english }
        };

        public Dictionary<string, string> Rear = new Dictionary<string, string>()
        {
            { rear_english, english }
        };

        public Dictionary<string, string> Turn = new Dictionary<string, string>()
        {
            { turn_english, english }
        };

        public Dictionary<string, string> Turn_On = new Dictionary<string, string>()
        {
            { turn_on_english, english }
        };

        public Dictionary<string, string> Record_Audio = new Dictionary<string, string>()
        {
            { wav_english, english },
            { mp3_english, english },
            { start_english, english },
            { dont_start_english, english }
        };

        public Dictionary<string, string> Catch_Audio = new Dictionary<string, string>()
        {
            { wav_english, english },
            { mp3_english, english }
        };

        public Dictionary<string, string> Catch_Start = new Dictionary<string, string>()
        {
            { start_english, english },
            { dont_start_english, english }
        };

        public Dictionary<string, string> Audio = new Dictionary<string, string>()
        {
            { audio_english, english }
        };

        public Dictionary<string, string> Start = new Dictionary<string, string>()
        {
            { start_english, english }
        };

        public Dictionary<string, string> Dont_Start = new Dictionary<string, string>()
        {
            { dont_start_english, english }
        };

        public Dictionary<string, string> Send = new Dictionary<string, string>()
        {
            { send_english, english }
        };

        public Dictionary<string, string> Dont_Send = new Dictionary<string, string>()
        {
            { dont_send_english, english }
        };

        public Dictionary<string, string> Load_Share = new Dictionary<string, string>()
        {
            { load_english, english },
        };

        public Dictionary<string, string> scan = new Dictionary<string, string>()
        {
            { scan_english, english },
        };

        public Dictionary<string, string> connect = new Dictionary<string, string>()
        {
            { connect_english, english },
        };

        public Dictionary<string, string> name = new Dictionary<string, string>()
        {
            { name_english, english },
        };

        public Dictionary<string, string> connection = new Dictionary<string, string>()
        {
            { connection_english, english },
        };

        public Dictionary<string, string> is_be = new Dictionary<string, string>()
        {
            { is_english, english },
        };

        public Dictionary<string, string> what = new Dictionary<string, string>()
        {
            { what_english, english },
        };

        public Dictionary<string, string> choose = new Dictionary<string, string>()
        {
            { choose_english, english },
        };

        public Dictionary<string, string> options = new Dictionary<string, string>()
        {
            { options_english, english },
        };

        public Dictionary<string, string> Share = new Dictionary<string, string>()
        {
            { share_english, english },
        };

        public Dictionary<string, string> Catch_Share = new Dictionary<string, string>()
        {
            { upload_english, english },
            { bluetooth_english, english },
            { bluetooth_3_english, english },
            { bluetooth_4_english, english },
            { download_english, english },
            { raspberry_english, english }
        };

        public Dictionary<string, string> Catch_Scan = new Dictionary<string, string>()
        {
            { scan_english, english },
            { bluetooth_english, english }
        };

        public Dictionary<string, string> End = new Dictionary<string, string>()
        {
            { end_english, english }
        };

        public Dictionary<string, string> Bot = new Dictionary<string, string>()
        {
            { bot_english, english }
        };

        public Dictionary<string, string> Terminate = new Dictionary<string, string>()
        {
            { terminate_english, english }
        };
    }
}
