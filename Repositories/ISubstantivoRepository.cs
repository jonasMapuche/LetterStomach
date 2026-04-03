using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface ISubstantivoRepository
    {
        Task<List<Substantivo>> GetAll();
        Task<int> Add(List<Substantivo> noun);
        void CreateTable();
        Task<int> DeleteAll();
        Task<int> DropTable();
        Task<int> ExistAsync();
        int Exist();
    }
}
