using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IMateriaRepository
    {
        public List<Materia> GetLessonSimple(bool lesson, string language);
        public Task<List<Materia>> GetLessonSimpleAsync(bool lesson, string language);
    }
}
