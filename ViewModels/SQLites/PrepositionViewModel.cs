using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites  
{
    public class PrepositionViewModel : IPrepositionViewModel
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
        private List<Juncao> FilterLanguage(List<Juncao> list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Preposition\" view model failed!");

                List<Juncao> new_list = new List<Juncao>();
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
        public List<Juncao> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Preposition\" view model failed!");

                return FilterLanguage(App.DataService.Juncao, language);
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
