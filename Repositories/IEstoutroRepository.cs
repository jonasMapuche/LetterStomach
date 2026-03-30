using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IEstoutroRepository
    {
        public Estoutro GetName(string name);
        public Task<Estoutro> GetNameAsync(string name);
        public List<Estoutro> GetLanguage(string language);
        public Task<List<Estoutro>> GetLanguageAsync(string language);
    }
}
