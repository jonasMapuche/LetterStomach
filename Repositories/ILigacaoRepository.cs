using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface ILigacaoRepository
    {
        public List<Ligacao> GetLanguage(string language);
        public Task<List<Ligacao>> GetLanguageAsync(string language);
    }
}
