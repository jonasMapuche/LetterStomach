using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IJuncaoRepository
    {
        public List<Juncao> GetLanguage(string language);
    }
}
