using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    public interface IArticleViewModel
    {
        public Preceito GetName(string name);
        public List<Preceito> GetLanguage(string language);
    }
}
