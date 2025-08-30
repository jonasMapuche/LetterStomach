using LetterStomach.Models;
using LetterStomach.ViewModels.Interfaces;

namespace LetterStomach.ViewModels.SQLites  
{
    public class PrepositionViewModel : IPrepositionViewModel
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

        private List<Juncao> FilterLanguage(List<Juncao> list, string language)
        {
            try
            {
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
                OnError?.Invoke(this, error_message);
                return null;
            }
        }

        public List<Juncao> GetLanguage(string language)
        {
            try
            {
                return FilterLanguage(App.DataService.Juncao, language);
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
