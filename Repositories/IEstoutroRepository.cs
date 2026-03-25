using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IEstoutroRepository
    {
        public Estoutro GetName(string name);
        public List<Estoutro> GetLanguage(string language);
    }
}
