using FftSharp;
using LetterStomach.Helpers;
using LetterStomach.Interfaces;
using LetterStomach.Models;
using LetterStomach.Services.Interfaces;
using System.Numerics;

namespace LetterStomach.Services
{
    public class PerceptionService : IPerceptionService
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
        public List<Audio> _audios;
        private IAudioService _audio_service;
        private IRecordService _record_service;
        private ITextSpeakService _text_speak_service;
        #endregion

        #region CONSTRUCTOR
        public PerceptionService(IRecordService recordService, IAudioService audioService, ITextSpeakService textSpeakService)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation contructor \"Perception\" service failed!");
                else this.error_message = string.Empty;

                this._record_service = recordService;
                this._record_service.OnError += OnError;

                this._audio_service = audioService;
                this._audio_service.OnError += OnError;

                this._text_speak_service = textSpeakService;
                this._text_speak_service.OnError += OnError;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
            }
        }
        #endregion

        #region GPS
        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation gps \"Perception\" service failed!");

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(10));
                Location location = await Geolocation.GetLocationAsync(request);
                return location;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }
        #endregion

        #region AUDIO
        public async Task DownloadAudio()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation download audio \"Perception\" service failed!");

                Audio audios = _audios.First();
                string file_path = audios.url;
                FileStream fs = new FileStream(file_path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
                MemoryStream ms = new MemoryStream();
                await fs.CopyToAsync(ms);
                ms.Position = 0;
                using StreamContent streamContent = new StreamContent(ms);
                IHttpService httpService = new HttpService();
                await httpService.HttpPost(streamContent, file_path);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public async Task<string> UploadAudio()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation upload audio \"Perception\" service failed!");

                FileResult? result = await FilePicker.Default.PickAsync();
                if (result != null)
                {
                    Stream sourceStream = await result.OpenReadAsync();
                    string name_file = result.FileName;
                    string[] file_names = name_file.Split('.');
                    if (!(file_names.Length == 2)) return null;
                    if ((file_names[1] == "wav") || (file_names[1] == "mp3"))
                    {
                        string output_path = FilePath.SetFileName(name_file);
                        using (FileStream destinationStream = File.Create(output_path))
                        {
                            await sourceStream.CopyToAsync(destinationStream);
                        }
                        return output_path;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public void AudioFFT(double[] audioData)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation audio fft \"Perception\" service failed!");

                int sampleRate = 48_000;
                Complex[] spectrum = FFT.Forward(audioData);
                double[] psd = FFT.Power(spectrum);
                double[] freq = FFT.FrequencyScale(psd.Length, sampleRate);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public void SendRecording(string file_path)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation send recording \"Perception\" service failed!");

                Audio audio = new Audio() { url = file_path};
                if (audio != null)
                {
                    audio.name = Path.GetFileName(file_path);
                    _audios.Insert(0, audio);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public string ReceiveRecording()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation receive recording \"Perception\" service failed!");

                Audio audio = _audios.First();
                string file_path = audio.url;
                return file_path;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public void PlayAudio(string file_path)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation play audio \"Perception\" service failed!");
                this._audio_service.OnError += OnError;
                this._audio_service.PlayAudio(file_path);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public void StopAudio()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation stop audio \"Perception\" service failed!");
                this._audio_service.OnError += OnError;
                this._audio_service.StopAudio();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public void StartRecordMP3()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation start record mp3 \"Perception\" service failed!");
                this._record_service.OnError += OnError;
                this._record_service.StartRecordMP3();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public void StartRecordWav()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation start record wav \"Perception\" service failed!");
                this._record_service.StartRecordWav();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public string StopRecordMP3()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation start record mp3 \"Perception\" service failed!");
                this._record_service.OnError += OnError;
                return this._record_service.StopRecordMP3();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public string StopRecordWav()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation start record wav \"Perception\" service failed!");
                this._record_service.OnError += OnError;
                return this._record_service.StopRecordWav();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public void SpeakText(string text)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation speak text \"Perception\" service failed!");
                this._text_speak_service.OnError += OnError;
                this._text_speak_service.SpeakText(text);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }

        public string FileText(string text)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation file text \"Perception\" service failed!");
                this._text_speak_service.OnError += OnError;
                return this._text_speak_service.FileText(text);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }
        #endregion

        #region IMAGE
        public async Task SaveImage(byte[] bytes)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation save image \"Perception\" service failed!");

                string file_name = FilePath.SetFileName("jpeg");
                string file_path = FilePath.SetAudioFilePath(file_name);
                Audio audio = new Audio() { url = file_path };
                if (audio != null)
                {
                    await File.WriteAllBytesAsync(file_path, bytes);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        public async Task DownloadImage()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation download image \"Perception\" service failed!");

                Audio audios = _audios.First();
                string file_path = audios.url;
                FileStream fs = new FileStream(file_path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
                MemoryStream ms = new MemoryStream();
                await fs.CopyToAsync(ms);
                ms.Position = 0;
                using StreamContent streamContent = new StreamContent(ms);
                IHttpService httpService = new HttpService();
                await httpService.HttpPost(streamContent, file_path);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion

        #region VIBRATION
        public void SetVibration(int time)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation set vibration \"Perception\" service failed!");

                int secondsToVibrate = Random.Shared.Next(1, time);
                TimeSpan vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);

                Vibration.Default.Vibrate(vibrationLength);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion

        #region BATTERY
        public double GetCharge()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get charge \"Perception\" service failed!");

                return (Battery.ChargeLevel * 100);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return -1;
            }
        }

        public string GetMode()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get mode \"Perception\" service failed!");

                return Battery.Default.EnergySaverStatus == EnergySaverStatus.On ? "On" : Battery.Default.EnergySaverStatus == EnergySaverStatus.Off ? "Off" : "Unknown";
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return string.Empty;
            }
        }

        public BatteryState GetState()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get state \"Perception\" service failed!");

                return Battery.Default.State;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return BatteryState.Unknown;
            }
        }

        public BatteryPowerSource GetSource()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get source \"Perception\" service failed!");

                return Battery.Default.PowerSource;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return BatteryPowerSource.Unknown;
            }
        }
        #endregion

        #region BLUETOOTH LE
        #endregion

        #region BLUETOOTH CLASSIC
        #endregion
    }
}
