using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites
{
    public class AdverbViewModel : IAdverbViewModel
    {
        private string _error_message;

        public string error_message
        {
            get => _error_message;
            set
            {
                _error_message = value;
            }
        }

        public event EventHandler<string> OnError;

        private List<Circunstancia> FilterLanguage(List<Circunstancia> list, string language)
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        private Circunstancia FilterName(List<Circunstancia> list, string name)
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Circunstancia> GetLanguage(string language)
        {
            try
            { 
                return FilterLanguage(App.DataService.Circunstancia, language);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public Circunstancia GetName(string name)
        {
            try
            { 
                return FilterName(App.DataService.Circunstancia, name);
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                OnError?.Invoke(this, error_message);
                return null;
            }
        }
    }
}
