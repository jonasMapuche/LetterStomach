using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
{
    public class AuxiliaryViewModel : IAuxiliaryViewModel
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
        private List<Assistente> FilterLanguage(List<Assistente> list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Auxiliary\" view model failed!");

                List<Assistente> new_list = new List<Assistente>();
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
        public List<Assistente> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Auxiliary\" view model failed!");

                return FilterLanguage(App.DataService.Assistente, language);
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
