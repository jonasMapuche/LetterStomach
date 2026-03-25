using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IVerboRepository
    {
        Task<List<Verbos>> GetAll();
        Task<int> Add(List<Verbos> verb);
        void CreateTable();
        Task<int> DeleteAll();
    }
}
