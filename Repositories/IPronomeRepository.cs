using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IPronomeRepository
    {
        Task<List<Pronomes>> GetAll();
        Task<int> Add(List<Pronomes> pronoun);
        void CreateTable();
        Task<int> DeleteAll();
        Task<int> DropTable();
        Task<int> ExistAsync();
        int Exist();
    }
}
