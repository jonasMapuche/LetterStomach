using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LetterStomach.ViewModels
{
    [QueryProperty(nameof(Refresh), "refresh")]
    public partial class HomeViewModel : GrammarViewModel, IQueryAttributable
    {
        public bool Refresh { get; set; }

        [ObservableProperty]
        public ObservableCollection<Message> recentChat;

        [ObservableProperty]
        public string leftImage;

        public ICommand BotCommand { get; set; }
        public ICommand SpeakCommand { get; set; }

        public static List<Message> Messages = new List<Message>();

        private readonly SettingService _singleton;

        public HomeViewModel(SettingService singleton)
        {
            try
            {
                BotCommand = new AsyncRelayCommand<object>(OnBotCommand);
                SpeakCommand = new AsyncRelayCommand<object>(OnSpeakCommand);

                _singleton = singleton;
                bool sqlite_database = _singleton.SQLiteDatabase;
                Connect(sqlite_database);
                Init();
                List<Message> messages = new List<Message>();
                messages = Load(MessageService.Instance, false);
                recentChat = new ObservableCollection<Message>(messages);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Connect(bool sqlite_database)
        {
            try
            { 
                if (sqlite_database)
                {
                    SQLite();
                    MessageService.Instance.Messages = MessageService.Instance.GetChatsSQLite();
                }
                else
                {
                    MongoDB();
                    MessageService.Instance.Messages = MessageService.Instance.GetChats();
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task OnSpeakCommand(object? arg)
        {
            try
            { 
                TextToSpeakService speak_service = new TextToSpeakService();
                int pitch_speak = _singleton.PitchSpeak;
                int volume_speak = _singleton.VolumeSpeak;
                if (SingletonService.Instance.SpeakEnglish)
                {
                    speak_service.SpeakText(MessageService.Instance.Messages, ENGLISH.Uppercase, pitch_speak, volume_speak);
                };
                if (SingletonService.Instance.SpeakDeutsch)
                {
                    speak_service.SpeakText(MessageService.Instance.Messages, DEUTSCH.Uppercase, pitch_speak, volume_speak);
                };
                if (SingletonService.Instance.SpeakItaliano)
                {
                    speak_service.SpeakText(MessageService.Instance.Messages, ITALIANO.Uppercase, pitch_speak, volume_speak);
                }
                ;
                if (SingletonService.Instance.SpeakFrancais)
                {
                    speak_service.SpeakText(MessageService.Instance.Messages, FRANCAIS.Uppercase, pitch_speak, volume_speak);
                }
                ; 
                if (SingletonService.Instance.SpeakEspanol)
                {
                    speak_service.SpeakText(MessageService.Instance.Messages, ESPANOL.Uppercase, pitch_speak, volume_speak);
                }
                ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task OnBotCommand(object user)
        {
            try
            { 
                Dictionary<string, object> navigationParameter = new Dictionary<string, object>
                {
                    { "username", user }
                };
                await Shell.Current.GoToAsync($"{nameof(BotView)}", true, navigationParameter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Init ()
        {
            try
            { 
                _adverb_english = GetAdverb(ENGLISH.Name).Distinct().ToList();
                _adverb_deutsch = GetAdverb(DEUTSCH.Name).Distinct().ToList();
                _adverb_italiano = GetAdverb(ITALIANO.Name).Distinct().ToList();
                _adverb_francais = GetAdverb(FRANCAIS.Name).Distinct().ToList();
                _adverb_espanol = GetAdverb(ESPANOL.Name).Distinct().ToList();

                _pronoun_english = GetPronoun(ENGLISH.Name).Distinct().ToList();
                _pronoun_deutsch = GetPronoun(DEUTSCH.Name).Distinct().ToList();
                _pronoun_italiano = GetPronoun(ITALIANO.Name).Distinct().ToList();
                _pronoun_francais = GetPronoun(FRANCAIS.Name).Distinct().ToList();
                _pronoun_espanol = GetPronoun(ESPANOL.Name).Distinct().ToList();

                _article_english = GetArticle(ENGLISH.Name).Distinct().ToList();
                _article_deutsch = GetArticle(DEUTSCH.Name).Distinct().ToList();
                _article_italiano = GetArticle(ITALIANO.Name).Distinct().ToList();
                _article_francais = GetArticle(FRANCAIS.Name).Distinct().ToList();
                _article_espanol = GetArticle(ESPANOL.Name).Distinct().ToList();

                _numeral_english = GetNumeral(ENGLISH.Name).Distinct().ToList();
                _numeral_deutsch = GetNumeral(DEUTSCH.Name).Distinct().ToList();
                _numeral_italiano = GetNumeral(ITALIANO.Name).Distinct().ToList();
                _numeral_francais = GetNumeral(FRANCAIS.Name).Distinct().ToList();
                _numeral_espanol = GetNumeral(ESPANOL.Name).Distinct().ToList();

                _preposition_english = GetPreposition(ENGLISH.Name).Distinct().ToList();
                _preposition_deutsch = GetPreposition(DEUTSCH.Name).Distinct().ToList();
                _preposition_italiano = GetPreposition(ITALIANO.Name).Distinct().ToList();
                _preposition_francais = GetPreposition(FRANCAIS.Name).Distinct().ToList();
                _preposition_espanol = GetPreposition(ESPANOL.Name).Distinct().ToList();

                _conjunction_english = GetConjunction(ENGLISH.Name).Distinct().ToList();
                _conjunction_deutsch = GetConjunction(DEUTSCH.Name).Distinct().ToList();
                _conjunction_italiano = GetConjunction(ITALIANO.Name).Distinct().ToList();
                _conjunction_francais = GetConjunction(FRANCAIS.Name).Distinct().ToList();
                _conjunction_espanol = GetConjunction(ESPANOL.Name).Distinct().ToList();

                _verb_english = GetVerb(ENGLISH.Name).Distinct().ToList();
                _verb_deutsch = GetVerb(DEUTSCH.Name).Distinct().ToList();
                _verb_italiano = GetVerb(ITALIANO.Name).Distinct().ToList();
                _verb_francais = GetVerb(FRANCAIS.Name).Distinct().ToList();
                _verb_espanol = GetVerb(ESPANOL.Name).Distinct().ToList();

                _sentence_english = GetSentence(ENGLISH.Name).Distinct().ToList();
                _sentence_deutsch = GetSentence(DEUTSCH.Name).Distinct().ToList();
                _sentence_italiano = GetSentence(ITALIANO.Name).Distinct().ToList();
                _sentence_francais = GetSentence(FRANCAIS.Name).Distinct().ToList();
                _sentence_espanol = GetSentence(ESPANOL.Name).Distinct().ToList();

                _letter_english = GetLetter(ENGLISH.Name).Distinct().ToList();
                _letter_deutsch = GetLetter(DEUTSCH.Name).Distinct().ToList();
                _letter_italiano = GetLetter(ITALIANO.Name).Distinct().ToList();
                _letter_francais = GetLetter(FRANCAIS.Name).Distinct().ToList();
                _letter_espanol = GetLetter(ESPANOL.Name).Distinct().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Message> Load (MessageService message_service, bool update)
        {
            try
            { 
                List<Message> messages = new List<Message>();
                messages = message_service.Messages;

                List<Message> new_messages = new List<Message>();
            
                User user_english = message_service.GetUser(ENGLISH.Lowercase);
                User user_deutsch = message_service.GetUser(DEUTSCH.Lowercase);
                User user_italiano = message_service.GetUser(ITALIANO.Lowercase);
                User user_francais = message_service.GetUser(FRANCAIS.Lowercase);
                User user_espanol = message_service.GetUser(ESPANOL.Lowercase);

                foreach (Message item in messages)
                {
                    if (item.Sender == user_english)
                    {
                        new_messages.Add(item);
                        continue;
                    }
                    if (item.Sender == user_deutsch)
                    {
                        new_messages.Add(item);
                        continue;
                    }
                    if (item.Sender == user_italiano)
                    {
                        new_messages.Add(item);
                        continue;
                    }
                    if (item.Sender == user_francais)
                    {
                        new_messages.Add(item);
                        continue;
                    }
                    if (item.Sender == user_espanol)
                    {
                        new_messages.Add(item);
                        continue;
                    }
                };
                return new_messages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            try
            { 
                bool sqlite_database = false;
                if (query.Count > 0) sqlite_database = query["refresh"] as string == "True"? true : false;
                if (sqlite_database) Update(sqlite_database);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(bool sqlite_database)
        {
            try
            { 
                Connect(sqlite_database);
                Init();
                List<Message> messages = new List<Message>();
                messages = Load(MessageService.Instance, true);
                RecentChat = new ObservableCollection<Message>(messages);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
