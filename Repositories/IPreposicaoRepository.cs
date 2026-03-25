using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IPreposicaoRepository
    {
        Task<List<Preposicoes>> GetAll();
        Task<int> Add(List<Preposicoes> model);
        void CreateTable();
        Task<int> DeleteAll();
    }
}
