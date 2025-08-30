using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;
using MongoDB.Driver;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class LetterViewModel : ILetterViewModel
    {
        public List<Materia> GetLessonSimple(bool lesson, string language)
        {
            try
            {
                return App.MongoDBService.Materia.Find(index => index.linguagem == language && index.licao == lesson).ToList<Materia>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
