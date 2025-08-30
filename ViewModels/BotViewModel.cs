using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.Views;
using System.Windows.Input;

namespace LetterStomach.ViewModels
{
    [QueryProperty(nameof(username), "Username")]
    public partial class BotViewModel : ObservableObject, IQueryAttributable
    {
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

        ICameraProvider _cameraProvider;

        public ICameraProvider MediaCamera {  get => _cameraProvider; set => _cameraProvider = value; }

        public BotViewModel() 
        {
            try 
            { 
                BackCommand = new AsyncRelayCommand(OnBackCommand);
                SendCommand = new AsyncRelayCommand<string>(OnSendCommand);
                MessageService message_service = new MessageService();
                Username = message_service.GetUser(PORTUGUES);
                if (HomeViewModel.Messages.Count > 0)
                {
                    Messages = message_service.GetMessages(Username);
                    HomeViewModel.Messages.ForEach(value =>
                    {
                        Messages.Add(value); 
                    });
                } else
                {
                    Messages = message_service.GetMessages(Username);
                };
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        private async Task OnSendCommand(string parameter)
        {
            try
            { 
                Message new_message = new Message()
                {
                    Sender = null,
                    Time = "18:32",
                    Text = parameter
                };
                Messages.Add(new_message);
                HomeViewModel.Messages.Add(new_message);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
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
                MessageService message_service = new MessageService();
                if (Username == null)
                {
                    Username = message_service.GetUser(PORTUGUES);
                }
                if (HomeViewModel.Messages.Count > 0)
                {
                    Messages = message_service.GetMessages(Username);
                    HomeViewModel.Messages.ForEach(value =>
                    {
                        Messages.Add(value);
                    });
                }
                else
                {
                    Messages = message_service.GetMessages(Username);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

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
    }
}
