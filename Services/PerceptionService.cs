using FftSharp;
using LetterStomach.Helpers;
using LetterStomach.Services.Interfaces;
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
        public async Task DownloadAudio(string file_path)
        {
            try
            {
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
                FileResult result = await FilePicker.Default.PickAsync();
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
        #endregion
    }
}
