using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LetterStomach.Interfaces;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using LetterStomach.Views;
using System.Windows.Input;

namespace LetterStomach.ViewModels
{
    [QueryProperty(nameof(username), "Username")]
    public partial class BotViewModel : ObservableObject, IQueryAttributable
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

        #region CONSTANTS
        private string VAR_DECLARATIVE = SettingService.Instance.Declarative;
        private string VAR_VERB = SettingService.Instance.Verb;
        private string VAR_NOUN = SettingService.Instance.Noun;
        private string VAR_ADJECTIVE = SettingService.Instance.Adjective;
        private string VAR_PREDICATE = SettingService.Instance.Predicate;
        private string VAR_NUMERAL = SettingService.Instance.Numeral;
        private string VAR_ESPECIAL = SettingService.Instance.Especial;
        private string VAR_PREPOSITION = SettingService.Instance.Preposition;

        private Language PORTUGUES = SettingService.Instance.Portugues;

        private Dictionary<string, string> VAR_LOAD = SettingService.Instance.Load;
        private Dictionary<string, string> VAR_EXECUTE = SettingService.Instance.Execute;
        private Dictionary<string, string> VAR_VIEW = SettingService.Instance.View;
        private Dictionary<string, string> VAR_PLAY = SettingService.Instance.Play;
        private Dictionary<string, string> VAR_ACTIVITY = SettingService.Instance.Activity;
        private Dictionary<string, string> VAR_RECORD = SettingService.Instance.Record;
        private Dictionary<string, string> VAR_STOP = SettingService.Instance.Stop;
        private Dictionary<string, string> VAR_ROTATE = SettingService.Instance.Rotate;
        private Dictionary<string, string> VAR_SPEAK = SettingService.Instance.Speak;
        private Dictionary<string, string> VAR_DOWNLOAD = SettingService.Instance.Download;
        private Dictionary<string, string> VAR_UPLOAD = SettingService.Instance.Upload;
        private Dictionary<string, string> VAR_CAPTURE = SettingService.Instance.Capture;
        private Dictionary<string, string> VAR_SAVE = SettingService.Instance.Save;
        private Dictionary<string, string> VAR_TURN = SettingService.Instance.Turn;
        private Dictionary<string, string> VAR_SHARE = SettingService.Instance.Share;
        private Dictionary<string, string> VAR_FEATURE = SettingService.Instance.Feature;
        private Dictionary<string, string> VAR_TERMINATE = SettingService.Instance.Terminate;
        private Dictionary<string, string> VAR_TURN_ON = SettingService.Instance.Turn_On;
        private Dictionary<string, string> VAR_START = SettingService.Instance.Start;
        private Dictionary<string, string> VAR_SCAN = SettingService.Instance.Scan;
        private Dictionary<string, string> VAR_WORK = SettingService.Instance.Work;
        private Dictionary<string, string> VAR_DONT_WORK = SettingService.Instance.Dont_Work;
        private Dictionary<string, string> VAR_SEND = SettingService.Instance.Send;
        private Dictionary<string, string> VAR_CONNECT = SettingService.Instance.Connect;
        private Dictionary<string, string> VAR_DISCONNECT = SettingService.Instance.Disconnect;
        private Dictionary<string, string> VAR_CONNECTED = SettingService.Instance.Connected;
        private Dictionary<string, string> VAR_JUNCAO = SettingService.Instance.Juncao;

        private Dictionary<string, string> VAR_GPS = SettingService.Instance.GPS;
        private Dictionary<string, string> VAR_BLUETOOTH = SettingService.Instance.Bluetooth;
        private Dictionary<string, string> VAR_BLUETOOTH3 = SettingService.Instance.Bluetooth3;
        private Dictionary<string, string> VAR_BLUETOOTH4 = SettingService.Instance.Bluetooth4;
        private Dictionary<string, string> VAR_CAMERA = SettingService.Instance.Camera;
        private Dictionary<string, string> VAR_WAV = SettingService.Instance.WAV;
        private Dictionary<string, string> VAR_MP3 = SettingService.Instance.MP3;
        private Dictionary<string, string> VAR_BATTERY = SettingService.Instance.Battery;
        private Dictionary<string, string> VAR_FILE = SettingService.Instance.File;
        private Dictionary<string, string> VAR_VIBRATION = SettingService.Instance.Vibration;
        private Dictionary<string, string> VAR_TEXT = SettingService.Instance.Text;
        private Dictionary<string, string> VAR_PHONE = SettingService.Instance.Phone;
        private Dictionary<string, string> VAR_FLASH = SettingService.Instance.Flash;
        private Dictionary<string, string> VAR_AUDIO = SettingService.Instance.Audio;
        private Dictionary<string, string> VAR_BOT = SettingService.Instance.Bot;
        private Dictionary<string, string> VAR_LONGITUDE = SettingService.Instance.Longitude;
        private Dictionary<string, string> VAR_LATITUDE = SettingService.Instance.Latitude;
        private Dictionary<string, string> VAR_LEVEL = SettingService.Instance.Level;

