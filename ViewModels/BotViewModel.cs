using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LetterStomach.Interfaces;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using LetterStomach.Views;
using MongoDB.Driver;
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

        private Dictionary<string, string> VAR_GPS = SettingService.Instance.GPS;
        private Dictionary<string, string> VAR_BLUETOOTH = SettingService.Instance.Bluetooth;
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

        private Dictionary<string, string> VAR_FRONT = SettingService.Instance.Front;
        private Dictionary<string, string> VAR_REAR = SettingService.Instance.Rear;

        private Dictionary<string, string> VAR_OFF = SettingService.Instance.Off;
        private Dictionary<string, string> VAR_AUTO = SettingService.Instance.Auto;
        private Dictionary<string, string> VAR_ON = SettingService.Instance.On;

        private Dictionary<string, string> VAR_CATCH = SettingService.Instance.Catch;
        private Dictionary<string, string> VAR_CATCH_CAMERA = SettingService.Instance.Catch_Camera;
        private Dictionary<string, string> VAR_CATCH_RECORD = SettingService.Instance.Catch_Record;
        #endregion

        #region VARIABLE
        private string PORTUGUES = "português";

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

        private async Task<string> DecisionMessage(string language, Locution locution)
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

                string result = string.Empty;
                if ((locution != null) && (locution.Kind == VAR_DECLARATIVE))
                {
                    string verb = string.Empty;
                    foreach (Vocable item in locution.Word)
                    {
                        if (item.Class == VAR_VERB)
                        {
                            if (Array.IndexOf(verbs.ToArray(), item.Term) != -1)
                                verb = item.Term;
                        }
                    }
                    string noun = string.Empty;
                    foreach (Vocable item in locution.Word)
                    {
                        if ((item.Sentence == VAR_PREDICATE) && (item.Class == VAR_NOUN))
                        {
                            if (Array.IndexOf(nouns.ToArray(), item.Term) != -1)
                                noun = item.Term;
                        }
                    }
                    string adjective = string.Empty;
                    foreach (Vocable item in locution.Word)
                    {
                        if ((item.Sentence == VAR_PREDICATE) && (item.Class == VAR_ADJECTIVE))
                        {
                            if (Array.IndexOf(adjectives.ToArray(), item.Term) != -1)
                                adjective = item.Term;
                        }
                    }
                    if ((verb != string.Empty) && (noun != string.Empty))
                        result = await OnCommandButton(language, verb, noun, adjective);
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

        private async Task<string> LoadMessage(string parameter, User user, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load message \"Bot\" view model failed!");

                List<Locution> locutions = new List<Locution>();
                locutions = await GoMessage(parameter, user, language);
                Locution locution = new Locution();
                locution = locutions[0];

                string declarative = string.Empty;
                declarative = await DecisionMessage(language, locution);

                return declarative;
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
                HashSet<string> cameras = VAR_CATCH_CAMERA
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();
                HashSet<string> records = VAR_CATCH_RECORD
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                HashSet<string> terminates = VAR_TERMINATE
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                string result = string.Empty;
                if (Array.IndexOf(catchs.ToArray(), parameter) != -1)
                {
                    string mount = string.Empty;
                    List<Message> messages = new List<Message>();
                    messages = MessageService.Instance.Bots(language);

                    if (Array.IndexOf(cameras.ToArray(), parameter) != -1)
                        mount = await this._botService.CaptureCamera(language, parameter, messages);

                    if (Array.IndexOf(records.ToArray(), parameter) != -1)
                        mount = await this._botService.RecordAudio(language, parameter, messages);

                    string declarative = string.Empty;
                    if (mount != string.Empty)
                        declarative = await LoadMessage(mount, user, language);

                    if (Array.IndexOf(terminates.ToArray(), parameter) != -1)
                        result = await this._botService.Terminate(language, messages);

                    messages = new List<Message>();
                    if (declarative != string.Empty)
                    {
                        messages = MessageService.Instance.Bots(user, declarative, language);
                        Messages = MessageService.Instance.Messages(user, declarative, language);
                    }

                    if (Array.IndexOf(cameras.ToArray(), parameter) != -1)
                        result = await this._botService.CaptureCamera(language, messages);
                    if (Array.IndexOf(records.ToArray(), parameter) != -1)
                        result = await this._botService.RecordAudio(language, messages);

                    if (result != string.Empty)
                    {
                        MessageService.Instance.Bots(user, result, language);
                        Messages = MessageService.Instance.Messages(user, result, language);

                        string load = string.Empty;
                        load = await LoadMessage(result, user, language);
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
                Locution locution = new Locution();
                locution = locutions[0];

                string result = string.Empty;
                result = await DecisionMessage(language, locution);

                return result;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> OnCommandButton(string language, string verb, string noun, string adjective)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation command button \"Bot\" view model failed!");

                string response = string.Empty;
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
                        PermissionStatus permission_status = await RequestandCheckPermission();
                        if (permission_status == PermissionStatus.Granted)
                        {
                            //this._perceptionService.StartRecordMP3();
                            response = verb + " " + noun + ".";
                            return response;
                        }
                    }
                }
                if (Array.IndexOf(verbs_record.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_wav.ToArray(), noun) != -1)
                    {
                        PermissionStatus permission_status = await RequestandCheckPermission();
                        if (permission_status == PermissionStatus.Granted)
                        {
                            //this._perceptionService.StartRecordWav();
                            response = verb + " " + noun + ".";
                            return response;
                        }
                    }
                }
                //-----
                if (Array.IndexOf(verbs_stop.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_mp3.ToArray(), noun) != -1)
                    {
                        //string audio_file_path = this._perceptionService.StopRecordMP3();
                        //_perceptionService.SendRecording(audio_file_path);
                        response = verb + " " + noun + ".";
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_stop.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_wav.ToArray(), noun) != -1)
                    {
                        //string audio_file_path = this._perceptionService.StopRecordWav();
                        //_perceptionService.SendRecording(audio_file_path);
                        response = verb + " " + noun + ".";
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_play.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_wav.ToArray(), noun) != -1) 
                    {
                        //this._perceptionService.StopAudio();
                        //string audio_file_path = _perceptionService.ReceiveRecording();
                        //this._perceptionService.PlayAudio(audio_file_path);
                        response = verb + " " + noun + ".";
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_play.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_mp3.ToArray(), noun) != -1)
                    {
                        //this._perceptionService.StopAudio();
                        //string audio_file_path = _perceptionService.ReceiveRecording();
                        //this._perceptionService.PlayAudio(audio_file_path);
                        response = verb + " " + noun + ".";
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_terminate.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bot.ToArray(), noun) != -1)
                    {
                        string result = verb + " " + noun + ".";
                        response = await EndBot(result, language);
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
                            //await RotateCamera();
                            string result = verb + " " + noun + " " + adjective + ".";
                            return result;
                        }
                    }
                }
                if (Array.IndexOf(verbs_rotate.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(adjective_rear.ToArray(), adjective) != -1)
                        {
                            //await RotateCamera();
                            string result = verb + " " + noun + " " + adjective + ".";
                            return result;
                        }
                    }
                }
                //-----
                if (Array.IndexOf(verbs_turn_on.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_flash.ToArray(), noun) != -1)
                    {
                        //await FlashCamera(noun, language);
                        string result = verb + " " + noun + ".";
                        return result;
                    }
                }
                if (Array.IndexOf(verbs_turn.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_flash.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(adjective_off.ToArray(), adjective) != -1)
                        {
                            //await FlashCamera(adjective, language);
                            string result = verb + " " + noun + " " + adjective + ".";
                            return result;
                        }
                    }
                }
                if (Array.IndexOf(verbs_turn.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_flash.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(adjective_auto.ToArray(), adjective) != -1)
                        {
                            //await FlashCamera(adjective, language);
                            string result = verb + " " + noun + " " + adjective + ".";
                            return result;
                        }
                    }
                }
                //-----
                if (Array.IndexOf(verbs_capture.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        //await this._cameraView.CaptureImage(Token);
                        string result = verb + " " + noun + ".";
                        return result;
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
                if (Array.IndexOf(verbs_view.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_gps.ToArray(), noun) != -1)
                    {
                        Location location = await _perceptionService.GetCurrentLocation();
                        if (location != null)
                            response = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}.";
                        else
                            response = "Not work.";
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_view.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        await this._cameraView.StartCameraPreview(Token);
                        response = "Start Camera.";
                        return response;
                    }
                }
                if (Array.IndexOf(verbs_view.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_battery.ToArray(), noun) != -1)
                    {
                        double battery = _perceptionService.GetCharge();
                        response = $"{battery.ToString()} %";
                        return response;
                    }
                }
                //-----
                if (Array.IndexOf(verbs_stop.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        this._cameraView.StopCameraPreview();
                        response = "Stop Camera.";
                    }
                }
                //-----
                if (Array.IndexOf(verbs_vibtate.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_phone.ToArray(), noun) != -1)
                    {
                        _perceptionService.SetVibration(7);
                        response = "Vibrate Phone.";
                    }
                }
                //-----
                if (Array.IndexOf(verbs_speak.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_text.ToArray(), noun) != -1)
                    {
                        string text = "Hello world!";
                        this._perceptionService.SpeakText(text);
                        response = "Speak text.";
                    }
                }
                if (Array.IndexOf(verbs_speak.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_file.ToArray(), noun) != -1)
                    {
                        string text = "Hello world!";
                        string auto_file_path = this._perceptionService.FileText(text);
                        response = $"Create file '{auto_file_path}'.";
                    }
                }
                //-----
                if (Array.IndexOf(verbs_download.ToArray(), verb) != -1)
                {
                    if ((Array.IndexOf(nouns_mp3.ToArray(), noun) != -1) ||
                        (Array.IndexOf(nouns_wav.ToArray(), noun) != -1))
                    {
                        await _perceptionService.DownloadAudio();
                        response = "Download Audio.";
                    }
                }
                if (Array.IndexOf(verbs_download.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        await _perceptionService.DownloadImage();
                        response = "Download Image.";
                    }
                }
                //-----
                if (Array.IndexOf(verbs_save.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        await _perceptionService.SaveImage(Bytes);
                        response = "Save Camera.";
                    }
                }
                //-----
                if (Array.IndexOf(verbs_upload.ToArray(), verb) != -1)
                {
                    if ((Array.IndexOf(nouns_mp3.ToArray(), noun) != -1) ||
                        (Array.IndexOf(nouns_wav.ToArray(), noun) != -1))
                    {
                        string audio_file_path = await _perceptionService.UploadAudio();
                        _perceptionService.SendRecording(audio_file_path);
                        response = "Upload Audio.";
                    }
                }
                //-----
                if (Array.IndexOf(verbs_load.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bluetooth.ToArray(), noun) != -1) { }
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

        private async Task OnBackCommand()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation back command \"Bot\" view model failed!");

                await Shell.Current.GoToAsync($"//{nameof(HomeView)}");
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
                    Username = MessageService.Instance.GetUser(PORTUGUES);
                }
                List<Message> message_language = MessageService.Instance.Messages(Username.Name);
                if (message_language.Count > 0)
                {
                    Messages = message_language;
                }
                else
                {
                    if (Username.Name != PORTUGUES)
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

        #region BUTTON
        private async Task FlashCamera(string kind, string language)
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

                HashSet<string> auto = VAR_AUTO
                    .Where(index => index.Value.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Keys.ToHashSet();

                if (this._cameraProvider.AvailableCameras is not null)
                {
                    if (flash.ToArray()[0] == kind)
                        this.FlashMode = CameraFlashMode.On;
                    if (off.ToArray()[0] == kind)
                        this.FlashMode = CameraFlashMode.Off;
                    if (auto.ToArray()[0] == kind)
                        this.FlashMode = CameraFlashMode.Auto;
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        private async Task RotateCamera()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation rotate camera \"Bot\" view model failed!");

                if (this._cameraProvider.AvailableCameras is not null)
                {
                    if (SelectedCamera.DeviceId == this._cameraProvider.AvailableCameras[0].DeviceId)
                        SelectedCamera = this._cameraProvider.AvailableCameras.First(x => x.Position == CameraPosition.Front);
                    else if (SelectedCamera.DeviceId == this._cameraProvider.AvailableCameras[1].DeviceId)
                        SelectedCamera = this._cameraProvider.AvailableCameras.First(x => x.Position == CameraPosition.Rear);
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

        private async Task<string> EndBot(string response, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation end bot \"Bot\" view model failed!");

                if (SettingService.Instance.ModeBot) SettingService.Instance.ModeBot = false;
                MessageService.Instance.Remove(language);
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
