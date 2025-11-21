using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
{
    public class SentenceViewModel : ISentenceViewModel
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
        private List<Sentenca> FilterLanguage(List<Sentenca> list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Sentence\" view model failed!");

                List<Sentenca> new_list = new List<Sentenca>();
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
        #endregion

        #region GET
        public List<Sentenca> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Sentence\" view model failed!");

                return FilterLanguage(App.DataService.Sentenca, language);
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
