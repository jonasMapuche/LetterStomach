using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
{
    public class PronounViewModel : IPronounViewModel
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
        private List<Estoutro> FilterLanguage(List<Estoutro> list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Pronoun\" view model failed!");

                List<Estoutro> new_list = new List<Estoutro>();
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

        private Estoutro FilterName(List<Estoutro> list, string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter name \"Pronoun\" view model failed!");

                Estoutro pronoun = new Estoutro();
                foreach (Estoutro item in list)
                {
                    if (item.nome == name)
                    {
                        pronoun = item;
                        break;
                    }
                }
                return pronoun;
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
        public List<Estoutro> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Pronoun\" view model failed!");

                return FilterLanguage(App.DataService.Estoutro, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public Estoutro GetName(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Pronoun\" view model failed!");

                return FilterName(App.DataService.Estoutro, name);
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
