using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
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

        #region FILTER
        private List<Elocucao> FilterLanguage(List<Elocucao> list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Verb\" view model failed!");

                List<Elocucao> new_list = new List<Elocucao>();
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

        private List<Elocucao> FilterModel(List<Elocucao> list, string language, string model)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter model \"Verb\" view model failed!");

                List<Elocucao> new_list = new List<Elocucao>();
                list.ForEach(value =>
                {
                    if ((value.linguagem == language) && (value.modelo == model))
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
        public List<Elocucao> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Verb\" view model failed!");

                return FilterLanguage(App.DataService.Elocucao, language);
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

                return FilterModel(App.DataService.Elocucao, language, model);
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
