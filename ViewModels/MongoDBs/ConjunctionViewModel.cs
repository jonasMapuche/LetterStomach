using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;
using MongoDB.Driver;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class ConjunctionViewModel : IConjunctionViewModel
    {
        public List<Ligacao> GetLanguage(string language)
        {
            try
            {
                return App.MongoDBService.Ligacao.Find(index => index.linguagem == language).ToList<Ligacao>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
