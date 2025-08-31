using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    public interface IGrammarViewModel
    {
        void MongoDB();
        void SQLite();
        void SetGrammar();
        List<Materia> GetLetter(string language);
        string GetOration(List<Word> oration);
        List<Word> GetOration(string language, Materia lesson, List<Materia> book);
        List<Word> GetOration(string language, List<Word> words, bool reverse);
    }
}
