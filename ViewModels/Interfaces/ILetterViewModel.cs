using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    public interface ILetterViewModel
    {
        public List<Materia> GetLessonSimple(bool lesson, string language);
    }
}
