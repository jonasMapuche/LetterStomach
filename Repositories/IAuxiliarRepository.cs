using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IAuxiliarRepository
    {
        Task<List<Auxiliares>> GetAll();
        Task<int> Add(List<Auxiliares> auxiliary);
        void CreateTable();
        Task<int> DeleteAll();
        Task<int> ExistAsync();
        int Exist();
    }
}
