using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IArtigoRepository
    {
        Task<List<Artigos>> GetAll();
        Task<int> Add(List<Artigos> article);
        void CreateTable();
        Task<int> DeleteAll();
        Task<int> DropTable();
        Task<int> ExistAsync();
        int Exist();
    }
}
