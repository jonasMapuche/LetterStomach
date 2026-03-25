using LetterStomach.Models;

namespace LetterStomach.Repositories
{
    public interface IPreceitoRepository
    {
        public Preceito GetName(string name);
        public List<Preceito> GetLanguage(string language);
    }
}
