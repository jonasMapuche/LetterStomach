using MongoDB.Driver;
using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class PronounViewModel : IPronounViewModel
    {
        public List<Estoutro> GetLanguage(string language)
        {
            try
            {
                return App.MongoDBService.Estoutro.Find(index => index.linguagem == language).ToList<Estoutro>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Estoutro GetName(string name)
        {
            try 
            { 
                return App.MongoDBService.Estoutro.Find(index => index.nome == name).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
