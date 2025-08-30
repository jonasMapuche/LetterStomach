using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;
using MongoDB.Driver;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class AuxiliaryViewModel : IAuxiliaryViewModel
    {
        public List<Assistente> GetLanguage(string language)
        {
            try
            {
                return App.MongoDBService.Assistente.Find(index => index.linguagem == language).ToList<Assistente>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
