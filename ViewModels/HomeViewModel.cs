using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.ViewModels.Interfaces;
using LetterStomach.Views;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace LetterStomach.ViewModels
{
    [QueryProperty(nameof(Refresh), "refresh")]
    public partial class HomeViewModel : ObservableObject, IQueryAttributable
    {
        #region ERROR
        private bool _error_test = false;
        private string _error_message;

        public string error_message
        {
            get => _error_message;
            set
            {
                _error_message = value;
            }
        }

        public event EventHandler<string> OnError;

        private async void OnErrorDown(object sender, string error_message)
        {
            this.error_message = error_message;
            OnError?.Invoke(this, this.error_message);
        }
        #endregion

        #region VARIABLE
        public bool Refresh { get; set; }

        [ObservableProperty]
        public ObservableCollection<Message> recentChat;

        public ICommand BotCommand { get; set; }
        public ICommand SpeakCommand { get; set; }
        public ICommand SwipedCommand { get; set; }

        private readonly SettingService _singleton;

        private IGrammarViewModel _grammarViewModel;

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
        #endregion

        #region CONTRUTOR
        public HomeViewModel(SettingService singleton)
        {
            try
            {
                this.BotCommand = new AsyncRelayCommand<object>(OnBotCommand);
                this.SpeakCommand = new AsyncRelayCommand<object>(OnSpeakCommand);
                this.SwipedCommand = new AsyncRelayCommand<SwipeDirection>(OnSwipedCommand);

                this._grammarViewModel = new GrammarViewModel();
                this._grammarViewModel.OnError += OnErrorDown;
                _singleton = singleton;
                bool sqlite_database = this._singleton.SQLiteDatabase;
                Connect(sqlite_database);
                Init();
                MountNext();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion

        #region COMMAND
        private async Task OnSwipedCommand(SwipeDirection direction)
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
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
                OnError?.Invoke(this, error_message);
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
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            try
            {
                bool sqlite_database = false;
                if (query.Count > 0) sqlite_database = query["refresh"] as string == "True" ? true : false;
                if (sqlite_database) Update(sqlite_database);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion

        #region MESSAGE
        private void Connect(bool sqlite_database)
        {
            try
            {
                if (sqlite_database)
                {
                    this._grammarViewModel.SQLite();
                    MessageService.Instance.Chats = MessageService.Instance.GetChatsSQLite();
                }
                else
                {
                    this._grammarViewModel.MongoDB();
                    MessageService.Instance.Chats = MessageService.Instance.GetChats();
                };
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void Init()
        {
            try
            {
                this._grammarViewModel.SetGrammar();
                this._lesson_english = this._grammarViewModel.GetLetter(ENGLISH.Lowercase).OrderBy(index => index.ordem).ToList();
                this._lesson_deutsch = this._grammarViewModel.GetLetter(DEUTSCH.Lowercase).OrderBy(index => index.ordem).ToList();
                this._lesson_italiano = this._grammarViewModel.GetLetter(ITALIANO.Lowercase).OrderBy(index => index.ordem).ToList();
                this._lesson_francais = this._grammarViewModel.GetLetter(FRANCAIS.Lowercase).OrderBy(index => index.ordem).ToList();
                this._lesson_espanol = this._grammarViewModel.GetLetter(ESPANOL.Lowercase).OrderBy(index => index.ordem).ToList();

                this._word_english = new List<Word>();
                this._word_deutsch = new List<Word>();
                this._word_italiano = new List<Word>();
                this._word_francais = new List<Word>();
                this._word_espanol = new List<Word>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void Update(bool sqlite_database)
        {
            try
            {
                Connect(sqlite_database);
                Init();
                Load(MessageService.Instance);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void Load(MessageService message_service, bool update, string language, string oration)
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
            }
        }

        private void Load(MessageService message_service)
        {
            try
            {
                User user_english = message_service.GetUser(ENGLISH.Lowercase);
                User user_deutsch = message_service.GetUser(DEUTSCH.Lowercase);
                User user_italiano = message_service.GetUser(ITALIANO.Lowercase);
                User user_francais = message_service.GetUser(FRANCAIS.Lowercase);
                User user_espanol = message_service.GetUser(ESPANOL.Lowercase);
                List<Message> messages = new List<Message>();
                messages = message_service.Chats;
                List<Message> memos = new List<Message>();
                foreach (Message item in messages)
                {
                    if (item.Sender == user_english)
                    {
                        memos.Add(item);
                        continue;
                    }
                    if (item.Sender == user_deutsch)
                    {
                        memos.Add(item);
                        continue;
                    }
                    if (item.Sender == user_italiano)
                    {
                        memos.Add(item);
                        continue;
                    }
                    if (item.Sender == user_francais)
                    {
                        memos.Add(item);
                        continue;
                    }
                    if (item.Sender == user_espanol)
                    {
                        memos.Add(item);
                        continue;
                    }
                };
                RecentChat = new ObservableCollection<Message>(memos);
                MessageService.Instance.Chats = memos;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion

        #region SET
        private void SetLesson(Materia materia, string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) this._english = materia;
                if (language == DEUTSCH.Lowercase) this._deutsch = materia;
                if (language == ITALIANO.Lowercase) this._italiano = materia;
                if (language == FRANCAIS.Lowercase) this._francais = materia;
                if (language == ESPANOL.Lowercase) this._espanol = materia;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void SetOration(List<Word> oration, string language)
        {
            try
            {
                if (language == ENGLISH.Lowercase) _word_english = oration;
                if (language == DEUTSCH.Lowercase) _word_deutsch = oration;
                if (language == ITALIANO.Lowercase) _word_italiano = oration;
                if (language == FRANCAIS.Lowercase) _word_francais = oration;
                if (language == ESPANOL.Lowercase) _word_espanol = oration;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region MOVE
        private void MountNext()
        {
            try
            {
                if (_error_test) throw new InvalidOperationException("Operation failed!");
                Next(_lesson_english, _english, ENGLISH.Lowercase);
                Next(_lesson_deutsch, _deutsch, DEUTSCH.Lowercase);
                Next(_lesson_italiano, _italiano, ITALIANO.Lowercase);
                Next(_lesson_francais, _francais, FRANCAIS.Lowercase);
                Next(_lesson_espanol, _espanol, ESPANOL.Lowercase);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void MountPrevious()
        {
            try
            {
                Previous(_lesson_english, _english, ENGLISH.Lowercase);
                Previous(_lesson_deutsch, _deutsch, DEUTSCH.Lowercase);
                Previous(_lesson_italiano, _italiano, ITALIANO.Lowercase);
                Previous(_lesson_francais, _francais, FRANCAIS.Lowercase);
                Previous(_lesson_espanol, _espanol, ESPANOL.Lowercase);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void MountDown()
        {
            try
            {
                if (SingletonService.Instance.PauseEnglish) Down(_english, ENGLISH.Lowercase, _word_english);
                if (SingletonService.Instance.PauseDeutsch) Down(_deutsch, DEUTSCH.Lowercase, _word_deutsch);
                if (SingletonService.Instance.PauseItaliano) Down(_italiano, ITALIANO.Lowercase, _word_italiano);
                if (SingletonService.Instance.PauseFrancais) Down(_francais, FRANCAIS.Lowercase, _word_francais);
                if (SingletonService.Instance.PauseEspanol) Down(_espanol, ESPANOL.Lowercase, _word_espanol);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void MountUp()
        {
            try
            {
                if (SingletonService.Instance.PauseEnglish) Up(_english, ENGLISH.Lowercase, _word_english);
                if (SingletonService.Instance.PauseDeutsch) Up(_deutsch, DEUTSCH.Lowercase, _word_deutsch);
                if (SingletonService.Instance.PauseItaliano) Up(_italiano, ITALIANO.Lowercase, _word_italiano);
                if (SingletonService.Instance.PauseFrancais) Up(_francais, FRANCAIS.Lowercase, _word_francais);
                if (SingletonService.Instance.PauseEspanol) Up(_espanol, ESPANOL.Lowercase, _word_espanol);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void Next(List<Materia> books, Materia lesson, string language)
        {
            try
            {
                int value = books.IndexOf(lesson) + 1;
                if (value == books.Count) value = books.IndexOf(lesson);
                if (books.Count != 0)
                {
                    lesson = books[value];
                    SetLesson(lesson, language);
                    Move(language, lesson, books);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void Previous(List<Materia> books, Materia lesson, string language)
        {
            try
            {
                int value = books.IndexOf(lesson) - 1;
                if (value == -1) value = 0;
                if (books.Count != 0)
                {
                    lesson = books[value];
                    SetLesson(lesson, language);
                    Move(language, lesson, books);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void Move(string language, Materia lesson, List<Materia> books)
        {
            try
            {
                List<Word> words = this._grammarViewModel.MountSyntax(language, lesson, books);
                SetOration(words, language);
                string oration = this._grammarViewModel.MountOration(language, words);
                Load(MessageService.Instance, true, language, oration);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void Move(string language, List<Word> words, bool reverse)
        {
            try
            {
                List<Word> terms = this._grammarViewModel.MountSyntax(language, words, reverse);
                SetOration(terms, language);
                string oration = this._grammarViewModel.MountOration(language, terms);
                Load(MessageService.Instance, true, language, oration);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void Down(Materia lesson, string language, List<Word> words)
        {
            try
            {
                Move(language, words, false);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private void Up(Materia lesson, string language, List<Word> words)
        {
            try
            {
                Move(language, words, true);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion
    }
}
