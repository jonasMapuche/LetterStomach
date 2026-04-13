using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LetterStomach.Interfaces;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
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

        #region VARIABLE
        private string _declarative;
        private string _verb;
        private string _noun;
        private string _adjective;
        private string _predicate;
        private string _numeral;
        private string _especial;
        private string _preposition;

        private Language _language_portugues;

        private Dictionary<string, string> _load;
        private Dictionary<string, string> _execute;
        private Dictionary<string, string> _view;
        private Dictionary<string, string> _play;
        private Dictionary<string, string> _activity;
        private Dictionary<string, string> _record;
        private Dictionary<string, string> _stop;
        private Dictionary<string, string> _rotate;
        private Dictionary<string, string> _speak;
        private Dictionary<string, string> _download;
        private Dictionary<string, string> _upload;
        private Dictionary<string, string> _capture;
        private Dictionary<string, string> _save;
        private Dictionary<string, string> _turn;
        private Dictionary<string, string> _share;
        private Dictionary<string, string> _feature;
        private Dictionary<string, string> _terminate;
        private Dictionary<string, string> _turn_on;
        private Dictionary<string, string> _start;
        private Dictionary<string, string> _scan;
        private Dictionary<string, string> _work;
        private Dictionary<string, string> _dont_work;
        private Dictionary<string, string> _send;
        private Dictionary<string, string> _connect;
        private Dictionary<string, string> _disconnect;
        private Dictionary<string, string> _connected;
        private Dictionary<string, string> _juncao;

        private Dictionary<string, string> _gps;
        private Dictionary<string, string> _bluetooth;
        private Dictionary<string, string> _bluetooth3;
        private Dictionary<string, string> _bluetooth4;
        private Dictionary<string, string> _camera;
        private Dictionary<string, string> _wav;
        private Dictionary<string, string> _mp3;
        private Dictionary<string, string> _battery;
        private Dictionary<string, string> _file;
        private Dictionary<string, string> _vibration;
        private Dictionary<string, string> _text;
        private Dictionary<string, string> _phone;
        private Dictionary<string, string> _flash;
        private Dictionary<string, string> _audio;
        private Dictionary<string, string> _bot;
        private Dictionary<string, string> _longitude;
        private Dictionary<string, string> _latitude;
        private Dictionary<string, string> _level;

        private Dictionary<string, string> _with;
        private Dictionary<string, string> _in;
        private Dictionary<string, string> _off;
        private Dictionary<string, string> _auto;
        private Dictionary<string, string> _on;
        private Dictionary<string, string> _to;

        private Dictionary<string, string> _and;

        private Dictionary<string, string> _front;
        private Dictionary<string, string> _rear;

        private Dictionary<string, string> _through;

        private Dictionary<string, string> _catch;
        private Dictionary<string, string> _catch_camera;
        private Dictionary<string, string> _catch_record;
        private Dictionary<string, string> _catch_share;

        private HashSet<int> _algarismo;

        private HashSet<int> _three;
        private HashSet<int> _four;

        private string _unknow;

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

        public CameraView _cameraView;

        private ICameraProvider _cameraProvider;
        private IHttpService _httpService;
        private IPerceptionService _perceptionService;
        private IBotService _botService;

        public ICameraProvider MediaCamera {  get => _cameraProvider; set => _cameraProvider = value; }

        public CancellationToken Token => CancellationToken.None;
        #endregion

        #region CONTRUCTOR
        public BotViewModel(IRecordService recordService, IAudioService audioService, ITextSpeakService textSpeakService) 
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation contructor \"Bot\" view model failed!");
                else this.error_message = string.Empty;

                this._declarative = SettingService.Instance.Declarative;
                this._verb = SettingService.Instance.Verb;
                this._noun = SettingService.Instance.Noun;
                this._adjective = SettingService.Instance.Adjective;
                this._predicate = SettingService.Instance.Predicate;
                this._numeral = SettingService.Instance.Numeral;
                this._especial = SettingService.Instance.Especial;
                this._preposition = SettingService.Instance.Preposition;

                this._language_portugues = SettingService.Instance.Portugues;

                this._load = SettingService.Instance.Load;
                this._execute = SettingService.Instance.Execute;
                this._view = SettingService.Instance.View;
                this._play = SettingService.Instance.Play;
                this._activity = SettingService.Instance.Activity;
                this._record = SettingService.Instance.Record;
                this._stop = SettingService.Instance.Stop;
                this._rotate = SettingService.Instance.Rotate;
                this._speak = SettingService.Instance.Speak;
                this._download = SettingService.Instance.Download;
                this._upload = SettingService.Instance.Upload;
                this._capture = SettingService.Instance.Capture;
                this._save = SettingService.Instance.Save;
                this._turn = SettingService.Instance.Turn;
                this._share = SettingService.Instance.Share;
                this._feature = SettingService.Instance.Feature;
                this._terminate = SettingService.Instance.Terminate;
                this._turn_on = SettingService.Instance.Turn_On;
                this._start = SettingService.Instance.Start;
                this._scan = SettingService.Instance.Scan;
                this._work = SettingService.Instance.Work;
                this._dont_work = SettingService.Instance.Dont_Work;
                this._send = SettingService.Instance.Send;
                this._connect = SettingService.Instance.Connect;
                this._disconnect = SettingService.Instance.Disconnect;
                this._connected = SettingService.Instance.Connected;
                this._juncao = SettingService.Instance.Juncao;

                this._gps = SettingService.Instance.GPS;
                this._bluetooth = SettingService.Instance.Bluetooth;
                this._bluetooth3 = SettingService.Instance.Bluetooth3;
                this._bluetooth4 = SettingService.Instance.Bluetooth4;
                this._camera = SettingService.Instance.Camera;
                this._wav = SettingService.Instance.WAV;
                this._mp3 = SettingService.Instance.MP3;
                this._battery = SettingService.Instance.Battery;
                this._file = SettingService.Instance.File;
                this._vibration = SettingService.Instance.Vibration;
                this._text = SettingService.Instance.Text;
                this._phone = SettingService.Instance.Phone;
                this._flash = SettingService.Instance.Flash;
                this._audio = SettingService.Instance.Audio;
                this._bot = SettingService.Instance.Bot;
                this._longitude = SettingService.Instance.Longitude;
                this._latitude = SettingService.Instance.Latitude;
                this._level = SettingService.Instance.Level;

                this._with = SettingService.Instance.With;
                this._in = SettingService.Instance.In;
                this._off = SettingService.Instance.Off;
                this._auto = SettingService.Instance.Auto;
                this._on = SettingService.Instance.On;
                this._to = SettingService.Instance.To;

                this._and = SettingService.Instance.And;

                this._front = SettingService.Instance.Front;
                this._rear = SettingService.Instance.Rear;
                this._through = SettingService.Instance.Through;

                this._catch = SettingService.Instance.Catch;
                this._catch_camera = SettingService.Instance.Catch_Camera;
                this._catch_record = SettingService.Instance.Catch_Record;
                this._catch_share = SettingService.Instance.Catch_Share;

                this._algarismo = SettingService.Instance.Algarismo;

                this._three = SettingService.Instance.Three;
                this._four = SettingService.Instance.Four;

                this._unknow = "unknown";

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private void OnInitMessage()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation init message \"Bot\" view model failed!");

                Username = MessageService.Instance.GetUser(this._language_portugues.Lowercase);
                List<Message> message_language = MessageService.Instance.Messages(Username.Name);
                if (message_language.Count > 0)
                    Messages = message_language;
                else Messages = MessageService.Instance.Messages(Username, "What can I do for you?", Username.Name);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
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
                throw new InvalidOperationException(this.error_message);
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
                    if (index.Class != this._especial)
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> DecisionMessage(string language, List<Locution> locutions)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation decision message \"Bot\" view model failed!");

                HashSet<string> verbs = this._execute
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns = this._activity
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> adjectives = this._feature
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> prepositions = this._juncao
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<int> numerals = this._algarismo;

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
                if ((period1 != null) && (period1.Kind == this._declarative))
                {
                    string verb = string.Empty;
                    foreach (Vocable item in period1.Word)
                    {
                        if (item.Class == this._verb)
                        {
                            if (Array.IndexOf(verbs.ToArray(), item.Term) != -1)
                                verb = item.Term;
                        }
                    }
                    string noun = string.Empty;
                    foreach (Vocable item in period1.Word)
                    {
                        if ((item.Sentence == this._predicate) && (item.Class == this._noun))
                        {
                            if (Array.IndexOf(nouns.ToArray(), item.Term) != -1)
                                noun = item.Term;
                        }
                    }
                    string adjective = string.Empty;
                    foreach (Vocable item in period1.Word)
                    {
                        if ((item.Sentence == this._predicate) && (item.Class == this._adjective))
                        {
                            if (Array.IndexOf(adjectives.ToArray(), item.Term) != -1)
                                adjective = item.Term;
                        }
                    }
                    string preposition = string.Empty;
                    foreach (Vocable item in period1.Word)
                    {
                        if ((item.Sentence == this._predicate) && (item.Class == this._preposition))
                        {
                            if (Array.IndexOf(prepositions.ToArray(), item.Term) != -1)
                                preposition = item.Term;
                        }
                    }
                    int numeral = -1;
                    foreach (Vocable item in period1.Word)
                    {
                        if ((item.Sentence == this._predicate) && (item.Class == this._numeral))
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
                throw new InvalidOperationException(this.error_message);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<List<Message>> LoadCommmand(string parameter, User user, string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load command \"Bot\" view model failed!");

                HashSet<string> cameras = this._catch_camera
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> records = this._catch_record
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> shares = this._catch_share
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> terminates = this._terminate
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> ChooseCommmand(string parameter, string language, List<Message> messages)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation choose command \"Bot\" view model failed!");

                HashSet<string> cameras = this._catch_camera
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> records = this._catch_record
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> shares = this._catch_share
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> terminates = this._terminate
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string result = string.Empty;
                if (messages.Count > 0)
                {
                    if (Array.IndexOf(cameras.ToArray(), parameter) != -1)
                        result = await this._botService.CaptureCamera(language, messages);
                    if (Array.IndexOf(records.ToArray(), parameter) != -1)
                        result = await this._botService.RecordAudio(language, messages);

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> OnSendBot(string parameter, User user, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation send bot \"Bot\" view model failed!");

                HashSet<string> catchs = this._catch
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
                throw new InvalidOperationException(this.error_message);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> OnCommandButton(string language, string verb, string noun, string adjective, int numeral, string preposition, string text)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation command button \"Bot\" view model failed!");

                HashSet<string> verbs_load = new HashSet<string>();
                verbs_load = this._load.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_view = new HashSet<string>();
                verbs_view = this._view.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_play = new HashSet<string>();
                verbs_play = this._play.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_record = new HashSet<string>();
                verbs_record = this._record.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_stop = new HashSet<string>();
                verbs_stop = this._stop.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_rotate = new HashSet<string>();
                verbs_rotate = this._rotate.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_speak = new HashSet<string>();
                verbs_speak = this._speak.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_download = new HashSet<string>();
                verbs_download = this._download.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_upload = new HashSet<string>();
                verbs_upload = this._upload.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_vibtate = new HashSet<string>();
                verbs_vibtate = this._vibration.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_capture = new HashSet<string>();
                verbs_capture = this._capture.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_save = new HashSet<string>();
                verbs_save = this._save.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_turn = new HashSet<string>();
                verbs_turn = this._turn.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_share = new HashSet<string>();
                verbs_share = this._share.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_terminate = new HashSet<string>();
                verbs_terminate = this._terminate.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_turn_on = new HashSet<string>();
                verbs_turn_on = this._turn_on.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_start = new HashSet<string>();
                verbs_start = this._start.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_scan = new HashSet<string>();
                verbs_scan = this._scan.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_send = new HashSet<string>();
                verbs_send = _send.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_connect = new HashSet<string>();
                verbs_connect = this._connect.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> verbs_disconnect = new HashSet<string>();
                verbs_disconnect = this._disconnect.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> nouns_gps = new HashSet<string>();
                nouns_gps = this._gps.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_camera = new HashSet<string>();
                nouns_camera = this._camera.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_battery = new HashSet<string>();
                nouns_battery = this._battery.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_file = new HashSet<string>();
                nouns_file = this._file.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_wav = new HashSet<string>();
                nouns_wav = this._wav.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_mp3 = new HashSet<string>();
                nouns_mp3 = this._mp3.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_vibration = new HashSet<string>();
                nouns_vibration = this._vibration.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_bluetooth = new HashSet<string>();
                nouns_bluetooth = this._bluetooth.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_text = new HashSet<string>();
                nouns_text = this._text.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_phone = new HashSet<string>();
                nouns_phone = this._phone.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_flash = new HashSet<string>();
                nouns_flash = this._flash.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_audio = new HashSet<string>();
                nouns_audio = this._audio.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_bot = new HashSet<string>();
                nouns_bot = this._bot.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_bluetooth3 = new HashSet<string>();
                nouns_bluetooth3 = this._bluetooth3.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> nouns_bluetooth4 = new HashSet<string>();
                nouns_bluetooth4 = this._bluetooth4.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> adjective_front = new HashSet<string>();
                adjective_front = this._front.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> adjective_rear = new HashSet<string>();
                adjective_rear = this._rear.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> adjective_on = new HashSet<string>();
                adjective_on = this._on.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> adjective_off = new HashSet<string>();
                adjective_off = this._off.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> adjective_auto = new HashSet<string>();
                adjective_auto = this._auto.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> preposition_to = new HashSet<string>();
                preposition_to = this._to.Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<int> numeral_three = this._three;
                HashSet<int> numeral_four = this._four;
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
                throw new InvalidOperationException(this.error_message);
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
                    Username = MessageService.Instance.GetUser(this._language_portugues.Lowercase);
                }
                List<Message> message_language = MessageService.Instance.Messages(Username.Name);
                if (message_language.Count > 0)
                {
                    Messages = message_language;
                }
                else
                {
                    if (!((Username.Name == this._language_portugues.Lowercase) || (Username.Name == this._language_portugues.Uppercase)))
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> StartCamera(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation start camera \"Bot\" view model failed!");

                HashSet<string> start = this._start
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> camera = this._camera
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> StopCamera(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation stop camera \"Bot\" view model failed!");

                HashSet<string> stop = this._stop
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> camera = this._camera
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> RotateCamera(string language, string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation rotate camera \"Bot\" view model failed!");

                HashSet<string> camera = this._camera
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> rotate = this._rotate
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> front = this._front
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> rear = this._rear
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> FlashCamera(string language, string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation flash camera \"Bot\" view model failed!");

                HashSet<string> flash = this._flash
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> off = this._off
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> on = this._on
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> auto = this._auto
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> turn = this._turn
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> turn_on = this._turn_on
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> CaptureImage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation capture image \"Bot\" view model failed!");

                HashSet<string> camera = this._camera
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> capture = this._capture
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> SaveImage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation save image \"Bot\" view model failed!");

                HashSet<string> save = this._save
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> camera = this._camera
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
                throw new InvalidOperationException(this.error_message);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> StartRecord(string language, string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation start record \"Bot\" view model failed!");

                HashSet<string> mp3 = this._mp3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> wav = this._wav
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> record = this._record
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> StopRecord(string language, string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation stop record \"Bot\" view model failed!");

                HashSet<string> mp3 = this._mp3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> wav = this._wav
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> stop = this._stop
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> PlayRecord(string language, string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation play record \"Bot\" view model failed!");

                HashSet<string> mp3 = this._mp3
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> wav = this._wav
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> play = this._play
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
                throw new InvalidOperationException(this.error_message);
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> UploadFile(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation upload file \"Bot\" view model failed!");

                HashSet<string> upload = this._upload
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> file = this._file
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> DownloadFile(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation download file \"Bot\" view model failed!");

                HashSet<string> download = this._download
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> file = this._file
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> EndBot(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation end bot \"Bot\" view model failed!");

                HashSet<string> terminate = this._terminate
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> bot = this._bot
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> GPS(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation gps \"Bot\" view model failed!");

                HashSet<string> work = this._work
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> dont_work = this._dont_work
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> with = this._with
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> and = this._and
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> longitude = this._longitude
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> latitude = this._latitude
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> Battery(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation battery \"Bot\" view model failed!");

                HashSet<string> load = this._load
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> Vibration(int number, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation vibration \"Bot\" view model failed!");

                HashSet<string> vibration = this._vibration
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> level = this._level
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
                throw new InvalidOperationException(this.error_message);
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
                    response = this._unknow;
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
                throw new InvalidOperationException(this.error_message);
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
                    response = this._unknow;
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> ConnectBluetooth3(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect bluetooth 4 \"Bot\" view model failed!");

                HashSet<string> bluetooth = this._bluetooth
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> connected = this._connected
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> in_proposition = this._in
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<int> numeral_three = this._three;

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> ConnectBluetooth4(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation connect bluetooth 4 \"Bot\" view model failed!");

                HashSet<string> bluetooth = this._bluetooth
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> connected = this._connected
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> in_proposition = this._in
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<int> numeral_four = this._four;

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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> DisconnectBluetooth3(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation disconnect bluetooth 3 \"Bot\" view model failed!");

                HashSet<string> bluetooth = this._bluetooth
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> disconnect = this._disconnect
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<int> numeral_three = this._three;

                await this._perceptionService.DisconnectBluetooth3();
                string response = string.Empty;
                response = $"{bluetooth.ToArray()[0]} {numeral_three.ToArray()[0]} {disconnect.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> DisconnectBluetooth4(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation disconnect bluetooth 4 \"Bot\" view model failed!");

                HashSet<string> bluetooth = this._bluetooth
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> disconnect = this._disconnect
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<int> numeral_four = this._four;

                string response = string.Empty;
                await this._perceptionService.DisconnectBluetooth4();
                response = $"{bluetooth.ToArray()[0]} {numeral_four.ToArray()[0]} {disconnect.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        
        private async Task<string> SendBluetooth3(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation disconnect bluetooth 3 \"Bot\" view model failed!");

                HashSet<string> bluetooth = this._bluetooth
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> send = _send
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> file = this._file
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> through = this._through
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<int> numeral_three = this._three;

                string response = string.Empty;
                await this._perceptionService.SendBluetooth3();
                response = $"{file.ToArray()[0]} {send.ToArray()[0]} {through.ToArray()[0]} {bluetooth.ToArray()[0]} {numeral_three.ToArray()[0]}.";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }
        
        private async Task<string> SendBluetooth4(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation disconnect bluetooth 4 \"Bot\" view model failed!");

                HashSet<string> bluetooth = this._bluetooth
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> send = _send
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> file = this._file
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> through = this._through
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<int> numeral_four = this._four;

                string response = string.Empty;
                await this._perceptionService.SendBluetooth4();
                response = $"{file.ToArray()[0]} {send.ToArray()[0]} {through.ToArray()[0]} {bluetooth.ToArray()[0]} {numeral_four.ToArray()[0]} .";
                return response;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> SpeakText(string language, string locution)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation speak text \"Bot\" view model failed!");

                HashSet<string> text = this._text
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> speak = this._speak
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
                throw new InvalidOperationException(this.error_message);
            }
        }

        private async Task<string> FileText(string language, string locution)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation file text \"Bot\" view model failed!");

                HashSet<string> file = this._file
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> save = this._save
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
                throw new InvalidOperationException(this.error_message);
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
                throw new InvalidOperationException(this.error_message);
            }
        }
        #endregion
    }
}
