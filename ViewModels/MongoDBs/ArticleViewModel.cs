using MongoDB.Driver;
using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.MongoDBs
{
    public class ArticleViewModel : IArticleViewModel
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
        public Preceito GetName(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Article\" view model failed!");

                return App.MongoDBService.Preceito.Find(index => index.nome == name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public List<Preceito> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Article\" view model failed!");

                return App.MongoDBService.Preceito.Find(index => index.linguagem == language).ToList<Preceito>();
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
