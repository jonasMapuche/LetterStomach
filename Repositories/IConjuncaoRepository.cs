using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IConjuncaoRepository
    {
        Task<List<Conjuncoes>> GetAll();
        Task<int> Add(List<Conjuncoes> conjunction);
        void CreateTable();
        Task<int> DeleteAll();
        Task<int> DropTable();
        Task<int> ExistAsync();
        int Exist();
    }
}
