using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LetterStomach.Helpers;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Services.Interfaces;
using LetterStomach.Views;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Windows.Input;

namespace LetterStomach.ViewModels
{
    [QueryProperty(nameof(username), "Username")]
    public partial class BotViewModel : ObservableObject, IQueryAttributable
    {
        #region ERROR
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
        #endregion

        #region CONSTANTS
        private string VAR_DECLARATIVE = SettingService.Instance.Declarative;
        private string VAR_VERB = SettingService.Instance.Verb;
        private string VAR_NOUN = SettingService.Instance.Noun;
        private string VAR_PREDICATE = SettingService.Instance.Predicate;
        private HashSet<string> VAR_LOAD = SettingService.Instance.Load;
        private HashSet<string> VAR_EXECUTE = SettingService.Instance.Execute;
        private HashSet<string> VAR_VIEW = SettingService.Instance.View;
        private HashSet<string> VAR_PLAY = SettingService.Instance.Play;
        private HashSet<string> VAR_ACTIVITY = SettingService.Instance.Activity;
        private HashSet<string> VAR_RECORD = SettingService.Instance.Record;
        private HashSet<string> VAR_STOP = SettingService.Instance.Stop;
        private HashSet<string> VAR_ROTATE = SettingService.Instance.Rotate;
        private HashSet<string> VAR_SPEAK = SettingService.Instance.Speak;
        private HashSet<string> VAR_DOWNLOAD = SettingService.Instance.Download;
        private HashSet<string> VAR_UPLOAD = SettingService.Instance.Upload;
        private HashSet<string> VAR_CHARGE = SettingService.Instance.Charge;
        private HashSet<string> VAR_VIBRATE = SettingService.Instance.Vibrate;
        private HashSet<string> VAR_CAPTURE = SettingService.Instance.Capture;
        private HashSet<string> VAR_SAVE = SettingService.Instance.Save;
        private string VAR_GPS = "gps";
        private string VAR_BLUETOOTH = "bluetooth";
        private string VAR_CAMERA = "camera";
        private string VAR_WAV = "wav";
        private string VAR_MP3 = "mp3";
        private string VAR_BATTERY = "battery";
        private string VAR_FILE = "file";
        private string VAR_TEXT = "text";
        private string VAR_PHONE = "phone";
        private string VAR_FLASH = "flash";
        private string VAR_OFF = "off";
        private string VAR_AUTO = "auto";
        private string VAR_ON = "on";
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
                BackCommand = new AsyncRelayCommand(OnBackCommand);
                SendCommand = new AsyncRelayCommand<string>(OnSendCommand);

                ShowCamera = false;
                ShowPhoto = false;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion

