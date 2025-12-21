using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface IBotService
    {
        event EventHandler<string> OnError;
        Task<string> Init(string language);
        Task<string> Load(string language, List<Message> messages);
        Task<string> Capture(string language, string parameter);
    }
}
