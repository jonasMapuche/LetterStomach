using LetterStomach.Models;

namespace LetterStomach.Bot.Interface
{
    public interface IRecordBot
    {
        Task<string> Audio(string language);
        Task<string> Stop(string language);
        Task<string> Choose(string language, List<Message> messages);
        Task<List<string>> Load(string language, string parameter, List<Message> messages);
        event EventHandler<string> OnError;
    }
}
