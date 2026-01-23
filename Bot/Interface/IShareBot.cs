using LetterStomach.Models;

namespace LetterStomach.Bot.Interface
{
    public interface IShareBot
    {
        Task<string> Share(string language);
        Task<List<string>> Load(string language, string parameter, List<Message> messages);
        Task<string> Choose(string language, List<Message> messages);
        event EventHandler<string> OnError;
        List<string> ShareScan { get; set; }
        string ShareDevice { get; set; }
        Task<string> FindDevice(string language, List<Message> messages, string parameter);
    }
}
