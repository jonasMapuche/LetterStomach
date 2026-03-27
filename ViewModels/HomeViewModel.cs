using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using LetterStomach.Views;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;

namespace LetterStomach.ViewModels
{
    [QueryProperty(nameof(Refresh), "refresh")]
    public partial class HomeViewModel : ObservableObject, IQueryAttributable
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;

        private string _error_message;

        public string error_message
        {
            get => this._error_message;
            set
            {
                this._error_message = value;
            }
        }

        public event EventHandler<string> OnError;
        #endregion

        #region VARIABLE
        public bool Refresh { get; set; }

        [ObservableProperty]
        public ObservableCollection<Message> recentChat;

        public ICommand BotCommand { get; set; }
        public ICommand SpeakCommand { get; set; }
        public ICommand SwipedCommand { get; set; }

        private List<Materia> _lesson_english;
        private List<Materia> _lesson_deutsch;
        private List<Materia> _lesson_italiano;
        private List<Materia> _lesson_francais;
        private List<Materia> _lesson_espanol;

        private Materia _english;
        private Materia _deutsch;
        private Materia _italiano;
        private Materia _francais;
        private Materia _espanol;

        private List<Word> _word_english;
        private List<Word> _word_deutsch;
        private List<Word> _word_italiano;
        private List<Word> _word_francais;
        private List<Word> _word_espanol;

        private Language ENGLISH = SettingService.Instance.English;
        private Language DEUTSCH = SettingService.Instance.Deutsch;
        private Language ITALIANO = SettingService.Instance.Italino;
        private Language FRANCAIS = SettingService.Instance.Francais;
        private Language ESPANOL = SettingService.Instance.Espanol;
        private HashSet<string> LESSON = SettingService.Instance.Lesson;

        public static ISQLiteService _sQLiteService;
        private IGrammarService _grammarService;
        private SettingService _settingService;
        #endregion

