using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;
using MongoDB.Driver;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class VerbViewModel : IVerbViewModel
    {
        public List<Elocucao> GetLanguage(string language)
        {
            try
            {
                return App.MongoDBService.Elocucao.Find(index => index.linguagem == language).ToList<Elocucao>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Elocucao> GetModel(string language, string model)
        {
            try
            {
                return App.MongoDBService.Elocucao.Find(index => index.linguagem == language && index.modelo == model).ToList<Elocucao>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
