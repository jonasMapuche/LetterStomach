namespace LetterStomach.Interfaces
{
    public interface IAudioService
    {
        void PlayAudio(string filePath);
        void StopAudio();
        event EventHandler<string> OnError;
    }
}
