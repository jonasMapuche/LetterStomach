using MongoDB.Driver;
using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class ArticleViewModel : IArticleViewModel
    {
        public Preceito GetName(string name)
        {
            try
            { 
                return App.MongoDBService.Preceito.Find(index => index.nome == name).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Preceito> GetLanguage(string language)
        {
            try
            { 
                return App.MongoDBService.Preceito.Find(index => index.linguagem == language).ToList<Preceito>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
