using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    public interface IGrammarViewModel
    {
        event EventHandler<string> OnError;
        void MongoDB();
        void SQLite();
        void SetGrammar();
        List<Materia> GetLetter(string language);
        string GetSyntax(List<Word> oration);
        List<Word> MountSyntax(string language, Materia lesson, List<Materia> book);
        List<Word> MountSyntax(string language, List<Word> words, bool reverse);
        string MountOration(string language, List<Word> lessons);
    }
}
