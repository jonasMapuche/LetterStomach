using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;
using MongoDB.Driver;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class PrepositionViewModel : IPrepositionViewModel
    {
        public List<Juncao> GetLanguage(string language)
        {
            try
            {
                return App.MongoDBService.Juncao.Find(index => index.linguagem == language).ToList<Juncao>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
