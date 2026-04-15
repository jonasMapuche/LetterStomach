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
        public ICommand SwipedSpeakCommand { get; set; }
        public ICommand SwipedMoveCommand { get; set; }

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

        private string _image_speak_on;
        private string _image_speak_off;
        private string _image_move_on;
        private string _image_move_off;

        private ISQLiteService _sQLiteService;
        private IGrammarService _grammarService;
        private ITextToSpeakService _textToSpeakService;
        private IMongoDBService _mongoDBService;
        private SettingService _settingService;
        #endregion

        #region CONSTRUCTOR
        public HomeViewModel(SQLiteService sQLiteService, MongoDBService mongoDBService)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation constructor \"Home\" view model failed!");
                else this.error_message = string.Empty;

                this.BotCommand = new AsyncRelayCommand<object>(OnBotCommand);
                this.SpeakCommand = new AsyncRelayCommand<object>(OnSpeakCommand);
                this.SwipedCommand = new AsyncRelayCommand<SwipeDirection>(OnSwipedCommand);
                this.SwipedSpeakCommand = new AsyncRelayCommand<object>(OnSwipedSpeakCommand);
                this.SwipedMoveCommand = new AsyncRelayCommand<object>(OnSwipedMoveCommand);
                this.LoadCommand = new AsyncRelayCommand(OnLoadCommand);

                IGrammarService grammarService = new GrammarService();
                this._grammarService = grammarService;
                ITextToSpeakService textToSpeakService = new TextToSpeakService();
                this._textToSpeakService = textToSpeakService;

                this._settingService = SettingService.Instance;

                this._sQLiteService = sQLiteService;
                this._mongoDBService = mongoDBService;

                this._language_english = this._settingService.English;
                this._language_deutsch = this._settingService.Deutsch;
                this._language_italiano = this._settingService.Italino;
                this._language_francais = this._settingService.Francais;
                this._language_espanol = this._settingService.Espanol;
                this._language_lesson = this._settingService.Lesson;

                this._image_speak_on = MessageService.Instance.image_speak_on;
                this._image_speak_off = MessageService.Instance.image_speak_off;
                this._image_move_on = MessageService.Instance.image_move_on;
                this._image_move_off = MessageService.Instance.image_move_off;
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
                        MountPrevious(this._language_deutsch.Lowercase);
                        MountPrevious(this._language_italiano.Lowercase);
                        MountPrevious(this._language_francais.Lowercase);
                        MountPrevious(this._language_espanol.Lowercase);
                    }
                    else if (direction == SwipeDirection.Right)
                    {
                        MountNext(this._language_english.Lowercase);
                        MountNext(this._language_deutsch.Lowercase);
                        MountNext(this._language_italiano.Lowercase);
                        MountNext(this._language_francais.Lowercase);
                        MountNext(this._language_espanol.Lowercase);
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

                float pitch_speak = this._settingService.PitchFloat;
                float volume_speak = this._settingService.VolumeFloat;

                if (this._settingService.SpeakEnglish)
                {
                    await this._textToSpeakService.SpeakText(MessageService.Instance.Chats, this._language_english.Uppercase, pitch_speak, volume_speak);
                }
                if (this._settingService.SpeakDeutsch)
                {
                    await this._textToSpeakService.SpeakText(MessageService.Instance.Chats, this._language_deutsch.Uppercase, pitch_speak, volume_speak);
                }
                if (this._settingService.SpeakItaliano)
                {
                    await this._textToSpeakService.SpeakText(MessageService.Instance.Chats, this._language_italiano.Uppercase, pitch_speak, volume_speak);
                }
                if (this._settingService.SpeakFrancais)
                {
                    await this._textToSpeakService.SpeakText(MessageService.Instance.Chats, this._language_francais.Uppercase, pitch_speak, volume_speak);
                }
                if (this._settingService.SpeakEspanol)
                {
                    await this._textToSpeakService.SpeakText(MessageService.Instance.Chats, this._language_espanol.Uppercase, pitch_speak, volume_speak);
                }
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

                bool database = await this._sQLiteService.ExistAsync();
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

        private void ChangeSpeak(string text, bool status)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation change speak \"Home Item Template\" view failed!!");

                if (text == this._language_english.Uppercase) this._settingService.SpeakEnglish = status;
                if (text == this._language_deutsch.Uppercase) this._settingService.SpeakDeutsch = status;
                if (text == this._language_italiano.Uppercase) this._settingService.SpeakItaliano = status;
                if (text == this._language_francais.Uppercase) this._settingService.SpeakFrancais = status;
                if (text == this._language_espanol.Uppercase) this._settingService.SpeakEspanol = status;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task OnSwipedSpeakCommand(object? arg)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation swiped speak command \"Home\" view model failed!");

                Message message = new Message();
                message = arg as Message;
                List<Message> messages = new List<Message>();
                List<Message> locutions = new List<Message>();
                messages = MessageService.Instance.Chats;
                int value = messages.IndexOf(message);
                int order = 0;
                foreach (Message item in messages)
                {
                    Message locution = new Message();
                    if (order == value)
                    {
                        locution.Text = item.Text;
                        User user = new User();
                        user = item.Sender;
                        locution.Sender = user;
                        locution.Move = item.Move;
                        if (item.Speak == this._image_speak_on)
                        {
                            locution.Speak = this._image_speak_off;
                            ChangeSpeak(item.Sender.Name, false);
                        }
                        else
                        {
                            locution.Speak = this._image_speak_on;
                            ChangeSpeak(item.Sender.Name, true);
                        }
                    }
                    else locution = item;
                    locutions.Add(locution);
                    order++;
                }
                RecentChat.Clear();
                RecentChat = new ObservableCollection<Message>(locutions);
                MessageService.Instance.Chats = locutions;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void ChangePause(string text, bool status)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation change pause \"Home Item Template\" view failed!!");

                if (text == this._language_english.Uppercase) this._settingService.PauseEnglish = status;
                if (text == this._language_deutsch.Uppercase) this._settingService.PauseDeutsch = status;
                if (text == this._language_italiano.Uppercase) this._settingService.PauseItaliano = status;
                if (text == this._language_francais.Uppercase) this._settingService.PauseFrancais = status;
                if (text == this._language_espanol.Uppercase) this._settingService.PauseEspanol = status;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task OnSwipedMoveCommand(object? parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation swiped move command \"Home\" view model failed!");

                Message? message = new Message();
                message = parameter as Message;
                List<Message> messages = new List<Message>();
                List<Message> locutions = new List<Message>();
                messages = MessageService.Instance.Chats;
                int value = messages.IndexOf(message);
                int order = 0;
                foreach (Message item in messages)
                {
                    Message locution = new Message();
                    if (order == value)
                    {
                        locution.Text = item.Text;
                        User user = new User();
                        user = item.Sender;
                        locution.Sender = user;
                        locution.Speak = item.Speak;
                        if (item.Move == this._image_move_on)
                        {
                            locution.Move = this._image_move_off;
                            ChangePause(item.Sender.Name, false);
                        }
                        else
                        {
                            locution.Move = this._image_move_on;
                            ChangePause(item.Sender.Name, true);
                        }
                    }
                    else locution = item;
                    locutions.Add(locution);
                    order++;
                }
                RecentChat.Clear();
                RecentChat = new ObservableCollection<Message>(locutions);
                MessageService.Instance.Chats = locutions;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> ChangePause(string text)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation change pause \"Home Item Template\" view failed!!");

                string pause = string.Empty;
                if (text == this._language_english.Uppercase)
                {
                    if (this._settingService.PauseEnglish)
                        pause = this._image_move_on;
                    else pause = this._image_move_off;
                }
                if (text == this._language_deutsch.Uppercase)
                {
                    if (this._settingService.PauseDeutsch)
                        pause = this._image_move_on;
                    else pause = this._image_move_off;
                }
                if (text == this._language_italiano.Uppercase)
                {
                    if (this._settingService.PauseItaliano)
                        pause = this._image_move_on;
                    else pause = this._image_move_off;
                }
                if (text == this._language_francais.Uppercase)
                {
                    if (this._settingService.PauseFrancais)
                        pause = this._image_move_on;
                    else pause = this._image_move_off;
                }
                if (text == this._language_espanol.Uppercase)
                {
                    if (this._settingService.PauseEspanol)
                        pause = this._image_move_on;
                    else pause = this._image_move_off;
                }
                return pause;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> ChangeSpeak(string text)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation change speak \"Home Item Template\" view failed!!");

                string speak = string.Empty;
                if (text == this._language_english.Uppercase)
                {
                    if (this._settingService.SpeakEnglish)
                        speak = this._image_speak_on;
                    else speak = this._image_speak_off;
                }
                if (text == this._language_deutsch.Uppercase)
                {
                    if (this._settingService.SpeakDeutsch)
                        speak = this._image_speak_on;
                    else speak = this._image_speak_off;
                }
                if (text == this._language_italiano.Uppercase)
                {
                    if (this._settingService.SpeakItaliano)
                        speak = this._image_speak_on;
                    else speak = this._image_speak_off;
                }
                if (text == this._language_francais.Uppercase)
                {
                    if (this._settingService.SpeakFrancais)
                        speak = this._image_speak_on;
                    else speak = this._image_speak_off;
                }
                if (text == this._language_espanol.Uppercase)
                {
                    if (this._settingService.SpeakEspanol)
                        speak = this._image_speak_on;
                    else speak = this._image_speak_off;
                }
                return speak;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task OnInitChat()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init chat \"Home\" view model failed!");

                ClearMessage();
                List<Message> memos = MessageService.Instance.GetChatsClear();
                List<Message> messages = new List<Message>();
                foreach (Message memo in memos)
                {
                    Message message = new Message();
                    message.Sender = memo.Sender;
                    message.Text = memo.Text;
                    message.Move = await ChangePause(memo.Sender.Name);
                    message.Speak = await ChangeSpeak(memo.Sender.Name);
                    messages.Add(message);
                }
                MessageService.Instance.Chats = messages;

                RecentChat.Clear();
                RecentChat = new ObservableCollection<Message>(messages);
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
                    try
                    {
                        bool database = await DatabaseInit();
                        await OnInitChat();
                        await OnInitAsync(database);
                        this._update_view = false;
                    }
                    finally
                    {
                        await Shell.Current.GoToAsync("..");
                    }
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
                
                OnConnect(sqlite);
                Grammar();
                MountNext();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task OnInitAsync(bool sqlite)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init async \"Home\" view model failed!");

                await OnConnectAsync(sqlite);
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

        private void OnConnect(bool database)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect \"Home\" view model failed!");

                if (database) this._grammarService.SQLite(this._sQLiteService);
                else this._grammarService.MongoDB(this._mongoDBService);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task OnConnectAsync(bool database)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect async \"Home\" view model failed!");

                if (database) await this._grammarService.SQLiteAsync(this._sQLiteService);
                else this._grammarService.MongoDB(this._mongoDBService);
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
