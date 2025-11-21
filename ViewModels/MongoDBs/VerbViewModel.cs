using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;
using MongoDB.Driver;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class VerbViewModel : IVerbViewModel
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
        public List<Elocucao> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Verb\" view model failed!");

                return App.MongoDBService.Elocucao.Find(index => index.linguagem == language).ToList<Elocucao>();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public List<Elocucao> GetModel(string language, string model)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get model \"Verb\" view model failed!");

                return App.MongoDBService.Elocucao.Find(index => index.linguagem == language && index.modelo == model).ToList<Elocucao>();
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
