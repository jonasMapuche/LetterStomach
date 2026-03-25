using LetterStomach.Models;

namespace LetterStomach.Services.Interfaces
{
    public interface IGrammarService
    {
        event EventHandler<string> OnError;
        void Init();
        void MongoDB();
        void SQLite(ISQLiteService sQLiteService);
        List<Materia> GetLetter(string language);
        List<Word> MountSyntax(string language, Materia lesson, List<Materia> book);
        List<Word> MountSyntax(string language, List<Word> terms, bool reverse);
        string MountOration(string language, List<Word> words);
    }
}
