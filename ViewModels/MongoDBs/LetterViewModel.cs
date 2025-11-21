using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;
using MongoDB.Driver;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class LetterViewModel : ILetterViewModel
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string _error_message;

        public string error_message
        {
            get => this._error_message;
            set
            {
                this._error_message = value;
            }
        }

        public event EventHandler<string> OnError;
        #endregion

        #region GET
        public List<Materia> GetLessonSimple(bool lesson, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get lesson simples \"Letter\" view model failed!");

                return App.MongoDBService.Materia.Find(index => index.linguagem == language && index.licao == lesson).ToList<Materia>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }
        #endregion
    }
}
