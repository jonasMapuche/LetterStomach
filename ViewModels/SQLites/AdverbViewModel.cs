using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
{
    public class AdverbViewModel : IAdverbViewModel
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
        private List<Circunstancia> FilterLanguage(List<Circunstancia> list, string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter language \"Adverb\" view model failed!");

                List<Circunstancia> new_list = new List<Circunstancia>();
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

        private Circunstancia FilterName(List<Circunstancia> list, string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation filter name \"Adverb\" view model failed!");

                Circunstancia adverb = new Circunstancia();
                foreach (Circunstancia item in list)
                {
                    if (item.nome == name)
                    {
                        adverb = item;
                        break;
                    }
                }
                return adverb;
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
        public List<Circunstancia> GetLanguage(string language)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get language \"Adverb\" view model failed!");

                return FilterLanguage(App.DataService.Circunstancia, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public Circunstancia GetName(string name)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get name \"Adverb\" view model failed!");

                return FilterName(App.DataService.Circunstancia, name);
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
