using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IAdverbioRepository
    {
        Task<List<Adverbios>> GetAll();
        Task<int> Add(List<Adverbios> adverb);
        void CreateTable();
        Task<int> DeleteAll();
        Task<int> DropTable();
        Task<int> ExistAsync();
        int Exist();
    }
}
