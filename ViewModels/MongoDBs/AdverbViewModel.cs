using MongoDB.Driver;
using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class AdverbViewModel : IAdverbViewModel
    {
        public AdverbViewModel() { }

        public List<Circunstancia> GetLanguage(string language)
        {
            try
            {
                return App.MongoDBService.Circunstancia.Find(index => index.linguagem == language).ToList<Circunstancia>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Circunstancia GetName(string name)
        {
            try
            { 
                return App.MongoDBService.Circunstancia.Find(index => index.nome == name).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
