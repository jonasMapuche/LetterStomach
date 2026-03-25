using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IModelRepository
    {
        Task<List<Model>> GetAll();
        Task<int> Add(List<Model> model);
        void CreateTable();
        Task<int> DeleteAll();
    }
}
