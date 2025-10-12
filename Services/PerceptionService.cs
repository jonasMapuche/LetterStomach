using FftSharp;
using LetterStomach.Helpers;
using LetterStomach.Models;
using LetterStomach.Services.Interfaces;
using SceneKit;
using System.Collections.ObjectModel;
using System.Numerics;

namespace LetterStomach.Services
{
    public class PerceptionService
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

        #region VARIABLE
        public List<Audio> _audios;
        #endregion

        #region GPS
        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(10));
                Location location = await Geolocation.GetLocationAsync(request);
                return location;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
        #endregion

        #region AUDIO
        public async Task DownloadAudio()
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
            }
        }

        public async Task<string> UploadAudio()
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public void AudioFFT(double[] audioData)
        {
            try
            {
                int sampleRate = 48_000;
                Complex[] spectrum = FFT.Forward(audioData);
                double[] psd = FFT.Power(spectrum);
                double[] freq = FFT.FrequencyScale(psd.Length, sampleRate);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
            }
        }

        public void SendRecording(string file_path)
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
            }
        }

        public string ReceiveRecording()
        {
            try
            {
                Audio audio = _audios.First();
                string file_path = audio.url;
                return file_path;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return string.Empty;
            }
        }
        #endregion

        #region IMAGE
        public async Task SaveImage(byte[] bytes)
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
            }
        }
        public async Task DownloadImage()
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
            }
        }
        #endregion

        #region VIBRATION
        public void SetVibration(int time)
        {
            int secondsToVibrate = Random.Shared.Next(1, time);
            TimeSpan vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);

            Vibration.Default.Vibrate(vibrationLength);
        }
        #endregion

        #region BATTERY
        public double GetCharge()
        {
            return (Battery.ChargeLevel * 100);
        }

        public string GetMode()
        {
            return Battery.Default.EnergySaverStatus == EnergySaverStatus.On ? "On" : Battery.Default.EnergySaverStatus == EnergySaverStatus.Off ? "Off" : "Unknown";
        }

        public BatteryState GetState()
        {
            return Battery.Default.State;
        }

        public BatteryPowerSource GetSource()
        {
            return Battery.Default.PowerSource;
        }
        #endregion

        #region BLUETOOTH LE
        #endregion

        #region BLUETOOTH CLASSIC
        #endregion
    }
}
