using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IElocucaoRepository
    {
        public List<Elocucao> GetLanguage(string language);
        public List<Elocucao> GetModel(string language, string model);
    }
}
