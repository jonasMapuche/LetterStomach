using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface ICircunstanciaRepository
    {
        public Circunstancia GetName(string name);
        public List<Circunstancia> GetLanguage(string language);
        public Task<List<Circunstancia>> GetLanguageAsync(string language);
    }
}
