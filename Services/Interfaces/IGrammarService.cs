using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface IGrammarService
    {
        event EventHandler<string> OnError;
        void Init();
        Task InitAsync();
        Task InitAsync(string language);
        void MongoDB(IMongoDBService mongoDBService);
        void SQLite(ISQLiteService sQLiteService);
        Task SQLiteAsync(ISQLiteService sQLiteService);
        List<Materia> GetLetter(string language);
        Task<List<Materia>> GetLetterAsync(string language);
        List<Word> Syntax(string language, Materia lesson, List<Materia> book);
        List<Word> Syntax(string language, List<Word> terms, bool reverse);
        string Oration(List<Word> words);
    }
}
