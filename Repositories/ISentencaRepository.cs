using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface ISentencaRepository
    {
        public List<Sentenca> GetLanguage(string language);
    }
}
