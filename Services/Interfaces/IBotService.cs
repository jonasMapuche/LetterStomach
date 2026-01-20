using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface IBotService
    {
        event EventHandler<string> OnError;
        Task<string> CaptureCamera(string language);
        Task<string> CaptureCamera(string language, List<Message> messages);
        Task<string> CaptureCamera(string language, string parameter, List<Message> messages);
        Task<string> RecordAudio(string language);
        Task<string> RecordAudio(string language, List<Message> messages);
        Task<string> RecordAudio(string language, string parameter, List<Message> messages);
        Task<string> ShareFile(string language);
        Task<string> ShareFile(string language, List<Message> messages);
        Task<string> ShareFile(string language, string parameter);
        Task<string> Terminate(string language, List<Message> messages);
    }
}
