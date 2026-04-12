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

        private string? _error_message;

        public string? error_message
        {
            get => this._error_message;
            set
            {
                this._error_message = value;
            }
        }

        public event EventHandler<string>? OnError;
        #endregion

        #region VARIABLE
        public bool Refresh { get; set; }
        private bool _update_view = true;

        [ObservableProperty]
        public ObservableCollection<Message> recentChat;

        public ICommand BotCommand { get; set; }
        public ICommand SpeakCommand { get; set; }
        public ICommand SwipedCommand { get; set; }
        public IAsyncRelayCommand LoadCommand { get; }

        private List<Materia> _book_english;
        private List<Materia> _book_deutsch;
        private List<Materia> _book_italiano;
        private List<Materia> _book_francais;
        private List<Materia> _book_espanol;

        private Materia _lesson_english;
        private Materia _lesson_deutsch;
        private Materia _lesson_italiano;
        private Materia _lesson_francais;
        private Materia _lesson_espanol;

        private List<Word> _word_english;
        private List<Word> _word_deutsch;
        private List<Word> _word_italiano;
        private List<Word> _word_francais;
        private List<Word> _word_espanol;

        private Language _language_english;
        private Language _language_deutsch;
        private Language _language_italiano;
        private Language _language_francais;
        private Language _language_espanol;

        private HashSet<string> _language_lesson;

        public static ISQLiteService? _sQLiteService;
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

                SQLiteService sQLiteService = new SQLiteService();
                _sQLiteService = sQLiteService;

                this._language_english = SettingService.Instance.English;
                this._language_deutsch = SettingService.Instance.Deutsch;
                this._language_italiano = SettingService.Instance.Italino;
                this._language_francais = SettingService.Instance.Francais;
                this._language_espanol = SettingService.Instance.Espanol;
                this._language_lesson = SettingService.Instance.Lesson;

                this.BotCommand = new AsyncRelayCommand<object>(OnBotCommand);
                this.SpeakCommand = new AsyncRelayCommand<object>(OnSpeakCommand);
                this.SwipedCommand = new AsyncRelayCommand<SwipeDirection>(OnSwipedCommand);
                this.LoadCommand = new AsyncRelayCommand(OnLoadCommand);
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
                        MountPrevious(this._language_english.Lowercase);
                        //MountPrevious(this._language_deutsch.Lowercase);
                        //MountPrevious(this._language_italiano.Lowercase);
                        //MountPrevious(this._language_francais.Lowercase);
                        //MountPrevious(this._language_espanol.Lowercase);
                    }
                    else if (direction == SwipeDirection.Right)
                    {
                        MountNext(this._language_english.Lowercase);
                        //MountNext(this._language_deutsch.Lowercase);
                        //MountNext(this._language_italiano.Lowercase);
                        //MountNext(this._language_francais.Lowercase);
                        //MountNext(this._language_espanol.Lowercase);
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
                if (this._settingService.SpeakEnglish)
                {
                    speak_service.SpeakText(MessageService.Instance.Chats, this._language_english.Uppercase, pitch_speak, volume_speak);
                }
                ;
                if (this._settingService.SpeakDeutsch)
                {
                    speak_service.SpeakText(MessageService.Instance.Chats, this._language_deutsch.Uppercase, pitch_speak, volume_speak);
                }
                ;
                if (this._settingService.SpeakItaliano)
                {
                    speak_service.SpeakText(MessageService.Instance.Chats, this._language_italiano.Uppercase, pitch_speak, volume_speak);
                }
                ;
                if (this._settingService.SpeakFrancais)
                {
                    speak_service.SpeakText(MessageService.Instance.Chats, this._language_francais.Uppercase, pitch_speak, volume_speak);
                }
                ;
                if (this._settingService.SpeakEspanol)
                {
                    speak_service.SpeakText(MessageService.Instance.Chats, this._language_espanol.Uppercase, pitch_speak, volume_speak);
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

        private async Task<bool> DatabaseInit()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation database init \"Home\" view model failed!");

                bool database = await _sQLiteService.ExistAsync();
                bool init = this._settingService.InitDatabase;
                if (!init) database = this._settingService.SQLiteDatabase;
                if (database) this._settingService.SQLiteDatabase = true;
                this._settingService.InitDatabase = false;

                return database;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task OnLoadCommand()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load command \"Home\" view model failed!");

                if (this._update_view) 
                {
                    await Shell.Current.GoToAsync(nameof(ModalView));

                    bool database = await DatabaseInit();
                    MessageService.Instance.Chats = MessageService.Instance.GetChats();
                    await InitAsync(database);

                    this._update_view = false;
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                if (this._update_view) await Shell.Current.GoToAsync("..");
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        private void ClearMessage()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation clear message \"Home\" view model failed!");

                List<Message> memos = new List<Message>();
                RecentChat = new ObservableCollection<Message>(memos);
                MessageService.Instance.Chats = memos;
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

                bool update = false;
                if (query.Count > 0)
                {
                    update = query["refresh"] as string == "True" ? true : false;
                    query.Remove("refresh");
                    this._update_view = update;
                    if (update) ClearMessage();
                }
                else this._update_view = false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion

        #region INITIALIZATION
        private void Init(bool sqlite)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init \"Home\" view model failed!");
                
                Connect(sqlite);
                Grammar();
                MountNext();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task InitAsync(bool sqlite)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init async \"Home\" view model failed!");

                await ConnectAsync(sqlite);
                if (this._language_english != null) await GrammarAsync(this._language_english.Lowercase);
                if (this._language_deutsch != null) await GrammarAsync(this._language_deutsch.Lowercase);
                if (this._language_italiano != null) await GrammarAsync(this._language_italiano.Lowercase);
                if (this._language_francais != null) await GrammarAsync(this._language_francais.Lowercase);
                if (this._language_espanol != null) await GrammarAsync(this._language_espanol.Lowercase);
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
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task ConnectAsync(bool database)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect async \"Home\" view model failed!");

                if (database) await this._grammarService.SQLiteAsync(_sQLiteService);
                else this._grammarService.MongoDB();
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

                this._book_english = this._grammarService.GetLetter(this._language_english.Lowercase).OrderBy(index => index.ordem).ToList();
                this._book_deutsch = this._grammarService.GetLetter(this._language_deutsch.Lowercase).OrderBy(index => index.ordem).ToList();
                this._book_italiano = this._grammarService.GetLetter(this._language_italiano.Lowercase).OrderBy(index => index.ordem).ToList();
                this._book_francais = this._grammarService.GetLetter(this._language_francais.Lowercase).OrderBy(index => index.ordem).ToList();
                this._book_espanol = this._grammarService.GetLetter(this._language_espanol.Lowercase).OrderBy(index => index.ordem).ToList();

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

        private async Task GrammarAsync()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation grammar async \"Home\" view model failed!");

                await this._grammarService.InitAsync();

                this._book_english = await this._grammarService.GetLetterAsync(this._language_english.Lowercase);
                this._book_deutsch = await this._grammarService.GetLetterAsync(this._language_deutsch.Lowercase);
                this._book_italiano = await this._grammarService.GetLetterAsync(this._language_italiano.Lowercase);
                this._book_francais = await this._grammarService.GetLetterAsync(this._language_francais.Lowercase);
                this._book_espanol = await this._grammarService.GetLetterAsync(this._language_espanol.Lowercase);

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

        private async Task GrammarAsync(string? language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation grammar async \"Home\" view model failed!");
                
                if ((this._language_english != null) && (language == this._language_english.Lowercase))
                {
                    this._book_english = await this._grammarService.GetLetterAsync(this._language_english.Lowercase);
                    await this._grammarService.InitAsync(this._language_english.Lowercase);
                    this._word_english = new List<Word>();
                    MountNext(this._language_english.Lowercase);
                }
                if ((this._language_deutsch != null) && (language == this._language_deutsch.Lowercase))
                {
                    this._book_deutsch = await this._grammarService.GetLetterAsync(this._language_deutsch.Lowercase);
                    await this._grammarService.InitAsync(this._language_deutsch.Lowercase);
                    this._word_deutsch = new List<Word>();
                    MountNext(this._language_deutsch.Lowercase);
                }
                if ((this._language_italiano != null) && (language == this._language_italiano.Lowercase))
                {
                    this._book_italiano = await this._grammarService.GetLetterAsync(this._language_italiano.Lowercase);
                    await this._grammarService.InitAsync(this._language_italiano.Lowercase);
                    this._word_italiano = new List<Word>();
                    MountNext(this._language_italiano.Lowercase);
                }
                if ((this._language_francais != null) && (language == this._language_francais.Lowercase))
                {
                    this._book_francais = await this._grammarService.GetLetterAsync(this._language_francais.Lowercase);
                    await this._grammarService.InitAsync(this._language_francais.Lowercase);
                    this._word_francais = new List<Word>();
                    MountNext(this._language_francais.Lowercase);
                }
                if ((this._language_espanol != null) && (language == this._language_espanol.Lowercase))
                {
                    this._book_espanol = await this._grammarService.GetLetterAsync(this._language_espanol.Lowercase);
                    await this._grammarService.InitAsync(this._language_espanol.Lowercase);
                    this._word_espanol = new List<Word>();
                    MountNext(this._language_espanol.Lowercase);
                }
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

                User user_english = message_service.GetUser(this._language_english.Lowercase);
                User user_deutsch = message_service.GetUser(this._language_deutsch.Lowercase);
                User user_italiano = message_service.GetUser(this._language_italiano.Lowercase);
                User user_francais = message_service.GetUser(this._language_francais.Lowercase);
                User user_espanol = message_service.GetUser(this._language_espanol.Lowercase);
                List<Message> messages = message_service.Chats;
                List<Message> memos = new List<Message>();
                message_service.Chats.ForEach(index => memos.Add(index));
                foreach (Message item in messages)
                {
                    if (((item.Sender == user_english) && (language == this._language_english.Lowercase))
                        || ((item.Sender == user_deutsch) && (language == this._language_deutsch.Lowercase))
                        || ((item.Sender == user_italiano) && (language == this._language_italiano.Lowercase))
                        || ((item.Sender == user_francais) && (language == this._language_francais.Lowercase))
                        || ((item.Sender == user_espanol) && (language == this._language_espanol.Lowercase)))
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

                if (language == this._language_english.Lowercase) this._lesson_english = materia;
                if (language == this._language_deutsch.Lowercase) this._lesson_deutsch = materia;
                if (language == this._language_italiano.Lowercase) this._lesson_italiano = materia;
                if (language == this._language_francais.Lowercase) this._lesson_francais = materia;
                if (language == this._language_espanol.Lowercase) this._lesson_espanol = materia;
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

                if (language == this._language_english.Lowercase) this._word_english = oration;
                if (language == this._language_deutsch.Lowercase) this._word_deutsch = oration;
                if (language == this._language_italiano.Lowercase) this._word_italiano = oration;
                if (language == this._language_francais.Lowercase) this._word_francais = oration;
                if (language == this._language_espanol.Lowercase) this._word_espanol = oration;
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

                Next(this._book_english, this._lesson_english, this._language_english.Lowercase);
                Next(this._book_deutsch, this._lesson_deutsch, this._language_deutsch.Lowercase);
                Next(this._book_italiano, this._lesson_italiano, this._language_italiano.Lowercase);
                Next(this._book_francais, this._lesson_francais, this._language_francais.Lowercase);
                Next(this._book_espanol, this._lesson_espanol, this._language_espanol.Lowercase);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void MountNext(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount next \"Home\" view model failed!");

                if ((this._language_english != null) && (this._language_english.Lowercase == language)) 
                    Next(this._book_english, this._lesson_english, this._language_english.Lowercase);
                if ((this._language_deutsch != null) && (this._language_deutsch.Lowercase == language))
                    Next(this._book_deutsch, this._lesson_deutsch, this._language_deutsch.Lowercase);
                if ((this._language_italiano != null) && (this._language_italiano.Lowercase == language))
                    Next(this._book_italiano, this._lesson_italiano, this._language_italiano.Lowercase);
                if ((this._language_francais != null) && (this._language_francais.Lowercase == language))
                    Next(this._book_francais, this._lesson_francais, this._language_francais.Lowercase);
                if ((this._language_espanol != null) && (this._language_espanol.Lowercase == language))
                    Next(this._book_espanol, this._lesson_espanol, this._language_espanol.Lowercase);
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

                Previous(this._book_english, this._lesson_english, this._language_english.Lowercase);
                Previous(this._book_deutsch, this._lesson_deutsch, this._language_deutsch.Lowercase);
                Previous(this._book_italiano, this._lesson_italiano, this._language_italiano.Lowercase);
                Previous(this._book_francais, this._lesson_francais, this._language_francais.Lowercase);
                Previous(this._book_espanol, this._lesson_espanol, this._language_espanol.Lowercase);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void MountPrevious(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount previous \"Home\" view model failed!");

                if ((this._language_english != null) && (this._language_english.Lowercase == language))
                    Previous(this._book_english, this._lesson_english, this._language_english.Lowercase);
                if ((this._language_deutsch != null) && (this._language_deutsch.Lowercase == language))
                    Previous(this._book_deutsch, this._lesson_deutsch, this._language_deutsch.Lowercase);
                if ((this._language_italiano != null) && (this._language_italiano.Lowercase == language))
                    Previous(this._book_italiano, this._lesson_italiano, this._language_italiano.Lowercase);
                if ((this._language_francais != null) && (this._language_francais.Lowercase == language))
                    Previous(this._book_francais, this._lesson_francais, this._language_francais.Lowercase);
                if ((this._language_espanol != null) && (this._language_espanol.Lowercase == language))
                    Previous(this._book_espanol, this._lesson_espanol, this._language_espanol.Lowercase);
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

                if (this._settingService.PauseEnglish) Down(this._lesson_english, this._language_english.Lowercase, this._word_english);
                if (this._settingService.PauseDeutsch) Down(this._lesson_deutsch, this._language_deutsch.Lowercase, this._word_deutsch);
                if (this._settingService.PauseItaliano) Down(this._lesson_italiano, this._language_italiano.Lowercase, this._word_italiano);
                if (this._settingService.PauseFrancais) Down(this._lesson_francais, this._language_francais.Lowercase, this._word_francais);
                if (this._settingService.PauseEspanol) Down(this._lesson_espanol, this._language_espanol.Lowercase, this._word_espanol);
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

                if (this._settingService.PauseEnglish) Up(this._lesson_english, this._language_english.Lowercase, this._word_english);
                if (this._settingService.PauseDeutsch) Up(this._lesson_deutsch, this._language_deutsch.Lowercase, this._word_deutsch);
                if (this._settingService.PauseItaliano) Up(this._lesson_italiano, this._language_italiano.Lowercase, this._word_italiano);
                if (this._settingService.PauseFrancais) Up(this._lesson_francais, this._language_francais.Lowercase, this._word_francais);
                if (this._settingService.PauseEspanol) Up(this._lesson_espanol, this._language_espanol.Lowercase, this._word_espanol);
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

                HashSet<string> lecture = new HashSet<string>(this._language_lesson);
                List<Materia> lessons = new List<Materia>();
                lessons = books.OrderBy(index => index.ordem).ToList();

                List<Materia> edition = new List<Materia>();
                lessons.ForEach(index =>
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

                List<Word> words = this._grammarService.Syntax(language, lesson, editions);
                Oration(words, language);
                string oration = this._grammarService.Oration(words);
                Message(MessageService.Instance, true, language, oration);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void Move(string language, List<Word> words, bool reverse)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation move \"Home\" view model failed!");

                List<Word> terms = this._grammarService.Syntax(language, words, reverse);
                Oration(terms, language);
                string oration = this._grammarService.Oration(terms);
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
