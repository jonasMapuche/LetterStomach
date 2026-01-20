namespace LetterStomach.Services.Interfaces
{
    public interface IPerceptionService
    {
        Task<Location> GetCurrentLocation();
        Task DownloadFile();
        Task<string> UploadFile();
        void AudioFFT(double[] audioData);
        void SendRecording(string file_path);
        string ReceiveRecording();
        double GetCharge();
        string GetMode();
        BatteryState GetState();
        BatteryPowerSource GetSource();
        void SetVibration(int time);
        Task SaveImage(byte[] bytes);
        event EventHandler<string> OnError;
        void PlayAudio(string file_path);
        void StopAudio();
        void StartRecordMP3();
        void StartRecordWav();
        string StopRecordMP3();
        string StopRecordWav();
        void SpeakText(string text);
        string FileText(string text);
    }
}
