using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    public interface IAdverbViewModel
    {
        public Circunstancia GetName(string name);
        public List<Circunstancia> GetLanguage(string language);
    }
}
