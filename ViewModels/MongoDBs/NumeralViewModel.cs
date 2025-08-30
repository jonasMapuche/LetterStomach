using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;
using MongoDB.Driver;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class NumeralViewModel : INumeralViewModel
    {
        public List<Algarismo> GetLanguage(string language)
        {
            try
            {
                return App.MongoDBService.Algarismo.Find(index => index.linguagem == language).ToList<Algarismo>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