        private Dictionary<string, string> VAR_WITH = SettingService.Instance.With;
        private Dictionary<string, string> VAR_IN = SettingService.Instance.In;
        private Dictionary<string, string> VAR_OFF = SettingService.Instance.Off;
        private Dictionary<string, string> VAR_AUTO = SettingService.Instance.Auto;
        private Dictionary<string, string> VAR_ON = SettingService.Instance.On;
        private Dictionary<string, string> VAR_TO = SettingService.Instance.To;

        private Dictionary<string, string> VAR_AND = SettingService.Instance.And;

        private Dictionary<string, string> VAR_FRONT = SettingService.Instance.Front;
        private Dictionary<string, string> VAR_REAR = SettingService.Instance.Rear;

        private Dictionary<string, string> VAR_THROUGH = SettingService.Instance.Through;

        private Dictionary<string, string> VAR_CATCH = SettingService.Instance.Catch;
        private Dictionary<string, string> VAR_CATCH_CAMERA = SettingService.Instance.Catch_Camera;
        private Dictionary<string, string> VAR_CATCH_RECORD = SettingService.Instance.Catch_Record;
        private Dictionary<string, string> VAR_CATCH_SHARE = SettingService.Instance.Catch_Share;

        private HashSet<int> VAR_ALGARISMO = SettingService.Instance.Algarismo;

        private HashSet<int> VAR_THREE = SettingService.Instance.Three;
        private HashSet<int> VAR_FOUR = SettingService.Instance.Four;

        private string UNKNOW = "unknown";
        #endregion

        #region VARIABLE
        public ICommand BackCommand { get; set; }

        public ICommand SendCommand { get; set; }

        [ObservableProperty]
        User username;

        [ObservableProperty]
        List<Message> messages;

        [ObservableProperty]
        private byte[]? bytes;

        [ObservableProperty]
        private CameraInfo selectedCamera;

        [ObservableProperty]
        private CameraFlashMode flashMode;

        [ObservableProperty]
        public bool showPhoto;

        [ObservableProperty]
        public bool showCamera;

        private ICameraProvider _cameraProvider;

        public CameraView _cameraView;

        public ICameraProvider MediaCamera {  get => _cameraProvider; set => _cameraProvider = value; }

        private IHttpService _httpService;

        private IPerceptionService _perceptionService;

        private IBotService _botService;

        public CancellationToken Token => CancellationToken.None;
        #endregion