        #region CONSTRUCTOR
        public HomeViewModel()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Home\" view model failed!");
                else this.error_message = string.Empty;

                this._grammarService = new GrammarService();
                this._settingService = SettingService.Instance;

                this.BotCommand = new AsyncRelayCommand<object>(OnBotCommand);
                this.SpeakCommand = new AsyncRelayCommand<object>(OnSpeakCommand);
                this.SwipedCommand = new AsyncRelayCommand<SwipeDirection>(OnSwipedCommand);

                Database();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region COMMAND
        private async Task OnSwipedCommand(SwipeDirection direction)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation swiped command \"Home\" view model failed!");

                await Shell.Current.GoToAsync(nameof(ModalView));
                Thread backgroundThread = new Thread(async () =>
                {
                    if (direction == SwipeDirection.Left)
                    {
                        MountPrevious();
                    }
                    else if (direction == SwipeDirection.Right)
                    {
                        MountNext();
                    }
                    else if (direction == SwipeDirection.Up)
                    {
                        MountUp();
                    }
                    else if (direction == SwipeDirection.Down)
                    {
                        MountDown();
                    }
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.GoToAsync("..");
                    });
                });
                backgroundThread.Start();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        private async Task OnSpeakCommand(object? arg)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation speak command \"Home\" view model failed!");

                ITextToSpeakService speak_service = new TextToSpeakService();
                float pitch_speak = this._settingService.PitchFloat;
                float volume_speak = this._settingService.PitchSpeak;
                if (SingletonService.Instance.SpeakEnglish)
                {
                    speak_service.SpeakText(MessageService.Instance.Chats, ENGLISH.Uppercase, pitch_speak, volume_speak);
                }
                ;
                if (SingletonService.Instance.SpeakDeutsch)
                {
                    speak_service.SpeakText(MessageService.Instance.Chats, DEUTSCH.Uppercase, pitch_speak, volume_speak);
                }
                ;
                if (SingletonService.Instance.SpeakItaliano)
                {
                    speak_service.SpeakText(MessageService.Instance.Chats, ITALIANO.Uppercase, pitch_speak, volume_speak);
                }
                ;
                if (SingletonService.Instance.SpeakFrancais)
                {
                    speak_service.SpeakText(MessageService.Instance.Chats, FRANCAIS.Uppercase, pitch_speak, volume_speak);
                }
                ;
                if (SingletonService.Instance.SpeakEspanol)
                {
                    speak_service.SpeakText(MessageService.Instance.Chats, ESPANOL.Uppercase, pitch_speak, volume_speak);
                }
                ;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
         
        private async Task OnBotCommand(object user)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation bot command \"Home\" view model failed!");

                Dictionary<string, object> navigationParameter = new Dictionary<string, object>
                {
                    { "username", user }
                };
                await Shell.Current.GoToAsync($"{nameof(BotView)}", true, navigationParameter);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation apply query attibutes \"Home\" view model failed!");

                bool database = false;
                bool refresh = false;
                if (query.Count > 0)
                {
                    refresh = true;
                    database = query["refresh"] as string == "True" ? true : false;
                    query.Remove("refresh");
                }
                if (refresh) Update(database);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion

        #region INITIALIZATION
        private void Database()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init database \"Home\" view model failed!");

                SQLiteService sQLiteService = new SQLiteService();
                _sQLiteService = sQLiteService;
                _sQLiteService.Exist();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public void Init()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init \"Home\" view model failed!");

                bool database = this._settingService.SQLiteDatabase;
                Connect(database);
                Grammar();
                MountNext();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Update(bool database)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation update \"Home\" view model failed!");

                Connect(database);
                Grammar();
                MountNext();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Connect(bool database)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect \"Home\" view model failed!");

                if (database) this._grammarService.SQLite(_sQLiteService);
                else this._grammarService.MongoDB();
                MessageService.Instance.Chats = MessageService.Instance.GetChats();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Grammar()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation grammar \"Home\" view model failed!");

                this._grammarService.Init();

                this._lesson_english = this._grammarService.GetLetter(ENGLISH.Lowercase).OrderBy(index => index.ordem).ToList();
                this._lesson_deutsch = this._grammarService.GetLetter(DEUTSCH.Lowercase).OrderBy(index => index.ordem).ToList();
                this._lesson_italiano = this._grammarService.GetLetter(ITALIANO.Lowercase).OrderBy(index => index.ordem).ToList();
                this._lesson_francais = this._grammarService.GetLetter(FRANCAIS.Lowercase).OrderBy(index => index.ordem).ToList();
                this._lesson_espanol = this._grammarService.GetLetter(ESPANOL.Lowercase).OrderBy(index => index.ordem).ToList();

                this._word_english = new List<Word>();
                this._word_deutsch = new List<Word>();
                this._word_italiano = new List<Word>();
                this._word_francais = new List<Word>();
                this._word_espanol = new List<Word>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Message(MessageService message_service, bool update, string language, string oration)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation message \"Home\" view model failed!");

                User user_english = message_service.GetUser(ENGLISH.Lowercase);
                User user_deutsch = message_service.GetUser(DEUTSCH.Lowercase);
                User user_italiano = message_service.GetUser(ITALIANO.Lowercase);
                User user_francais = message_service.GetUser(FRANCAIS.Lowercase);
                User user_espanol = message_service.GetUser(ESPANOL.Lowercase);
                List<Message> messages = message_service.Chats;
                List<Message> memos = new List<Message>();
                message_service.Chats.ForEach(index => memos.Add(index));
                foreach (Message item in messages)
                {
                    if (((item.Sender == user_english) && (language == ENGLISH.Lowercase))
                        || ((item.Sender == user_deutsch) && (language == DEUTSCH.Lowercase))
                        || ((item.Sender == user_italiano) && (language == ITALIANO.Lowercase))
                        || ((item.Sender == user_francais) && (language == FRANCAIS.Lowercase))
                        || ((item.Sender == user_espanol) && (language == ESPANOL.Lowercase)))
                    {
                        int index = memos.IndexOf(item);
                        Message message = new Message();
                        message = memos[index];
                        message.Text = oration;
                        memos[index] = message;
                    }
                }
                RecentChat = new ObservableCollection<Message>(memos);
                MessageService.Instance.Chats = memos;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region LESSON
        private void Lesson(Materia materia, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation lesson \"Home\" view model failed!");

                if (language == ENGLISH.Lowercase) this._english = materia;
                if (language == DEUTSCH.Lowercase) this._deutsch = materia;
                if (language == ITALIANO.Lowercase) this._italiano = materia;
                if (language == FRANCAIS.Lowercase) this._francais = materia;
                if (language == ESPANOL.Lowercase) this._espanol = materia;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Oration(List<Word> oration, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation oration \"Home\" view model failed!");

                if (language == ENGLISH.Lowercase) _word_english = oration;
                if (language == DEUTSCH.Lowercase) _word_deutsch = oration;
                if (language == ITALIANO.Lowercase) _word_italiano = oration;
                if (language == FRANCAIS.Lowercase) _word_francais = oration;
                if (language == ESPANOL.Lowercase) _word_espanol = oration;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region MOUNT
        private void MountNext()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount next \"Home\" view model failed!");

                Next(_lesson_english, _english, ENGLISH.Lowercase);
                Next(_lesson_deutsch, _deutsch, DEUTSCH.Lowercase);
                Next(_lesson_italiano, _italiano, ITALIANO.Lowercase);
                Next(_lesson_francais, _francais, FRANCAIS.Lowercase);
                Next(_lesson_espanol, _espanol, ESPANOL.Lowercase);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void MountPrevious()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount previous \"Home\" view model failed!");

                Previous(_lesson_english, _english, ENGLISH.Lowercase);
                Previous(_lesson_deutsch, _deutsch, DEUTSCH.Lowercase);
                Previous(_lesson_italiano, _italiano, ITALIANO.Lowercase);
                Previous(_lesson_francais, _francais, FRANCAIS.Lowercase);
                Previous(_lesson_espanol, _espanol, ESPANOL.Lowercase);
                throw new InvalidOperationException(this.error_message);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void MountDown()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount down \"Home\" view model failed!");

                if (SingletonService.Instance.PauseEnglish) Down(_english, ENGLISH.Lowercase, _word_english);
                if (SingletonService.Instance.PauseDeutsch) Down(_deutsch, DEUTSCH.Lowercase, _word_deutsch);
                if (SingletonService.Instance.PauseItaliano) Down(_italiano, ITALIANO.Lowercase, _word_italiano);
                if (SingletonService.Instance.PauseFrancais) Down(_francais, FRANCAIS.Lowercase, _word_francais);
                if (SingletonService.Instance.PauseEspanol) Down(_espanol, ESPANOL.Lowercase, _word_espanol);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void MountUp()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount up \"Home\" view model failed!");

                if (SingletonService.Instance.PauseEnglish) Up(_english, ENGLISH.Lowercase, _word_english);
                if (SingletonService.Instance.PauseDeutsch) Up(_deutsch, DEUTSCH.Lowercase, _word_deutsch);
                if (SingletonService.Instance.PauseItaliano) Up(_italiano, ITALIANO.Lowercase, _word_italiano);
                if (SingletonService.Instance.PauseFrancais) Up(_francais, FRANCAIS.Lowercase, _word_francais);
                if (SingletonService.Instance.PauseEspanol) Up(_espanol, ESPANOL.Lowercase, _word_espanol);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion

        #region MOVE
        private List<Materia> BookLesson(List<Materia> books)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation book lesson \"Home\" view model failed!");

                HashSet<string> lecture = new HashSet<string>();
                lecture = LESSON;
                List<Materia> edition = new List<Materia>();
                books.ForEach(index =>
                {
                    if (Array.IndexOf(lecture.ToArray(), index.titulo) != -1)
                        edition.Add(index);
                });
                return edition;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Next(List<Materia> books, Materia lesson, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation next \"Home\" view model failed!");

                List<Materia> editions = new List<Materia>();
                editions = BookLesson(books);

                int value = editions.IndexOf(lesson) + 1;
                if (value == editions.Count) value = editions.IndexOf(lesson);
                if (editions.Count != 0)
                {
                    lesson = editions[value];
                    Lesson(lesson, language);
                    Move(language, lesson, editions);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Previous(List<Materia> books, Materia lesson, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation previous \"Home\" view model failed!");

                List<Materia> editions = new List<Materia>();
                editions = BookLesson(books);

                int value = editions.IndexOf(lesson) - 1;
                if (value == -1) value = 0;
                if (editions.Count != 0)
                {
                    lesson = editions[value];
                    Lesson(lesson, language);
                    Move(language, lesson, editions);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Down(Materia lesson, string language, List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation down \"Home\" view model failed!");

                Move(language, words, false);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Up(Materia lesson, string language, List<Word> words)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation up \"Home\" view model failed!");

                Move(language, words, true);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Move(string language, Materia lesson, List<Materia> books)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation move \"Home\" view model failed!");

                List<Materia> editions = new List<Materia>();
                editions = BookLesson(books);

                List<Word> words = this._grammarService.MountSyntax(language, lesson, editions);
                Oration(words, language);
                string oration = this._grammarService.MountOration(language, words);
                Message(MessageService.Instance, true, language, oration);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        public void Move(string language, List<Word> words, bool reverse)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation move \"Home\" view model failed!");

                List<Word> terms = this._grammarService.MountSyntax(language, words, reverse);
                Oration(terms, language);
                string oration = this._grammarService.MountOration(language, terms);
                Message(MessageService.Instance, true, language, oration);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion
    }
}
