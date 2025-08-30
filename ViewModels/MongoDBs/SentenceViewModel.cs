using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;
using MongoDB.Driver;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class SentenceViewModel : ISentenceViewModel
    {
        public List<Sentenca> GetLanguage(string language)
        {
            try
            {
                return App.MongoDBService.Sentenca.Find(index => index.linguagem == language).ToList<Sentenca>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
