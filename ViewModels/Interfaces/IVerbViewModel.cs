using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    public interface IVerbViewModel
    {
        public List<Elocucao> GetLanguage(string language);
        public List<Elocucao> GetModel(string language, string model);
    }
}