        #region CONTRUCTOR
        public BotViewModel(IRecordService recordService, IAudioService audioService, ITextSpeakService textSpeakService) 
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation contructor \"Bot\" view model failed!");
                else this.error_message = string.Empty;

                this.BackCommand = new AsyncRelayCommand(OnBackCommand);
                this.SendCommand = new AsyncRelayCommand<string>(OnSendCommand);

                this.ShowCamera = false;
                this.ShowPhoto = false;

                this._perceptionService = new PerceptionService(recordService, audioService, textSpeakService);
                this._perceptionService.OnError += OnError;

                this._botService = new BotService();
                this._botService.OnError += OnError;

                OnInitMessage();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
            }
        }

        private void OnInitMessage()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init message \"Bot\" view model failed!");

                Username = MessageService.Instance.GetUser(PORTUGUES.Lowercase);

                List<Message> message_language = MessageService.Instance.Messages(Username.Name);
                if (message_language.Count > 0)
                {
                    Messages = message_language;
                }
                else
                {
                    Messages = MessageService.Instance.Messages(Username, "What can I do for you?", Username.Name);
                }

            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
            }
        }
        #endregion

        #region COMMAND
        private async Task OnSendCommand(string parameter)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation send command \"Bot\" view model failed!");

                string language = Username.Name.ToLower();
                User user = Username;
                Messages = MessageService.Instance.Messages(null, parameter, language);

                if (SettingService.Instance.ModeBot)
                {
                    MessageService.Instance.Bots(null, parameter, language);
                    string response = string.Empty;
                    response = await OnSendBot(parameter, user, language);
                }
                else
                {
                    string declarative = string.Empty;
                    declarative = await OnSendButton(parameter, user, language);
                    if (declarative != string.Empty)
                        Messages = MessageService.Instance.Messages(Username, declarative, Username.Name);
                    if (SettingService.Instance.ModeBot)
                        MessageService.Instance.Bots(Username, declarative, Username.Name);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        private async Task<List<Locution>> GoMessage(string parameter, User user, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation go message \"Bot\" view model failed!");

                GoMessage goMessage = new GoMessage();
                User bot = new User();
                bot = new User
                {
                    Name = user.Name,
                    Image = user.Image
                };
                goMessage.sender = bot;
                goMessage.language = language;
                goMessage.message = parameter;

                List<Locution> locutions = new List<Locution>();
                this._httpService = new HttpService();
                this._httpService.OnError += OnError;
                locutions = await this._httpService.HttpGo(goMessage);

                return locutions;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        private async Task<string> LocutionText(Locution locution)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation locution text \"Bot\" view model failed!");

                string result = string.Empty;
                List<Vocable> vocables = new List<Vocable>();
                vocables = locution.Word;
                vocables.ForEach(index =>
                {
                    if (index.Class != VAR_ESPECIAL)
                    {
                        if (result == string.Empty) result = index.Term;
                        else
                            result += " " + index.Term;
                    }
                });                             
                return result;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> DecisionMessage(string language, List<Locution> locutions)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation decision message \"Bot\" view model failed!");

                HashSet<string> verbs = VAR_EXECUTE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns = VAR_ACTIVITY
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> adjectives = VAR_FEATURE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> prepositions = VAR_JUNCAO
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<int> numerals = VAR_ALGARISMO;

                Locution period1 = new Locution();
                Locution period2 = new Locution();
                int quantity = 0;
                locutions.ForEach(index =>
                {
                    if (quantity == 0) period1 = index;
                    if (quantity == 1) period2 = index;
                    quantity++;
                });

                string phrase = string.Empty;
                if (locutions.Count > 1) phrase = await LocutionText(period2);

                string result = string.Empty;
                if ((period1 != null) && (period1.Kind == VAR_DECLARATIVE))
                {
                    string verb = string.Empty;
                    foreach (Vocable item in period1.Word)
                    {
                        if (item.Class == VAR_VERB)
                        {
                            if (Array.IndexOf(verbs.ToArray(), item.Term) != -1)
                                verb = item.Term;
                        }
                    }
                    string noun = string.Empty;
                    foreach (Vocable item in period1.Word)
                    {
                        if ((item.Sentence == VAR_PREDICATE) && (item.Class == VAR_NOUN))
                        {
                            if (Array.IndexOf(nouns.ToArray(), item.Term) != -1)
                                noun = item.Term;
                        }
                    }
                    string adjective = string.Empty;
                    foreach (Vocable item in period1.Word)
                    {
                        if ((item.Sentence == VAR_PREDICATE) && (item.Class == VAR_ADJECTIVE))
                        {
                            if (Array.IndexOf(adjectives.ToArray(), item.Term) != -1)
                                adjective = item.Term;
                        }
                    }
                    string preposition = string.Empty;
                    foreach (Vocable item in period1.Word)
                    {
                        if ((item.Sentence == VAR_PREDICATE) && (item.Class == VAR_PREPOSITION))
                        {
                            if (Array.IndexOf(prepositions.ToArray(), item.Term) != -1)
                                preposition = item.Term;
                        }
                    }
                    int numeral = -1;
                    foreach (Vocable item in period1.Word)
                    {
                        if ((item.Sentence == VAR_PREDICATE) && (item.Class == VAR_NUMERAL))
                        {
                            if (Array.IndexOf(numerals.ToArray(), int.Parse(item.Term)) != -1)
                                numeral = int.Parse(item.Term);
                        }
                    }
                    if ((verb != string.Empty) && (noun != string.Empty))
                        result = await OnCommandButton(language, verb, noun, adjective, numeral, preposition, phrase);
                }
                return result;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<List<Message>> LoopCommmand(List<string> share, User user, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation loop command \"Bot\" view model failed!");

                List<Message> messages = new List<Message>();
                foreach (string item in share)
                {
                    //-----
                    if (SettingService.Instance.ModeBot)
                        messages = MessageService.Instance.Bots(user, item, language);
                    Messages = MessageService.Instance.Messages(user, item, language);
                    //-----
                    string response = string.Empty;
                    if (item != string.Empty)
                        response = await OnSendButton(item, user, language);
                    //-----
                    if (response != string.Empty)
                    {
                        if (SettingService.Instance.ModeBot)
                            messages = MessageService.Instance.Bots(user, response, language);
                        Messages = MessageService.Instance.Messages(user, response, language);
                    }
                }
                return messages;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return new List<Message>();
            }
        }

        private async Task<List<Message>> LoadCommmand(string parameter, User user, string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load command \"Bot\" view model failed!");

                HashSet<string> cameras = VAR_CATCH_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> records = VAR_CATCH_RECORD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> shares = VAR_CATCH_SHARE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> terminates = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                List<Message> notes = new List<Message>();

                if (Array.IndexOf(terminates.ToArray(), parameter) != -1)
                {
                    List<string> share = new List<string>();
                    share = await this._botService.Terminate(language, messages);
                    notes = await LoopCommmand(share, user, language);
                }

                if (Array.IndexOf(cameras.ToArray(), parameter) != -1)
                {
                    List<string> share = new List<string>();
                    share = await this._botService.CaptureCamera(language, parameter, messages);
                    notes = await LoopCommmand(share, user, language);
                }

                if (Array.IndexOf(records.ToArray(), parameter) != -1)
                {
                    List<string> share = new List<string>();
                    share = await this._botService.RecordAudio(language, parameter, messages);
                    notes = await LoopCommmand(share, user, language);
                }

                bool device = false;
                device = await this._botService.DeviceShare(language, messages, parameter);

                if ((Array.IndexOf(shares.ToArray(), parameter) != -1) || device)
                {
                    List<string> share = new List<string>();
                    share = await this._botService.ShareFile(language, parameter, messages);
                    notes = await LoopCommmand(share, user, language);
                }

                return notes;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return new List<Message>();
            }
        }

        private async Task<string> ChooseCommmand(string parameter, string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation choose command \"Bot\" view model failed!");

                HashSet<string> cameras = VAR_CATCH_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> records = VAR_CATCH_RECORD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> shares = VAR_CATCH_SHARE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> terminates = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string result = string.Empty;
                if (messages.Count > 0)
                {
                    if (Array.IndexOf(cameras.ToArray(), parameter) != -1)
                        result = await this._botService.CaptureCamera(language, messages);
                    if (Array.IndexOf(records.ToArray(), parameter) != -1)
                        result = await this._botService.RecordAudio(language, messages);
                    //------
                    bool device = false;
                    device = await this._botService.DeviceShare(language, messages, parameter);
                    if ((Array.IndexOf(shares.ToArray(), parameter) != -1) || device)
                        result = await this._botService.ShareFile(language, messages);
                }
                return result;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> OnSendBot(string parameter, User user, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation send bot \"Bot\" view model failed!");

                HashSet<string> catchs = VAR_CATCH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                List<Message> messages = new List<Message>();
                messages = MessageService.Instance.Bots(language);
                bool device = await this._botService.DeviceShare(language, messages, parameter);

                string result = string.Empty;
                if ((Array.IndexOf(catchs.ToArray(), parameter) != -1) || device) 
                {
                    List<Message> notes = new List<Message>();
                    notes = await LoadCommmand(parameter, user, language, messages);

                    result = await ChooseCommmand(parameter, language, notes);
                    if ((result != string.Empty) && (notes.Count > 0))
                    {
                        MessageService.Instance.Bots(user, result, language);
                        Messages = MessageService.Instance.Messages(user, result, language);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> OnSendButton(string parameter, User user, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation send button \"Bot\" view model failed!");

                List<Locution> locutions = new List<Locution>();
                locutions = await GoMessage(parameter, user, language);
                string result = string.Empty;
                result = await DecisionMessage(language, locutions);

                return result;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> OnCommandButton(string language, string verb, string noun, string adjective, int numeral, string preposition, string text)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation command button \"Bot\" view model failed!");

                HashSet<string> verbs_load = new HashSet<string>();
                verbs_load = VAR_LOAD.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_view = new HashSet<string>();
                verbs_view = VAR_VIEW.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_play = new HashSet<string>();
                verbs_play = VAR_PLAY.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_record = new HashSet<string>();
                verbs_record = VAR_RECORD.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_stop = new HashSet<string>();
                verbs_stop = VAR_STOP.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_rotate = new HashSet<string>();
                verbs_rotate = VAR_ROTATE.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_speak = new HashSet<string>();
                verbs_speak = VAR_SPEAK.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_download = new HashSet<string>();
                verbs_download = VAR_DOWNLOAD.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_upload = new HashSet<string>();
                verbs_upload = VAR_UPLOAD.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_vibtate = new HashSet<string>();
                verbs_vibtate = VAR_VIBRATION.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_capture = new HashSet<string>();
                verbs_capture = VAR_CAPTURE.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_save = new HashSet<string>();
                verbs_save = VAR_SAVE.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_turn = new HashSet<string>();
                verbs_turn = VAR_TURN.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_share = new HashSet<string>();
                verbs_share = VAR_SHARE.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_terminate = new HashSet<string>();
                verbs_terminate = VAR_TERMINATE.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_turn_on = new HashSet<string>();
                verbs_turn_on = VAR_TURN_ON.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_start = new HashSet<string>();
                verbs_start = VAR_START.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_scan = new HashSet<string>();
                verbs_scan = VAR_SCAN.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_send = new HashSet<string>();
                verbs_send = VAR_SEND.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_connect = new HashSet<string>();
                verbs_connect = VAR_CONNECT.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_disconnect = new HashSet<string>();
                verbs_disconnect = VAR_DISCONNECT.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> nouns_gps = new HashSet<string>();
                nouns_gps = VAR_GPS.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_camera = new HashSet<string>();
                nouns_camera = VAR_CAMERA.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_battery = new HashSet<string>();
                nouns_battery = VAR_BATTERY.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_file = new HashSet<string>();
                nouns_file = VAR_FILE.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_wav = new HashSet<string>();
                nouns_wav = VAR_WAV.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_mp3 = new HashSet<string>();
                nouns_mp3 = VAR_MP3.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_vibration = new HashSet<string>();
                nouns_vibration = VAR_VIBRATION.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_bluetooth = new HashSet<string>();
                nouns_bluetooth = VAR_BLUETOOTH.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_text = new HashSet<string>();
                nouns_text = VAR_TEXT.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_phone = new HashSet<string>();
                nouns_phone = VAR_PHONE.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_flash = new HashSet<string>();
                nouns_flash = VAR_FLASH.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_audio = new HashSet<string>();
                nouns_audio = VAR_AUDIO.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_bot = new HashSet<string>();
                nouns_bot = VAR_BOT.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_bluetooth3 = new HashSet<string>();
                nouns_bluetooth3 = VAR_BLUETOOTH3.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_bluetooth4 = new HashSet<string>();
                nouns_bluetooth4 = VAR_BLUETOOTH4.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> adjective_front = new HashSet<string>();
                adjective_front = VAR_FRONT.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> adjective_rear = new HashSet<string>();
                adjective_rear = VAR_REAR.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> adjective_on = new HashSet<string>();
                adjective_on = VAR_ON.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> adjective_off = new HashSet<string>();
                adjective_off = VAR_OFF.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> adjective_auto = new HashSet<string>();
                adjective_auto = VAR_AUTO.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> preposition_to = new HashSet<string>();
                preposition_to = VAR_TO.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<int> numeral_three = VAR_THREE;
                HashSet<int> numeral_four = VAR_FOUR;
                //-----
                string response = string.Empty;
                //-----
                if (Array.IndexOf(verbs_record.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_audio.ToArray(), noun) != -1)
                    {
                        response = await RecordAudio(language);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_record.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_mp3.ToArray(), noun) != -1)
                    {
                        response = await StartRecord(language, nouns_mp3.ToArray()[0]);
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_record.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_wav.ToArray(), noun) != -1)
                    {
                        response = await StartRecord(language, nouns_wav.ToArray()[0]);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_stop.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_mp3.ToArray(), noun) != -1)
                    {
                        response = await StopRecord(language, nouns_mp3.ToArray()[0]);
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_stop.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_wav.ToArray(), noun) != -1)
                    {
                        response = await StopRecord(language, nouns_wav.ToArray()[0]);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_play.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_wav.ToArray(), noun) != -1) 
                    {
                        response = await PlayRecord(language, nouns_wav.ToArray()[0]);
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_play.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_mp3.ToArray(), noun) != -1)
                    {
                        response = await PlayRecord(language, nouns_mp3.ToArray()[0]);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_terminate.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bot.ToArray(), noun) != -1)
                    {
                        response = await EndBot(language);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_load.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        response = await CaputreCamera(language);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_rotate.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(adjective_front.ToArray(), adjective) != -1)
                        {
                            response = await RotateCamera(language, adjective_front.ToArray()[0]);
                            return response;
                        }
                    }
                }
                if (Array.IndexOf(verbs_rotate.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(adjective_rear.ToArray(), adjective) != -1)
                        {
                            response = await RotateCamera(language, adjective_rear.ToArray()[0]);
                            return response;
                        }
                    }
                }
                //-----
                if (Array.IndexOf(verbs_turn_on.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_flash.ToArray(), noun) != -1)
                    {
                        response = await FlashCamera(language, adjective_on.ToArray()[0]);
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_turn.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_flash.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(adjective_off.ToArray(), adjective) != -1)
                        {
                            response = await FlashCamera(language, adjective_off.ToArray()[0]);
                            return response;
                        }
                    }
                }
                if (Array.IndexOf(verbs_turn.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_flash.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(adjective_auto.ToArray(), adjective) != -1)
                        {
                            response = await FlashCamera(language, adjective_auto.ToArray()[0]);
                            return response;
                        }
                    }
                }
                //-----
                if (Array.IndexOf(verbs_start.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        response = await StartCamera(language);
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_stop.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        response = await StopCamera(language);
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_capture.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        response = await CaptureImage(language);
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_save.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        response = await SaveImage(language);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_share.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_file.ToArray(), noun) != -1)
                    {
                        response = await ShareFile(language);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_upload.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_file.ToArray(), noun) != -1)
                    {
                        response = await UploadFile(language);
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_download.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_file.ToArray(), noun) != -1)
                    {
                        response = await DownloadFile(language);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_view.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_gps.ToArray(), noun) != -1)
                    {
                        response = await GPS(language);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_view.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_battery.ToArray(), noun) != -1)
                    {
                        response = await Battery(language);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_vibtate.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_phone.ToArray(), noun) != -1)
                    {
                        response = await Vibration(7, language);
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_scan.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bluetooth.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(numeral_three.ToArray(), numeral) != -1)
                        {
                            response = await ScanBluetooth3(language);
                            return response;
                        }
                    }
                }
                if (Array.IndexOf(verbs_scan.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bluetooth.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(numeral_four.ToArray(), numeral) != -1)
                        {
                            response = await ScanBluetooth4(language);
                            return response;
                        }
                    }
                }
                if (Array.IndexOf(verbs_connect.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bluetooth.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(numeral_three.ToArray(), numeral) != -1)
                        {
                            response = await ConnectBluetooth3(language);
                            return response;
                        }
                    }
                }
                if (Array.IndexOf(verbs_connect.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bluetooth.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(numeral_four.ToArray(), numeral) != -1)
                        {
                            response = await ConnectBluetooth4(language);
                            return response;
                        }
                    }
                }
                if (Array.IndexOf(verbs_disconnect.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bluetooth.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(numeral_three.ToArray(), numeral) != -1)
                        {
                            response = await DisconnectBluetooth3(language);
                            return response;
                        }
                    }
                }
                if (Array.IndexOf(verbs_disconnect.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bluetooth.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(numeral_four.ToArray(), numeral) != -1)
                        {
                            response = await DisconnectBluetooth4(language);
                            return response;
                        }
                    }
                }
                if (Array.IndexOf(verbs_send.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bluetooth.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(numeral_three.ToArray(), numeral) != -1)
                        {
                            response = await SendBluetooth3(language);
                            return response;
                        }
                    }
                }
                if (Array.IndexOf(verbs_send.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bluetooth.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(numeral_four.ToArray(), numeral) != -1)
                        {
                            response = await SendBluetooth4(language);
                            return response;
                        }
                    }
                }
                //-----
                if (Array.IndexOf(verbs_speak.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_text.ToArray(), noun) != -1)
                    {
                        response = await SpeakText(language, text);
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_speak.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_file.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(preposition_to.ToArray(), preposition) != -1)
                        {
                            response = await FileText(language, text);
                            return response;
                        }
                    }
                }
                //-----
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task OnBackCommand()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation back command \"Bot\" view model failed!");
                await Shell.Current.GoToAsync($"..");
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
                if (this._error_off) throw new InvalidOperationException("Operation apply query attibutes \"Bot\" view model failed!");

                Username = query["username"] as User;
                if (Username == null)
                {
                    Username = MessageService.Instance.GetUser(PORTUGUES.Lowercase);
                }
                List<Message> message_language = MessageService.Instance.Messages(Username.Name);
                if (message_language.Count > 0)
                {
                    Messages = message_language;
                }
                else
                {
                    if (!((Username.Name == PORTUGUES.Lowercase) || (Username.Name == PORTUGUES.Uppercase)))
                    {
                        List<Message> chats = MessageService.Instance.Chats;
                        Message? chat = chats.Find(index => index.Sender == Username);
                        List<Message> reports = MessageService.Instance.Messages(chat.Sender, chat.Text, chat.Sender.Name);
                        Messages = reports;
                    }
                    else
                    {
                        Messages = MessageService.Instance.Messages(Username, "What can I do for you?", Username.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion

        #region BOT
        private async Task<string> CaputreCamera(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture camera \"Bot\" view model failed!");

                if (!SettingService.Instance.ModeBot) SettingService.Instance.ModeBot = true;
                string response = await this._botService.CaptureCamera(language);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> StartCamera(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation start camera \"Bot\" view model failed!");

                HashSet<string> start = VAR_START
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                //await this._cameraView.StartCameraPreview(Token);
                response = $"{camera.ToArray()[0]} {start.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> StopCamera(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation stop camera \"Bot\" view model failed!");

                HashSet<string> stop = VAR_STOP
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                //this._cameraView.StopCameraPreview();
                response = $"{camera.ToArray()[0]} {stop.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> RotateCamera(string language, string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation rotate camera \"Bot\" view model failed!");

                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> rotate = VAR_ROTATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> front = VAR_FRONT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> rear = VAR_REAR
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                /*
                if (this._cameraProvider.AvailableCameras is not null)
                {
                    if (SelectedCamera.DeviceId == this._cameraProvider.AvailableCameras[0].DeviceId)
                        SelectedCamera = this._cameraProvider.AvailableCameras.First(x => x.Position == CameraPosition.Front);
                    else if (SelectedCamera.DeviceId == this._cameraProvider.AvailableCameras[1].DeviceId)
                        SelectedCamera = this._cameraProvider.AvailableCameras.First(x => x.Position == CameraPosition.Rear);
                }
                */
                if (kind == front.ToArray()[0]) 
                    response = $"{camera.ToArray()[0]} {front.ToArray()[0]} {rotate.ToArray()[0]}.";
                else
                    response = $"{camera.ToArray()[0]} {rear.ToArray()[0]} {rotate.ToArray()[0]}.";

                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> FlashCamera(string language, string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation flash camera \"Bot\" view model failed!");

                HashSet<string> flash = VAR_FLASH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> off = VAR_OFF
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> on = VAR_ON
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> auto = VAR_AUTO
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> turn = VAR_TURN
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> turn_on = VAR_TURN_ON
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                /*
                if (this._cameraProvider.AvailableCameras is not null)
                {
                    if (flash.ToArray()[0] == kind)
                        this.FlashMode = CameraFlashMode.On;
                    if (off.ToArray()[0] == kind)
                        this.FlashMode = CameraFlashMode.Off;
                    if (auto.ToArray()[0] == kind)
                        this.FlashMode = CameraFlashMode.Auto;
                }
                */
                if (kind == on.ToArray()[0])
                    response = $"{flash.ToArray()[0]} {turn_on.ToArray()[0]}.";
                else
                {
                    if (kind == off.ToArray()[0])
                        response = $"{flash.ToArray()[0]} {off.ToArray()[0]} {turn.ToArray()[0]}.";
                    else
                        response = $"{flash.ToArray()[0]} {auto.ToArray()[0]} {turn.ToArray()[0]}.";
                }
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> CaptureImage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture image \"Bot\" view model failed!");

                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> capture = VAR_CAPTURE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                //await this._cameraView.CaptureImage(Token);
                response = $"{camera.ToArray()[0]} {capture.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> SaveImage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation save image \"Bot\" view model failed!");

                HashSet<string> save = VAR_SAVE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> camera = VAR_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                //await _perceptionService.SaveImage(Bytes);
                response = $"{camera.ToArray()[0]} {save.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> RecordAudio(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation record audio \"Bot\" view model failed!");

                if (!SettingService.Instance.ModeBot) SettingService.Instance.ModeBot = true;
                string response = await this._botService.RecordAudio(language);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> StartRecord(string language, string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation start record \"Bot\" view model failed!");

                HashSet<string> mp3 = VAR_MP3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> wav = VAR_WAV
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> record = VAR_RECORD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                PermissionStatus permission_status = await RequestandCheckPermission();
                if (permission_status == PermissionStatus.Granted)
                {
                    if (kind == mp3.ToArray()[0])
                    {
                        //this._perceptionService.StartRecordMP3();
                        response = $"{mp3.ToArray()[0]} {record.ToArray()[0]}.";
                    } 
                    else
                    {
                        //this._perceptionService.StartRecordWav();
                        response = $"{wav.ToArray()[0]} {record.ToArray()[0]}.";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> StopRecord(string language, string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation stop record \"Bot\" view model failed!");

                HashSet<string> mp3 = VAR_MP3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> wav = VAR_WAV
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> stop = VAR_STOP
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                if (kind == mp3.ToArray()[0])
                {
                    //string audio_file_path = this._perceptionService.StopRecordMP3();
                    //this._perceptionService.SendRecording(audio_file_path);
                    response = $"{mp3.ToArray()[0]} {stop.ToArray()[0]}.";
                }
                else
                {
                    //string audio_file_path = this._perceptionService.StopRecordWav();
                    //this._perceptionService.SendRecording(audio_file_path);
                    response = $"{wav.ToArray()[0]} {stop.ToArray()[0]}.";
                }
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> PlayRecord(string language, string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation play record \"Bot\" view model failed!");

                HashSet<string> mp3 = VAR_MP3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> wav = VAR_WAV
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> play = VAR_PLAY
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                /*                
                this._perceptionService.StopAudio();
                string audio_file_path = _perceptionService.ReceiveRecording();
                this._perceptionService.PlayAudio(audio_file_path);
                */
                if (kind == mp3.ToArray()[0]) response = $"{mp3.ToArray()[0]} {play.ToArray()[0]}.";
                else
                    response = $"{wav.ToArray()[0]} {play.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> ShareFile(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation share file \"Bot\" view model failed!");

                if (!SettingService.Instance.ModeBot) SettingService.Instance.ModeBot = true;
                string response = await this._botService.ShareFile(language);
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> UploadFile(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation upload file \"Bot\" view model failed!");

                HashSet<string> upload = VAR_UPLOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> file = VAR_FILE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                /*
                string audio_file_path = await this._perceptionService.UploadFile();
                this._perceptionService.SendRecording(audio_file_path);
                */
                response = $"{file.ToArray()[0]} {upload.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> DownloadFile(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation download file \"Bot\" view model failed!");

                HashSet<string> download = VAR_DOWNLOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> file = VAR_FILE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                //await this._perceptionService.DownloadFile();
                response = $"{file.ToArray()[0]} {download.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> EndBot(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation end bot \"Bot\" view model failed!");

                HashSet<string> terminate = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bot = VAR_BOT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                if (SettingService.Instance.ModeBot) SettingService.Instance.ModeBot = false;
                MessageService.Instance.Remove(language);
                response = $"{bot.ToArray()[0]} {terminate.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> GPS(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation gps \"Bot\" view model failed!");

                HashSet<string> work = VAR_WORK
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> dont_work = VAR_DONT_WORK
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> with = VAR_WITH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> and = VAR_AND
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> longitude = VAR_LONGITUDE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> latitude = VAR_LATITUDE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                Location location = new Location();
                //location = await _perceptionService.GetCurrentLocation();
                if (location != null)
                    response = $"{work.ToArray()[0]} {with.ToArray()[0]} {latitude.ToArray()[0]} {location.Latitude} {and.ToArray()[0]} {longitude.ToArray()[0]} {location.Longitude}.";
                else
                    response = $"{dont_work.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Battery(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation battery \"Bot\" view model failed!");

                HashSet<string> load = VAR_LOAD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                double battery = 0;
                //battery = _perceptionService.GetCharge();
                response = $"{battery.ToString()}% {load.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> Vibration(int number, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation vibration \"Bot\" view model failed!");

                HashSet<string> vibration = VAR_VIBRATION
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> level = VAR_LEVEL
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                //this._perceptionService.SetVibration(number);
                response = $"{vibration.ToArray()[0]} {level.ToArray()[0]} {number}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> ScanBluetooth3(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation scan bluetooth 3 \"Bot\" view model failed!");

                List<string> bluetooth = await this._perceptionService.ScanBluetooth3();
                string response = string.Empty;
                if (bluetooth.Count == 0)
                {
                    response = UNKNOW;
                    List<string> unknow = new List<string>();
                    unknow.Add(response);
                    await this._botService.ShareScan(unknow);
                    return response;
                } else
                    await this._botService.ShareScan(bluetooth);
                foreach (string item in bluetooth)
                    {
                        if (response == string.Empty)
                            response = item;
                        else
                            response += ", " + item;
                    }
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> ScanBluetooth4(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation scan bluetooth 4 \"Bot\" view model failed!");

                List<string> bluetooth = await this._perceptionService.ScanBluetooth4();
                string response = string.Empty;
                if (bluetooth.Count == 0)
                {
                    response = UNKNOW;
                    List<string> unknow = new List<string>();
                    unknow.Add(response);
                    await this._botService.ShareScan(unknow);
                    return response;
                }
                else
                    await this._botService.ShareScan(bluetooth);
                foreach (string item in bluetooth)
                {
                    if (response == string.Empty)
                        response = item;
                    else
                        response += ", " + item;
                }
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> ConnectBluetooth3(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect bluetooth 4 \"Bot\" view model failed!");

                HashSet<string> bluetooth = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> connected = VAR_CONNECTED
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> in_proposition = VAR_IN
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<int> numeral_three = VAR_THREE;

                string device = string.Empty;
                device = await this._botService.DeviceShare();
                string result = string.Empty;
                result = await this._perceptionService.ConnectBluetooth3(device);
                string response = string.Empty;
                response = $"{device} {connected.ToArray()[0]} {in_proposition.ToArray()[0]} {bluetooth.ToArray()[0]} {numeral_three.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> ConnectBluetooth4(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect bluetooth 4 \"Bot\" view model failed!");

                HashSet<string> bluetooth = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> connected = VAR_CONNECTED
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> in_proposition = VAR_IN
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<int> numeral_four = VAR_FOUR;

                string device = string.Empty;
                device = await this._botService.DeviceShare();
                string result = string.Empty;
                result = await this._perceptionService.ConnectBluetooth4(device);
                string response = string.Empty;
                response = $"{device} {connected.ToArray()[0]} {in_proposition.ToArray()[0]} {bluetooth.ToArray()[0]} {numeral_four.ToArray()[0]} .";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> DisconnectBluetooth3(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation disconnect bluetooth 3 \"Bot\" view model failed!");

                HashSet<string> bluetooth = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> disconnect = VAR_DISCONNECT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<int> numeral_three = VAR_THREE;

                await this._perceptionService.DisconnectBluetooth3();
                string response = string.Empty;
                response = $"{bluetooth.ToArray()[0]} {numeral_three.ToArray()[0]} {disconnect.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> DisconnectBluetooth4(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation disconnect bluetooth 4 \"Bot\" view model failed!");

                HashSet<string> bluetooth = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> disconnect = VAR_DISCONNECT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<int> numeral_four = VAR_FOUR;

                string response = string.Empty;
                await this._perceptionService.DisconnectBluetooth4();
                response = $"{bluetooth.ToArray()[0]} {numeral_four.ToArray()[0]} {disconnect.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }
        private async Task<string> SendBluetooth3(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation disconnect bluetooth 3 \"Bot\" view model failed!");

                HashSet<string> bluetooth = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> send = VAR_SEND
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> file = VAR_FILE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> through = VAR_THROUGH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<int> numeral_three = VAR_THREE;

                string response = string.Empty;
                await this._perceptionService.SendBluetooth3();
                response = $"{file.ToArray()[0]} {send.ToArray()[0]} {through.ToArray()[0]} {bluetooth.ToArray()[0]} {numeral_three.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }
        private async Task<string> SendBluetooth4(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation disconnect bluetooth 4 \"Bot\" view model failed!");

                HashSet<string> bluetooth = VAR_BLUETOOTH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> send = VAR_SEND
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> file = VAR_FILE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> through = VAR_THROUGH
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<int> numeral_four = VAR_FOUR;

                string response = string.Empty;
                await this._perceptionService.SendBluetooth4();
                response = $"{file.ToArray()[0]} {send.ToArray()[0]} {through.ToArray()[0]} {bluetooth.ToArray()[0]} {numeral_four.ToArray()[0]} .";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> SpeakText(string language, string locution)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation speak text \"Bot\" view model failed!");

                HashSet<string> text = VAR_TEXT
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> speak = VAR_SPEAK
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                //this._perceptionService.SpeakText(locution);
                response = $"{text.ToArray()[0]} {speak.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> FileText(string language, string locution)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation file text \"Bot\" view model failed!");

                HashSet<string> file = VAR_FILE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> save = VAR_SAVE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string response = string.Empty;
                /*
                string file_path = this._perceptionService.FileText(locution);
                _perceptionService.SendRecording(file_path);
                */
                response = $"{file.ToArray()[0]} {save.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }
        #endregion

        #region PERMISSION
        public async Task<PermissionStatus> RequestandCheckPermission()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation request check permission \"Bot\" view model failed!");

                PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted)
                    await Permissions.RequestAsync<Permissions.StorageWrite>();
                status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
                if (status != PermissionStatus.Granted)
                    await Permissions.RequestAsync<Permissions.Microphone>();
                PermissionStatus storagePermission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                PermissionStatus microPhonePermission = await Permissions.CheckStatusAsync<Permissions.Microphone>();
                if (storagePermission == PermissionStatus.Granted && microPhonePermission == PermissionStatus.Granted)
                {
                    return PermissionStatus.Granted;
                }
                return PermissionStatus.Denied;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return PermissionStatus.Denied;
            }
        }
        #endregion
    }
}
