using LetterStomach.Models;

namespace LetterStomach.Bot.Interface
{
    public interface ICaptureBot
    {
        Task<string> Rotate(string language);
        Task<string> Capture(string language);
        Task<string> Choose(string language, List<Message> messages);
        Task<List<string>> Load(string language, string parameter, List<Message> messages);
        event EventHandler<string> OnError;
    }
}
