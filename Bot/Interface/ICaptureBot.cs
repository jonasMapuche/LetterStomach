using LetterStomach.Models;

namespace LetterStomach.Bot.Interface
{
    public interface ICaptureBot
    {
        Task<string> Flash(string language);
        Task<string> Load(string language, List<Message> messages);
        Task<string> Mount(string language, string parameter);
        event EventHandler<string> OnError;
    }
}
