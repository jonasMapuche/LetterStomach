using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IAlgarismoRepository
    {
        public List<Algarismo> GetLanguage(string language);
        public Task<List<Algarismo>> GetLanguageAsync(string language);
    }
}
