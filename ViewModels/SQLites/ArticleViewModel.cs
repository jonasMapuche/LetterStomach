using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
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

        #region FILTER
        private List<Preceito> FilterLanguage(List<Preceito> list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Article\" view model failed!");

                List<Preceito> new_list = new List<Preceito>();
                list.ForEach(value =>
                {
                    if (value.linguagem == language)
                        new_list.Add(value);
                });
                return new_list;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        private Preceito FilterName(List<Preceito> list, string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter name \"Article\" view model failed!");

                Preceito article = new Preceito();
                foreach (Preceito item in list)
                {
                    if (item.nome == name)
                    {
                        article = item;
                        break;
                    }
                }
                return article;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }
        #endregion

        #region GET
        public List<Preceito> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Article\" view model failed!");

                return FilterLanguage(App.DataService.Preceito, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public Preceito GetName(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Article\" view model failed!");

                return FilterName(App.DataService.Preceito, name);
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
