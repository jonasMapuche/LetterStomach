using LetterStomach.Interfaces;

namespace LetterStomach.Services.Interfaces
{
    public interface IPerceptionService: IRecordService, IAudioService, ITextSpeakService
    {
        Task<Location> GetCurrentLocation();
        Task DownloadAudio();
        Task<string> UploadAudio();
        void AudioFFT(double[] audioData);
        void SendRecording(string file_path);
        string ReceiveRecording();
        double GetCharge();
        string GetMode();
        BatteryState GetState();
        BatteryPowerSource GetSource();
        void SetVibration(int time);
        Task SaveImage(byte[] bytes);
        Task DownloadImage();
    }
}
