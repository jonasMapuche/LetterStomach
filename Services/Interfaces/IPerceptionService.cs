namespace LetterStomach.Services.Interfaces
{
    public interface IPerceptionService
    {
        Task<Location> GetCurrentLocation();
        Task DownloadAudio(string file_path);
        Task<string> UploadAudio();
        void AudioFFT(double[] audioData);
    }
}