        #region COMMAND
        private async Task OnSendCommand(string parameter)
        {
            try
            {
                Messages = MessageService.Instance.Messages(null, parameter, Username.Name);
                /*
                string declarative = string.Empty;
                declarative = await OnSendButton(parameter);
                if (declarative != string.Empty)
                    Messages = MessageService.Instance.Messages(Username, declarative, Username.Name);
                */
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private async Task<string> OnSendButton(string parameter)
        {
            try
            {
                HashSet<string> verbs = new HashSet<string>();
                verbs = VAR_EXECUTE;

                HashSet<string> nouns = new HashSet<string>();
                nouns = VAR_ACTIVITY;

                GoMessage goMessage = new GoMessage();
                goMessage.id = 1;
                goMessage.sender = 2;
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
                                    result = await OnCommandButton(verb, item.Term, string.Empty, string.Empty);
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
                OnError?.Invoke(this, error_message);
                return string.Empty;
            }
        }

        private async Task<string> OnCommandButton(string verb, string noun, string adjective, string quote)
        {
            try
            {
                string response = string.Empty;
                HashSet<string> verbs_load = new HashSet<string>();
                verbs_load = VAR_LOAD;
                HashSet<string> verbs_view = new HashSet<string>();
                verbs_view = VAR_VIEW;
                HashSet<string> verbs_play = new HashSet<string>();
                verbs_play = VAR_PLAY;
                HashSet<string> verbs_record = new HashSet<string>();
                verbs_record = VAR_RECORD;
                HashSet<string> verbs_stop = new HashSet<string>();
                verbs_stop = VAR_STOP;
                HashSet<string> verbs_rotate = new HashSet<string>();
                verbs_rotate = VAR_ROTATE;
                HashSet<string> verbs_speak = new HashSet<string>();
                verbs_speak = VAR_SPEAK;
                HashSet<string> verbs_download = new HashSet<string>();
                verbs_download = VAR_DOWNLOAD;
                HashSet<string> verbs_upload = new HashSet<string>();
                verbs_upload = VAR_UPLOAD;
                HashSet<string> verbs_charge = new HashSet<string>();
                verbs_charge = VAR_CHARGE;
                HashSet<string> verbs_vibtate = new HashSet<string>();
                verbs_vibtate = VAR_VIBRATE;
                HashSet<string> verbs_capture = new HashSet<string>();
                verbs_capture = VAR_CAPTURE;
                HashSet<string> verbs_save = new HashSet<string>();
                verbs_save = VAR_SAVE;

                if (Array.IndexOf(verbs_view.ToArray(), verb) != -1)
                {
                    if (noun == VAR_GPS) 
                    {
                        Location location = await _perceptionService.GetCurrentLocation();
                        if (location != null)
                            response = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}.";
                        else
                            response = "Not work.";
                    }
                    if (noun == VAR_CAMERA) 
                    {
                        await this._cameraView.StartCameraPreview(Token);
                        response = "Start Camera.";
                    }
                }
                if (Array.IndexOf(verbs_charge.ToArray(), verb) != -1)
                {
                    if (noun == VAR_BATTERY)
                    {
                        double battery = _perceptionService.GetCharge();
                        response = $"{battery.ToString()} %";
                    }
                }
                if (Array.IndexOf(verbs_load.ToArray(), verb) != -1)
                {
                    if (noun == VAR_BLUETOOTH) { }
                    if (noun == VAR_FLASH)
                    {
                        if (adjective == VAR_OFF)
                            await FlashCamera(VAR_OFF);
                        else
                        {
                            if (adjective == VAR_AUTO)
                                await FlashCamera(VAR_AUTO);
                            else
                                await FlashCamera(VAR_ON);
                        }
                    }
                }
                if (Array.IndexOf(verbs_play.ToArray(), verb) != -1)
                {
                    if ((noun == VAR_MP3) || (noun == VAR_WAV))
                    {
                        _perceptionService.StopAudio();
                        string audio_file_path = _perceptionService.ReceiveRecording();
                        _perceptionService.PlayAudio(audio_file_path);
                        if (noun == VAR_MP3) response = "Play MP3.";
                        if (noun == VAR_WAV) response = "Play WAV.";
                    }
                }
                if (Array.IndexOf(verbs_record.ToArray(), verb) != -1)
                {
                    if (noun == VAR_MP3) 
                    {
                        PermissionStatus permission_status = await RequestandCheckPermission();
                        if (permission_status == PermissionStatus.Granted)
                        {
                            _perceptionService.StartRecordMP3();
                            response = "Record MP3.";
                        }
                    }
                    if (noun == VAR_WAV) 
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
                    if (noun == VAR_MP3) 
                    {
                        string audio_file_path = _perceptionService.StopRecordMP3();
                        _perceptionService.SendRecording(audio_file_path);
                        response = "Stop MP3.";
                    }
                    if (noun == VAR_WAV) 
                    {
                        string audio_file_path = _perceptionService.StopRecordWav();
                        _perceptionService.SendRecording(audio_file_path);
                        response = "Stop WAV.";
                    }
                    if (noun == VAR_CAMERA) 
                    {
                        this._cameraView.StopCameraPreview();
                        response = "Stop Camera.";
                    }
                }
                if (Array.IndexOf(verbs_capture.ToArray(), verb) != -1)
                {
                    if (noun == VAR_CAMERA)
                    {
                        await this._cameraView.CaptureImage(Token);
                        response = "Capture Camera.";
                    }
                }
                if (Array.IndexOf(verbs_rotate.ToArray(), verb) != -1)
                {
                    if (noun == VAR_CAMERA) 
                    {
                        await RotateCamera();
                        response = "Rotate Camera.";
                    }
                }
                if (Array.IndexOf(verbs_vibtate.ToArray(), verb) != -1)
                {
                    if (noun == VAR_PHONE)
                    {
                        _perceptionService.SetVibration(7);
                        response = "Vibrate Phone.";
                    }
                }
                if (Array.IndexOf(verbs_speak.ToArray(), verb) != -1)
                {
                    if (noun == VAR_TEXT) 
                    {
                        string text = "Hello world!";
                        _perceptionService.SpeakText(text);
                        response = "Speak text.";
                    }
                    if (noun == VAR_FILE) 
                    {
                        string text = "Hello world!";
                        string auto_file_path = _perceptionService.FileText(text);
                        response = $"Create file '{auto_file_path}'.";
                    }
                }
                if (Array.IndexOf(verbs_download.ToArray(), verb) != -1)
                {
                    if ((noun == VAR_MP3) || (noun == VAR_WAV))
                    {
                        await _perceptionService.DownloadAudio();
                        response = "Download Audio.";
                    }
                    if (noun == VAR_CAMERA) 
                    {
                        await _perceptionService.DownloadImage();
                        response = "Download Image.";
                    }
                }
                if (Array.IndexOf(verbs_save.ToArray(), verb) != -1)
                {
                    if (noun == VAR_CAMERA)
                    {
                        await _perceptionService.SaveImage(Bytes);
                        response = "Save Camera.";
                    }
                }
                if (Array.IndexOf(verbs_upload.ToArray(), verb) != -1)
                {
                    if ((noun == VAR_MP3) || (noun == VAR_WAV))
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
                OnError?.Invoke(this, error_message);
                return string.Empty;
            }
        }

        private async Task OnBackCommand()
        {
            try
            { 
                await Shell.Current.GoToAsync($"//{nameof(HomeView)}");
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
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion

        #region BUTTON
        private async Task FlashCamera(string kind)
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
            }
        }

        private async Task RotateCamera()
        {
            try
            { 
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
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion

        #region PERMISSION
        public async Task<PermissionStatus> RequestandCheckPermission()
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
                return PermissionStatus.Denied;
            }
        }
        #endregion
    }
}
