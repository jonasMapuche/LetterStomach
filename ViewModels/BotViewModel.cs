using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        private Dictionary<string, string> VAR_OFF = SettingService.Instance.Off;
        private Dictionary<string, string> VAR_AUTO = SettingService.Instance.Auto;
        private Dictionary<string, string> VAR_ON = SettingService.Instance.On;
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

        public CancellationToken Token => CancellationToken.None;
        #endregion

        #region CONTRUCTOR
        public BotViewModel() 
        {
            try 
            {
                if (this._error_off) throw new InvalidOperationException("Operation contructor \"Bot\" view model failed!");
                else this.error_message = string.Empty;

                BackCommand = new AsyncRelayCommand(OnBackCommand);
                SendCommand = new AsyncRelayCommand<string>(OnSendCommand);

                ShowCamera = false;
                ShowPhoto = false;
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

                string language = Username.Name;
                User user = Username;
                Messages = MessageService.Instance.Messages(null, parameter, language);

                string declarative = string.Empty;
                declarative = await OnSendButton(parameter, user, language);
                if (declarative != string.Empty)
                    Messages = MessageService.Instance.Messages(Username, declarative, Username.Name);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        private async Task<string> OnSendButton(string parameter, User user, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation send button \"Bot\" view model failed!");

                HashSet<string> verbs = VAR_EXECUTE
                    .Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns = VAR_ACTIVITY
                    .Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();

                GoMessage goMessage = new GoMessage();
                goMessage.sender = user;
                goMessage.language = language;
                goMessage.message = parameter;
                Locution locution = new Locution();
                locution = await _httpService.HttpGo(goMessage);
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
                    if (verb != string.Empty)
                    {
                        foreach (Vocable item in locution.Word)
                        {
                            if ((item.Sentence == VAR_PREDICATE) && (item.Class == VAR_NOUN))
                            {
                                if (Array.IndexOf(nouns.ToArray(), item.Term) != -1)
                                {
                                    string result = string.Empty;
                                    result = await OnCommandButton(language, verb, item.Term, string.Empty, string.Empty);
                                    return result;
                                }
                            }
                        }
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        private async Task<string> OnCommandButton(string language, string verb, string noun, string adjective, string quote)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation picker command \"Bot\" view model failed!");

                string response = string.Empty;
                HashSet<string> verbs_load = new HashSet<string>();
                verbs_load = VAR_LOAD.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_view = new HashSet<string>();
                verbs_view = VAR_VIEW.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_play = new HashSet<string>();
                verbs_play = VAR_PLAY.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_record = new HashSet<string>();
                verbs_record = VAR_RECORD.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_stop = new HashSet<string>();
                verbs_stop = VAR_STOP.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_rotate = new HashSet<string>();
                verbs_rotate = VAR_ROTATE.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_speak = new HashSet<string>();
                verbs_speak = VAR_SPEAK.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_download = new HashSet<string>();
                verbs_download = VAR_DOWNLOAD.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_upload = new HashSet<string>();
                verbs_upload = VAR_UPLOAD.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_vibtate = new HashSet<string>();
                verbs_vibtate = VAR_VIBRATION.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_capture = new HashSet<string>();
                verbs_capture = VAR_CAPTURE.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> verbs_save = new HashSet<string>();
                verbs_save = VAR_SAVE.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();

                HashSet<string> nouns_gps = new HashSet<string>();
                nouns_gps = VAR_GPS.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns_camera = new HashSet<string>();
                nouns_camera = VAR_CAMERA.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns_battery = new HashSet<string>();
                nouns_battery = VAR_BATTERY.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns_file = new HashSet<string>();
                nouns_file = VAR_FILE.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns_wav = new HashSet<string>();
                nouns_wav = VAR_WAV.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns_mp3 = new HashSet<string>();
                nouns_mp3 = VAR_MP3.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns_vibration = new HashSet<string>();
                nouns_vibration = VAR_VIBRATION.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns_bluetooth = new HashSet<string>();
                nouns_bluetooth = VAR_BLUETOOTH.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns_text = new HashSet<string>();
                nouns_text = VAR_TEXT.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns_phone = new HashSet<string>();
                nouns_phone = VAR_PHONE.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> nouns_flash = new HashSet<string>();
                nouns_flash = VAR_FLASH.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();

                HashSet<string> adjective_on = new HashSet<string>();
                adjective_on = VAR_ON.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> adjective_off = new HashSet<string>();
                adjective_off = VAR_OFF.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();
                HashSet<string> adjective_auto = new HashSet<string>();
                adjective_auto = VAR_AUTO.Where(index => index.Key.Contains(language))
                    .ToDictionary(index => index.Key, index => index.Value).Values.ToHashSet();

                if (Array.IndexOf(verbs_view.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_gps.ToArray(), noun) != -1)
                    {
                        Location location = await _perceptionService.GetCurrentLocation();
                        if (location != null)
                            response = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}.";
                        else
                            response = "Not work.";
                    }
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        await this._cameraView.StartCameraPreview(Token);
                        response = "Start Camera.";
                    }
                    if (Array.IndexOf(nouns_battery.ToArray(), noun) != -1)
                    {
                        double battery = _perceptionService.GetCharge();
                        response = $"{battery.ToString()} %";
                    }
                }
                if (Array.IndexOf(verbs_load.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_bluetooth.ToArray(), noun) != -1) { }
                    if (Array.IndexOf(nouns_flash.ToArray(), noun) != -1)
                    {
                        if (Array.IndexOf(adjective_off.ToArray(), adjective) != -1)
                            await FlashCamera(adjective);
                        else
                        {
                            if (Array.IndexOf(adjective_auto.ToArray(), noun) != -1)
                                await FlashCamera(adjective);
                            if (Array.IndexOf(adjective_on.ToArray(), noun) != -1)
                                await FlashCamera(adjective);
                        }
                    }
                }
                if (Array.IndexOf(verbs_play.ToArray(), verb) != -1)
                {
                    if ((Array.IndexOf(nouns_mp3.ToArray(), noun) != -1) || 
                        (Array.IndexOf(nouns_wav.ToArray(), noun) != -1))
                    {
                        _perceptionService.StopAudio();
                        string audio_file_path = _perceptionService.ReceiveRecording();
                        _perceptionService.PlayAudio(audio_file_path);
                        if (Array.IndexOf(nouns_mp3.ToArray(), noun) != -1) response = "Play MP3.";
                        if (Array.IndexOf(nouns_wav.ToArray(), noun) != -1) response = "Play WAV.";
                    }
                }
                if (Array.IndexOf(verbs_record.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_mp3.ToArray(), noun) != -1)
                    {
                        PermissionStatus permission_status = await RequestandCheckPermission();
                        if (permission_status == PermissionStatus.Granted)
                        {
                            _perceptionService.StartRecordMP3();
                            response = "Record MP3.";
                        }
                    }
                    if (Array.IndexOf(nouns_wav.ToArray(), noun) != -1)
                    {
                        PermissionStatus permission_status = await RequestandCheckPermission();
                        if (permission_status == PermissionStatus.Granted)
                        {
                            _perceptionService.StartRecordWav();
                            response = "Record WAV.";
                        }
                    }
                }
                if (Array.IndexOf(verbs_stop.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_mp3.ToArray(), noun) != -1)
                    {
                        string audio_file_path = _perceptionService.StopRecordMP3();
                        _perceptionService.SendRecording(audio_file_path);
                        response = "Stop MP3.";
                    }
                    if (Array.IndexOf(nouns_wav.ToArray(), noun) != -1)
                    {
                        string audio_file_path = _perceptionService.StopRecordWav();
                        _perceptionService.SendRecording(audio_file_path);
                        response = "Stop WAV.";
                    }
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        this._cameraView.StopCameraPreview();
                        response = "Stop Camera.";
                    }
                }
                if (Array.IndexOf(verbs_capture.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        await this._cameraView.CaptureImage(Token);
                        response = "Capture Camera.";
                    }
                }
                if (Array.IndexOf(verbs_rotate.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        await RotateCamera();
                        response = "Rotate Camera.";
                    }
                }
                if (Array.IndexOf(verbs_vibtate.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_phone.ToArray(), noun) != -1)
                    {
                        _perceptionService.SetVibration(7);
                        response = "Vibrate Phone.";
                    }
                }
                if (Array.IndexOf(verbs_speak.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_text.ToArray(), noun) != -1)
                    {
                        string text = "Hello world!";
                        _perceptionService.SpeakText(text);
                        response = "Speak text.";
                    }
                    if (Array.IndexOf(nouns_file.ToArray(), noun) != -1)
                    {
                        string text = "Hello world!";
                        string auto_file_path = _perceptionService.FileText(text);
                        response = $"Create file '{auto_file_path}'.";
                    }
                }
                if (Array.IndexOf(verbs_download.ToArray(), verb) != -1)
                {
                    if ((Array.IndexOf(nouns_mp3.ToArray(), noun) != -1) ||
                        (Array.IndexOf(nouns_wav.ToArray(), noun) != -1))
                    {
                        await _perceptionService.DownloadAudio();
                        response = "Download Audio.";
                    }
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        await _perceptionService.DownloadImage();
                        response = "Download Image.";
                    }
                }
                if (Array.IndexOf(verbs_save.ToArray(), verb) != -1)
                {
                    if (Array.IndexOf(nouns_camera.ToArray(), noun) != -1)
                    {
                        await _perceptionService.SaveImage(Bytes);
                        response = "Save Camera.";
                    }
                }
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
                return string.Empty;
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
        private async Task FlashCamera(string kind)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation flash camera \"Bot\" view model failed!");

                if (this._cameraProvider.AvailableCameras is not null)
                {
                    switch (kind)
                    {
                        case "On":
                            this.FlashMode = CameraFlashMode.On;
                            break;
                        case "Off":
                            this.FlashMode = CameraFlashMode.Off;
                            break;
                        case "Auto":
                            this.FlashMode = CameraFlashMode.Auto;
                            break;
                    }
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
