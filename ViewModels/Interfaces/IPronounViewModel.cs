using LetterStomach.Models;

namespace LetterStomach.ViewModels.Interfaces
{
    public interface IPronounViewModel
    {
        public Estoutro GetName(string name);
        public List<Estoutro> GetLanguage(string language);
    }
}
