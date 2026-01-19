namespace LetterStomach.Bot.Interface
{
    public interface IShareBot
    {
        Task<string> Share(string language);
        event EventHandler<string> OnError;
    }
}
