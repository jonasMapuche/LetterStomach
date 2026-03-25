using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IDitadoRepository
    {
        Task<List<Sentencas>> GetAll();
        Task<int> Add(List<Sentencas> sentence);
        void CreateTable();
        Task<int> DeleteAll();
    }
}
