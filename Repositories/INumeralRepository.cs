using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface INumeralRepository
    {
        Task<List<Numerais>> GetAll();
        Task<int> Add(List<Numerais> numeral);
        void CreateTable();
        Task<int> DeleteAll();
    }
}
